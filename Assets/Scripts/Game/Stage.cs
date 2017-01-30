using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google2u;

/// <summary>
/// ステージを管理するクラス
/// </summary>
public class Stage : SingletonMonoBehaviour<Stage>
{
	#region Enums
	#endregion// Enums

	#region Variables
	[SerializeField, HeaderAttribute("StageID => Inspectorから設定する")]
	private int m_StageID = 0;
	[SerializeField, HeaderAttribute("ステージデータ")]
	private StageMasterRow m_StageData = null;

	[SerializeField, HeaderAttribute("苗床")]
	private Platform m_Platform = null;
	#endregion// Variables

	#region Properties
    public Platform platform { 
        get { return m_Platform
			?? (m_Platform = GameObject.Find("Platform").GetComponent<Platform>()); }
        private set { m_Platform = value; }
    }
	#endregion// Properties

	#region UnityCallbacks
	private void Awake()
	{
		Init();
	}
	#endregion UnityCallbacks

	#region PublicMethods
	/// <summary>
	/// 初期化(IDはInspectorから手動設定)
	/// </summary>
	public void Init()
	{
		// IDを0埋めして、ID_000の形に整形する
		var idPadLeft = m_StageID.ToString().PadLeft(3, '0');
		var id = ("ID_" + idPadLeft);
		var stageData = StageMaster.Instance.GetRow(id);
		SetStageData(stageData);
		// 苗床を初期化
		platform.Init();
	}
	#endregion// PublicMethods

	#region PrivateMethods
	/// <summary>
	/// ステージデータを設定
	/// </summary>
	private void SetStageData(StageMasterRow _stageDataRow)
	{
		m_StageData = _stageDataRow;
	}
    #endregion// PrivateMethods

}// Class.
