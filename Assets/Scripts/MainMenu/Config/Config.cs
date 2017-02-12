using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Config : SingletonMonoBehaviour<Config>
{
	#region Variables
	private CanvasGroup m_CanvasGroup = null;
	#endregion// Variables

	#region UnityCallbacks
	private void Awake()
	{
		Init();
	}
	#endregion// UnityCallbacks

	#region PublicMethods
	public void Init()
	{
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	public void Show()
	{
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
		m_CanvasGroup.blocksRaycasts = true;
	}

	public void Hide()
	{
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
		m_CanvasGroup.blocksRaycasts = false;
	}
	#endregion// PublicMethods
}// Config
