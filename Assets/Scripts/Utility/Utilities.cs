using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	#region PublicMethods
	/// <summary>
	/// intのIDを、Masterで扱う"ID_XXX"の形に整形する
	/// </summary>
	/// <returns>Master用整形済みstringのID</returns>
	public static string ConvertMasterRowID(int _id)
	{
		var padleft = _id.ToString().PadLeft(3, '0');
		var combine = ("ID_" + padleft);
		Debug.Log("Convert Key [" + _id.ToString() + "] => [" + combine);
		return combine;
	}

	/// <summary>
	/// ワールド座標をスクリーン座標に変換
	/// </summary>
	/// <returns>スクリーン座標</returns>
	public static Vector2 GetScreenPosition(Vector3 _worldPos)
	{
		var pos = Vector2.zero;

		var uiManager = UIManager.Instance;
		var mainCanvasRect = uiManager.GetMainCanvasRect();
		var mainCamera = uiManager.GetMainCamera();
		var uiCamera = uiManager.GetUICamera();

		var screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, _worldPos);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			mainCanvasRect, screenPos, uiCamera, out pos);

		return pos;
	}

	/// <summary>
	/// CanvasGroupの表示切り替え
	/// </summary>
	/// <param name="_canvasGroup">対象CanvasGroup</param>
	/// <param name="_value">ON / OFF</param>
	public static void ToggleCanvasGroup(CanvasGroup _canvasGroup, bool _value)
	{
		if (_value)
		{
			// => ON
			_canvasGroup.alpha = 1;
			_canvasGroup.interactable = true;
			_canvasGroup.blocksRaycasts = true;
			Debug.Log("Showing: " + _canvasGroup);
		}
		else
		{
			// => OFF
			_canvasGroup.alpha = 0;
			_canvasGroup.interactable = false;
			_canvasGroup.blocksRaycasts = false;
			Debug.Log("Hidding: " + _canvasGroup);
		}
	}
	#endregion// PublicMethods
}
