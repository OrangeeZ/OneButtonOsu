using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GameObjectExtensions {
	public static Bounds GetBounds( this GameObject self ) {
		var result = new Bounds( self.transform.position, Vector3.zero );

		foreach ( var each in self.GetComponentsInChildren<Renderer>() )
			result.Encapsulate( each.bounds );

		return result;
	}
}
