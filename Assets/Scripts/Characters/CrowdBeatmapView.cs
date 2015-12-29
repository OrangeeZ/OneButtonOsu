using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CrowdBeatmapView : MonoBehaviour {

	public Beatmap targetBeatmap;

	public CrowdCharacter crowdCharacterPrefab;

	public int maxCharacters = 10;

	public float respawnInterval = 1f;

	private CrowdCharacter[] crowdCharactersPool;

	IEnumerator Start() {

		crowdCharactersPool = Enumerable.Range( 0, maxCharacters ).Select( _ => SpawnCharacter( crowdCharacterPrefab ) ).ToArray();

		while ( true ) {

			var inactiveCharacter = crowdCharactersPool.FirstOrDefault( where => !where.gameObject.activeSelf );

			if ( inactiveCharacter != null ) {

				inactiveCharacter.gameObject.SetActive( value: true );
			}

			yield return new WaitForSeconds( respawnInterval );
		}
	}

	void OnEnable() {

		targetBeatmap.BeatCompleted += OnBeatCompleted;
	}

	void OnDisable() {

		targetBeatmap.BeatCompleted -= OnBeatCompleted;
	}

	private void OnBeatCompleted( Beatmap beatmap, Beatmap.Beat beat, float rateHit ) {

		for ( var i = 0; i < ( beat.isLong ? 2 : 1 ); i++ ) {

			var activeCharacters = crowdCharactersPool.Where( each => each.gameObject.activeSelf && each.isActive );

			if ( !activeCharacters.Any() ) {

				return;
			}

			activeCharacters.ElementAt( Random.Range( 0, activeCharacters.Count() ) ).NotifySuccess();
		}
	}

	private CrowdCharacter SpawnCharacter( CrowdCharacter prefab ) {

		var instance = Instantiate( prefab ) as CrowdCharacter;

		instance.transform.parent = transform;
		instance.transform.localPosition = Vector3.zero;

		return instance;
	}
}
