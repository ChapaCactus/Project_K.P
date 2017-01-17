using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryItem : MonoBehaviour
{
    [Serializable]
    public class Reference
    {
        public Image bgImage;// Background
        public Image image;// Front(Icon)

        public Button button;
    }

    [Serializable]
    public class Data
    {
        public int bagIndex;// カバンのどのインデックスにあるアイテムか
    }

    #region variables
    [SerializeField] private Reference m_Reference;
    [SerializeField] private Data m_Data;
    #endregion// variables

    #region properties
    public Reference reference { get { return m_Reference; } private set { m_Reference = value;} }
    public Data data { get { return m_Data; } private set { m_Data = value; } }
    #endregion// properties

    #region public methods
    public void Init(int _bagIndex = -1)
    {
        data.bagIndex = _bagIndex;
    }
    #endregion// public methods

}// class
