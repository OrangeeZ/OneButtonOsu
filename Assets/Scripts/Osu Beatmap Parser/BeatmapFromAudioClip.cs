using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BeatmapFromAudioClip : MonoBehaviour {

	[SerializeField]
	private int _maxBeats = 50;

	[SerializeField]
	private float _beatThreshold = 0.6f;

	[SerializeField]
	private AudioClip _targetClip;

#if UNITY_EDITOR
	[ContextMenu( "Parse" )]
	public void Parse() {
		var coroutine = ParseLoop();
		UnityEditor.EditorApplication.CallbackFunction updateAction = () => { };
		updateAction = () => {
			if ( !coroutine.MoveNext() ) {
				UnityEditor.EditorApplication.update -= updateAction;
			}
		};

		UnityEditor.EditorApplication.update += updateAction;

		//StartCoroutine( ParseLoop() );
	}

	private IEnumerator ParseLoop() {

		var samples = new float[_targetClip.samples];
		if ( !_targetClip.GetData( samples, 0 ) ) {

			Debug.LogAssertion( "Could not read samples" );
			yield break;
		}

		var beats = new List<Beatmap.Beat>( capacity: _targetClip.samples );
		var itrCount = 0;
		var sampleScaleFactor = 1f / _targetClip.frequency;
		for ( var i = 0; i < samples.Length && beats.Count < _maxBeats; ++i ) {

			if ( samples[i] >= _beatThreshold ) {

				beats.Add( new Beatmap.Beat { time = i * sampleScaleFactor } );
			}

			if ( ++itrCount > 1000 ) {

				itrCount = 0;

				var isCancelled = UnityEditor.EditorUtility.DisplayCancelableProgressBar( "Processing samples", beats.Count.ToString(), (float)beats.Count / _maxBeats );

				if ( isCancelled ) {

					UnityEditor.EditorUtility.ClearProgressBar();

					yield break;
				}

				yield return null;
			}
		}

		var beatmapComponent = gameObject.GetComponent<Beatmap>() ?? gameObject.AddComponent<Beatmap>();

		beatmapComponent.beatmap = beats.ToArray();

		UnityEditor.EditorUtility.ClearProgressBar();
	}
#endif
}
