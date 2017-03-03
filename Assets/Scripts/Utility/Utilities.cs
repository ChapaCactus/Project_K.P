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
		return ("ID_" + padleft);
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
	#endregion// PublicMethods
}
