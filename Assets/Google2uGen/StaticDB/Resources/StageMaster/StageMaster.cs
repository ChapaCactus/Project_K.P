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
	public class StageMasterRow : IGoogle2uRow
	{
		public string _Name;
		public int _Distance;
		public StageMasterRow(string __ID, string __Name, string __Distance) 
		{
			_Name = __Name.Trim();
			{
			int res;
				if(int.TryParse(__Distance, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Distance = res;
				else
					Debug.LogError("Failed To Convert _Distance string: "+ __Distance +" to int");
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
					ret = _Distance.ToString();
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
				case "Distance":
					ret = _Distance.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Distance" + " : " + _Distance.ToString() + "} ";
			return ret;
		}
	}
	public sealed class StageMaster : IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001, ID_002, ID_003, ID_004, ID_005, ID_006, ID_007, ID_008, ID_009, ID_010
		};
		public string [] rowNames = {
			"ID_000", "ID_001", "ID_002", "ID_003", "ID_004", "ID_005", "ID_006", "ID_007", "ID_008", "ID_009", "ID_010"
		};
		public System.Collections.Generic.List<StageMasterRow> Rows = new System.Collections.Generic.List<StageMasterRow>();

		public static StageMaster Instance
		{
			get { return NestedStageMaster.instance; }
		}

		private class NestedStageMaster
		{
			static NestedStageMaster() { }
			internal static readonly StageMaster instance = new StageMaster();
		}

		private StageMaster()
		{
			Rows.Add( new StageMasterRow("ID_000", "テストの星", "5"));
			Rows.Add( new StageMasterRow("ID_001", "はじまりの星", "1"));
			Rows.Add( new StageMasterRow("ID_002", "つぎの星", "10"));
			Rows.Add( new StageMasterRow("ID_003", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_004", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_005", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_006", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_007", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_008", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_009", "つぎの星", "100"));
			Rows.Add( new StageMasterRow("ID_010", "つぎの星", "100"));
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
		public StageMasterRow GetRow(rowIds in_RowID)
		{
			StageMasterRow ret = null;
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
		public StageMasterRow GetRow(string in_RowString)
		{
			StageMasterRow ret = null;
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
