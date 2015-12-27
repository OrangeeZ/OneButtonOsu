using System;
using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour {

	public Action Hit = delegate { };

	public Action Died = delegate { };

	public Beatmap beatmap;

	public int lives = 5;

	void OnEnable() {
		
		beatmap.BeatFailed += OnBeatFailed;
	}

	void OnDisable() {

		beatmap.BeatFailed -= OnBeatFailed;
	}

	private void OnBeatFailed( Beatmap beatmap, Beatmap.Beat beat ) {

		--lives;

		if ( lives > 0 ) {

			Hit();
		} else {

			Died();
		}
	}
}
