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
		#region Properties
		public string name
		{
			get
			{
				return m_Name;
			}
		}
		#endregion// Properties

		#region Variables
		public int toolID = 0;
		public int level = 0;
		public int maxLevel = 0;

		private string m_Name = "";
		private int m_Power = 0;
		#endregion// Variableas

		#region PublicMethods
		public static Tool Create(int _toolID, int _level)
		{
			var master = ToolMaster.Instance.GetRow(Utilities.ConvertMasterRowID(_toolID));
			Tool tool = new Tool(_toolID, _level, master);

			return tool;
		}

		public Tool(int _toolID, int _level, ToolMasterRow _master)
		{
			toolID = _toolID;
			m_Name = _master._Name;
			level = _level;
			maxLevel = _master._MaxLevel;

			m_Power = _master._Power;
		}

		public int GetTotalPower()
		{
			return (m_Power + level);
		}
		#endregion// PublicMethods

	}// Tool

}// KP