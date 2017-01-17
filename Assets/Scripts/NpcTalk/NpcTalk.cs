using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Npcの会話ダイアログ表示・管理クラス
/// </summary>
public class NpcTalk : MonoBehaviour
{
	#region variables
	private string[] m_TempMessages = null;
	#endregion// variables

	#region public methods
	public void Init()
	{
		m_TempMessages = null;
	}

	/// <summary>
	/// 会話内容をセットする
	/// </summary>
	public void SetMessages(string[] _messages)
	{
		// 配列のコピー
		_messages.CopyTo (m_TempMessages, 0);
	}

	/// <summary>
	/// 会話を開始する
	/// </summary>
	public void StartTalking()
	{
		// Start Talking
	}
	#endregion// public methods

}// NpcTalk
