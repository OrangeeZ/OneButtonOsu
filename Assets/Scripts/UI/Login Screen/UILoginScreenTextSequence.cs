using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoginScreenTextSequence : MonoBehaviour {

	[SerializeField]
	private float _textScreenDuration = 2f;

	public void Play( Action onFinish ) {

		StartCoroutine( PlayCoroutine( onFinish ) );
	}

	private IEnumerator PlayCoroutine( Action onFinish ) {

		foreach ( var each in transform ) {

			( each as Transform ).gameObject.SetActive( false );
		}

		foreach ( var each in transform.OfType<Transform>() ) {

			each.gameObject.SetActive( true );

			yield return StartCoroutine( FadeText( each, shouldFadeIn: true ) );

			var timer = _textScreenDuration;
			while ( ( timer -= Time.unscaledDeltaTime ) > 0f ) {

				yield return null;
			}

			yield return StartCoroutine( FadeText( each, shouldFadeIn: false ) );

			each.gameObject.SetActive( true );
		}

		yield return null;

		onFinish();
	}

	private IEnumerator FadeText( Transform target, bool shouldFadeIn ) {

		var images = target.GetComponentsInChildren<Graphic>();
		var duration = 0.3f;
		var timer = 0f;
		var fromAlpha = shouldFadeIn ? 0f : 1f;
		var toAlpha = shouldFadeIn ? 1f : 0f;

		do {

			foreach ( var each in images ) {

				var color = each.color;
				color.a = Mathf.Lerp( fromAlpha, toAlpha, timer / duration );
				each.color = color;
			}

			yield return null;

		} while ( ( timer += Time.unscaledDeltaTime ) < duration );
	}
}
