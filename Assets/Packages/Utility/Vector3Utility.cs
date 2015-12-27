using UnityEngine;
using System.Collections;

public static class Vector3Utility {

	public static bool ApproximateEqual( Vector3 a, Vector3 b ) {

		return ( a - b ).sqrMagnitude < float.Epsilon;
	}
}
