using System.Text;
using UnityEngine;

public static class MonoBehaviourExtensions {

	public static void GetComponent<T>( this Component self, out T result ) where T : Component {

		result = self.GetComponent<T>();
	}

	public static void GetComponentsInChildren<T>( this Component self, out T[] result, bool includeInactive = false ) where T : Component {

		result = self.GetComponentsInChildren<T>( includeInactive );
	}

	public static void Log( this MonoBehaviour self, params object[] messages ) {

		var result = new StringBuilder( 1024 );

		result.Append( "<color=yellow>[" );
		result.Append( System.DateTime.Now );
		result.Append( "]</color> <color=cyan>" );
		result.Append( self.gameObject.name );
		result.Append( "</color>: " );

		foreach ( var each in messages ) {

			result.Append( each == null ? "null" : each.ToString() );

			result.Append( " " );
		}

		Debug.Log( result, self );
	}
}
