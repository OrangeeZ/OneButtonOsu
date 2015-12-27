using UnityEngine;

[System.Serializable]
public class ScaledCurve {

	public AnimationCurve curve = null;
	public float yScale = 1f;

	public float Evaluate( float t ) {

		return curve.Evaluate( t ) * yScale;
	}
}
