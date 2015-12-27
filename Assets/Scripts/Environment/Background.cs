using System.Linq;
using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public GameObject[] elements;

	public float speed = 5f;

	public float shiftAmount = 10;

	void Update() {

		foreach ( var each in elements ) {

			each.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		foreach ( var each in elements.Where( each => !each.gameObject.activeSelf ) ) {
			
			each.transform.position += Vector3.right * shiftAmount;
			each.SetActive( value: true );
		}
	}
}
