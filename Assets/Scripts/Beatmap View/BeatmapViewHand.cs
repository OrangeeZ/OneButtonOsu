using System;
using UnityEngine;
using System.Collections;

public class BeatmapViewHand : MonoBehaviour {

	public Animator animator;

	public string successAnimationName = "Success";

	public Beatmap beatmap;

	public float JumpDelay;

	void OnEnable() {

		beatmap.BeatCompleted += OnBeatCompleted;
	}

	void OnDisable() {

		beatmap.BeatCompleted -= OnBeatCompleted;
	}

	private IEnumerator ToggleAnimation( string animationName )
	{

		yield return new WaitForSeconds(JumpDelay);

		animator.SetBool( animationName, true );

		yield return new WaitForSeconds( .1f );

		animator.SetBool( animationName, false );
	}

	private void OnBeatCompleted( Beatmap beatmap, Beatmap.Beat beat ) {

		StartCoroutine( ToggleAnimation( successAnimationName ) );
	}
}
