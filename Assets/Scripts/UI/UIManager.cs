using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DG.Tweening;

/// <summary>
/// UIの管理を行うクラス、オブジェクトプーリングも行う。
/// </summary>
public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Serializable]
    public class UI
    {
		private Canvas m_Canvas = null;
		public Canvas canvas {
			get { return m_Canvas ?? (m_Canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ()); }
		}

        // ステータステキスト
        public Text goldText = null;
        // メニュー
		public Button menuButton = null;// 親
		public Button itemButton = null;// 子
        [SerializeField, HeaderAttribute("__________Parents__________")]
        public Transform particlesParent = null;
    }
    #region variables
    [SerializeField] private UI m_UI;

	[SerializeField]
	private ObjectPooling m_FloatingTextPooling = null;

    private bool isMenuAnimRunning = false;
    #endregion// variables
    #region properties
    public UI ui { get { return m_UI; } private set { m_UI = value; } }

    public ObjectPooling floatingTextPooling { get { return m_FloatingTextPooling; } }
    #endregion// properties

    #region unity callbacks
    private void Awake()
    {
        Init ();
    }
    #endregion// unity callbacks

    #region public methods
    public void Init()
    {
        isMenuAnimRunning = false;

        // 子メニューボタンの配置初期化
        ui.itemButton.transform.localPosition = ui.menuButton.transform.localPosition;

        UnityAction menuClick = OnClickMenu;
        ui.menuButton.onClick.RemoveAllListeners ();
        ui.menuButton.onClick.AddListener (menuClick);

        UnityAction itemClick = OnClickItemMenu;
        ui.itemButton.onClick.RemoveAllListeners ();
        ui.itemButton.onClick.AddListener (itemClick);

        ui.goldText = GameObject.Find ("Canvas").transform.FindChild ("StatusBar/GoldIcon/Gold").GetComponent<Text>();

        m_FloatingTextPooling = new ObjectPooling ();
        m_FloatingTextPooling.Init ("Prefabs/UI/Texts/FloatingText", 10);
    }

    public void OnClickMenu()
    {
        bool isMenu = GlobalData.Instance.isMenu;

        if (isMenu) {
            // 閉じる
            StartCoroutine(PlayMenuAnimation(true));
            Inventory.Instance.Init ();
        } else {
            // 開く
            StartCoroutine(PlayMenuAnimation(false));
        }
    }

    /// <summary>
    /// OnCricking ItemMenu
    /// </summary>
    public void OnClickItemMenu()
    {
        if (Inventory.Instance.data.isActive) {
            Inventory.Instance.Hide ();
        } else {
            Inventory.Instance.Show ();
        }
    }

	/// <summary>
	/// メインキャンバスを返す(null時自動取得)
	/// </summary>
	public Canvas GetCanvas()
	{
		return ui.canvas;
	}
    #endregion// public methods
    #region private methods
    private IEnumerator PlayMenuAnimation(bool _IsReverse = false)
    {
        if (isMenuAnimRunning) {
            yield break;
        }
        isMenuAnimRunning = true;

        Vector3 from = ui.menuButton.transform.localPosition;
        Vector3 to = new Vector3 (from.x - 50, from.y - 200, 0);
        Debug.Log ("TO: " + to);

        if (!_IsReverse) {
            ui.itemButton.transform.localPosition = from;
            ui.itemButton.transform.DOLocalMove (to, 0.3f).
            OnComplete(() => isMenuAnimRunning = false);
        } else {
            ui.itemButton.transform.localPosition = to;
            ui.itemButton.transform.DOLocalMove (from, 0.3f).
            OnComplete(() => isMenuAnimRunning = false);
        }

        var wait = new WaitWhile (() => isMenuAnimRunning);
        yield return wait;

        GlobalData.Instance.isMenu = !GlobalData.Instance.isMenu;
        yield break;
    }
    #endregion// private methods

}// UIManager

/// <summary>
/// オブジェクトプーリング管理を行う。UIの種類毎にインスタンス保持して扱う。
/// </summary>
[Serializable]
public class ObjectPooling
{
    #region variables
    private GameObject m_Prefab = null;

    private GameObject m_PoolingGO = null;// 複製元

	[SerializeField]
	private List<GameObject> m_PoolingList = null;

    private int m_MaxCount = 0;// 最大プール数
    #endregion// variables
    public List<GameObject> poolingList { get { return m_PoolingList; } private set { m_PoolingList = value; } }
    #region properties

    #endregion// properties

    #region public methods
    public void Init(string _prefabPath, int _maxCount)
    {
        m_PoolingList = new List<GameObject> ();

        m_Prefab = Resources.Load ("Prefabs/UI/Texts/FloatingText") as GameObject;
        m_MaxCount = _maxCount;
    }

    public GameObject PickOut()
    {
        int count = poolingList.Count;
        // 未使用のプールがあればそれを返す
        for (int i = 0; i < count; i++) {
            if (poolingList [i].gameObject.activeSelf == false) {
                // 非アクティブなら
                var go = poolingList [i].gameObject;
                go.SetActive (true);

                return go;
            }
        }
        // 全て使用中で、かつ最大生成数を超えていないとき
        if (count < m_MaxCount) {
            var go = GameObject.Instantiate (m_Prefab);
            go.SetActive (true);
            m_PoolingList.Add (go);

            return go;
        }

        return null;
    }
    #endregion// public methods
}// UIObjectPooling