using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゲームの進行管理
/// </summary>
public class GameManager : MonoBehaviour
{
	#region Properties
	#endregion// Properties

	#region Variables
	// 現在の遷移状態
	private GameState m_CurrentGameState;
	#endregion// Variables

	#region UnityCallbacks
	private void Awake()
	{
		Init();
	}
	#endregion// UnityCallbacks

	#region PublicMethods
	public void Init()
	{
		DOTween.Init();
		GlobalData.Init();
	}

	/// <summary>
	/// ゲーム終了時
	/// </summary>
	public void OnApplicationQuit()
	{
		GlobalData.Save();
		Debug.Log("ゲーム終了");
	}

	/// <summary>
	/// 現在のゲーム全体の遷移状態を取得
	/// </summary>
	public GameState GetCurrentGameState()
	{
		return m_CurrentGameState;
	}

	public void SetGameState(GameState _next)
	{
		m_CurrentGameState = _next;

		switch (m_CurrentGameState)
		{
			default:
				break;
		}
	}
	#endregion// PublicMethods

}// GameManager

/// <summary>
/// ゲーム全体の遷移状態
/// </summary>
public enum GameState
{
	Start,
	Playing,
	End
}
