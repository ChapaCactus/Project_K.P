using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// レベルアップ時など、テキストのみで情報を表示するUIクラス
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FloatingText : PoolingBaseClass
{
    #region Enums
    public enum AnimationType
    {
        None,
        Fade,
    }
	#endregion// Enums

	#region Variables
    private Text m_BodyText = null;

	private CanvasGroup m_CanvasGroup = null;
    #endregion// Variables

    #region Properties
	public Text bodyText {
		get { return m_BodyText ?? (m_BodyText = transform.FindChild("Text").GetComponent<Text>()); }
	}

	public CanvasGroup canvasGroup {
		get { return m_CanvasGroup ?? (m_CanvasGroup = GetComponent<CanvasGroup>()); }
	}
    #endregion// Properties

    #region PublicMethods
    /// <summary>
    /// Pickout in Pooling
    /// </summary>
    public static FloatingText Create()
    {
        var go = UIManager.Instance.floatingTextPooling.PickOut ();
        var floatingText = go.GetComponent<FloatingText> ();
		Debug.Log ("floating " + floatingText);

		floatingText.Init();

        return floatingText;
    }

	public override void Init()
	{
		Hide();
		SetText("");
	}

	/// <summary>
	/// 表示 => 一定時間後に非表示
	/// SetText()で初期化してから呼び出すこと
	/// </summary>
	public override void Show(float _duration = 1.0f)
	{
		StartCoroutine(ShowCoroutine(_duration));
	}

	public IEnumerator ShowCoroutine(float _duration)
	{
		if (isActive) {
			yield break;
		}
		isActive = true;

		var cg = canvasGroup;
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;

		// 指定時間、待機
		var wait = new WaitForSeconds(_duration);
		yield return wait;
		// 一定時間後、非表示
		Hide();
    }

    public void Hide()
    {
		isActive = false;

		var cg = canvasGroup;
		cg.alpha = 0;
		cg.interactable = false;
		cg.blocksRaycasts = false;
    }

    public string GetText()
    {
		return bodyText.text;
    }

	public void SetText(string _str)
	{
		bodyText.text = _str;
	}
    #endregion// PublicMethods

}// class
