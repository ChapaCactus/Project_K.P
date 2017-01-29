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
	public enum State
	{
		None,
		Moving,
		Already,
		Clear,
		Result
	}

	[Serializable]
	public class Setting
	{
		[SerializeField, HeaderAttribute("ステージID")]
		public int StageID = 0;
	}

	#region Variables
	[SerializeField]
	private StageMasterRow m_StageData = null;

	[SerializeField, HeaderAttribute("苗床")]
	private Platform m_Platform = null;
	[SerializeField]
	private Setting m_Setting = null;

	// アイテム生成位置 Items generate position(Local offsets).
	[SerializeField]
	private Vector3[] m_CreateOffsets = new Vector3[0];
	#endregion// Variables

	#region Properties
    public Platform platform { 
        get { return m_Platform
			?? (m_Platform = GameObject.Find("Platform").GetComponent<Platform>()); }
        private set { m_Platform = value; }
    }
    public Setting setting {get { return m_Setting; } private set { m_Setting = value; } }

    public Vector3[] createOffsets { get { return m_CreateOffsets; } private set { m_CreateOffsets = value; } }
    #endregion// Properties

    public delegate void OnComplete (string _Message);
	#region PublicMethods
	public void Init(int _stageID)
	{
		// IDを0埋めして、ID_000の形に整形する
		var idPadLeft = _stageID.ToString().PadLeft(3, '0');
		var id = ("ID_" + idPadLeft);
		var stageData = StageMaster.Instance.GetRow(id);

		SetStageData(stageData);
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
