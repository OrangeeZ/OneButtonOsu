using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public static GameplayController Instance { get; private set; }

	public bool IsPaused { get { return Time.timeScale == 0f; } set { Time.timeScale = value ? 0f : 1f; } }

	void Awake() {

		Instance = this;
	}
}
