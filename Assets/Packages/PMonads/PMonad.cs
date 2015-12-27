using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Object = System.Object;

public partial class PMonad {

	private class ImmediateBlock : IEnumerable<object>, IEnumerator<object> {

		public Action action = null;

		public bool MoveNext() {

			action();

			return false;
		}

		public void Reset() {
		}

		public object Current { get; private set; }

		public IEnumerator<object> GetEnumerator() {

			return this;
		}

		IEnumerator IEnumerable.GetEnumerator() {

			return GetEnumerator();
		}

		public void Dispose() {

			action = null;
		}
	}

	private readonly LinkedList<IEnumerable<object>> actions = new LinkedList<IEnumerable<object>>();

	private IEnumerator enumerator = null;

	public PMonad Add<T>( Action<T> action, T arg1 ) {

		Add( () => action( arg1 ) );

		return this;
	}

	public PMonad Add( Action action ) {

		actions.AddLast( new ImmediateBlock { action = action } );

		Reset();

		return this;
	}

	public PMonad Add<T>( IEnumerable<T> coroutine ) {

		actions.AddLast( coroutine.Cast<object>() );

		Reset();

		return this;
	}

	public PMonad Add( IEnumerable coroutine ) {

		if ( coroutine == null ) {

			return this;
		}

		actions.AddLast( coroutine.Cast<object>() );

		Reset();

		return this;
	}

	public PMonadPartial AddVar() {

		var newNode = new LinkedListNode<IEnumerable<object>>( null );

		actions.AddLast( newNode );

		return new PMonadPartial( newNode, owner: this );
	}

	public void Reset() {

		enumerator = ToEnumerable().GetEnumerator();
	}

	public bool Step() {

		return enumerator == null ? false : enumerator.MoveNext();
	}

	public void Evaluate() {

		while ( enumerator.MoveNext() ) { }
	}

	public IEnumerable<object> ToEnumerable() {

		return actions.SelectMany( _ => _ );
	}

	public IEnumerator ToEnumerator() {

		return ToEnumerable().GetEnumerator();
	}
}

public partial class PMonad {

	private static IEnumerable TimedAction( Action action, float duration ) {

		var timer = duration;

		while ( timer > 0 ) {

			timer -= Time.unscaledDeltaTime;

			action();

			yield return null;
		}
	}

	public static IEnumerable Empty() {

		yield return null;
	}

	public PMonad AddTimed( Action action, float duration ) {

		return Add( TimedAction( action, duration ) );
	}
	
	public PMonad AddDelay( float duration ) {

		return AddTimed( () => { }, duration );
	}
	
	public void Execute() {

		PMonadExecutor.Add( this );
	}

	public void Stop() {
		
		PMonadExecutor.Remove( this );
	}
}

public class PMonadPartial {

	private readonly LinkedListNode<IEnumerable<object>> destinationNode;

	private readonly PMonad owner;

	public PMonadPartial( LinkedListNode<IEnumerable<object>> destinationNode, PMonad owner ) {

		this.destinationNode = destinationNode;
		this.owner = owner;
	}

	public PMonad ApplyFirst( PMonad p ) {

		destinationNode.Value = p.ToEnumerable();

		return owner;
	}
}
