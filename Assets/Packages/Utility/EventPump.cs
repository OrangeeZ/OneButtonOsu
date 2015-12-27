using UnityEngine;
using System.Collections.Generic;

public class EventPump : MonoBehaviour {

	public enum EventType {
	}

	private Queue<EventType> events = new Queue<EventType>();

	public void Call( EventType eventType ) {

		events.Enqueue( eventType );
	}

	private void ProcessEvents() {

		while ( events.Count > 0 ) {

			events.Dequeue();
		}
	}
}
