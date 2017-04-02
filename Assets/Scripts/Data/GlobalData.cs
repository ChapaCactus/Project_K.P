using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google2u;
using KP;

public static class GlobalData
{
	#region Enums
	public enum State
	{
		NotInitialize, Initialized
	}
	// ゲーム状態
	public enum GameState { Title = 0, Game, Num }

	public enum Equipments
	{
		Tool,// 採集道具
		Boots,// ブーツ
		Accessory,// アクセサリー
	}
	#endregion// Enums

	#region Properties
	public static string playerName { get { return m_PlayerName; } private set { m_PlayerName = value; } }

	public static string globalID { get { return m_GlobalID; } private set { m_GlobalID = value; } }

	public static int days {
		get { return m_Days; }
		private set {
			m_Days = value;
			Mathf.Clamp(m_Days, 0, 999);// 0 ~ 999の間に収める

			UIManager.Instance.ui.daysText.text = (m_Days + "日目");

			Save();
		}
	}

	public static int score { get { return m_Score; } private set { m_Score = value; } }
	public static int level { get { return m_Level; } private set { m_Level = value; } }// Stage Level

	public static int basePower { get { return (m_BasePower + e_Tool.GetTotalPower()); } private set { m_BasePower = value; } }

	public static int exp
	{
		get { return m_Exp; }
		private set
		{
			m_Exp = value;
			if (m_Exp < 0)
				m_Exp = 0;
			
		}
	}
	/// <summary>
	/// 所持金(ReadOnly)
	/// </summary>
	public static int gold {
		get { return m_Gold; }
		private set
		{
			int addGold = (value - m_Gold);
			m_Gold = value;
			if (m_Gold < 0)
				m_Gold = 0;
			var goldText = UIManager.Instance.ui.goldText;
			goldText.text = m_Gold.ToString();

			var text = FloatingText.Create();
			text.transform.SetParent(UIManager.Instance.GetMainCanvas().transform, false);
			text.transform.localPosition += new Vector3(0, 0, 0);
			text.SetText("+" + addGold.ToString());
			text.Play(FloatingText.AnimationType.Fade, 1f, 0, 20);

			Debug.Log("gold : " + m_Gold);
		}
	}

	// メニューを開いているか
	public static bool isMenu {
		get { return m_IsMenu; }
		set { m_IsMenu = value; }
	}

	public static Inventory.Item[] inventorySlots {
		get { return m_InventorySlots; }
		private set { m_InventorySlots = value; }
	}

	public static HashSet<int> itemIndex {
		get { return m_ItemIndex; }
		private set { m_ItemIndex = value; }
	}

	public static Tool e_Tool {
		get { return m_E_Tool; }
		private set {
			m_E_Tool = value;
			//string rowKey = Utilities.ConvertMasterRowID(m_E_Glove);
			//m_E_GloveData = GloveMaster.Instance.GetRow(rowKey);
		}
	}

	public static int e_Boots {
		get { return m_E_Boots; }
		private set { m_E_Boots = value; }
	}

	public static int e_Accessory
	{
		get { return m_E_Accessory; }
		private set { m_E_Accessory = value; }
	}

	public static State state
	{
		get
		{
			return m_State;
		}
		private set
		{
			m_State = value;
		}
	}
	#endregion// Properties

	#region Variables
	private static string m_PlayerName = "";
	private static string m_GlobalID = "";

	private static int m_Days = 0;

	private static int m_Score = 0;
	private static int m_Level = 0;

	private static int m_BasePower = 0;// 基礎攻撃力

    private static int m_Exp = 0;
    private static int m_Gold = 0;

	// Equipments// 装備状態 ID == 0 : 無装備, == 1 ~ : 装備中
	private static Tool m_E_Tool = null;
	private static int m_E_Boots = 0;
	private static int m_E_Accessory = 0;

	// アイテム図鑑データ(アイテム収集済みIDを追加・管理)
	private static HashSet<int> m_ItemIndex = new HashSet<int>();

	private static State m_State = State.NotInitialize;
    public static GameState gameState { get; set; }

    private static bool m_IsMenu = false;
    // 所持品リスト
	[SerializeField, HeaderAttribute("カバンの内容")]
	private static Inventory.Item[] m_InventorySlots = null;

