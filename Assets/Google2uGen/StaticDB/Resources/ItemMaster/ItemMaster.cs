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
		public Vector3 _Offset;
		public Vector3 _Rotation;
		public ItemMasterRow(string __ID, string __Name, string __Type, string __Price, string __Rarity, string __Health, string __Resource, string __Prefab, string __Exp, string __Offset, string __Rotation) 
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
			{
				string [] splitpath = __Offset.Split(",".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
				if(splitpath.Length != 3)
					Debug.LogError("Incorrect number of parameters for Vector3 in " + __Offset );
				float []results = new float[splitpath.Length];
				for(int i = 0; i < 3; i++)
				{
					float res;
					if(float.TryParse(splitpath[i], NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					{
						results[i] = res;
					}
					else 
					{
						Debug.LogError("Error parsing " + __Offset + " Component: " + splitpath[i] + " parameter " + i + " of variable _Offset");
					}
				}
				_Offset.x = results[0];
				_Offset.y = results[1];
				_Offset.z = results[2];
			}
			{
				string [] splitpath = __Rotation.Split(",".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
				if(splitpath.Length != 3)
					Debug.LogError("Incorrect number of parameters for Vector3 in " + __Rotation );
				float []results = new float[splitpath.Length];
				for(int i = 0; i < 3; i++)
				{
					float res;
					if(float.TryParse(splitpath[i], NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					{
						results[i] = res;
					}
					else 
					{
						Debug.LogError("Error parsing " + __Rotation + " Component: " + splitpath[i] + " parameter " + i + " of variable _Rotation");
					}
				}
				_Rotation.x = results[0];
				_Rotation.y = results[1];
				_Rotation.z = results[2];
			}
		}

		public int Length { get { return 10; } }

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
				case 8:
					ret = _Offset.ToString();
					break;
				case 9:
					ret = _Rotation.ToString();
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
				case "Type":
					ret = _Type.ToString();
					break;
				case "Price":
					ret = _Price.ToString();
					break;
				case "Rarity":
					ret = _Rarity.ToString();
					break;
				case "Health":
					ret = _Health.ToString();
					break;
				case "Resource":
					ret = _Resource.ToString();
					break;
				case "Prefab":
					ret = _Prefab.ToString();
					break;
				case "Exp":
					ret = _Exp.ToString();
					break;
				case "Offset":
					ret = _Offset.ToString();
					break;
				case "Rotation":
					ret = _Rotation.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Type" + " : " + _Type.ToString() + "} ";
			ret += "{" + "Price" + " : " + _Price.ToString() + "} ";
			ret += "{" + "Rarity" + " : " + _Rarity.ToString() + "} ";
			ret += "{" + "Health" + " : " + _Health.ToString() + "} ";
			ret += "{" + "Resource" + " : " + _Resource.ToString() + "} ";
			ret += "{" + "Prefab" + " : " + _Prefab.ToString() + "} ";
			ret += "{" + "Exp" + " : " + _Exp.ToString() + "} ";
			ret += "{" + "Offset" + " : " + _Offset.ToString() + "} ";
			ret += "{" + "Rotation" + " : " + _Rotation.ToString() + "} ";
			return ret;
		}
	}
	public sealed class ItemMaster : IGoogle2uDB
	{
		public enum rowIds {
			ID_000, ID_001, ID_002, ID_003, ID_004, ID_005, ID_006, ID_007, ID_008, ID_009, ID_010
		};
		public string [] rowNames = {
			"ID_000", "ID_001", "ID_002", "ID_003", "ID_004", "ID_005", "ID_006", "ID_007", "ID_008", "ID_009", "ID_010"
		};
		public System.Collections.Generic.List<ItemMasterRow> Rows = new System.Collections.Generic.List<ItemMasterRow>();

		public static ItemMaster Instance
		{
			get { return NestedItemMaster.instance; }
		}

		private class NestedItemMaster
		{
			static NestedItemMaster() { }
			internal static readonly ItemMaster instance = new ItemMaster();
		}

		private ItemMaster()
		{
			Rows.Add( new ItemMasterRow("ID_000", "Dummy", "Dummy", "1", "1", "1", "Dummy", "Dummy", "1", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_001", "たべかけスイカ", "Crop", "1", "1", "1", "Item/Suica/Suica_D", "Prefabs/Items/Suica1", "3", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_002", "ひとくちスイカ", "Crop", "1", "1", "1", "Item/Suica/Suica_C", "Prefabs/Items/Suica2", "4", "0,1.4,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_003", "たっぷりスイカ", "Crop", "1", "1", "1", "Item/Suica/Suica_B", "Prefabs/Items/Suica3", "7", "0,1.5,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_004", "ぜいたくスイカ", "Crop", "1", "1", "1", "Item/Suica/Suica_A", "Prefabs/Items/Suica4", "15", "0,1.4,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_005", "かんぺきスイカ", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Suica5", "400", "0,1.4,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_006", "ダイコン06", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Daikon", "1", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_007", "ダイコン07", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Daikon", "1", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_008", "ダイコン08", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Daikon", "1", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_009", "ダイコン09", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Daikon", "1", "0,1.3,0", "0,0,0"));
			Rows.Add( new ItemMasterRow("ID_010", "ダイコン10", "Crop", "1", "1", "1", "Daikon", "Prefabs/Items/Daikon", "1", "0,1.3,0", "0,0,0"));
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
