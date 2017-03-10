using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Google2u;

/// <summary>
/// 苗床っぽい何か
/// </summary>
public class Platform : MonoBehaviour
{
	// (最終)回転角度
	public enum Angle
	{
		Angle0 	 = 0,
		Angle45  = 45,
		Angle90  = 90,
		Angle135 = 135,
		Angle180 = 180,
		Angle225 = 225,
		Angle270 = 270,
		Angle315 = 315,
		Angle360 = 360,
		Num      = 9
	}

    #region Variables
    [SerializeField] protected BaseItem m_Item = null;

	[SerializeField] protected int m_Round = 0;
    [SerializeField] protected int m_HideRound = 0;// 実際の生成数
	[SerializeField] protected Angle m_Angle = Angle.Angle0;

    [SerializeField] protected float m_GeneratingTimer = 0;
    // Items
    [SerializeField] protected Queue<BaseItem> m_ItemsQueue = new Queue<BaseItem>();
    [SerializeField] protected List<BaseItem> m_ItemList = new List<BaseItem>();

    [SerializeField] protected bool m_IsCreatingItem = false;
    #region properties
    public BaseItem item { get { return m_Item; } protected set { m_Item = value; } }
    public List<BaseItem> itemList { get { return m_ItemList; } protected set { m_ItemList = value; } }
    public Queue<BaseItem> itemsQueue { get { return m_ItemsQueue; } private set { m_ItemsQueue = value; } }

    public int round { get { return m_Round; } private set { m_Round = value; } }
    public int hideRound { get { return m_HideRound; } private set { m_HideRound = value; } }
    #endregion// properties
    protected const int ITEMS_MAX_SIZE = 8;// 後でCSVから読む
    protected const int QUEUE_MAX_SIZE = 8;

    // Tween
    private Tweener m_RotateTween = null;
    private const int ROTATE_BUFF = 45;

    #endregion

    #region UnityCallbacks        
    private void Update()
    { 
        if (m_IsCreatingItem && m_GeneratingTimer <= 0) {
            item = GetItem ();
            m_IsCreatingItem = false;
            Debug.Log ("Getted Item....");
        } else {
            m_GeneratingTimer -= (Time.deltaTime * 1.0f);
        }

        if (CheckItemIsNull () && !m_IsCreatingItem) {
            m_IsCreatingItem = true;
            m_GeneratingTimer = 1f;

            Debug.Log ("Start Item Generate....");
        }
    }
    #endregion

// Public Methods.
	public static Platform Create()
	{
		var go = new GameObject("Platform");
        var result = go.AddComponent<Platform>();

		return result;
	}

	public void Init()
	{
        round = 0;
        hideRound = 0;
        item = GetItem ();
        item.StartPopCoroutine ();// アイテム成長始動
	}

    /// <summary>
    /// このプラットフォームに現在セットされているアイテムを返す
    /// </summary>
    public BaseItem GetItem()
    {
        // アイテムが存在しなければ新たなアイテムを生成する
        if (item == null) {
            item = CreateItem ();
        }

        return item;
    }

    public BaseItem CreateItem()
    {
		return Stage.Instance.GenerateItem();
    }

    public void KillItem()
    {
		Destroy(item.gameObject);
        item = null;
    }
        
    #region PrivatMethods
	/// <summary>
    /// Setup field and items. (and player?)
    /// </summary>
	protected void Setup()
	{
		//
	}

    protected bool CheckItemIsNull()
    {
        if (item == null) {
            return true;
        } else {
            return false;
        }
    }

    [ContextMenu("MoveNext()")]
	protected void MoveNext() 
	{
        if (m_RotateTween != null) {
            return;
        }

        // Get GameItem (1) Remove
        ItemListRemoveAt(0);
	}

    protected void DequeueLastItem()
    {
        //
    }

    protected void ItemListRemoveAt(int _GameItemIndex)
    {
        Destroy(itemList[_GameItemIndex].gameObject);
        itemList.RemoveAt (_GameItemIndex);
    }

    private void TestCallback(string _Message)
    {
        Debug.Log (_Message + " が、帰って来ました！！");
    }
    #endregion

}// Class.
