using System;
using UnityEngine;
using System.Collections;

public class BeatmapViewHand : MonoBehaviour {

	public Animator Animator;

	public string successAnimationName = "Success";

	public Beatmap Beatmap;

	public float JumpDelay;

	[Header( "Effect settings" )] //public GameObject JumpDustPrefab;
	public GameObject AttackEffectPrefab;
	public GameObject UltimateAttackEffectPrefab;

	void OnEnable() {

		Beatmap.BeatCompleted += OnBeatCompleted;
	}

	void OnDisable() {

		Beatmap.BeatCompleted -= OnBeatCompleted;
	}

	//private IEnumerator ToggleAnimation( string animationName ) {
	//	//yield return new WaitForSeconds(JumpDelay);

	//	//Animator.SetBool( animationName, true );

	//	//yield return new WaitForSeconds( .1f );

	//	//Animator.SetBool( animationName, false );
	//}

	private void OnBeatCompleted( Beatmap beatmap, Beatmap.Beat beat, float rateHit ) {

		var prefab = beat.isLong ? UltimateAttackEffectPrefab : AttackEffectPrefab;
		Instantiate( prefab ).transform.position = transform.position;

		//StartCoroutine( ToggleAnimation( successAnimationName ) );
	}

	public void NotifyJumpEnd() {

		//Instantiate( JumpDustPrefab ).transform.position = transform.position;
	}
}
