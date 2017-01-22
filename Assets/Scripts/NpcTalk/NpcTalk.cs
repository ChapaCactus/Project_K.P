using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Google2u;

[RequireComponent(typeof(CanvasGroup))]
/// <summary>
/// Npcの会話ダイアログ表示・管理クラス
/// </summary>
public class NpcTalk : SingletonMonoBehaviour<NpcTalk>
{
	#region enums
	/// <summary>
	/// 状態
	/// </summary>
	public enum States
	{
		None = -1,
		Talking = 0// 会話中
	}
	#endregion// enums

	#region variables
	// 本文の文字流しTween
	private Tween m_BodyTextTween = null;

	// 会話設定
	private bool m_IsTalking = false;// 会話中か
	private int m_CurrentPage = 0;// どこまで読み進めたか
	private string[] m_Messages = null;// 会話内容
	// ページを捲ったか？
	[SerializeField] private bool m_CallbackTap = false;
	// DOTextを完了したか？
	private bool m_CallbackDrawnText = false;

	private bool m_TextDrawing = false;

	private CanvasGroup m_CanvasGroup = null;
	private CanvasGroup m_TapMarkCanvasGroup = null;

	private Text m_NameText = null;
	private Text m_BodyText = null;

	private Image m_FaceImage = null;
	private Image m_TapMarkImage = null;

	[SerializeField] private States m_State = States.None;

	private const string NAME_GO_PATH = "TextArea/NameBG/Text";
	private const string BODY_GO_PATH = "TextArea/BodyBG/Text";
	private const string FACE_GO_PATH = "FaceArea/Image";
	private const string TAPMARK_GO_PATH = "TextArea/BodyBG/TapMark";
	#endregion// variables

	#region properties
	public Tween bodyTextTween { get { return m_BodyTextTween; } private set { m_BodyTextTween = value; } }

	public bool isTalking { get { return m_IsTalking; } private set { m_IsTalking = value; } }
	public int currentPage { get { return m_CurrentPage; } private set { m_CurrentPage = value; } }
	public string[] messages { get { return m_Messages; } private set { m_Messages = value; } }

	public bool callbackTap { get { return m_CallbackTap; } private set { m_CallbackTap = value; } }

	public CanvasGroup canvasGroup {
		get { return m_CanvasGroup ?? (m_CanvasGroup = GetComponent<CanvasGroup>()); }
		private set { m_CanvasGroup = value; }
	}

	public Text nameText {
		get { return m_NameText ?? (m_NameText); }
		private set { m_NameText = value; }
	}

	public Text bodyText {
		get { return m_BodyText; }
		private set { m_BodyText = value; }
	}

	public Image faceImage {
		get { return m_FaceImage; }
		private set { m_FaceImage = value; }
	}

	public Image tapMarkImage {
		get { return m_TapMarkImage; }
		private set { m_TapMarkImage = value; }
	}

	public States state {
		get { return m_State; }
		private set { m_State = value; }
	}
	#endregion// properties

	#region unity callbacks
	private void Awake()
	{
		Init ();
	}

	public void Update()
	{
		if (Input.GetButtonDown ("Jump")) {
			Tap ();
		}
	}
	#endregion// unity callbacks

	#region public methods
	public void Init()
	{
		m_IsTalking = false;
		currentPage = 0;
		messages = null;

		StopCoroutine (TalkingCoroutine ());
		isTalking = false;

		bodyTextTween.Kill ();
		bodyTextTween = null;

		canvasGroup = GetComponent<CanvasGroup> ();

		state = States.None;

		// UIコンポーネント初期化
		nameText = transform.FindChild (NAME_GO_PATH).GetComponent<Text> ();
		bodyText = transform.FindChild (BODY_GO_PATH).GetComponent<Text> ();
		faceImage = transform.FindChild (FACE_GO_PATH).GetComponent<Image> ();
		nameText.text = "";
		bodyText.text = "";
		faceImage.sprite = null;

		tapMarkImage = transform.FindChild (TAPMARK_GO_PATH).GetComponent<Image> ();
	}

	/// <summary>
	/// 表示する
	/// </summary>
	public void Show()
	{
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}

	/// <summary>
	/// 隠す
	/// </summary>
	public void Hide()
	{
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// 会話内容をセットする
	/// </summary>
	public void SetMessages(string[] _messages)
	{
		// 配列のコピー
		messages = new string[_messages.Length];
		_messages.CopyTo (messages, 0);
	}

	/// <summary>
	/// 会話を開始する
	/// </summary>
	public void StartTalking()
	{
		StartCoroutine (TalkingCoroutine ());
	}

	/// <summary>
	/// 会話
	/// </summary>
	public IEnumerator TalkingCoroutine(UnityAction _callback = null)
	{
		if (isTalking || messages == null || messages.Length == 0) {
			Debug.LogError ("会話開始エラー 既に動作中または、会話内容がありません");
			yield break;
		}
		isTalking = true;

		// 会話開始前の初期化
		m_CallbackDrawnText = false;
		callbackTap = false;

		// 会話用一時リスト
		for (int i = 0; i < messages.Length; i++) {
			// 会話開始
			yield return SetText (messages[i]);

			Debug.Log ("Check1");
			// 表示完了後、タップ待機
			callbackTap = false;
			var waitTap = new WaitWhile (() => callbackTap == false);
			yield return waitTap;
			callbackTap = false;
			Debug.Log ("Check2");
		}

		Hide ();
		isTalking = false;

		if (_callback != null) {
			_callback ();
		}

		yield break;
	}

	[ContextMenu("DebugStartTalking")]
	public void DebugStartTalking()
	{
		var row = TalkMaster.Instance.GetRow ("ID_000");
		var talk1 = row._Talk1;
		var talk2 = row._Talk2;
		var talk3 = row._Talk3;
		var talk4 = row._Talk4;
		var talk5 = row._Talk5;

		var debugMessage = new string[] {
			talk1, talk2, talk3, talk4, talk5
		};
		SetMessages(debugMessage);
		StartTalking ();
	}
	#endregion// public methods

	#region private methods
	private IEnumerator SetText(string _str)
	{
		Debug.Log ("会話開始");
		// 初期化
		nameText.text = "";
		bodyText.text = "";
		faceImage.sprite = null;
		// テキストを流し込む(自動的に表示が終了後、タップ扱いにする)
		bodyTextTween = bodyText.DOText (_str, 1f, true);
		Debug.Log ("End Check0 : " + (bodyTextTween.IsPlaying()).ToString());
		// 表示完了するまで待機
		var wait = new WaitWhile (() => bodyTextTween.IsPlaying());
		yield return wait;
		Debug.Log ("End Check1 : " + (bodyTextTween.IsPlaying()).ToString());
		callbackTap = false;
		Debug.Log ("End Check2 : " + (bodyTextTween.IsPlaying()).ToString());

		yield break;
	}

	/// <summary>
	/// 画面押下時
	/// </summary>
	private void Tap()
	{
		callbackTap = true;
		if (bodyTextTween.IsPlaying()) {
			// Tween再生中だったら
			// Tweenを終わらせる
			bodyTextTween.Complete ();
		} else {
		}
	}

	/// <summary>
	/// タップページめくり可能アイコンを表示
	/// </summary>
	private void ShowTapMark()
	{
		tapMarkImage.enabled = true;
	}

	/// <summary>
	/// タップページめくり可能アイコンを非表示
	/// </summary>
	private void HideTapMark()
	{
		tapMarkImage.enabled = false;
	}
	#endregion// private methods

}// NpcTalk