    // 最大アイテム所持数
    public static readonly int MAX_INVENTORY_SIZE = 15;
	public static readonly int MAX_STACK_SIZE = 99;
    // SAVEDATA KEY
	private static readonly string SAVE_KEY_PLAYER_NAME      = "SAVE_KEY_PLAYER_NAME";
	private static readonly string SAVE_KEY_GOLD             = "SAVE_KEY_GOLD";
	private static readonly string SAVE_KEY_EXP              = "SAVE_KEY_EXP";
	private static readonly string SAVE_KEY_DAYS             = "Days";
	private static readonly string SAVE_KEY_BASE_POWER       = "SAVE_KEY_BASE_POWER";
	private static readonly string SAVE_KEY_ITEM_INDEX       = "ITEM_INDEX";// 図鑑
	private static readonly string SAVE_KEY_EQUIP_TOOL_ID    = "ToolID";
	private static readonly string SAVE_KEY_EQUIP_TOOL_Level = "ToolLevel";
	private static readonly string SAVE_KEY_EQUIP_BOOTS      = "Boots";
	private static readonly string SAVE_KEY_EQUIP_ACCESSORY  = "Accessory";
    #endregion// Variables

    #region PublicMethods
    public static void Init()
    {
		// インベントリの初期化
		inventorySlots = new Inventory.Item[MAX_INVENTORY_SIZE];
		for (int i = 0; i < inventorySlots.Length; i++)
		{
			// 後でセーブデータから読む
			inventorySlots[i] = new Inventory.Item(0, 0);
		}
		// セーブデータのロード
		Load();
		Refresh();
        // ステート初期化
        gameState = GameState.Title;
        // フラグ初期化
        isMenu = false;

		m_State = State.Initialized;
    }

	// ES2 Loading
	public static void Load()
    {
		// Set variables from savedata.
		if (ES2.Exists(SAVE_KEY_PLAYER_NAME)) {
			playerName = ES2.Load<string>(SAVE_KEY_PLAYER_NAME);
		}
		else {
			playerName = "No Name";
			Debug.Log("playerName セーブデータが存在しません。");
		}
		// お金
		if (ES2.Exists(SAVE_KEY_GOLD)) {
			gold = ES2.Load<int>(SAVE_KEY_GOLD);
		}
		else {
			gold = 0;
			Debug.Log("gold セーブデータが存在しません。");
		}
		// 経験値
		if (ES2.Exists(SAVE_KEY_EXP))
		{
			exp = ES2.Load<int>(SAVE_KEY_EXP);
		}
		else
		{
			exp = 0;
			Debug.Log("exp セーブデータが存在しません。");
		}
		// 経過日数
		if (ES2.Exists(SAVE_KEY_DAYS))
		{
			days = ES2.Load<int>(SAVE_KEY_DAYS);
		}
		else
		{
			days = 0;
			Debug.Log("days セーブデータが存在しません。");
		}
		// 基礎攻撃力
		if (ES2.Exists(SAVE_KEY_BASE_POWER)) {
			basePower = ES2.Load<int>(SAVE_KEY_BASE_POWER);
		}
		else {
			basePower = 1;
			Debug.Log("basePower セーブデータが存在しません。");
		}

		// アイテム図鑑
		if (ES2.Exists(SAVE_KEY_ITEM_INDEX))
		{
			itemIndex = ES2.LoadHashSet<int>(SAVE_KEY_ITEM_INDEX);
		}
		else {
			itemIndex = new HashSet<int>();
			Debug.Log("アイテム図鑑データが存在しません。新規に作成します。");
		}

		// Tool装備状態
		if (ES2.Exists(SAVE_KEY_EQUIP_TOOL_ID) && ES2.Exists(SAVE_KEY_EQUIP_TOOL_Level))
		{
			var toolID = ES2.Load<int>(SAVE_KEY_EQUIP_TOOL_ID);
			var toolLevel = ES2.Load<int>(SAVE_KEY_EQUIP_TOOL_Level);
			e_Tool = Tool.Create(toolID, toolLevel);
		}
		else
		{
			e_Tool = Tool.Create(0, 1);
			Debug.Log("アイテム図鑑データが存在しません。新規に作成します。");
		}

		// ブーツ装備状態
		if (ES2.Exists(SAVE_KEY_EQUIP_BOOTS))
		{
			e_Boots = ES2.Load<int>(SAVE_KEY_EQUIP_BOOTS);
		}
		else
		{
			e_Boots = 0;
			Debug.Log("アイテム図鑑データが存在しません。新規に作成します。");
		}

		// アクセサリー装備状態
		if (ES2.Exists(SAVE_KEY_EQUIP_ACCESSORY))
		{
			e_Accessory = ES2.Load<int>(SAVE_KEY_EQUIP_ACCESSORY);
		}
		else
		{
			e_Accessory = 0;
			Debug.Log("アイテム図鑑データが存在しません。新規に作成します。");
		}

		Debug.Log("Loaded");
    }

