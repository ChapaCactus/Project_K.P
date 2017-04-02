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
    #endregion// Variables

    #region Properties
	public Text bodyText {
		get { return m_BodyText ?? (m_BodyText = transform.FindChild("Text").GetComponent<Text>()); }
	}
    #endregion// Properties

    #region PublicMethods
    /// <summary>1
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
	}

	public void SetText(string _str)
	{
		bodyText.text = _str;
	}

	/// <summary>
	/// 表示 => 一定時間後に非表示
	/// SetText()で初期化してから呼び出すこと
	/// </summary>
	public void Play(AnimationType _type, float _duration = 1.0f, float _toX = 0, float _toY = 0)
	{
		var toLocalPos = new Vector2(_toX, _toY);
		
		StartCoroutine(PlayCoroutine(_type, _duration, toLocalPos));
	}

	public IEnumerator PlayCoroutine(AnimationType _type, float _duration, Vector2 _toLocalPos)
	{
		if (isActive) {
			yield break;
		}
		isActive = true;

		Utilities.ToggleCanvasGroup(GetComponent<CanvasGroup>(), true);

		var text = bodyText;

		switch (_type)
		{
			case AnimationType.Fade:
				Sequence sequence = DOTween.Sequence()
					.OnStart(() => {
					text.color = Color.white;
				});
				sequence.Prepend(transform.DOLocalMove(_toLocalPos, _duration)
				                 .SetRelative()
				                 .SetEase(Ease.OutExpo)
				                 .OnComplete(() => Hide()));
				sequence.Join(text.DOFade(0, _duration)
				              .SetEase(Ease.InQuint));
				sequence.Play();
				break;

			default:
				break;
		}

    }

    public void Hide()
    {
		isActive = false;

		Utilities.ToggleCanvasGroup(GetComponent<CanvasGroup>(), false);
    }

    public string GetText()
    {
		return bodyText.text;
    }
    #endregion// PublicMethods

}// class
