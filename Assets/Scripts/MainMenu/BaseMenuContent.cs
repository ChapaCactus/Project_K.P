using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMainMenuContent : MonoBehaviour
{
	#region Variables
	#endregion// Variables

	#region PublicMethods
	public virtual void Show()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}

	public virtual void Hide()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}
	#endregion// PublicMethods
}