	public static void Save()
    {
		if (m_State == State.Initialized)
		{
			ES2.Save(playerName, SAVE_KEY_PLAYER_NAME);

			ES2.Save(gold, SAVE_KEY_GOLD);
			ES2.Save(exp, SAVE_KEY_EXP);
			ES2.Save(days, SAVE_KEY_DAYS);

			ES2.Save(itemIndex, SAVE_KEY_ITEM_INDEX);

			if (e_Tool != null)
			{
				ES2.Save(e_Tool.toolID, SAVE_KEY_EQUIP_TOOL_ID);
				ES2.Save(e_Tool.level, SAVE_KEY_EQUIP_TOOL_Level);
			}

			Debug.Log("Saved");
		}
		else
		{
			Debug.Log("初期化が完了していないためSave出来ません。");
		}
    }

	public static void Refresh()
	{
		UIManager.Instance.ui.goldText.text = gold.ToString();
	}

	public static int ToNextDay()
    {
		var before = days;
        var after = days++;

		Debug.Log("before: " + before + ", after: " + after);

        return days;
    }

	/// <summary>
	/// 装備変更
	/// </summary>
	public static void SetEquip(Equipments _equipments, int _equipID)
	{
		Debug.Log("SetEquip()");

		switch (_equipments)
		{
			case Equipments.Tool:
				//e_Glove = _equipID;
				break;
			case Equipments.Boots:
				e_Boots = _equipID;
				break;
			case Equipments.Accessory:
				e_Accessory = _equipID;
				break;
		}
	}

	public static int GetInventorySlotsLength()
	{
		return inventorySlots.Length;
	}

	/// <summary>
	/// アイテムを入手
	/// </summary>
	public static void AddItem(int _ItemID, int _Quantity = 1)
	{
		Inventory.Item[] invenSlots = inventorySlots;
		bool incrementFlag = false;// インクリメントできたか(== 所持していたか)
		for (int i = 0; i < invenSlots.Length; i++)
		{
			if (invenSlots[i] != null)
			{
				// 加えようとしたアイテムが既にあれば、加算
				// && 既に99個無ければ
				if (invenSlots[i].id == _ItemID && invenSlots[i].stack < 99)
				{
					invenSlots[i].stack += _Quantity;// 加算
					incrementFlag = true;

					if (invenSlots[i].stack > 99)
					{
						int remain = (invenSlots[i].stack - 99);
						invenSlots[i].stack = 99;
						// 再帰的に呼んで他のスロットに加算
						AddItem(_ItemID, remain);
					}

					Menu.Inventory.Refresh();

					return;
				}
			}
		}

		// インクリメントされて居なければ(== 所持していなければ)、新規追加
		if (!incrementFlag)
		{
			for (int i = 0; i < invenSlots.Length; i++)
			{
				// 空いていればそこに追加する
				if (invenSlots[i].id <= 0)
				{
					var item = new Inventory.Item(_ItemID, _Quantity);
					invenSlots[i] = item;

					Menu.Inventory.Refresh();

					return;
				}
			}
		}
	}

	public static int AddMoney(int _point)
    {
		gold += _point;
		PlayerPrefs.SetInt(SAVE_KEY_GOLD, gold);
		return gold;
    }

    /// <summary>
    /// 経験値取得
    /// </summary>
	public static void GainExp(int _gainExp)
    {
        // 経験値加算
        exp += _gainExp;
        var maxExp = (10 * level * level);
        // 最大EXPを超えていればレベルアップ処理
        if (exp >= maxExp)
        {
            // 超過分を計算
            var remain = (maxExp - exp);
            LevelUp(remain);
        }
    }

    #endregion// PublicMethods
    #region PrivateMethods
    /// <summary>
    /// レベルアップ処理
    /// </summary>
	private static void LevelUp(int _remainExp)
    {
        Debug.Log("Level UP !!! " + level.ToString() + " => " + (level + 1).ToString());
        level++;
        exp = _remainExp;

        // 超過分でレベルアップチェック(再帰的呼び出し)
        GainExp(0);
    }
    #endregion// PrivateMethods

}// Class.
