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

        var mainCanvasRect = UIManager.Instance.GetMainCanvasRect();
        var mainCamera = UIManager.Instance.GetMainCamera();
        var uiCamera = UIManager.Instance.GetUICamera();

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
    private void Attack(BaseItem _targetItem)
    {
		if (_targetItem == null) {
            return;
        }

		var state = _targetItem.GetState ();
		int health = _targetItem.GetHealth ();
		if (health > 0 && state == BaseItem.State.Ready) {
			// ターゲットのHPが残っていれば
			health = _targetItem.Damage (totalPower);
			Debug.Log ("Target AfterHealth : " + health.ToString());

			if (health <= 0) {
				// HPが0になっていたら消す
				Debug.Log("Destroying : " + _targetItem);
				GlobalData.Instance.AddMoney (1);// Test money
                GlobalData.Instance.GainExp(1);

                var message = ("+" + 1.ToString());
                var floatingText = FloatingText.Create();
                floatingText.transform.localPosition = GetScreenPosition();
                floatingText.Show(message, 1f);
                Debug.Log("Check expText : " + floatingText);

				Stage.Instance.platform.KillItem();
			}
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
