using UnityEngine;
using UnityEditor;

namespace Google2u
{
	[CustomEditor(typeof(ItemMaster))]
	public class ItemMasterEditor : Editor
	{
		public int Index = 0;
		public override void OnInspectorGUI ()
		{
			ItemMaster s = target as ItemMaster;
			ItemMasterRow r = s.Rows[ Index ];

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button("<<") )
			{
				Index = 0;
			}
			if ( GUILayout.Button("<") )
			{
				Index -= 1;
				if ( Index < 0 )
					Index = s.Rows.Count - 1;
			}
			if ( GUILayout.Button(">") )
			{
				Index += 1;
				if ( Index >= s.Rows.Count )
					Index = 0;
			}
			if ( GUILayout.Button(">>") )
			{
				Index = s.Rows.Count - 1;
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "ID", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.LabelField( s.rowNames[ Index ] );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Name", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Name );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Type", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Type );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Price", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._Price );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Rarity", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._Rarity );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Health", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._Health );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Resource", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Resource );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Prefab", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Prefab );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Exp", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._Exp );
			}
			EditorGUILayout.EndHorizontal();

		}
	}
}
