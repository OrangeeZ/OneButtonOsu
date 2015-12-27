using System;
using System.Collections;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using System.Collections.Generic;

public class BeatmapGlassView : MonoBehaviour {

	public Beatmap beatmap;

	public float timelineLength = 3f;

	public float timelineSize = 4f;

	public bool fadeFailedBeats = false;

	[SerializeField]
	private List<GameObject> shortBeatInstances;

	[SerializeField]
	private List<GameObject> longBeatInstances;

	private readonly HashSet<Beatmap.Beat> _currentBeats = new HashSet<Beatmap.Beat>();

	void Start() {

		foreach ( var each in longBeatInstances.Concat( shortBeatInstances ) ) {

			each.SetActive( value: false );
		}

		beatmap.BeatFailed += BeatFailed;
	}

	private void BeatFailed( Beatmap beatmap, Beatmap.Beat beat ) {

		if ( fadeFailedBeats ) {
			
			var allInstances = longBeatInstances.Concat( shortBeatInstances ).Where( each => each.gameObject.activeSelf );

			var beatPosition = ( beat.time - beatmap.GetTimer() ) * Vector3.right / timelineLength * timelineSize;

			var instance = allInstances.FirstOrDefault( where => ( where.transform.position.x - beatPosition.x ) < 0.02f );

			if ( instance == null ) {

				return;
			}

			StartCoroutine( Fade( 0.5f, color => instance.GetComponent<SpriteRenderer>().color = color, Color.white, Color.clear, false, instance.SetActive ) );
		}
	}

	void Update() {

		//foreach ( var each in longBeatInstances.Concat( shortBeatInstances ) ) {

		//	each.SetActive( value: false );
		//}

		var shortInstanceItr = shortBeatInstances.Where( each => !each.gameObject.activeSelf ).GetEnumerator();
		var longInstanceItr = longBeatInstances.Where( each => !each.gameObject.activeSelf ).GetEnumerator();

		var beatsInWindow = beatmap.GetBeats( timelineLength );
		var newBeats = beatsInWindow.Except( _currentBeats ).ToArray();

		var allInstances = longBeatInstances.Concat( shortBeatInstances ).Where( each => each.gameObject.activeSelf );

		//newBeats.MapImmediate( beat => _currentBeats.Add( beat ) );

		foreach ( var each in newBeats ) {

			_currentBeats.Add(each);

			var instance = default( GameObject );

			if ( each.isLong ) {

				instance = !longInstanceItr.MoveNext() ? longBeatInstances.FirstOrDefault( where => !where.gameObject.activeSelf ) : longInstanceItr.Current;
			} else {

				instance = !shortInstanceItr.MoveNext() ? shortBeatInstances.FirstOrDefault( where => !where.gameObject.activeSelf ) : shortInstanceItr.Current;
			}

			if ( instance == null ) {

				continue;
			}

			instance.transform.localPosition = ( each.time - beatmap.GetTimer() ) * Vector3.right / timelineLength * timelineSize;

			instance.SetActive( value: true );
		}

		foreach ( var each in allInstances ) {

			each.transform.localPosition += ( Vector3.left * timelineSize ) / timelineLength * Time.deltaTime;
		}
	}

	private IEnumerator Fade( float duration, Action<Color> setter, Color from, Color to, bool activeOnComplete, Action<bool> activeSetter ) {

		var timer = 0f;
		while ( ( timer += Time.unscaledDeltaTime ) <= duration ) {

			setter( Color.Lerp( from, to, timer / duration ) );

			yield return null;
		}

		activeSetter( activeOnComplete );

		if ( !activeOnComplete ) {

			setter( from );
		}
	}

	private GameObject SpawnInstance( GameObject prefab ) {

		var result = Instantiate( prefab ) as GameObject;

		result.transform.parent = transform;

		return result;
	}

#if UNITY_EDITOR

	void OnDrawGizmos() {

		Gizmos.matrix = transform.localToWorldMatrix;

		foreach ( var each in beatmap.beatmap ) {

			var from = ( each.time - beatmap.Timer ) / timelineLength * Vector3.right * timelineSize;
			var to = ( beatmap.GetBeatEndTime( each ) - beatmap.Timer ) / timelineLength * Vector3.right * timelineSize;

			Gizmos.color = each.isLong ? Color.red : Color.blue;
			Gizmos.DrawLine( from, to );

			UnityEditor.Handles.Label( transform.localToWorldMatrix.MultiplyPoint3x4( from ), Array.IndexOf( beatmap.beatmap, each ).ToString() );
		}
	}

#endif
}
