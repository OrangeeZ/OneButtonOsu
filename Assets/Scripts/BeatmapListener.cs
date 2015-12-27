using UnityEngine;
using System.Collections;

public class BeatmapListener : MonoBehaviour {

	public Beatmap targetBeatmap;

	void OnEnable() {

		targetBeatmap.BeatCompleted += ( beatmap, beat ) => Debug.Log( "Win" );
		targetBeatmap.BeatFailed += ( beatmap, beat ) => Debug.Log( "Lose" );
	}
}
