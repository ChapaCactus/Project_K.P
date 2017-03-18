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

	#region Properties
	public Platform[] platforms
	{
		get { return m_Platforms; }
		private set { m_Platforms = value; }
	}
	#endregion// Properties

	#region Variables
	[SerializeField, HeaderAttribute("StageID => Inspectorから設定すること")]
	private int m_StageID = 0;

	[SerializeField, HeaderAttribute("ステージデータ")]
	private StageMasterRow m_StageData = null;

	[SerializeField]
	private Platform[] m_Platforms = new Platform[1];
	private Dictionary<int, GameObject> m_ItemPrefabDic = null;// Key: ItemID, Value: (GameObject)prefab
	#endregion// Variables

	#region UnityCallbacks
	private void Awake()
	{
		Init();
	}
	#endregion// UnityCallbacks

	#region PublicMethods
	/// <summary>
	/// 初期化(IDはInspectorから手動設定)
	/// </summary>
	public void Init()
	{
		///// ステージデータ設定 /////4
		// IDを0埋めして、ID_000の形に整形する
		var id = Utilities.ConvertMasterRowID(m_StageID);
		var idCombine = ("ID_" + id);

		var stageData = StageMaster.Instance.GetRow(id);
		SetStageData(stageData);

		// プレハブのキャッシュを初期化
		m_ItemPrefabDic = new Dictionary<int, GameObject>();
		// 苗床を初期化
		for (int i = 0; i < platforms.Length; i++)
		{
			if (platforms[i] != null)
				platforms[i].Init();
		}
	}

	/// <summary>
	/// このステージのアイテムをランダムに取得する
	/// 取得はキャッシュしておいたプレハブから。生成して返す。
	/// アイテムの初期化もここで行う
	/// </summary>
	public BaseItem GenerateItem()
	{
		// このステージの生成枠内でランダムにIDを返す
		var itemID = GetRandomItemIDInStage();
		// アイテムデータ初期化
		var id = ("ID_" + itemID.ToString().PadLeft(3, '0'));
		var masterData = ItemMaster.Instance.GetRow(id);
		// ゲームオブジェクト生成
		var prefab = GetPrefabInDic(itemID);
		var parent = platforms[0].transform;
		var go = Instantiate(prefab, parent, false);
		go.transform.localPosition = masterData._Offset;

		BaseItem baseItem = go.GetComponent<BaseItem>();
		baseItem.Init();
		baseItem.SetParams(itemID, masterData);
		baseItem.StartPopCoroutine();

		Debug.Log(baseItem.name);

		return baseItem;
	}

	public StageMasterRow GetStageData()
	{
		return m_StageData;
	}

	/// <summary>
	/// プレハブキャッシュからデータを探して返す
	/// </summary>
	/// <returns>登録したPrefab</returns>
	/// <param name="_itemID">探したいアイテムのID</param>
	public GameObject GetPrefabInDic(int _itemID)
	{
		GameObject prefab = null;

		if (m_ItemPrefabDic.ContainsKey(_itemID))
		{
			// 既にDictionaryに登録されていれば、それを返す
			prefab = m_ItemPrefabDic[_itemID];
		}
		else
		{
			// Dictionaryに無ければ新たにロードして登録する
			var id = ("ID_" + _itemID.ToString().PadLeft(3, '0'));

			var masterData = ItemMaster.Instance.GetRow(id);
			var prefabPath = masterData._Prefab;
			prefab = Resources.Load(prefabPath) as GameObject;

			m_ItemPrefabDic.Add(_itemID, prefab);
		}

		return prefab;
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

	private int GetRandomItemIDInStage()
	{
		int random = UnityEngine.Random.Range(0, 101);

		var stageData = GetStageData();

		if (random <= 100 && random >= 50)
		{
			// Rank1
			return stageData._Item1;
		}
		else if (random < 50 && random >= 25)
		{
			// Rank2
			return stageData._Item2;
		}
		else if (random < 25 && random >= 7)
		{
			// Rank3
			return stageData._Item3;
		}
		else if (random < 7 && random >= 2)
		{
			// Rank4
			return stageData._Item4;
		}
		else if (random < 2 && random >= 0)
		{
			// Rank5
			return stageData._Item5;
		}
		else
		{
			Debug.Log("例外エラー");
			// Rank1を返す
			return stageData._Item1;
		}

	}
    #endregion// PrivateMethods

}// Stage
