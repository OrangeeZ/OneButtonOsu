using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

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

	public void OnPointerDown(PointerEventData eventData)
	{
		beatmap.OnPress();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		beatmap.OnRelease();
	}
}