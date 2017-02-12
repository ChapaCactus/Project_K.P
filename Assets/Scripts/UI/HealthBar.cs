using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// HPバークラス
/// 170212:１画面に一つまでとする(T.Titansみたいな感じ？)
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class HealthBar : MonoBehaviour
{
	#region PublicMethods
	public static HealthBar Create(Transform _parent)
	{
		var prefab = Resources.Load("") as GameObject;
		HealthBar hpBar = Instantiate(prefab, _parent, false).GetComponent<HealthBar>();

		return hpBar;
	}
	#endregion// PublicMethods
}
