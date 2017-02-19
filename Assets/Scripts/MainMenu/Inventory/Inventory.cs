using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google2u;

[RequireComponent(typeof (CanvasGroup))]
public class Inventory : BaseMainMenuContent
{
    [Serializable]
    public class Item
    {
        public int id = 0;
        public int stack = 0;

		public Item(int _ID, int _Stack)
        {
            id = _ID;
            stack = _Stack;
        }
    }

	/// <summary>
	/// インベントリボタン個々の参照
	/// </summary>
	[Serializable]
	public struct Content
	{
		public int invenSlotIndex;// GlobalData.inventorySlotsの対応番地

		public Image icon;

		public Button button;

		public Text stackText;// 2/99 所持数/最大所持数
	}

    [Serializable]
    public class Reference
    {
		public Transform inventoryContentParent = null;
    }

    [Serializable]
    public class Data
    {
        public bool isActive = false;
    }

	#region Variables
	[SerializeField]
	private Content[] m_Contents = null;

    [SerializeField] private Reference m_Reference;
    [SerializeField] private Data m_Data;
    #endregion// Variables
    #region Properties
    public Reference reference { get { return m_Reference; } private set { m_Reference = value; } }
    public Data data { get { return m_Data; } private set { m_Data = value; } }
    #endregion// Properties

    #region UnityCallbacks
	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			// DummyCall
			DummyCall();
		}
	}
    #endregion// UnityCallbacks

    #region PublicMethods
    public void Init()
    {
        Hide ();
		CreateListContents();
    }

	/// <summary>
	/// スロット内、各ボタン押下時の処理
	/// </summary>
	/// <param name="_slotIndex">GlobalData.InventorySlotsと対応</param>
    public void OnClickInvenButton(int _slotIndex)
    {
    }

	public void Refresh()
	{
		// 表示中ならUIの更新をかける
		if (GetComponent<CanvasGroup>().alpha > 0)
		{
			UpdateListContent();
		}
	}

	#endregion// PublicMethods
	#region PrivateMethods
	private void CreateListContents()
	{
		// 初期化
		m_Contents = null;
		m_Contents = new Content[GlobalData.Instance.GetInventorySlotsLength()];

		var parent = transform.FindChild("Viewport/Content");
		var prefab = parent.transform.FindChild("Template").gameObject;
		prefab.SetActive(true);

		// インベントリ最大数まで要素を作る
		for (int i = 0; i < GlobalData.Instance.GetInventorySlotsLength(); i++)
		{
			// 初期化
			var go = Instantiate(prefab, parent, false);
			var content = new Content();
			// 参照設定
			content.invenSlotIndex = i;
			content.button = go.GetComponent<Button>();
			content.icon = go.transform.FindChild("Image").GetComponent<Image>();
			content.stackText = go.transform.FindChild("Stack/Text").GetComponent<Text>();
			// データセット
			AddEventInvenButton(content.button, i);
			SetIconSprite(content.icon, i);

			m_Contents[i] = content;
		}

		prefab.SetActive(false);
		// GlobalDataの情報通りに更新
		UpdateListContent();
	}

	/// <summary>
	/// GlobalDataの情報通りに更新
	/// </summary>
	private void UpdateListContent()
	{
		if (m_Contents == null)
		{
			CreateListContents();
		}
		else
		{
			var slots = GlobalData.Instance.inventorySlots;
			var contents = m_Contents;

			for (int i = 0; i < contents.Length; i++)
			{
				var index = contents[i].invenSlotIndex;
				var slot = slots[index];
				if (slot != null)
				{
					var sprite = Resources.Load<Sprite>("Sprites/Icons/Pocha");
					contents[i].icon.sprite = sprite;
					contents[i].stackText.text = (slot.stack + "/" + 99).ToString();
				}
				else
				{
					// スロットが空なら
					contents[i].icon.sprite = null;
					contents[i].stackText.text = "";
				}
			}
		}
	}

	/// <summary>
	/// タッチイベントセット
	/// </summary>
	/// <param name="_button">対象ボタン</param>
	/// <param name="_slotIndex">GlobalData.InventorySlotsと対応</param>
	private void AddEventInvenButton(Button _button, int _slotIndex)
	{
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(() => OnClickInvenButton(_slotIndex));
	}

	/// <summary>
	/// アイコンをセットする(場合によってはキャッシュからロードする様にする)
	/// </summary>
	/// <param name="_image">IconのImageなど</param>
	/// <param name="_spriteIndex">Sprite参照用</param>
	private void SetIconSprite(Image _image, int _spriteIndex)
	{
		Sprite sprite = new Sprite();
		_image.sprite = sprite;
	}

	/// <summary>
	/// Dummy
	/// </summary>
	private void DummyCall()
	{
		Debug.Log("DummyCalling...");
	}
    #endregion// PrivateMethods

}// class