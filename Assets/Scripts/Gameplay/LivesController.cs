using System;
using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour {

	public Action Hit = delegate { };

	public Action Died = delegate { };

	public Beatmap beatmap;

	public int lives = 5;

	private bool _isDead = false;

	void OnEnable() {

		beatmap.BeatFailed += OnBeatFailed;
	}

	void OnDisable() {

		beatmap.BeatFailed -= OnBeatFailed;
	}

	private void OnBeatFailed( Beatmap beatmap, Beatmap.Beat beat ) {

		if (_isDead)
		{
			return;
		}

		--lives;

		if ( lives > 0 ) {

			Hit();
		} else {

			Died();

			_isDead = true;
		}
	}
}
