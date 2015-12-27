using System.Collections.Generic;
using System.Linq;

public static class ListExtensions {

	public static bool IsEmpty<T>( this IEnumerable<T> self ) {

		return self !=null && !self.Any();
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

		return self.IsNullOrEmpty() ? "[]" : "[" + self.Select( each => object.Equals( each, default( T ) ) ? "null" : each.ToString() ).Aggregate( ( result, each ) => result + ", " + each ) + "]";
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

	//public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>( this IEnumerable<T1> self, IEnumerable<T2> other ) {

	//	var itrSelf = self.GetEnumerator();
	//	var itrOther = other.GetEnumerator();

	//	var canIterateSelf = itrSelf.MoveNext();
	//	var canIterateOther = itrOther.MoveNext();

	//	while ( canIterateSelf || canIterateOther ) {

	//		yield return new Tuple<T1, T2> ( 
	//			canIterateSelf ? itrSelf.Current : default( T1 ),
	//			canIterateOther ? itrOther.Current : default( T2 ) 
	//		);

	//		canIterateSelf = itrSelf.MoveNext();
	//		canIterateOther = itrOther.MoveNext();
	//	}
	//}
}
