using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google2u;

/// <summary>
/// フィールドに生成されるアイテム(GameObject)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BaseItem : MonoBehaviour
{
    /// <summary>
    /// データ受け渡し、初期化用
    /// </summary>
    [Serializable]
    public struct Params
    {
        public int id;
        public string name;
        public Type type;
        public int price;
        public int rarity;
        public int health;
        public int exp;

        public string resource;
        public string prefab;

        public Params(int _id, string _name, Type _type, int _price, int _rarity, int _health
            , int _exp, string _resource, string _prefab)
        {
            id       = _id;
            name     = _name;
            type     = _type;
            price    = _price;
            rarity   = _rarity;
            health   = _health;
            resource = _resource;
            prefab   = _prefab;
            exp      = _exp;
        }
    }

    [Serializable]
    public class Data
    {
		#region Properties
		public State state { get { return m_State; } set { m_State = value; } }
		public int id { get { return m_ID; } set { m_ID = value; } }
		public string name { get { return m_Name; } set { m_Name = value; } }
		public Type type { get { return m_Type; } set { m_Type = value; } }
		public int price { get { return m_Price; } set { m_Price = value; } }
		public int rarity { get { return m_Rarity; } set { m_Rarity = value; } }

		public int health {
			get { return m_Health; }
			set {
				m_Health = value;
				if (m_Health < 0) m_Health = 0;
			}
		}
		public int exp { get { return m_Exp; } set { m_Exp = value; } }
		public Rigidbody2D rigid2D { get { return m_Rigid2D; } set { m_Rigid2D = value; } }
		public SpriteRenderer[] renderers { get { return m_SpriteRenderersInChild; } set { m_SpriteRenderersInChild = value; } }
		#endregion// Properties

		#region Variables
		[SerializeField]
        private State m_State = State.None;
        [SerializeField, HeaderAttribute("基本情報")]
        private int m_ID              = 0;
        [SerializeField]
        private string m_Name         = "NO NAME";
        [SerializeField]
        private Type m_Type           = Type.Plug;
        [SerializeField]
        private int m_Price           = 0;
        [SerializeField]
        private int m_Rarity          = 0;
        [SerializeField, HeaderAttribute("ステータス")]
        private int m_Health          = 0;
        [SerializeField]
        private int m_Exp             = 0;// take exp.
        [SerializeField]
		// Component系 //
        private Rigidbody2D m_Rigid2D = null;
		[SerializeField]
		private SpriteRenderer[] m_SpriteRenderersInChild = null;// 透過アニメーション用、子全てのレンダラー
        #endregion// Variables
    }

    #region enum
	// ItemType.
	public enum Type { Crop = 0, Plug, Num }
    // State.
    public enum State { None, Ready, Dead }
    #endregion// enum

    #region variables
    [SerializeField] private Data m_Data = new Data ();

    [SerializeField] private bool m_IsRunningPopCoroutine = false;
    #endregion// variables

    #region properties
    public Data data { get { return m_Data; } private set { m_Data = value; } }
    #endregion// properties

    #region public methods
    public virtual void Init()
	{
        // Init Components
        data.rigid2D = GetComponent<Rigidbody2D> ();

        data.state = State.None;

        // Lock Rigidbody
        ChangeConstraints(true, true, true);
		// 子のレンダラーを全て取得
		data.renderers = GetComponentsInChildren<SpriteRenderer> ();

		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
	}

	public int GetHealth()
	{
		return data.health;
	}

	/// <summary>
	/// このアイテムの状態を返す
	/// </summary>
	public State GetState()
	{
		return data.state;
	}

    public void StartPopCoroutine()
    {
        StartCoroutine (PopCoroutine ());
    }

    /// <summary>
    /// パラメータ設定
    /// </summary>
	public virtual void SetParams(int _id, ItemMasterRow _row)
    {
		data.id = _id;// 割り出し用
		data.name = _row._Name;
		//data.type = _row._Type;
		data.price = _row._Price;
		data.rarity = _row._Rarity;

		data.health = _row._Health;
		data.exp = _row._Exp;
    }

    public int Damage(int _point)
    {
		data.health -= _point;

		// HPが0になっていたら消してインベントリへ
		if (data.health <= 0)
		{
			AddInventory();

			//GlobalData.Instance.AddMoney(data.exp);// Test money
			//GlobalData.Instance.GainExp(data.exp);

			var message = ("+" + data.exp.ToString());
			var floatingText = FloatingText.Create();
			floatingText.transform.localPosition = Player.Instance.GetScreenPosition();
			floatingText.SetText(message);
			floatingText.Show(1f);
		}

		return data.health;
    }
	#endregion// public methods

	#region Private methods
	/// <summary>
	/// インベントリにアイテムとして追加
	/// </summary>
	private void AddInventory()
	{
		GlobalData.Instance.AddItem(data.id, 1);
		Stage.Instance.platforms[0].KillItem();
	}

    /// <summary>
    /// Locking Position XY(Position), Z(Rotation)
    /// </summary>
    protected void ChangeConstraints(bool _LockX = false, bool _LockY = false, bool _LockZ = false)
    {
        // Initialize
        data.rigid2D.constraints = RigidbodyConstraints2D.None;
        // Switching
        if (_LockX && _LockY && _LockZ) {
            data.rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
        } else if (_LockX && _LockY && !_LockZ) {
            data.rigid2D.constraints = RigidbodyConstraints2D.FreezePosition;
        } else if (!_LockX && !_LockY && _LockZ) {
            data.rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else if (_LockX && !_LockY) {
            data.rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            if(_LockZ)
                data.rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else if (!_LockX && _LockY) {
            data.rigid2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            if(_LockZ)
                data.rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    /// <summary>
    /// アイテムが出現するコルーチン
    /// </summary>
    protected IEnumerator PopCoroutine()
    {
        if (m_IsRunningPopCoroutine) {
            yield break;
        }
        m_IsRunningPopCoroutine = true;
		// 出現待機
		var wait = new WaitForSeconds (0.3f);
		yield return wait;

        data.state = State.Ready;

        m_IsRunningPopCoroutine = false;
        yield return null;
    }

    protected void PlayAnimation(int _Type)
    {
        switch (_Type) {
        case 0:
            AnimationTypeA ();
            break;
        }
    }

    protected virtual void AnimationTypeA()
    {
        
    }

    protected void Dead()
    {
        Destroy (gameObject);
    }
    #endregion

}// Class.
