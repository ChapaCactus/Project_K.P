using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GlobalData : SingletonMonoBehaviour<GlobalData>
{
    public enum GameState { Title = 0, Game, Num }

    [Serializable]
    public class PlayerStatus
    {
        #region Variables
        [SerializeField]
        private int m_Level = 0;
        [SerializeField]
        private int m_Exp = 0;
        [SerializeField]
        private int m_Power = 0;
        #endregion// Variables

        #region properties
        public int level
        {
            get { return m_Level; }
            private set { m_Level = value; }
        }

        public int exp
        {
            get { return m_Exp; }
            private set { m_Exp = value; }
        }

        public int power { get { return m_Power; } set { m_Power = value; } }
        #endregion// properties
    }

    #region variables
    public PlayerStatus playerStatus = new PlayerStatus();

    public string playerName { get; private set; }
    public string globalID { get; private set; }

    public int days { get; set; }

    public int score { get; set; }
    public int level { get; set; }// Stage Level

    private int m_Exp = 0;
    private int m_Gold = 0;

    public GameState gameState { get; set; }

    private bool m_IsMenu = false;
    // 所持品リスト
	[SerializeField, HeaderAttribute("カバンの内容")]
	private Inventory.Item[] m_InventorySlots = null;

    // 最大アイテム所持数
    public static readonly int MAX_INVENTORY_SIZE = 15;
	public static readonly int MAX_STACK_SIZE = 99;
    // SAVEDATA LOAD KEY
    private static readonly string EXP_KEY = "EXP";
    private static readonly string GOLD_KEY = "GOLD";
    private static readonly string POWER_KEY = "POWER";
    #endregion// variables
    #region properties
    public int exp
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
    public int gold {
        get { return m_Gold; }
        private set {
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

    public bool isMenu {
		get { return m_IsMenu; }
		set { m_IsMenu = value; }
	}// メニューを開いているか

	public Inventory.Item[] inventorySlots {
		get { return m_InventorySlots; }
		private set { m_InventorySlots = value; }
	}
    #endregion// properties

    #region unity callbacks
    private void Awake()
    {
        Init();
    }
    #endregion// unity callbacks

    #region PublicMethods
    public void Init()
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

    public void Load()
    {
        // Set variables from savedata.
        playerStatus = new PlayerStatus();// 実際はセーブデータから読み込む
        playerName = PlayerPrefs.GetString("playerName", "NO NAME");
        score = PlayerPrefs.GetInt("score", 0);
        gold = PlayerPrefs.GetInt(GOLD_KEY, 0);
        playerStatus.power = PlayerPrefs.GetInt(POWER_KEY, 1);
        exp = PlayerPrefs.GetInt(EXP_KEY, 0);
    }

    public void Save()
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("score", score);

        PlayerPrefs.SetInt(GOLD_KEY, gold);
        PlayerPrefs.SetInt(EXP_KEY, exp);

        PlayerPrefs.Save();
        Debug.Log("Saved.");
    }

	public void Refresh()
	{
		UIManager.Instance.ui.goldText.text = gold.ToString();
	}

    public int ToNextDay()
    {
        days++;

        return days;
    }

	public int GetInventorySlotsLength()
	{
		return inventorySlots.Length;
	}

	/// <summary>
	/// アイテムを入手
	/// </summary>
	public void AddItem(int _ItemID, int _Quantity = 1)
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

    public int AddMoney(int _point)
    {
		gold += _point;
		PlayerPrefs.SetInt(GOLD_KEY, gold);
		return gold;
    }

    /// <summary>
    /// 経験値取得
    /// </summary>
    public void GainExp(int _gainExp)
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
    private void LevelUp(int _remainExp)
    {
        Debug.Log("Level UP !!! " + level.ToString() + " => " + (level + 1).ToString());
        level++;
        exp = _remainExp;

        // 超過分でレベルアップチェック(再帰的呼び出し)
        GainExp(0);
    }
    #endregion// PrivateMethods

}// Class.
