using UnityEngine;

public class UIShowWinScreen : MonoBehaviour {

	public Beatmap Beatmap;
	//public AudioSource beatmapSound;

	//public GameObject root;

	public float Progress { get { return Beatmap.GetRateFinished(); } }

	void Update() {

		if ( Progress >= 1f ) {

			GameplayController.Instance.IsPaused = true;
			ScreenManager.GetScreen<UIWinScreen>().Show();
		}
	}
}
