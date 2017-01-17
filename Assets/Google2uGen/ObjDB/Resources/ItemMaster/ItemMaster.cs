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
	public class ItemMasterRow : IGoogle2uRow
	{
		public string _Name;
		public string _Type;
		public int _Price;
		public int _Rarity;
		public int _Health;
		public string _Resource;
		public string _Prefab;
		public int _Exp;
		public ItemMasterRow(string __ID, string __Name, string __Type, string __Price, string __Rarity, string __Health, string __Resource, string __Prefab, string __Exp) 
		{
			_Name = __Name.Trim();
			_Type = __Type.Trim();
			{
			int res;
				if(int.TryParse(__Price, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Price = res;
				else
					Debug.LogError("Failed To Convert _Price string: "+ __Price +" to int");
			}
			{
			int res;
				if(int.TryParse(__Rarity, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Rarity = res;
				else
					Debug.LogError("Failed To Convert _Rarity string: "+ __Rarity +" to int");
			}
			{
			int res;
				if(int.TryParse(__Health, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Health = res;
				else
					Debug.LogError("Failed To Convert _Health string: "+ __Health +" to int");
			}
			_Resource = __Resource.Trim();
			_Prefab = __Prefab.Trim();
			{
			int res;
				if(int.TryParse(__Exp, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Exp = res;
				else
					Debug.LogError("Failed To Convert _Exp string: "+ __Exp +" to int");
			}
		}

		public int Length { get { return 8; } }

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
					ret = _Type.ToString();
					break;
				case 2:
					ret = _Price.ToString();
					break;
				case 3:
					ret = _Rarity.ToString();
					break;
				case 4:
					ret = _Health.ToString();
					break;
				case 5:
					ret = _Resource.ToString();
					break;
				case 6:
					ret = _Prefab.ToString();
					break;
				case 7:
					ret = _Exp.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "_Name":
					ret = _Name.ToString();
					break;
				case "_Type":
					ret = _Type.ToString();
					break;
				case "_Price":
					ret = _Price.ToString();
					break;
				case "_Rarity":
					ret = _Rarity.ToString();
					break;
				case "_Health":
					ret = _Health.ToString();
					break;
				case "_Resource":
					ret = _Resource.ToString();
					break;
				case "_Prefab":
					ret = _Prefab.ToString();
					break;
				case "_Exp":
					ret = _Exp.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "_Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "_Type" + " : " + _Type.ToString() + "} ";
			ret += "{" + "_Price" + " : " + _Price.ToString() + "} ";
			ret += "{" + "_Rarity" + " : " + _Rarity.ToString() + "} ";
			ret += "{" + "_Health" + " : " + _Health.ToString() + "} ";
			ret += "{" + "_Resource" + " : " + _Resource.ToString() + "} ";
			ret += "{" + "_Prefab" + " : " + _Prefab.ToString() + "} ";
			ret += "{" + "_Exp" + " : " + _Exp.ToString() + "} ";
			return ret;
		}
	}
	public class ItemMaster :  Google2uComponentBase, IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001, ID_002, ID_003, ID_004, ID_005, ID_006, ID_007, ID_008, ID_009, ID_010
		};
		public string [] rowNames = {
			"ID_000", "ID_001", "ID_002", "ID_003", "ID_004", "ID_005", "ID_006", "ID_007", "ID_008", "ID_009", "ID_010"
		};
		public System.Collections.Generic.List<ItemMasterRow> Rows = new System.Collections.Generic.List<ItemMasterRow>();
		public override void AddRowGeneric (System.Collections.Generic.List<string> input)
		{
			Rows.Add(new ItemMasterRow(input[0],input[1],input[2],input[3],input[4],input[5],input[6],input[7],input[8]));
		}
		public override void Clear ()
		{
			Rows.Clear();
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
		public ItemMasterRow GetRow(rowIds in_RowID)
		{
			ItemMasterRow ret = null;
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
		public ItemMasterRow GetRow(string in_RowString)
		{
			ItemMasterRow ret = null;
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
