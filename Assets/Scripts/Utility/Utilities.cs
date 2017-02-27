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
	/// <param name="_id">intのID</param>
	public static string GetMasterRowID(int _id)
	{
		var padleft = _id.ToString().PadLeft(3, '0');
		return ("ID_" + padleft);
	}
	#endregion// PublicMethods
}
