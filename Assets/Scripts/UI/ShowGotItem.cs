using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 手に入れたアイテムを表示するウィンドウUI
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class ShowGotItem : MonoBehaviour 
{
    #region variables
    [SerializeField] private Text m_ItemNameText = null;
    [SerializeField] private Image m_GotItemImage = null;

    [SerializeField] private ParticleSystem m_SparkleEffect = null;

    private Coroutine m_ShowCoroutine = null;

    private CanvasGroup m_CanvasGroup = null;
    #endregion// variables

    #region properties
    public CanvasGroup canvasGroup {
        get { return m_CanvasGroup ?? (m_CanvasGroup = GetComponent<CanvasGroup> ()); }
        private set { m_CanvasGroup = value; }
    }
    #endregion// properties

    #region public methods
    public void Init()
    {
        canvasGroup = GetComponent<CanvasGroup> ();
    }
        
    public void Show(string _name, Sprite _sprite, bool _isFirstGet = false)
    {
        if (m_ShowCoroutine != null) {
            StopCoroutine (m_ShowCoroutine);
            m_ShowCoroutine = null;
        }

        // アイテムを表示
        m_ItemNameText.text = _name;
        m_GotItemImage.sprite = _sprite;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        m_SparkleEffect.Play ();

        m_ShowCoroutine = StartCoroutine (ShowCoroutine ());
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        m_SparkleEffect.Stop ();
    }

    [ContextMenu("TestShow")]
    public void TestShow()
    {
        Show ("Test", null, true);
    }

    [ContextMenu("TestHide")]
    public void TestHide()
    {
        Hide ();
    }
    #endregion// public methods

    #region private methods
    private IEnumerator ShowCoroutine()
    {
        var wait = new WaitForSeconds (3f);
        yield return wait;

        Hide ();

        m_ShowCoroutine = null;
        yield break;
    }
    #endregion// private methods

}// ShowGotItem
