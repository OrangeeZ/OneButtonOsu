using UnityEngine;
using System.Collections;

public class BeatmapStartDelay : MonoBehaviour {

	public float delay = 3;

	public Beatmap beatmap;

	IEnumerator Start() {

		yield return new WaitForSeconds( delay );

		beatmap.Play();
	}
}
