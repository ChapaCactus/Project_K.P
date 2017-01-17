using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof (CanvasGroup))]
public class Inventory : SingletonMonoBehaviour<Inventory>
{
    [Serializable]
    public class Item
    {
        public int id = -1;
        public int quantity = 1;

        public Item(int _ID, int _Quantity)
        {
            id = _ID;
            quantity = _Quantity;
        }
    }

    [Serializable]
    public class Reference
    {
        public GameObject invenButtonTemplate;

        public Transform inventoryContentParent;

        public CanvasGroup canvasGroup;
    }

    [Serializable]
    public class Data
    {
        public bool isActive = false;
    }

    #region variables
    [SerializeField] private Reference m_Reference;
    [SerializeField] private Data m_Data;
    #endregion// variables
    #region properties
    public Reference reference { get { return m_Reference; } private set { m_Reference = value; } }
    public Data data { get { return m_Data; } private set { m_Data = value; } }
    #endregion// properties
    #region unity callbacks
    private void Awake()
    {
        Init ();
    }
    #endregion// unity callbacks
    #region public methods
    public void Init()
    {
        Hide ();
    }

    /// <summary>
    /// Show bag instance.
    /// </summary>
    public void Show()
    {
        data.isActive = true;

        CreateBagButtons();

        reference.canvasGroup.alpha = 1;
        reference.canvasGroup.blocksRaycasts = true;
        reference.canvasGroup.interactable = true;
    }

    public void Hide()
    {
        data.isActive = false;

        reference.canvasGroup.alpha = 0;
        reference.canvasGroup.blocksRaycasts = false;
        reference.canvasGroup.interactable = false;
    }

    public void GetItem(int _ItemID, int _Quantity = 1)
    {
        for (int i = 0; i < GlobalData.MAX_INVENTORY_SIZE; i++)
        {
            if (GlobalData.Instance.bag[i] == null)
            {
                var item = new Item(_ItemID, _Quantity);
                GlobalData.Instance.bag[i] = item;
            }
        }
    }

    public void LoadDebugData()
    {
        int loops = 5;
        Item[] items = new Item[loops];
        for (int i = 0; i < loops; i++)
        {
            items[i] = new Item(i, 1);
        }

        GlobalData.Instance.bag = items;
    }

    public void OnClickInvenButton(int _Index)
    {
        
    }
    #endregion// public methods
    #region private methods
    private InventoryItem[] CreateBagButtons()
    {
        if (GlobalData.Instance.bag == null)
            return null;

        InventoryItem[] result;

        Item[] bag = GlobalData.Instance.bag;
        int loops = bag.Length;
        var prefab = reference.invenButtonTemplate;
        result = new InventoryItem [loops];

        for (int i = 0; i < loops; i++)
        {
            var parent = reference.inventoryContentParent;
            InventoryItem item = Instantiate(prefab, parent).GetComponent<InventoryItem>();
            item.reference.button.onClick.AddListener(() => OnClickInvenButton(i));
            result[i] = item;
        }

        return result;
    }
    #endregion// private methods

}// class