using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Launch scene and controlling.
public class SceneController : MonoBehaviour
{
// Variables.
	[SerializeField] private SceneState m_SceneState = SceneState.None;

// Unity Callbacks.
	private void Awake()
	{
		// First running in scene.
		Init();
	}

    #region Public methods
	public void Init()
	{
		// Initializing GameState.
		m_SceneState = SceneState.Title;

		MenuController.Instance.Init();
	}

	public SceneState GetState()
	{
		return m_SceneState;
	}

	public SceneState SetState(SceneState _State)
	{
		return m_SceneState = _State;
	}
    #endregion

}// Class.

public enum SceneState
{
	None = -1,
	Title,
	StageSelect,
	Intro,
	Gaming,// Stage
	Cleared,
	Result,
	Num
}
