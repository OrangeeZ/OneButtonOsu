using System.Linq;
using UnityEngine;
using System.Collections;

public static class AnimationCurveExtensions {

	public static float GetDuration( this AnimationCurve self ) {

		var keys = self.keys;
		var lastKey = keys.Length > 0 ? keys [keys.Length - 1] : default(Keyframe);

		return lastKey.time;
	}
}
