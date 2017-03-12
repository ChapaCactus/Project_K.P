using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google2u;

[RequireComponent(typeof (CanvasGroup))]
public class Inventory : BaseMainMenuContent
{
	#region Properties
	public InventoryInfo inventoryInfo { get { return m_InventoryInfo; } }

	public Reference reference { get { return m_Reference; } private set { m_Reference = value; } }
	public Data data { get { return m_Data; } private set { m_Data = value; } }
	#endregion// Properties

	#region Variables
	[SerializeField] private Content[] m_Contents = null;

	// InventoryInfo
	private InventoryInfo m_InventoryInfo = null;

    [SerializeField] private Reference m_Reference;
    [SerializeField] private Data m_Data;
    #endregion// Variables

    #region UnityCallbacks
    #endregion// UnityCallbacks

    #region PublicMethods
    public override void Init()
    {
		// アイテム詳細パネルの設定
		var infoTF = transform.Find("InventoryInfo").transform;
		var infoCanvasGroup = infoTF.GetComponent<CanvasGroup>();
		var infoNameText = infoTF.Find("Name/Text").GetComponent<Text>();
		var infoIconImage = infoTF.Find("Icon/Image").GetComponent<Image>();
		var infoExplainText = infoTF.Find("Explain/Text").GetComponent<Text>();
		var infoCloseButton = infoTF.Find("Close/Button").GetComponent<Button>();
		m_InventoryInfo = new InventoryInfo(infoCanvasGroup, infoIconImage, infoNameText
											, infoExplainText, infoCloseButton);
		// アイテム詳細を非表示にしておく
		m_InventoryInfo.Hide();

        Hide ();
		CreateListContents();
    }

	/// <summary>
	/// インベントリの更新
	/// ボタンUIも更新する
	/// </summary>
	public void Refresh()
	{
		// 表示中ならUIの更新をかける(非表示ならデータの更新だけ)
		if (GetComponent<CanvasGroup>().alpha > 0)
		{
			UpdateListContents();
		}
	}

	public override void Show()
	{
		base.Show();
		Refresh();
	}
	#endregion// PublicMethods
	#region PrivateMethods
	private void CreateListContents()
	{
		// 初期化
		m_Contents = new Content[GlobalData.GetInventorySlotsLength()];

		// インベントリ最大数まで要素を作る
		var length = GlobalData.GetInventorySlotsLength();
		for (int i = 0; i < length; i++)
		{
			// 生成
			var go = transform.Find("InventoryList/Viewport/Content/Slot (" + i + ")").gameObject;
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

		Debug.Log(length + " 個のボタンを登録しました。");

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
				Debug.Log("CHKCHKCHK : " + i);
				m_Contents[i].Update();
			}

			foreach (var item in m_Contents)
			{
				Debug.Log("KKKKKKK : " + GlobalData.inventorySlots[item.invenSlotIndex].id);
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

	#region InnerClasses
	public class InventoryInfo
	{
		#region Properties
		#endregion// Properties

		#region Variables
		private CanvasGroup m_CanvasGroup = null;

		private Image m_IconImage = null;
		private Text m_NameText = null;
		private Text m_ExplainText = null;

		private Button m_CloseButton = null;
		#endregion// Variables

		#region PublicMethods
		public void Init()
		{
			m_IconImage = null;
			m_NameText = null;
			m_ExplainText = null;
			m_CloseButton = null;
		}

		public void Show()
		{
			Utilities.ToggleCanvasGroup(m_CanvasGroup, true);
			Debug.Log("InventoryInfo.Show()");
		}

		public void Hide()
		{
			Utilities.ToggleCanvasGroup(m_CanvasGroup, false);
			Debug.Log("InventoryInfo.Hide()");
		}

		public void SetupUI(ItemMasterRow _row)
		{
			m_NameText.text = _row._Name;
			m_ExplainText.text = "説明テキストです";
		}
		#endregion// PublicMethods

		#region PrivateMethods
		private void AddEventButton()
		{
			m_CloseButton.onClick.RemoveAllListeners();
			m_CloseButton.onClick.AddListener(() => OnClick_CloseButton());
		}

		private void OnClick_CloseButton()
		{
			Hide();
			Debug.Log("OnClick_CloseButton()");
		}
		#endregion// PrivateMethods

		#region Consructor
		public InventoryInfo(CanvasGroup _canvasGroup, Image _iconImage, Text _nameText, Text _explainText, Button _closeButton)
		{
			Init();

			m_CanvasGroup = _canvasGroup;

			m_IconImage = _iconImage;
			m_NameText = _nameText;
			m_ExplainText = _explainText;
			m_CloseButton = _closeButton;

			AddEventButton();
		}
		#endregion// Constructor
	}

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
		[SerializeField] private int m_InvenSlotIndex = -1;

		[SerializeField] private Button m_Button = null;

		[SerializeField] private Text m_StackText = null;// 2/99 所持数/最大所持数

		[SerializeField] private Image m_IconImage = null;
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

			Update();
		}

		/// <summary>
		/// 所持数/最大所持数テキストを更新
		/// </summary>
		public void SetStackText(int _myStack, int _maxStack)
		{
			if (_myStack == 0 && _maxStack == 0)
			{
				m_StackText.enabled = false;
			}
			else
			{
				m_StackText.text = (_myStack + "/" + _maxStack);
				m_StackText.enabled = true;
			}
		}

		/// <summary>
		/// Global.Inventory(の該当Index)の情報に更新
		/// </summary>
		public void Update()
		{
			if (invenSlotIndex < 0)
			{
				Debug.Log("対応スロット 番号エラー => m_InvenSlotIndex: " + m_InvenSlotIndex.ToString());
				return;
			}

			// アイテムデータ取得
			var slot = GlobalData.inventorySlots[m_InvenSlotIndex];
			if (slot != null && slot.id > 0)
			{
				var rowID = Utilities.ConvertMasterRowID(slot.id);
				var itemData = ItemMaster.Instance.GetRow(rowID);
				// 所持数テキスト更新
				var stack = slot.stack;
				var maxStack = GlobalData.MAX_STACK_SIZE;
				SetStackText(stack, maxStack);
			}
			else
			{
				// データが存在しない、またはIDが0以下
				m_IconImage.sprite = null;
				SetStackText(0, 0);
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
			_button.onClick.AddListener(() => OnClickInvenButton());
		}

		/// <summary>
		/// スロット内、各ボタン押下時の処理
		/// </summary>
		private void OnClickInvenButton()
		{
			var id = GlobalData.inventorySlots[invenSlotIndex].id;
			if (id > 0)
			{
				var combine = Utilities.ConvertMasterRowID(id);
				var master = ItemMaster.Instance.GetRow(combine);
				var info = Menu.Inventory.inventoryInfo;
				info.SetupUI(master);
				info.Show();
			} else
			{
				// 何もしない
				Debug.Log("空IDのスロットです。 => id: " + id);
			}
			Debug.Log("OnClickInvenButton()");
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
	#endregion// InnerClasses

}// Inventory
