using UnityEngine;

public class UIShowWinScreen : MonoBehaviour {

	public AudioSource beatmapSound;

	public GameObject root;

	void Update(){

		if ( ( beatmapSound.time / beatmapSound.clip.length ) >= 1f ) {
			
			root.SetActive(value: true);

			Time.timeScale = 0f;
		}
	}
}
