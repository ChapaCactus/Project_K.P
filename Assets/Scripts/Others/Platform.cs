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
	#region Enums
	/// <summary>
	/// 状態
	/// </summary>
	public enum State
	{
		Sleep,// 停止中
		Started,// 開始
		Provisioning,// 生成準備中
		Standby,// 生成準備完了(いつでも生成してOK)
		Generating,// 生成中
		Generated,// 生成完了
	}
	#endregion// Enums

	#region Properties
	public State state { get { return m_State; } private set { m_State = value; } }
	public BaseItem item { get { return m_Item; } protected set { m_Item = value; } }

	public float timer {
		get { return m_GeneratingTimer; }
		private set {
			m_GeneratingTimer = value;
			if (m_GeneratingTimer < 0) m_GeneratingTimer = 0;
		}
	}
	#endregion// Properties

	#region Variables
	[SerializeField] private State m_State = State.Sleep;
	// このプラットフォームが生成したアイテム
	// nullなら未生成 or 収穫済 => 再生成開始へ
	[SerializeField] protected BaseItem m_Item = null;

    [SerializeField] protected float m_GeneratingTimer = 0;
    #endregion// Variables

    #region UnityCallbacks        
    private void Update()
    {
		switch (state)
		{
			case State.Sleep:
				break;
			case State.Started:
				timer = 1;
				state = State.Provisioning;
				break;
			case State.Provisioning:
				timer -= Time.deltaTime;
				if (timer <= 0) state = State.Standby;
				break;
			case State.Standby:
				state = State.Generating;
				break;
			case State.Generating:
				CreateItem();
				state = State.Generated;
				break;
			case State.Generated:
				if (item == null) state = State.Started;
				break;
		}
    }
    #endregion

	#region PublicMethods
	public static Platform Create()
	{
		var go = new GameObject("Platform");
        var result = go.AddComponent<Platform>();

		return result;
	}

	public void Init()
	{
		state = State.Started;
	}

    /// <summary>
    /// このプラットフォームに現在セットされているアイテムを返す
    /// </summary>
    public BaseItem GetItem()
    {
        return item;
    }

    public BaseItem CreateItem()
    {
		this.item = Stage.Instance.GenerateItem();
		return item;
    }

    public void KillItem()
    {
		item.ChangeConstraints();
		item.transform.DOLocalMove(new Vector3(4, 4, 0), 0.5f).OnComplete(() => Destroy(item.gameObject));
		//Destroy(item.gameObject);
        this.item = null;
    }
	#endregion// PublicMethods

    #region PrivatMethods
    #endregion// PrivateMethods

}// Class.
