using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIContinueOnClick : MonoBehaviour, IPointerClickHandler {

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

	public void OnPointerDown(PointerEventData eventData)
	{
		OnClick();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick();
	}

	public void OnMove(AxisEventData eventData)
	{
		Debug.Log(eventData);
	}
}
