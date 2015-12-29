using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoginScreen : UIScreen {

	[SerializeField]
	private Button _loginButton;

	[SerializeField]
	private UILoginScreenTextSequence _loginScreenTextSequence;

	void Awake() {

		GameplayController.Instance.IsPaused = true;
		_loginButton.onClick.AddListener( OnLoginClick );
	}

	private void OnLoginClick() {

		_loginScreenTextSequence.Play( OnTextSequenceFinish );
	}

	private void OnTextSequenceFinish() {

		GameplayController.Instance.IsPaused = false;

		ScreenManager.GetScreen<UIGameScreenController>().Show();
	}
}
