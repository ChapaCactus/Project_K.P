using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainMenu))]
public class MainMenuController : MonoBehaviour
{
	#region Variables
	private MainMenu m_MainMenu;
	#endregion// Variables

	#region Properties
	#endregion// Properties

	#region PublicMethods
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		
	}

	/// <summary>
	/// 他クラスから情報のセットが必要な場合の初期化、
	/// Button.OnClick等のセット
	/// </summary>
	public void Setup(MainMenu _model)
	{
		m_MainMenu = _model;
	}

	/// <summary>
	/// インベントリタブ押下時
	/// </summary>
	public void OnClickInventory()
	{
		if (m_MainMenu.state != MainMenu.State.Inventory)
		{
			// インベントリ以外であればインベントリを表示
			m_MainMenu.ShowInventory();
		}
		else
		{
			// インベントリなら全てを非表示
			m_MainMenu.HideAllContents();
		}

		Debug.Log("OnClickInventory");
	}

	/// <summary>
	/// コンフィグタブ押下時
	/// </summary>
	public void OnClickConfig()
	{
		if (m_MainMenu.state != MainMenu.State.Config)
		{
			// コンフィグ以外であればコンフィグを表示
			m_MainMenu.ShowConfig();
		}
		else
		{
			// コンフィグなら全てを非表示
			m_MainMenu.HideAllContents();
		}

		Debug.Log("OnClickConfig");
	}
    #endregion// PublicMethods
}// MainMenuController
