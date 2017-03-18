using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム図鑑
/// アイテムを収集したか、HashSetで登録する
/// 必要な関数が少なくなりそうなら、GlobalDataに統合してもいいかも。
/// </summary>
public static class ItemIndex
{
	#region Properties
	private static HashSet<int> itemIndex { get { return GlobalData.itemIndex; } }
	#endregion// Properties

	#region PublicMethods
	/// <summary>
	/// 図鑑に追加 [追加成功] return true : [追加失敗] return false
	/// </summary>
	public static bool CheckIsIDInIndex(int _itemID)
	{
		foreach (var check in itemIndex)
		{
			if (check == _itemID)
			{
				Debug.Log("図鑑追加失敗 既に登録されています id => " + _itemID);
				return true;
			}
		}

		Debug.Log("図鑑に登録されていません id => " + _itemID);
		return false;
	}
	#endregion// PublicMethods
}
