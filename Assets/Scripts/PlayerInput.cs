using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public string keyName = "P1 Fire";

	public Beatmap beatmap;

	void Update() {

		if ( Input.GetButtonDown( keyName ) ) {

			beatmap.OnPress();
		}

		if ( Input.GetButtonUp( keyName ) ) {

			beatmap.OnRelease();
		}
	}
}