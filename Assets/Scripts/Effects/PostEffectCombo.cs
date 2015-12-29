using System;
using UnityEngine;
using System.Collections;

public class PostEffectCombo : MonoBehaviour {

	[Serializable]
	public class Combo {

		public Behaviour[] Components;

		public void SetActive( bool isActive ) {

			foreach ( var each in Components ) {

				each.enabled = isActive;
			}
		}
	}

	[SerializeField]
	private Combo[] _combos;

	private Combo _activeCombo;

	void Start() {

		foreach ( var each in _combos ) {

			each.SetActive( false );
		}
	}

	public void ToggleRandomEffect() {

		DeactivateEffect();

		_activeCombo = _combos.RandomElement();
		_activeCombo.SetActive( true );
	}

	public void DeactivateEffect() {

		if ( _activeCombo != null ) {

			_activeCombo.SetActive( false );
		}

	}
}
