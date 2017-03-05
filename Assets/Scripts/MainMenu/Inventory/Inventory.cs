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
	public class Content
	{
		#region Properties
		public int invenSlotIndex { get { return m_InvenSlotIndex; } }
		#endregion// Properties

		#region Variables
		// GlobalData.inventorySlotsの対応番地
		private int m_InvenSlotIndex = -1;

		private Button m_Button = null;

		private Text m_StackText = null;// 2/99 所持数/最大所持数

		private Image m_IconImage = null;
		#endregion// Variables

		#region PublicMethods
		// コンストラクタ
		public Content(int _invenSlotIndex, Text _stackText, Image _iconImage, Button _button)
		{
			m_InvenSlotIndex = _invenSlotIndex;
			m_StackText = _stackText;
			m_IconImage = _iconImage;
			m_Button = _button;

			AddEventInvenButton(m_Button, m_InvenSlotIndex);
		}

		public void SetStackText(int _myStack, int _maxStack)
		{
			m_StackText.text = (_myStack + "/" + _maxStack);
		}

		/// <summary>
		/// Global.Inventory(の該当Index)の情報に更新
		/// </summary>
		public void Update()
		{
			if (m_InvenSlotIndex == -1)
			{
				Debug.Log("対応スロット 番号エラー => m_InvenSlotIndex: " + m_InvenSlotIndex.ToString());
				return;
			}

			// アイテムデータ取得
			var slot = GlobalData.inventorySlots[m_InvenSlotIndex];
			if (slot != null)
			{
				var rowID = Utilities.ConvertMasterRowID(slot.id);
				var itemData = ItemMaster.Instance.GetRow(rowID);
				// 所持数テキスト更新
				var stack = slot.stack;
				var maxStack = GlobalData.MAX_STACK_SIZE;
				SetStackText(stack, maxStack);
			}
		}
		#endregion// PublicMethods

		#region PrivateMethods
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
		/// スロット内、各ボタン押下時の処理
		/// </summary>
		/// <param name="_slotIndex">GlobalData.InventorySlotsと対応</param>
		private void OnClickInvenButton(int _slotIndex)
		{
			Debug.Log("OnClick....InvenButton....");
		}
		#endregion// PrivateMethods

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

	public void Refresh()
	{
		// 表示中ならUIの更新をかける
		if (GetComponent<CanvasGroup>().alpha > 0)
		{
			UpdateListContents();
		}
	}

	#endregion// PublicMethods
	#region PrivateMethods
	private void CreateListContents()
	{
		// 初期化
		m_Contents = new Content[GlobalData.GetInventorySlotsLength()];

		// インベントリ最大数まで要素を作る
		for (int i = 0; i < GlobalData.GetInventorySlotsLength(); i++)
		{
			// 生成
			var go = transform.Find("Viewport/Content/Slot (" + i + ")").gameObject;
			// 初期化
			var index = i;// スロット参照用
			var stackText = go.transform.FindChild("Stack/Text").GetComponent<Text>();
			var iconImage = go.transform.FindChild("Image").GetComponent<Image>();
			var button = go.GetComponent<Button>();
			var content = new Content(index, stackText, iconImage, button);

			if (!go.activeSelf) go.SetActive(true);
			// 登録
			m_Contents[i] = content;
		}

		// GlobalDataの情報通りに更新
		UpdateListContents();
	}

	/// <summary>
	/// GlobalDataの情報通りに更新
	/// </summary>
	/// <param name="_slotIndexes">更新スロットの指定(nullなら全てのスロットを更新)</param>
	private void UpdateListContents(int[] _slotIndexes = null)
	{
		var slots = GlobalData.inventorySlots;

		if (_slotIndexes == null)
		{
			// 未指定なら全てのスロットを
			// 更新
			for (int i = 0; i < m_Contents.Length; i++)
			{
				m_Contents[i].Update();
			}
		}
		else
		{
			// 指定があれば、そのスロットだけ更新
			for (int i = 0; i < m_Contents.Length; i++)
			{
				for (int j = 0; j < _slotIndexes.Length; j++)
				{
					if (m_Contents[i].invenSlotIndex == _slotIndexes[j])
					{
						// 指定Indexと同じ対応Indexを持つスロットがあれば
						// 更新
						m_Contents[i].Update();
					}
				}
			}
		}
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
	/// スロットの要素を取得する
	/// </summary>
	//private Content[] GetSlotContents()
	//{
	//	var parent = transform.Find("Viewport/Content");
	//}

	/// <summary>
	/// Dummy
	/// </summary>
	private void DummyCall()
	{
		Debug.Log("DummyCalling...");
	}
    #endregion// PrivateMethods

}// class