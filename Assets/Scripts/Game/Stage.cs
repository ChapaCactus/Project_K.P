using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google2u;

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
    [SerializeField, HeaderAttribute("苗床")]
    private Platform m_Platform = null;
    [SerializeField]
    private Setting m_Setting = null;

    // アイテム生成位置 Items generate position(Local offsets).
    [SerializeField] private Vector3[] m_CreateOffsets = new Vector3[0];
    #endregion

    #region Properties
    public Platform platform { 
        get { return m_Platform ?? (m_Platform = GameObject.Find("Platform").GetComponent<Platform>()); }
        private set { m_Platform = value;}
    }
    public Setting setting {get { return m_Setting; } private set { m_Setting = value; } }

    public Vector3[] createOffsets { get { return m_CreateOffsets; } private set { m_CreateOffsets = value; } }
    #endregion

    public delegate void OnComplete (string _Message);
    #region Public methods
    public void OnReady (OnComplete _Callback)
    {
        Debug.Log ("Ready!");
        _Callback ("Hi");
    }
    #endregion

    #region private methods
	private StageMasterRow GetStageDataOnMaster ()
    {
        int id = setting.StageID;
		return StageMaster.Instance.GetRow(id.ToString());
    }
    #endregion// private methods

}// Class.
