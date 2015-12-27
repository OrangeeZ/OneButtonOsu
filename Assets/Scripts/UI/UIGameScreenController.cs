using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameScreenController : UIScreen
{

	[SerializeField]
	private Text _timer;

	public void SetTimer(float normalizedValue)
	{
		_timer.text = normalizedValue.ToString();
	}
}
