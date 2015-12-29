using System;
using UnityEngine;

public class Interval<T> where T : IComparable {

	public T from, to;

	public bool Contains( T value ) {

		var fromComparison = value.CompareTo( from );

		if ( fromComparison < 0 ) {

			return false;
		}

		var toComparison = value.CompareTo( to );

		return toComparison <= 0;
	}
}

[Serializable]
public class IntInterval : Interval<int> { }

[Serializable]
public class FloatInterval : Interval<float> { }