using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuController))]
public class Menu : MonoBehaviour
{
	#region Enums
	/// <summary>
	/// メニューボタン全体の遷移状態
	/// </summary>
	public enum State
	{
		None = -1,
		Inventory = 0,
		Equip,
		Home,
		Config,
		Num
	}
	#endregion// Enums

	#region Properties
	public State state
	{
		get
		{
			return m_State;
		}
	}
	#endregion// Properties

	#region Variables
	private State m_State = State.None;
	// TabButtons
	private Button m_InventoryTabButton = null;
	private Button m_EquipTabButton = null;
	private Button m_HomeTabButton = null;
	private Button m_ConfigTabButton = null;
	// TabActiveImages
	private Image m_InventoryActiveImage = null;
	private Image m_EquipActiveImage = null;
	private Image m_HomeActiveImage = null;
	private Image m_ConfigActiveImage = null;
	// Controller
	private MenuController m_Controller = null;

	public static Inventory Inventory = null;
	public static Equip Equip = null;
	public static Home Home = null;
	public static Config Config = null;

	#endregion// Variables

	#region PublicMethods
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		m_Controller = GetComponent<MenuController>();
		
		// TabButtonsの初期化
		m_InventoryTabButton = transform.Find("Buttons/Tabs/Inventory (Tab)/Button").GetComponent<Button>();
		m_InventoryTabButton.onClick.RemoveAllListeners();
		m_InventoryTabButton.onClick.AddListener(() => m_Controller.OnClickInventory());
		m_ConfigTabButton = transform.Find("Buttons/Tabs/Config (Tab)/Button").GetComponent<Button>();
		m_ConfigTabButton.onClick.RemoveAllListeners();
		m_ConfigTabButton.onClick.AddListener(() => m_Controller.OnClickConfig());
		// ActiveImageの参照
		m_InventoryActiveImage = transform.Find("Buttons/Tabs/Inventory (Tab)/ActiveImage").GetComponent<Image>();
		m_EquipActiveImage = transform.Find("Buttons/Tabs/Equip (Tab)/ActiveImage").GetComponent<Image>();
		m_HomeActiveImage = transform.Find("Buttons/Tabs/Home (Tab)/ActiveImage").GetComponent<Image>();
		m_ConfigActiveImage = transform.Find("Buttons/Tabs/Config (Tab)/ActiveImage").GetComponent<Image>();
		// コンテンツの参照
		Inventory = transform.Find("Contents/Inventory").GetComponent<Inventory>();
		Equip = transform.Find("Contents/Equip").GetComponent<Equip>();
		Home = transform.Find("Contents/Home").GetComponent<Home>();
		Config = transform.Find("Contents/Config").GetComponent<Config>();
		// コンテンツの初期化
		Inventory.Init();
		//Equip.Init();
		//Home.Init();
		Config.Init();

		// UI表示の初期化
		HideAllContents();
	}

	public void ShowInventory()
	{
		Inventory.Show();
		m_State = State.Inventory;

		m_InventoryActiveImage.enabled = true;

		Debug.Log("ShowInventory()");
	}

	public void ShowConfig()
	{
		Config.Show();
		m_State = State.Config;

		m_ConfigActiveImage.enabled = true;

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
		Inventory.Hide();
		m_State = State.None;

		m_InventoryActiveImage.enabled = false;

		Debug.Log("HideInventory()");
	}

	public void HideConfig()
	{
		Config.Hide();
		m_State = State.None;

		m_ConfigActiveImage.enabled = false;

		Debug.Log("HideConfig()");
	}
	#endregion// PublicMethods
}// MainMenu
