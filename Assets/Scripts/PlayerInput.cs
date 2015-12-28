using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public string keyName = "P1 Fire";

	public Beatmap beatmap;

	public float DoubleTapWindowLength = 0.1f;

	private float _previousPressTime = 0f;
	private bool _waitForSecondTap = false;

	private float DeltaTimeSinceLastPress { get { return Time.time - _previousPressTime; } }

	void Update() {

		if (_waitForSecondTap && DeltaTimeSinceLastPress > DoubleTapWindowLength)
		{
			beatmap.OnPress();

			_waitForSecondTap = false;
		}

		if ( Input.GetButtonDown( keyName ) ) {

			//beatmap.OnPress();
			OnPointerDown(null);
		}

		//if ( Input.GetButtonUp( keyName ) ) {

		//	beatmap.OnRelease();
		//}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (DeltaTimeSinceLastPress <= DoubleTapWindowLength && _waitForSecondTap)
		{
			beatmap.OnDoublePress();
			_waitForSecondTap = false;
		}
		else
		{
			_waitForSecondTap = true;
		}

		_previousPressTime = Time.time;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//beatmap.OnRelease();
	}
}