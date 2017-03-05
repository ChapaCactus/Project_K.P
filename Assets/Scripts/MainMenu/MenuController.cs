using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : SingletonMonoBehaviour<MenuController>
{
	#region Variables
	private Menu m_Menu;
	#endregion// Variables

	#region Properties
	#endregion// Properties

	#region PublicMethods
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		m_Menu = GetComponent<Menu>();
		m_Menu.Init();
	}

	/// <summary>
	/// 他クラスから情報のセットが必要な場合の初期化、
	/// Button.OnClick等のセット
	/// </summary>
	public void Setup()
	{
	}

	/// <summary>
	/// インベントリタブ押下時
	/// </summary>
	public void OnClickInventory()
	{
		if (m_Menu.state != Menu.State.Inventory)
		{
			// 全て非表示にしてから
			m_Menu.HideAllContents();
			// インベントリ以外であればインベントリを表示
			m_Menu.ShowInventory();
		}
		else
		{
			// インベントリなら全てを非表示
			m_Menu.HideAllContents();
		}

		Debug.Log("OnClickInventory");
	}

	/// <summary>
	/// コンフィグタブ押下時
	/// </summary>
	public void OnClickConfig()
	{
		if (m_Menu.state != Menu.State.Config)
		{
			// 全て非表示にしてから
			m_Menu.HideAllContents();
			// コンフィグ以外であればコンフィグを表示
			m_Menu.ShowConfig();
		}
		else
		{
			// コンフィグなら全てを非表示
			m_Menu.HideAllContents();
		}

		Debug.Log("OnClickConfig");
	}
    #endregion// PublicMethods
}// MainMenuController
