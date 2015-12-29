using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWinScreen : UIScreen {

	[SerializeField]
	private Scoring _scoring;

	[SerializeField]
	private Text _scoreValue;

	[SerializeField]
	private Text _killsValue;

	[SerializeField]
	private Text _deathsValue;

	[SerializeField]
	private Button _restartButton;

	public override void Show() {

		_scoreValue.text = _scoring.Score.RoundToInt().ToString();
		_killsValue.text = _scoring.Kills.ToString();
		_deathsValue.text = _scoring.Deaths.ToString();

		base.Show();
	}
}
