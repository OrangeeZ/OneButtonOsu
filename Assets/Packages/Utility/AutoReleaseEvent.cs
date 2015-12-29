using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class AutoReleaseEvent {

	private struct EventSubscriber {

		public Action action;
		public Object owner;
	}

	private LinkedList<EventSubscriber> actionList = new LinkedList<EventSubscriber>();

	public void Call() {

		for ( var node = actionList.First; node != null; ) {

			var next = node.Next;
			var each = node.Value;

			if ( each.owner == null ) {

				actionList.Remove( node );
			} else {

				each.action();
			}

			node = next;

		}
	}

	public void Register( Object owner, Action action ) {

		actionList.AddLast( new EventSubscriber {
			action = action,
			owner = owner
		} );
	}

	public void Unregister( Action action ) {

		for ( var node = actionList.First; node != null; node = node.Next ) {
			if ( node.Value.action == action ) {

				actionList.Remove( node );
				break;
			}
		}
	}

	public void UnregisterAll() {
		actionList.Clear();
	}
}

public class AutoReleaseEvent<T> {

	private struct EventSubscriber {

		public Action<T> action;
		public Object owner;

	}

	private LinkedList<EventSubscriber> actionList = new LinkedList<EventSubscriber>();

	public void Call( T arg1 ) {

		for ( var node = actionList.First; node != null; ) {

			var next = node.Next;
			var each = node.Value;

			if ( each.owner == null ) {

				actionList.Remove( node );

			} else {

				each.action( arg1 );
			}

			node = next;

		}

	}

	public void Register( Object owner, Action<T> action ) {

		actionList.AddLast( new EventSubscriber {
			action = action,
			owner = owner
		} );

	}

	public void Unregister( Action<T> action ) {

		for ( var node = actionList.First; node != null; node = node.Next ) {

			if ( node.Value.action == action ) {

				actionList.Remove( node );
				break;

			}

		}

	}

	public void UnregisterAll() {

		actionList.Clear();

	}

}

public class AutoReleaseEvent<T1, T2> {

	private struct EventSubscriber {

		public Action<T1, T2> action;
		public Object owner;

	}

	private LinkedList<EventSubscriber> actionList = new LinkedList<EventSubscriber>();

	public void Call( T1 arg1, T2 arg2 ) {

		for ( var node = actionList.First; node != null; ) {

			var next = node.Next;
			var each = node.Value;

			if ( each.owner == null ) {

				actionList.Remove( node );

			} else {

				each.action( arg1, arg2 );
			}

			node = next;

		}

	}

	public void Register( Object owner, Action<T1, T2> action ) {

		actionList.AddLast( new EventSubscriber {
			action = action,
			owner = owner
		} );

	}

	public void Unregister( Action<T1, T2> action ) {

		for ( var node = actionList.First; node != null; node = node.Next ) {

			if ( node.Value.action == action ) {

				actionList.Remove( node );
				break;

			}

		}

	}

	public void UnregisterAll() {

		actionList.Clear();

	}

}

public class AutoReleaseEvent<T1, T2, T3> {

	private struct EventSubscriber {

		public Action<T1, T2, T3> action;
		public Object owner;

	}

	private LinkedList<EventSubscriber> actionList = new LinkedList<EventSubscriber>();

	public void Call( T1 arg1, T2 arg2, T3 arg3 ) {

		for ( var node = actionList.First; node != null; ) {

			var next = node.Next;
			var each = node.Value;

			if ( each.owner == null ) {

				actionList.Remove( node );

			} else {

				each.action( arg1, arg2, arg3 );
			}

			node = next;

		}

	}

	public void Register( Object owner, Action<T1, T2, T3> action ) {

		actionList.AddLast( new EventSubscriber() {
			action = action,
			owner = owner
		} );

	}

	public void Unregister( Action<T1, T2, T3> action ) {

		for ( var node = actionList.First; node != null; node = node.Next ) {

			if ( node.Value.action == action ) {

				actionList.Remove( node );
				break;

			}

		}

	}

	public void UnregisterAll() {

		actionList.Clear();

	}

}