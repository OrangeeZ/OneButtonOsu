using UnityEngine;
using System.Collections;

public class UIContinueOnClick : MonoBehaviour {

	//public dfControl root;

	public GameObject gameUI;

	void OnEnable() {

		Time.timeScale = 0f;
	}

	void Update() {

		if ( Input.GetButtonDown( "P1 Fire" ) ) {
			
			OnClick();
		}
	}

	public void OnClick() {

		Time.timeScale = 1f;

		gameObject.SetActive(false);
		//root.Hide();
		gameUI.SetActive(true);//Show();
	}
}
