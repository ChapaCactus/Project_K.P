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
	public class TalkMasterRow : IGoogle2uRow
	{
		public string _Name;
		public string _Talk1;
		public string _Talk2;
		public string _Talk3;
		public string _Talk4;
		public string _Talk5;
		public TalkMasterRow(string __ID, string __Name, string __Talk1, string __Talk2, string __Talk3, string __Talk4, string __Talk5) 
		{
			_Name = __Name.Trim();
			_Talk1 = __Talk1.Trim();
			_Talk2 = __Talk2.Trim();
			_Talk3 = __Talk3.Trim();
			_Talk4 = __Talk4.Trim();
			_Talk5 = __Talk5.Trim();
		}

		public int Length { get { return 6; } }

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
					ret = _Talk1.ToString();
					break;
				case 2:
					ret = _Talk2.ToString();
					break;
				case 3:
					ret = _Talk3.ToString();
					break;
				case 4:
					ret = _Talk4.ToString();
					break;
				case 5:
					ret = _Talk5.ToString();
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
				case "Talk1":
					ret = _Talk1.ToString();
					break;
				case "Talk2":
					ret = _Talk2.ToString();
					break;
				case "Talk3":
					ret = _Talk3.ToString();
					break;
				case "Talk4":
					ret = _Talk4.ToString();
					break;
				case "Talk5":
					ret = _Talk5.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Talk1" + " : " + _Talk1.ToString() + "} ";
			ret += "{" + "Talk2" + " : " + _Talk2.ToString() + "} ";
			ret += "{" + "Talk3" + " : " + _Talk3.ToString() + "} ";
			ret += "{" + "Talk4" + " : " + _Talk4.ToString() + "} ";
			ret += "{" + "Talk5" + " : " + _Talk5.ToString() + "} ";
			return ret;
		}
	}
	public sealed class TalkMaster : IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001, ID_002, ID_003, ID_004, ID_005, ID_006, ID_007, ID_008, ID_009, ID_010
		};
		public string [] rowNames = {
			"ID_000", "ID_001", "ID_002", "ID_003", "ID_004", "ID_005", "ID_006", "ID_007", "ID_008", "ID_009", "ID_010"
		};
		public System.Collections.Generic.List<TalkMasterRow> Rows = new System.Collections.Generic.List<TalkMasterRow>();

		public static TalkMaster Instance
		{
			get { return NestedTalkMaster.instance; }
		}

		private class NestedTalkMaster
		{
			static NestedTalkMaster() { }
			internal static readonly TalkMaster instance = new TalkMaster();
		}

		private TalkMaster()
		{
			Rows.Add( new TalkMasterRow("ID_000", "Dummy", "あー", "やあ", "こんにちは", "元気？", "じゃあね"));
			Rows.Add( new TalkMasterRow("ID_001", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_002", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_003", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_004", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_005", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_006", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_007", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_008", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_009", "たろー", "", "", "", "", ""));
			Rows.Add( new TalkMasterRow("ID_010", "たろー", "", "", "", "", ""));
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
		public TalkMasterRow GetRow(rowIds in_RowID)
		{
			TalkMasterRow ret = null;
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
		public TalkMasterRow GetRow(string in_RowString)
		{
			TalkMasterRow ret = null;
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
