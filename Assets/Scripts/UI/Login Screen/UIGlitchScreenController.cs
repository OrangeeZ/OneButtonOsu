using UnityEngine;
using System.Collections;

public class UIGlitchScreenController : MonoBehaviour {

	[SerializeField]
	private PostEffectCombo _postEffectCombo;

	private float _minGlitchDelay = 0.3f;
	private float _maxGlitchDelay = 0.7f;

	void OnEnable() {

		StartCoroutine( Loop() );
	}

	void OnDisable() {

		StopAllCoroutines();

		_postEffectCombo.DeactivateEffect();
	}

	private IEnumerator Loop() {

		while ( true ) {

			var delay = Random.Range( _minGlitchDelay, _maxGlitchDelay );
			while ( ( delay -= Time.unscaledDeltaTime ) > 0f ) {

				yield return null;
			}

			_postEffectCombo.ToggleRandomEffect();

			delay = Random.Range( _minGlitchDelay, _maxGlitchDelay ) * 0.4f;
			while ( ( delay -= Time.unscaledDeltaTime ) > 0f ) {

				yield return null;
			}

			_postEffectCombo.DeactivateEffect();
		}
	}
}
