using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行管理
/// </summary>
public class GameManager : MonoBehaviour
{
	#region variables
	// 現在の遷移状態
	private GameState m_CurrentGameState;
	#endregion// variables

	#region public methods
	/// <summary>
	/// 現在のゲーム全体の遷移状態を取得
	/// </summary>
	public GameState GetCurrentGameState()
	{
		return m_CurrentGameState;
	}
	#endregion// public methods
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
