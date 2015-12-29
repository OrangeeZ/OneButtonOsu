using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWinScreen : UIScreen {

	[SerializeField]
	private Scoring _scoring;

	[SerializeField]
	private Text _scoreValue;

	public override void Show() {

		_scoreValue.text = _scoring.Score.RoundToInt().ToString();
		
		base.Show();
	}
}
