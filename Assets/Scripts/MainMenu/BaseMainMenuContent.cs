using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMainMenuContent : SingletonMonoBehaviour<BaseMainMenuContent>
{
	#region Variables
	protected CanvasGroup m_CanvasGroup = null;
	#endregion// Variables

	#region PublicMethods
	public virtual void Init()
	{
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	public virtual void Show()
	{
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
		m_CanvasGroup.blocksRaycasts = true;
	}

	public virtual void Hide()
	{
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
		m_CanvasGroup.blocksRaycasts = false;
	}
	#endregion// PublicMethods
}
