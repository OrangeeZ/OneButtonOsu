using System.Monads;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions {

	public static bool IsEmpty<T>( this IEnumerable<T> self ) {

		return self.Count() == 0;
	}

	public static bool IsNullOrEmpty<T>( this IEnumerable<T> self ) {

		return self == null || IsEmpty( self );
	}

	public static IEnumerable<T> TakeWhile<T>( this IEnumerable<T> self, int startFrom, System.Func<T, bool> predicate ) {

		return self.Skip( startFrom ).TakeWhile( predicate );
	}

	public static HashSet<T> ToHashSet<T>( this IEnumerable<T> self ) {

		return new HashSet<T>( self );
	}

	public static string ContentsToString<T>( this IEnumerable<T> self ) {

		return self.IsEmpty() ? "[]" : "[" + self.Select( each => each.ToString() ).Aggregate( ( result, each ) => result + ", " + each.Return( _ => _, "null" ) ) + "]";
	}

	public static IEnumerable<T> Map<T>( this IEnumerable<T> self, System.Action<T> action ) {

		foreach ( var each in self ) {

			action( each );

			yield return each;
		}
	}

	public static void MapImmediate<T>( this IEnumerable<T> self, System.Action<T> action ) {

		foreach ( var each in self ) {

			action( each );
		}
	}
}
