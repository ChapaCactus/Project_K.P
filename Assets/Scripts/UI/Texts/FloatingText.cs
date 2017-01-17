using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// レベルアップ時など、テキストのみで情報を表示するUIクラス
/// </summary>
public class FloatingText : MonoBehaviour
{
    #region enums
    public enum AnimationType
    {
        None,
        Fade,
    }
    #endregion// enums

    #region variables
    private Text m_Text = null;
    #endregion// variables

    #region properties
    public Text floatingText { get { return m_Text; } private set { m_Text = value; } }

    public string text { get { return m_Text.text; } set { m_Text.text = value; } }
    #endregion// properties

    #region public methods
    /// <summary>
    /// Pickout in Pooling
    /// </summary>
    public static FloatingText Create()
    {
        var go = UIManager.Instance.floatingTextPooling.PickOut ();
        var floatingText = go.GetComponent<FloatingText> ();

        return floatingText;
    }

    public void Show(string _message, float _duration = 1.0f)
    {
        floatingText.text = _message;
        floatingText.enabled = true;
        gameObject.SetActive (true);
    }

    public void Hide()
    {
        floatingText.enabled = false;
        gameObject.SetActive (false);
    }

    /// <summary>
    /// Get floating text strings
    /// </summary>
    public string GetText()
    {
        return floatingText.text;
    }
    #endregion// public methods

}// class
