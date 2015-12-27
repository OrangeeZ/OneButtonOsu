using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBeatmapProgress : MonoBehaviour
{

	public Beatmap Beatmap;
	public Image ProgressBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProgressBar.fillAmount = Beatmap.Timer / Beatmap.EndTime;
	}
}
