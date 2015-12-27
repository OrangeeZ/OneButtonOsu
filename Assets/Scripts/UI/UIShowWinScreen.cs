using UnityEngine;

public class UIShowWinScreen : MonoBehaviour {

	public AudioSource beatmapSound;

	//public GameObject root;

	public float Progress { get { return beatmapSound.time/beatmapSound.clip.length; } }

	void Update(){

		if ( Progress  >= 1f ) {
			
			ScreenManager.GetScreen<UIWinScreen>().Show();
			//root.SetActive(value: true);

			Time.timeScale = 0f;
		}
	}
}
