using UnityEngine;
using System.Collections;

public static class TransformExtensions {

	public static void ResetLocalTransform( this Transform self ) {

		self.localPosition = Vector3.zero;
		self.localRotation = Quaternion.identity;
		self.localScale = Vector3.one;
	}
}
