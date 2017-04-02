using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KP;

[RequireComponent(typeof(CanvasGroup))]
public class Equip : BaseMainMenuContent
{
	#region Enums
	#endregion// Enums

	#region Properties
	public Tool toolData
	{
		get
		{
			return GlobalData.e_Tool;
		}
	}
	#endregion// Properties

	#region Variables
	private Image m_ToolImage = null;
	private Text m_NameText = null;
	private Text m_LevelText = null;
	#endregion// Variables

	#region UnityCallbacks
	#endregion// UnityCallbacks

	#region PublicMethods
	public void Init()
	{
		var tf = transform;
		m_ToolImage = tf.Find("Viewport/Content/Tool/Image").GetComponent<Image>();
		m_NameText = tf.Find("Viewport/Content/Tool/Name/Text").GetComponent<Text>();
		m_LevelText = tf.Find("Viewport/Content/Tool/Level/Text").GetComponent<Text>();

		Refresh();
	}

	public void SetEquip(GlobalData.Equipments _equipments, int _equipID)
	{
		GlobalData.SetEquip(_equipments, _equipID);
	}

	public void Refresh()
	{
		if (GlobalData.state == GlobalData.State.Initialized)
		{
			m_NameText.text = toolData.name;
			m_LevelText.text = ("Lv " + toolData.level);
		}
	}
	#endregion// PublicMethods

}// Equip
