using UnityEngine;
using System.Collections;

public class MoveAndDestroy : MonoBehaviour {

	[SerializeField]
	private float _duration = 1f;

	[SerializeField]
	private float _moveSpeed = 2f;

	IEnumerator Start() {

		var fromY = transform.position.y;
		var toY = fromY - 4f;

		var timer = new AutoTimer( _duration );
		while ( timer.ValueNormalized < 1f ) {

			var position = transform.position;
			position.x += _moveSpeed * Time.deltaTime;
			//position.y = Mathf.Lerp( fromY, toY, timer.ValueNormalized.Pow( 2f ) );
			transform.position = position;

			yield return null;
		}

		Destroy( gameObject );
	}
}
