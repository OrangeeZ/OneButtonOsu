using UnityEngine;
using System.Collections;
using System.Text;

public class AObject : MonoBehaviour {

	public Vector3 position {
		get {
			return transform.position;
		}

		set {
			transform.position = value;
		}
	}

	public Vector3 localPosition {
		get {
			return transform.localPosition;
		}

		set {
			transform.localPosition = value;
		}
	}

	public Vector3 localScale {
		get {
			return transform.localScale;
		}

		set {
			transform.localScale = value;
		}
	}

	public Quaternion rotation {
		get {
			return transform.rotation;
		}

		set {
			transform.rotation = value;
		}
	}

	public Vector3 up {
		get {
			return transform.up;
		}
	}

	public Vector3 forward {
		set {
			transform.forward = value;
		}

		get {
			return transform.forward;
		}
	}

	public Vector3 right {
		get {
			return transform.right;
		}
	}

	public void GetComponent<T>( out T target ) where T : Component {

        target = GetComponent<T>();
	}

    public T Instantiate<T>( T prefab, Vector3 position ) where T: Object {

        return Instantiate( prefab, position, Quaternion.identity ) as T;
    }

	public void Log( params object[] messages ) {
		var result = new StringBuilder( 1024 );

		result.Append( "[" );
		result.Append( System.DateTime.Now );
		result.Append( "] " );
		result.Append( gameObject.name );
		result.Append( ": " );

		foreach ( var message in messages ) {
			if ( message == null )
				result.Append( "null" );
			else
				result.Append( message );

			result.Append( " " );
		}

		Debug.Log( result, this );
	}
}
