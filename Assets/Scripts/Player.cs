using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Spine.Unity;

/// <summary>
/// Controll player character.
/// </summary>
public class Player : SingletonMonoBehaviour<Player>
{
	#region Enums
	public enum AnimationType
	{
		Idle = 0,
		Num
	}
	#endregion// Enums

	#region Properties
	public Transform tf { get { return m_Transform ?? (m_Transform = transform); } }

	/// プレイヤーの補正後最終攻撃力(能力上昇や、ステータスダウンも含める)
	public int totalPower { get { return GlobalData.basePower; } }

	public int exp { get { return GlobalData.exp; } }
	public int level { get { return GlobalData.level; } }
	public float charge
	{
		get { return m_Charge; }
		private set { Mathf.Clamp(m_Charge = value, MIN_CHARGE_VALUE, MAX_CHARGE_VALUE); }
	}
	#endregion// Properties

	#region Variables
	private EventSystem m_EventSystem = null;

	private Transform m_Transform = null;// Transformキャッシュ用

    [SerializeField] private BaseItem m_Target = null;
    [SerializeField] private bool m_ChargeTrigger = false;
    // チャージ
    [SerializeField] private float m_Charge = 0;

	// SpineAnimation
	private SkeletonAnimation m_SkeletonAnimation = null;
	private Spine.AnimationState m_AnimationState = null;

    private const int   MAX_CHARGE_VALUE = 1;
    private const int   MIN_CHARGE_VALUE = 0;
    private const float CHARGE_BUFF      = 0.1f;
	#endregion// Variables

	#region UnityCallbacks
	private void Start()
	{
		Init();
	}

    private void Update()
    {
        if (Input.GetMouseButtonUp (0)) {
			Debug.Log("Touched UI is... => " + m_EventSystem.currentSelectedGameObject);
			if (m_EventSystem.currentSelectedGameObject == null)
			{
				// UIを選択していなければ 攻撃
				Attack();
				TouchParticle.Instance.PlayParticle();
			}
			else
			{
				// 何もしない
			}
        }

        if (m_ChargeTrigger) {
            // Charging
            Charge ();
        }
    }
    #endregion// UnityCallbacks

    #region PublicMethods
    public void Init()
    {
		Debug.Log("Player Initialize.....");

		m_SkeletonAnimation = GetComponent<SkeletonAnimation>();
		m_AnimationState = m_SkeletonAnimation.state;

		PlayAnimation("01. Idle", true);

		m_EventSystem = GameObject.Find("Canvas/EventSystem").GetComponent<EventSystem>();
    }

    public BaseItem GetTarget()
    {
        return m_Target;
    }

    public void SetTarget(BaseItem _BaseItem)
    {
        m_Target = _BaseItem;
    }

    public void AddExp(int _point)
    {
        
    }
    #endregion// PublicMethods

    #region Private methods
    private void Attack()
    {
		var item = Stage.Instance.platforms[0].GetItem();

		if (item == null)
		{
			Debug.Log("Player : Itemがナーイ！");

			return;
		}
		else
		{
			var state = item.GetState();// 状態
			int health = item.GetHealth();// 体力

			// 体力があって、かつ準備完了状態なら
			if (state == BaseItem.State.Ready)
			{
				if (health > 0)
				{
					// ターゲットのHPが残っていれば
					item.Damage(totalPower);

					PlayAnimation("04. Jump", false, 2);
				}
				else
				{
					PlayAnimation("05. Attack", false, 2);
				}

				Debug.Log("Attacked.");
			}
		}
    }

    private float Charge()
    {
        return charge += (Time.deltaTime * CHARGE_BUFF);
    }

	/// <summary>
	/// アニメーション再生
	/// </summary>
	/// <param name="_type">どのアニメーションか</param>
	/// <param name="_loop">ループするかどうか</param>
	/// <param name="_speed">再生速度</param>
	private void PlayAnimation(string _animationName, bool _loop = false, float _speed = 1.0f)
    {
		Debug.Log("animationName : " + _animationName + ", loop : " + _loop + ", speed : " + _speed);

		var animationName = _animationName;
		var loop = _loop;

		var animState = m_AnimationState;
		animState.TimeScale = _speed;
		animState.SetAnimation(0, animationName, loop);
		if (loop)
		{
			// 何もしない
		}
		else
		{
			// ループしなければ、Idleに戻る
			animState.Complete += PlayDefaultAnimation;
		}
    }

	private void PlayDefaultAnimation(Spine.AnimationState state, int trackIndex, int loopCount)
	{
		PlayAnimation("01. Idle", true);
		m_AnimationState.Complete -= PlayDefaultAnimation;
	}
    #endregion

}// Class
