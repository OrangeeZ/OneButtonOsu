using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class PMonadExecutor : MonoBehaviour {

	private static PMonadExecutor self = null;
	private LinkedList<PMonad> pMonads = new LinkedList<PMonad>();

	public static void Add( PMonad pMonad ) {

		if ( self == null ) {

			self = new GameObject( typeof ( PMonadExecutor ).Name ).AddComponent<PMonadExecutor>();
		}

		self.pMonads.AddLast( pMonad );
	}

	public static void Remove( PMonad pMonad ) {

		self.pMonads.Remove( pMonad );
	}

	void Update() {

		for ( var node = pMonads.First; node != null; ) {

			var next = node.Next;

			if ( !node.Value.Step() && node.List != null ) {

				pMonads.Remove( node );
			}

			node = next;
		}
	}
}
