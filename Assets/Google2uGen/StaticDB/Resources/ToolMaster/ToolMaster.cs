//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;
using System.Globalization;

namespace Google2u
{
	[System.Serializable]
	public class ToolMasterRow : IGoogle2uRow
	{
		public string _Name;
		public int _Power;
		public int _Cost;
		public int _MaxLevel;
		public ToolMasterRow(string __ID, string __Name, string __Power, string __Cost, string __MaxLevel) 
		{
			_Name = __Name.Trim();
			{
			int res;
				if(int.TryParse(__Power, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Power = res;
				else
					Debug.LogError("Failed To Convert _Power string: "+ __Power +" to int");
			}
			{
			int res;
				if(int.TryParse(__Cost, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Cost = res;
				else
					Debug.LogError("Failed To Convert _Cost string: "+ __Cost +" to int");
			}
			{
			int res;
				if(int.TryParse(__MaxLevel, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_MaxLevel = res;
				else
					Debug.LogError("Failed To Convert _MaxLevel string: "+ __MaxLevel +" to int");
			}
		}

		public int Length { get { return 4; } }

		public string this[int i]
		{
		    get
		    {
		        return GetStringDataByIndex(i);
		    }
		}

		public string GetStringDataByIndex( int index )
		{
			string ret = System.String.Empty;
			switch( index )
			{
				case 0:
					ret = _Name.ToString();
					break;
				case 1:
					ret = _Power.ToString();
					break;
				case 2:
					ret = _Cost.ToString();
					break;
				case 3:
					ret = _MaxLevel.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "Name":
					ret = _Name.ToString();
					break;
				case "Power":
					ret = _Power.ToString();
					break;
				case "Cost":
					ret = _Cost.ToString();
					break;
				case "MaxLevel":
					ret = _MaxLevel.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Power" + " : " + _Power.ToString() + "} ";
			ret += "{" + "Cost" + " : " + _Cost.ToString() + "} ";
			ret += "{" + "MaxLevel" + " : " + _MaxLevel.ToString() + "} ";
			return ret;
		}
	}
	public sealed class ToolMaster : IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001
		};
		public string [] rowNames = {
			"ID_000", "ID_001"
		};
		public System.Collections.Generic.List<ToolMasterRow> Rows = new System.Collections.Generic.List<ToolMasterRow>();

		public static ToolMaster Instance
		{
			get { return NestedToolMaster.instance; }
		}

		private class NestedToolMaster
		{
			static NestedToolMaster() { }
			internal static readonly ToolMaster instance = new ToolMaster();
		}

		private ToolMaster()
		{
			Rows.Add( new ToolMasterRow("ID_000", "テストの装備", "5", "1", "99"));
			Rows.Add( new ToolMasterRow("ID_001", "ぼろぼろピッケル", "1", "1", "99"));
		}
		public IGoogle2uRow GetGenRow(string in_RowString)
		{
			IGoogle2uRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}
		public IGoogle2uRow GetGenRow(rowIds in_RowID)
		{
			IGoogle2uRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public ToolMasterRow GetRow(rowIds in_RowID)
		{
			ToolMasterRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public ToolMasterRow GetRow(string in_RowString)
		{
			ToolMasterRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}

	}

}
