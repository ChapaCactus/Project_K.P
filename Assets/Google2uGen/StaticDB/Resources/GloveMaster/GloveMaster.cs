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
	public class GloveMasterRow : IGoogle2uRow
	{
		public string _Name;
		public int _Power;
		public GloveMasterRow(string __ID, string __Name, string __Power) 
		{
			_Name = __Name.Trim();
			{
			int res;
				if(int.TryParse(__Power, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Power = res;
				else
					Debug.LogError("Failed To Convert _Power string: "+ __Power +" to int");
			}
		}

		public int Length { get { return 2; } }

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
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Power" + " : " + _Power.ToString() + "} ";
			return ret;
		}
	}
	public sealed class GloveMaster : IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001, ID_002, ID_003
		};
		public string [] rowNames = {
			"ID_000", "ID_001", "ID_002", "ID_003"
		};
		public System.Collections.Generic.List<GloveMasterRow> Rows = new System.Collections.Generic.List<GloveMasterRow>();

		public static GloveMaster Instance
		{
			get { return NestedGloveMaster.instance; }
		}

		private class NestedGloveMaster
		{
			static NestedGloveMaster() { }
			internal static readonly GloveMaster instance = new GloveMaster();
		}

		private GloveMaster()
		{
			Rows.Add( new GloveMasterRow("ID_000", "テストの装備", "5"));
			Rows.Add( new GloveMasterRow("ID_001", "はじまりグローブ", "1"));
			Rows.Add( new GloveMasterRow("ID_002", "ドワーフグローブ", "2"));
			Rows.Add( new GloveMasterRow("ID_003", "トゲトゲグローブ", "5"));
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
		public GloveMasterRow GetRow(rowIds in_RowID)
		{
			GloveMasterRow ret = null;
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
		public GloveMasterRow GetRow(string in_RowString)
		{
			GloveMasterRow ret = null;
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
