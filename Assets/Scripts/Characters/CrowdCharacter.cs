using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CrowdCharacter : MonoBehaviour {

	public Vector2 speedRange = new Vector2( 1f, 2f );

	public SpriteRenderer[] renderers = null;

	public Animator animator;

	public float fadeInDuration = 0.5f;

	public float fadeOutDuration = 0.5f;

	public bool isActive { get; private set; }

	private float speed;

	private float randomPhaseShift;

	void Start() {

		renderers = GetComponentsInChildren<SpriteRenderer>();
	}

	void OnEnable() {

		speed = Random.Range( speedRange.x, speedRange.y );

		randomPhaseShift = Random.Range( 0, 100 );

		//animator.playbackTime += Random.Range( 0, 15f );

		StartCoroutine( Fade( fadeInDuration, SetColor, Color.clear, Color.white, true ) );

		transform.localScale = Vector3.one * Random.Range( 0.9f, 1.1f );
	}

	void Update() {

		animator.speed = 1f + ( Mathf.Sin( randomPhaseShift + Time.timeSinceLevelLoad * speed ) * 0.2f );

		transform.localPosition = Mathf.Sin( randomPhaseShift + Time.timeSinceLevelLoad * speed ) * Vector3.right;
	}

	public void NotifySuccess() {

		isActive = false;

		StartCoroutine( Fade( fadeOutDuration, SetColor, Color.white, Color.clear, false ) );
	}

	private void SetColor( Color newColor ) {
		
		renderers.MapImmediate( _=>_.color = newColor );
	}

	private IEnumerator Fade( float duration, Action<Color> setter, Color from, Color to, bool activeOnComplete ) {

		var timer = 0f;
		while ( ( timer += Time.unscaledDeltaTime ) <= duration ) {

			setter( Color.Lerp( from, to, timer / duration ) );

			yield return null;
		}

		isActive = true;

		gameObject.SetActive( value: activeOnComplete );
	}
}
