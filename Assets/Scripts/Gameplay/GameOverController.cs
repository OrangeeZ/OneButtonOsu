using System;
using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public LivesController livesController;

	void OnEnable() {

		livesController.Died += OnDie;
	}

	void OnDisable() {

		livesController.Died -= OnDie;
	}

	private void OnDie() {

		StartCoroutine( DelayedDeath() );
	}

	private IEnumerator DelayedDeath() {

		yield return new WaitForSeconds( .1f );

		ScreenManager.GetScreen<UIGameOverController>().Show();

		Time.timeScale = 0f;
	}
}
