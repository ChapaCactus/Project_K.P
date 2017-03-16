using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

/// <summary>
/// 選択肢UI
/// </summary>
public class Choices : MonoBehaviour
{
	#region Enums
	/// <summary>
	/// 選択肢の種類
	/// </summary>
	public enum Type
	{
		YesOrNo,
	}

	/// <summary>
	/// 回答パターン
	/// </summary>
	public enum Answer
	{
		None = -1,
		Yes = 0,
		No = 1
	}
	#endregion// Enums

	#region Properties
	/// <summary>
	/// 選択した答え(未選択時はNone)
	/// </summary>
	public Answer answer { get { return m_Answer; } }
	#endregion// Properties

	#region Variables
	private Answer m_Answer = Answer.None;

	private static readonly string PREFAB_PATH = "Prefabs/UI/Notice/Choices";
	#endregion// Variables

	#region PublicMethods
	public static Choices Create(string _message, Transform _parent = null, Type _type = Type.YesOrNo)
	{
		var prefab = Resources.Load(PREFAB_PATH) as GameObject;
		var parent = _parent;
		if (parent == null) parent = GameObject.FindWithTag("MainCanvas").transform;
		var go = Instantiate(prefab, _parent, false);
		var choices = go.GetComponent<Choices>();

		choices.Init(_message);
		choices.Setup(_type);

		return choices;
	}

	public void Init(string _message)
	{
		var text = transform.Find("Title").GetComponent<Text>();
		text.text = _message;

		m_Answer = Answer.None;

		Debug.Log("初期化中...選択終了&待機完了時は、待機側からKill()を呼んで終了して下さい。");
	}

	public void Setup(Type _type)
	{
		var yesButton = transform.Find("Buttons/YesButton").GetComponent<Button>();
		var noButton = transform.Find("Buttons/NoButton").GetComponent<Button>();

		switch (_type)
		{
			case Type.YesOrNo:
				Debug.Log("[はい/いいえ] 選択肢で起動");
				yesButton.gameObject.SetActive(true);
				noButton.gameObject.SetActive(true);
				yesButton.onClick.RemoveAllListeners();
				noButton.onClick.RemoveAllListeners();
				yesButton.onClick.AddListener(() => Select(Answer.Yes));
				noButton.onClick.AddListener(() => Select(Answer.No));
				break;
		}
	}

	public void Kill()
	{
		Destroy(gameObject);
	}
	#endregion// PublicMethods

	#region PrivateMethods
	private void Select(Answer _answer)
	{
		m_Answer = _answer;
		Debug.Log(_answer + " を選択");
	}
	#endregion// PrivateMethods
}
