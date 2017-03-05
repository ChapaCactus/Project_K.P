using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class GlobalData
{
	#region Enums
	// ゲーム状態
    public enum GameState { Title = 0, Game, Num }
	#endregion// Enums

	#region Properties
	public static string playerName { get { return m_PlayerName; } private set { m_PlayerName = value; } }

	public static string globalID { get { return m_GlobalID; } private set { m_GlobalID = value; } }

	public static int days { get { return m_Days; } private set { m_Days = value; } }

	public static int score { get { return m_Score; } private set { m_Score = value; } }
	public static int level { get { return m_Level; } private set { m_Level = value; } }// Stage Level

	public static int power { get { return m_Power; } private set { m_Power = value; } }

	public static int exp {
		get { return m_Exp; }
		private set {
			m_Exp = value;
			if (m_Exp < 0)
				m_Exp = 0;
		}
	}
	/// <summary>
	/// 所持金(ReadOnly)
	/// </summary>
	public static int gold
	{
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
			text.transform.SetParent(goldText.transform, false);
			text.transform.localPosition += new Vector3(40, 0, 0);
			text.SetText("+" + addGold.ToString());
			text.Show(1f);
		}
	}

	public static bool isMenu
	{
		get { return m_IsMenu; }
		set { m_IsMenu = value; }
	}// メニューを開いているか

	public static Inventory.Item[] inventorySlots
	{
		get { return m_InventorySlots; }
		private set { m_InventorySlots = value; }
	}
	#endregion// Properties

	#region Variables
	private static string m_PlayerName = "";
	private static string m_GlobalID = "";

	private static int m_Days = 0;

	private static int m_Score = 0;
	private static int m_Level = 0;

	private static int m_Power = 0;

    private static int m_Exp = 0;
    private static int m_Gold = 0;

    public static GameState gameState { get; set; }

    private static bool m_IsMenu = false;
    // 所持品リスト
	[SerializeField, HeaderAttribute("カバンの内容")]
	private static Inventory.Item[] m_InventorySlots = null;

    // 最大アイテム所持数
    public static readonly int MAX_INVENTORY_SIZE = 15;
	public static readonly int MAX_STACK_SIZE = 99;
    // SAVEDATA KEY
	private static readonly string SAVE_KEY_PLAYER_NAME = "SAVE_KEY_PLAYER_NAME";
	private static readonly string SAVE_KEY_GOLD        = "SAVE_KEY_GOLD";
	private static readonly string SAVE_KEY_EXP         = "SAVE_KEY_EXP";
    #endregion// Variables

    #region PublicMethods
    public static void Init()
    {
		// インベントリの初期化
		inventorySlots = new Inventory.Item[MAX_INVENTORY_SIZE];
		// セーブデータのロード
        Load();
		Refresh();
        // ステート初期化
        gameState = GameState.Title;
        // フラグ初期化
        isMenu = false;
    }

	// ES2 Loading
	public static void Load()
    {
		// Set variables from savedata.
		if (ES2.Exists(SAVE_KEY_PLAYER_NAME))
		{
			playerName = ES2.Load<string>(SAVE_KEY_PLAYER_NAME);
		}
		else
		{
			playerName = "No Name";
			Debug.Log("playerName セーブデータが存在しません。");
		}

		if (ES2.Exists(SAVE_KEY_GOLD))
		{
			gold = ES2.Load<int>(SAVE_KEY_GOLD);
		}
		else
		{
			gold = 0;
			Debug.Log("gold セーブデータが存在しません。");
		}

		if (ES2.Exists(SAVE_KEY_EXP))
		{
			exp = ES2.Load<int>(SAVE_KEY_EXP);
		}
		else
		{
			exp = 0;
			Debug.Log("exp セーブデータが存在しません。");
		}

		Debug.Log("Loaded");
    }

	public static void Save()
    {
		ES2.Save(playerName, SAVE_KEY_PLAYER_NAME);

		ES2.Save(gold, SAVE_KEY_GOLD);
		ES2.Save(exp, SAVE_KEY_EXP);

        Debug.Log("Saved");
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
				if (invenSlots[i] == null)
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
