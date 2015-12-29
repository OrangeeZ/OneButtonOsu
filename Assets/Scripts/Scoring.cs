using System;
using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour {

	public float Score { get; private set; }
	public int Kills { get; private set; }
	public int Deaths { get; private set; }

	public float ExpPerKill;

	[SerializeField]
	private Beatmap _beatmap;

	void Start() {

		_beatmap.BeatCompleted += BeatCompleted;
		_beatmap.BeatFailed += BeatFailed;
	}

	private void BeatFailed( Beatmap beatmap, Beatmap.Beat beat ) {

		Deaths++;
	}

	private void BeatCompleted( Beatmap beatmap, Beatmap.Beat beat, float hitRate ) {

		Score += hitRate * ExpPerKill;
		Kills++;
	}
}
