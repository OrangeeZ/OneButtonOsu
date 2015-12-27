using UnityEngine;
using System.Collections;

public class UIRestartOnClick : MonoBehaviour {

	public void OnClick() {

		Application.LoadLevel( Application.loadedLevel );

		//Time.timeScale = 1f;
	}
}
