using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Daikon : BaseItem
{
    #region variables
	[SerializeField] protected SpriteRenderer m_TopRenderer = null;
	[SerializeField] protected SpriteRenderer m_BottomRenderer;

	// Tween
	private Sequence m_SwingSequence = null;

	protected const string TOP_PATH = "Daikon_Top";
	protected const string BOTTOM_PATH = "Daikon_Bottom";

    #endregion// variables

    #region properties
    public SpriteRenderer topRenderer {
        get {
            return m_TopRenderer ? m_TopRenderer : m_TopRenderer = transform.FindChild(TOP_PATH).GetComponent<SpriteRenderer>();
        }
        protected set { m_TopRenderer = value; }
    }

    public SpriteRenderer bottomRenderer {
        get {
            return m_BottomRenderer ? m_BottomRenderer : m_BottomRenderer = transform.FindChild(BOTTOM_PATH).GetComponent<SpriteRenderer>();
        }
        protected set { m_BottomRenderer = value; }
    }
    #endregion// properties

    #region unity callbacks
	private void Awake()
	{
		Init();
		CreateSwingSequence();
	}

    #endregion// unity callbacks

    #region public methods
    public static Daikon Create(Vector3 _LocalPos, Transform _parent = null)
    {
        var path = "Prefabs/Items/Daikon";
        var prefab = Resources.Load (path) as GameObject;

        Daikon result = Instantiate (prefab, _parent).GetComponent<Daikon>();
        result.name = prefab.name;
        result.transform.localPosition = _LocalPos;

        return result;
    }

	public override void Init()
    {
		base.Init ();
		m_SwingSequence = null;
	}

    #endregion// public methods

    #region private methods
	private void CreateSwingSequence()
	{
		m_SwingSequence = DOTween.Sequence();
		m_SwingSequence.Prepend(topRenderer.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.5f));
		m_SwingSequence.Append(
			topRenderer.transform.DOLocalRotate(new Vector3(0, 0, -20), 1f)//.SetLoops(100, LoopType.Yoyo)
		).SetLoops(-1, LoopType.Yoyo);
	}

    #endregion// private methods
}// Class.
