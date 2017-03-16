using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// HPバークラス
/// 170212:１画面に一つまでとする(T.Titansみたいな感じ？)
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class HealthBar : SingletonMonoBehaviour<HealthBar>
{
	#region Properties
	public float sliderValue {
		get { return m_Slider.value; }
		private set { m_Slider.value = value; }
	}
	#endregion// Properties

	#region Variables
	private Slider m_Slider = null;
	#endregion// Variables

	#region PublicMethods
	public static HealthBar Create(Transform _parent)
	{
		var prefab = Resources.Load("Prefabs/UI/HealthBar") as GameObject;
		HealthBar hpBar = Instantiate(prefab, _parent, false).GetComponent<HealthBar>();

		return hpBar;
	}

	public void Init()
	{
		m_Slider = transform.FindChild("Slider").GetComponent<Slider>();
		m_Slider.minValue = 0;
	}

	/// <summary>
	/// スライダー上限等の設定
	/// </summary>
	public void Setup(int _maxHP)
	{
		m_Slider.maxValue = _maxHP;
		m_Slider.value = _maxHP;
	}

	/// <summary>
	/// スライダーの値を更新
	/// </summary>
	/// <param name="_afterValue">変更後の残HPなど</param>
	public void UpdateSliderValue(int _afterValue)
	{
		sliderValue = _afterValue;
	}

	public void Show()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		Utilities.ToggleCanvasGroup(canvasGroup, true);
	}

	public void Hide()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		Utilities.ToggleCanvasGroup(canvasGroup, false);
	}
	#endregion// PublicMethods
}
