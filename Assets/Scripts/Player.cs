using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controll player character.
/// </summary>
public class Player : MonoBehaviour
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
    #endregion

    #region properties
    /// <summary>
    /// プレイヤーの補正後最終攻撃力(能力上昇や、ステータスダウンも含める)
    /// </summary>
    public int totalPower { get { return GlobalData.Instance.playerStatus.power; } }

    public int exp { get { return GlobalData.Instance.exp; } }
    #endregion// properties

    #region unity callbacks
    private void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            BaseItem item = Stage.Instance.platform.GetItem();
            if (item != null) {
                // アイテムがあれば
                Attack(item);
            }
        }

        if (m_ChargeTrigger) {
            // Charging
            Charge ();
        }

//        if (Input.GetMouseButtonUp (0)) {
//            // アニメーション再生
//            PlayAnimTypeA ();
//        }
    }
    #endregion// unity callbacks

    #region public methods
    public void Init()
    {
        m_Transform = gameObject.transform;
    }

    public Transform GetTransform()
    {
        return m_Transform ?? (m_Transform = gameObject.transform);
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
    private void Attack(BaseItem _targetItem)
    {
        Debug.Log ("_targetItem : " + _targetItem);
        if (_targetItem.data.state != BaseItem.State.Ready || _targetItem == null) {
            return;
        }

        int health = _targetItem.Damage (totalPower);

        Debug.Log ("Attack power: " + totalPower + ", Damaged Health: " + health);
        if (health <= 0) {
            // HPが0になっていたら消す
            Debug.Log("Destroying : " + _targetItem);
            GlobalData.Instance.AddMoney (1);// Test money
            Stage.Instance.platform.KillItem();
        }

        Debug.Log ("Attacked.");
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
