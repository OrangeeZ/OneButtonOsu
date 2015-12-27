using UnityEngine;
using System.Collections.Generic;

public class BeatmapSimpleView : MonoBehaviour {

	public Beatmap beatmap;

	public float timelineLength = 3f;

	public float timelineSize = 4f;

	public List<GameObject> timelineMarkerPrefabs = new List<GameObject>();

	void Update() {

		foreach ( var each in timelineMarkerPrefabs ) {

			each.SetActive( value: false );
		}

		var itr = timelineMarkerPrefabs.GetEnumerator();

		foreach ( var each in beatmap.GetBeats( timelineLength ) ) {

			if ( !itr.MoveNext() ) {

				return;
			}

			itr.Current.transform.localPosition = ( each.time - beatmap.GetTimer() ) / timelineLength * Vector3.right * timelineSize;
			itr.Current.SetActive( value: true );

			var relativeScale = ( beatmap.GetBeatEndTime( each ) - each.time ) / timelineLength * timelineSize;

			itr.Current.transform.localScale = new Vector3( relativeScale, 1f, 1f );
			itr.Current.transform.localPosition += new Vector3( relativeScale * 0.5f, 0, 0 );
		}
	}
}
