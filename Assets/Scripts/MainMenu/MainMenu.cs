using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MainMenuController))]
public class MainMenu : MonoBehaviour
{
	#region Enums
	/// <summary>
	/// メニューボタン全体の遷移状態
	/// </summary>
	public enum State
	{
		None = -1,
		Inventory = 0,
		Artifact,
		MyHome,
		Config,
		Num
	}
	#endregion// Enums

	#region Variables
	private State m_State = State.None;
	// TabButtons
	private Button m_InventoryTabButton = null;
	private Button m_ArtifactTabButton = null;
	private Button m_MyHomeTabButton = null;
	private Button m_ConfigTabButton = null;
	// Controller
	private MainMenuController m_Controller = null;
	#endregion// Variables

	#region Properties
	public State state
	{
		get
		{
			return m_State;
		}
	}
	#endregion// Properties

	#region UnityCallbacks
	private void Awake()
	{
		Init();
	}
	#endregion// UnityCallbacks

	#region PublicMethods
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		// コントローラの初期化
		m_Controller = GetComponent<MainMenuController>();
		m_Controller.Init();
		m_Controller.Setup(this);

		// UI表示の初期化
		HideAllContents();

		// TabButtonsの初期化
		m_InventoryTabButton = transform.FindChild("MenuButtons/TabButtons/InventoryTab").GetComponent<Button>();
		m_InventoryTabButton.onClick.RemoveAllListeners();
		m_InventoryTabButton.onClick.AddListener(() => m_Controller.OnClickInventory());
		m_ConfigTabButton = transform.FindChild("MenuButtons/TabButtons/ConfigTab").GetComponent<Button>();
		m_ConfigTabButton.onClick.RemoveAllListeners();
		m_ConfigTabButton.onClick.AddListener(() => m_Controller.OnClickConfig());
	}

	public void ShowInventory()
	{
		Inventory.Instance.Show();
		m_State = State.Inventory;

		Debug.Log("ShowInventory()");
	}

	public void ShowConfig()
	{
		Config.Instance.Show();
		m_State = State.Config;

		Debug.Log("ShowConfig()");
	}

	/// <summary>
	/// 全てのMainMenuContentsを非表示にする
	/// </summary>
	public void HideAllContents()
	{
		HideInventory();
		HideConfig();

		m_State = State.None;
	}

	public void HideInventory()
	{
		Inventory.Instance.Hide();
		m_State = State.None;

		Debug.Log("HideInventory()");
	}

	public void HideConfig()
	{
		Config.Instance.Hide();
		m_State = State.None;

		Debug.Log("HideConfig()");
	}
	#endregion// PublicMethods
}// MainMenu
