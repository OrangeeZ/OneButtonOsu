using System.Linq;
using UnityEngine;
using System.Collections;

public static class AnimationCurveExtensions {

	public static float GetDuration( this AnimationCurve self ) {

		var lastKey = self.keys.LastOrDefault();

		return lastKey.time;
	}
}
