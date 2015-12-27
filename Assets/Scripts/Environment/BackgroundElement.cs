using UnityEngine;
using System.Collections;

public class BackgroundElement : MonoBehaviour {

	void OnBecameInvisible() {
		
		gameObject.SetActive( value: false );
	}
}
