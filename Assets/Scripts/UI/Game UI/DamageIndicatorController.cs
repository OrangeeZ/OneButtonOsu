using UnityEngine;
using System.Collections;

public class DamageIndicatorController : MonoBehaviour {

	[SerializeField]
	private Camera _targetCamera;

	[SerializeField]
	private int _targetTeamId;

	[SerializeField]
	private DamageIndicator _damageIndicator;

	[SerializeField]
	private BeatmapGlassView _targetBeatmapView;

	[SerializeField]
	private float _criticalDamageChance = 0.7f;

	private void Start() {

		_targetBeatmapView.BeatCompleted += OnCharacterReceiveDamage;

		//EventSystem.Events.SubscribeOfType<Character.RecievedDamage>( OnCharacterReceiveDamage );
	}

	private void OnCharacterReceiveDamage( Vector3 position ) {

		if ( _targetCamera == null ) {

			return;
		}

		var instance = Instantiate( _damageIndicator );
		var screenPosition = Camera.main.WorldToScreenPoint( position );

		instance.transform.position = _targetCamera.ScreenToWorldPoint( screenPosition ) * 0.5f;
		instance.transform.SetParent( transform );

		var isCritical = 1f.Random() >= _criticalDamageChance;
		var damageValue = isCritical ? Random.Range( 2000, 3000 ) : Random.Range( 500, 700 );

		instance.Initialize( damageValue, true, isCritical );
	}

}