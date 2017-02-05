using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controll player character.
/// </summary>
public class Player : SingletonMonoBehaviour<Player>
{
    #region Variables
    private Transform m_Transform = null;

    [SerializeField] private BaseItem m_Target = null;
    [SerializeField] private bool m_ChargeTrigger = false;
    // チャージ
    [SerializeField] private float m_Charge = 0;

    public float charge {
        get { return m_Charge; }
        private set { Mathf.Clamp(m_Charge = value, MIN_CHARGE_VALUE, MAX_CHARGE_VALUE); }
    }

    private const int   MAX_CHARGE_VALUE = 1;
    private const int   MIN_CHARGE_VALUE = 0;
    private const float CHARGE_BUFF      = 0.1f;
    #endregion// Variables

    #region Properties
    /// <summary>
    /// プレイヤーの補正後最終攻撃力(能力上昇や、ステータスダウンも含める)
    /// </summary>
    public int totalPower { get { return GlobalData.Instance.playerStatus.power; } }

    public int exp { get { return GlobalData.Instance.exp; } }
    #endregion// Properties

    #region UnityCallbacks
    private void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
			Attack();
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
        m_Transform = gameObject.transform;
    }

    public Transform GetTransform()
    {
        return m_Transform ?? (m_Transform = gameObject.transform);
    }

    /// <summary>
    /// このPlayerのスクリーン座標をUI用に変換して返す
    /// </summary>
    public Vector2 GetScreenPosition()
    {
        var pos = Vector2.zero;

		var uiManager = UIManager.Instance;
        var mainCanvasRect = uiManager.GetMainCanvasRect();
        var mainCamera = uiManager.GetMainCamera();
        var uiCamera = uiManager.GetUICamera();

        var screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvasRect, screenPos, uiCamera, out pos);

        return pos;
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
    #endregion// public methods

    #region Private methods
    private void Attack()
    {
		BaseItem item = Stage.Instance.platforms[0].GetItem();

		if (item == null)
		{
			return;
		}
		else
		{
			var state = item.GetState();// 状態
			int health = item.GetHealth();// 体力
			// 体力があって、かつ準備完了状態なら
			if (health > 0 && state == BaseItem.State.Ready)
			{
				// ターゲットのHPが残っていれば
				item.Damage(totalPower);
			}

			Debug.Log("Attacked.");
		}
    }

    private float Charge()
    {
        return charge += (Time.deltaTime * CHARGE_BUFF);
    }

    private void PlayAnimTypeA()
    {
        transform.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();
        seq.Append (transform.DOScaleX(1.3f, 0.3f));
        seq.OnComplete (() => transform.localScale = Vector3.one);
    }
    #endregion

}// Class
