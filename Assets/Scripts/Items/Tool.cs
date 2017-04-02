using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google2u;

namespace KP
{
	/// <summary>
	/// 装備ー道具クラス
	/// </summary>
	public class Tool
	{
		#region Variables
		public int toolID = 0;

		public string name = "";

		public int level = 0;
		public int maxLevel = 0;

		public int m_Power = 0;
		#endregion// Variableas

		#region PublicMethods
		public static Tool Create(int _toolID, int _level)
		{
			Tool tool = new Tool();
			var master = ToolMaster.Instance.GetRow(Utilities.ConvertMasterRowID(_toolID));

			tool.toolID = _toolID;
			tool.name = master._Name;
			tool.level = _level;
			tool.maxLevel = master._MaxLevel;

			tool.m_Power = master._Power;

			return tool;
		}

		public int GetTotalPower()
		{
			return (m_Power + level);
		}
		#endregion// PublicMethods

	}// Tool

}// KP