using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoginScreen : UIScreen {

	[SerializeField]
	private Button _loginButton;

	[SerializeField]
	private UILoginScreenTextSequence _loginScreenTextSequence;

	[SerializeField]
	private GameObject _spinner;

	[SerializeField]
	private float _spinnerDuration;

	void Start() {

		GameplayController.Instance.IsPaused = true;

		_loginButton.onClick.AddListener( OnLoginClick );
		_spinner.SetActive( false );
	}

	private void OnLoginClick() {

		_loginButton.gameObject.SetActive( false );

		StartCoroutine( ShowSpinner() );
	}

	private IEnumerator ShowSpinner() {

		_spinner.SetActive( true );
		var delay = _spinnerDuration;

		while ( ( delay -= Time.unscaledDeltaTime ) > 0f ) {

			yield return null;
		}
		
		_spinner.SetActive( false );

		_loginScreenTextSequence.Play( OnTextSequenceFinish );
	}

	private void OnTextSequenceFinish() {

		GameplayController.Instance.IsPaused = false;

		ScreenManager.GetScreen<UIGameScreenController>().Show();
	}


}
