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
		Utilities.ToggleCanvasGroup(canvasGroup, true);
	}

	public virtual void Hide()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		Utilities.ToggleCanvasGroup(canvasGroup, false);
	}
	#endregion// PublicMethods
}
