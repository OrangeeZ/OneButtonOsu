using UnityEngine;
using System.Collections;

public static class NumbersExtensions {
	public static float Abs( this float number ) {
		return Mathf.Abs( number );
	}

	public static float Random( this float number ) {

		return UnityEngine.Random.Range ( 0f, number );
	}

	public static float Looped( this float number, float rightBound ) {
		return ( number + rightBound ) % rightBound;
	}

	public static float Clamped( this float number, float leftBound, float rightBound ) {
		return Mathf.Clamp( number, leftBound, rightBound );
	}
	
	public static int Clamped( this int number, int leftBound, int rightBound ) {
		return Mathf.Clamp( number, leftBound, rightBound );
	}

	public static float Negative( this float number ) {
		return -number;
	}

	public static float Reciprocal( this float number ) {
		return 1f / number;
	}

	public static int RoundToInt( this float number ) {

		return Mathf.RoundToInt ( number );
	}

	public static float Sign( this float number ) {
		return Mathf.Sign( number );
	}

	public static float Floor( this float number ) {
		return Mathf.Floor( number );
	}

	public static float Ceil( this float number ) {
		return Mathf.Ceil( number );
	}

	public static int FloorToInt( this float number ) {
		return Mathf.FloorToInt( number );
	}

	public static int CeilToInt( this float number ) {
		return Mathf.CeilToInt( number );
	}

	public static int Random( this int number ) {

		return UnityEngine.Random.Range ( 0, number );
	}

	public static int Abs( this int number ) {
		return Mathf.Abs( number );
	}

	public static float Pow( this float number, float power ) {
		return Mathf.Pow( number, power );
	}
}