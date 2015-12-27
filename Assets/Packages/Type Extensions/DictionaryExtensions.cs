using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class DictionaryExtensions {

	public static Dictionary<T3, T4> ToSignature<T1, T2, T3, T4>( this Dictionary<T1, T2> self ) {

		var result = new Dictionary<T3, T4>( capacity: self.Count );

		foreach ( var each in self ) {

			result.Add( (T3)Convert.ChangeType( each.Key, typeof( T3 ) ), (T4)Convert.ChangeType( each.Value, typeof( T4 ) ) );
		}

		return result;
	}
}
