using System.Monads;
using UnityEngine;
using System.Collections;

public class ScreenSizeAnimation : ScreenAnimation {

	public AnimationCurve xAxis;
	public AnimationCurve yAxis;

	public AnimationCurve timeCurve;

	public bool invert = false;

	//public dfControl control;

	//public float duration = 0.2f;

	//private IEnumerable LerpValue( System.Action<Vector3> Setter, Vector3 from, Vector3 to, float duration ) {

	//	var timer = 0f;

	//	Setter( from );

	//	yield return null;

	//	while ( timer < duration ) {

	//		Setter( Vector3.Lerp( from, to, timeCurve.Evaluate( timer / duration ) ) );

	//		timer += Time.unscaledDeltaTime;

	//		yield return null;
	//	}

	//	Setter( to );
	//}

	//public override IEnumerable GetAnimation( float? overrideDuration ) {

	//	var targetDuration = overrideDuration == null ? this.duration : overrideDuration.Value;
	//	var targetControl = control ?? screen.windowControl;

	//	var from = new Vector3( xAxis.Evaluate( 0f ), yAxis.Evaluate( 0f ) );
	//	var to = new Vector3( xAxis.Evaluate( 1f ), yAxis.Evaluate( 1f ) );

	//	return LerpValue( value => targetControl.transform.localScale = value, invert ? to : from, invert ? from : to, targetDuration );
	//}

	//public override void Cleanup() {

	//	( control ?? screen.windowControl ).transform.localScale = Vector3.one;
	//}
}
