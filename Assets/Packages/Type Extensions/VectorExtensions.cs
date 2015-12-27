using System;
using UnityEngine;
using System.Collections;

public static class VectorExtensions {

    public static Vector2 ToXZ( this Vector3 self ) {

        return new Vector2( self.x, self.z );
    }

    public static Vector3 ToXZ( this Vector2 self ) {

        return new Vector3( self.x, 0, self.y );
    }

	public static Vector3 ClampMagnitude( this Vector3 self, float maxLength ) {

		return Vector3.ClampMagnitude( self, maxLength );
	}

    public static Vector3 Set( this Vector3 self, float x = float.NaN, float y = float.NaN, float z = float.NaN ) {

        self.x = x.IsNan() ? self.x : x;
        self.y = y.IsNan() ? self.y : y;
        self.z = z.IsNan() ? self.z : z;

        return self;
    }

}