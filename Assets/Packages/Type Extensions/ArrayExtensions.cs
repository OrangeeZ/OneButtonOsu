using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions {
	public static float[] ToFlat( this float[,] source ) {
		var rows = source.Length / source.GetLength( 0 );
		var result = new float[source.Length];

		for ( var column = 0; column < rows; ++column ) {
			for ( var i = 0; i < source.GetLength( 0 ); ++i ) {
				result[column * source.GetLength( 0 ) + i] = source[column, i];
			}
		}

		return result;
	}

	public static float[,] ToArray2D( this float[] source, int rowLength ) {
		var result = new float[source.Length / rowLength, rowLength];

		for ( var column = 0; column < source.Length / rowLength; ++column ) {

			for ( var i = 0; i < rowLength; ++i ) {
				result[column, i] = source[column * rowLength + i];
			}

		}

		return result;
	}

	public static T[] Randomized<T>( this T[] self ) {
		var result = self.ToList();

		for ( var i = 0; i < result.Count; ++i ) {
			var randomIndex = Random.Range( 0, result.Count );
			var randomElement = result[randomIndex];

			result.RemoveAt( randomIndex );
			result.Add( randomElement );
		}

		return result.ToArray();
	}

	public static T RandomElement<T>( this IEnumerable<T> self ) {

		if ( self.IsEmpty() ) {

			return default( T );
		}

		return self.ElementAt( Random.Range( 0, self.Count() ) );
	}
}