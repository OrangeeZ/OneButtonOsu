using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Beatmap : MonoBehaviour {

	public System.Action<Beatmap, Beat> BeatAvailable = delegate { };
	public System.Action<Beatmap, Beat, float> BeatCompleted = delegate { };
	public System.Action<Beatmap, Beat> BeatFailed = delegate { };

	public AudioSource associatedSound;

	[System.Serializable]
	public struct Beat {

		public float time;

		public bool isLong;

		public override string ToString() {

			return string.Format( "({0} {1})", time, isLong );
		}

		//public float duration;
	}

	public float shorBeatDuration = 0.3f;

	public float longBeatDuration = 0.9f;

	public Beat[] beatmap;

	public bool isEnabled = false;

	public float PlayTimer { get; private set; }

	public float Timer { get; private set; }

	public float EndTime {
		get { return beatmap.Last().time; }
	}

	private Queue<Beat> beatQueue = new Queue<Beat>();

	private float pressTime;
	private float releaseTime;

	private void OnEnable() {

		beatQueue = new Queue<Beat>( beatmap );
	}

	public void Play() {

		isEnabled = true;

		associatedSound.Play();

		Timer = 0f;
	}

	public void Stop() {

		isEnabled = false;

		associatedSound.Stop();

	}

	public void OnDoublePress() {

		Debug.Log( "Double: " + PlayTimer * 1000f );

		if ( !isEnabled ) {

			return;
		}

		pressTime = Timer;

		if ( !beatQueue.Any() ) {

			return;
		}

		var beat = beatQueue.Peek();

		if ( Timer >= beat.time && Timer <= GetBeatEndTime( beat ) && beat.isLong ) {

			var percentHit = 1f - ( Timer - beat.time ) / longBeatDuration;
			BeatCompleted( this, beatQueue.Dequeue(), percentHit );
		} else {

			Debug.LogWarning( GetBeatEndTime( beat ) + " failed at " + Timer );

			BeatFailed( this, beatQueue.Dequeue() );
		}
	}

	public void OnPress() {

		Debug.Log( "Single: " + PlayTimer * 1000f );

		if ( !isEnabled ) {

			return;
		}

		pressTime = Timer;

		if ( !beatQueue.Any() ) {

			return;
		}

		var beat = beatQueue.Peek();

		if ( Timer >= beat.time && Timer <= GetBeatEndTime( beat ) ) {

			var percentHit = 1f - ( Timer - beat.time ) / shorBeatDuration;
			BeatCompleted( this, beatQueue.Dequeue(), percentHit );
		} else {

			Debug.LogWarning( GetBeatEndTime( beat ) + " failed at " + Timer );
			BeatFailed( this, beatQueue.Dequeue() );
		}
	}

	public void OnRelease() {

		if ( !isEnabled ) {

			return;
		}

		releaseTime = Timer;

		if ( !beatQueue.Any() ) {

			return;
		}

		//var beat = beatQueue.Peek();
		//if ( beat.isLong ) {

		//	if ( pressTime >= beat.time && pressTime <= GetBeatEndTime( beat ) ) {

		//		if ( releaseTime >= GetBeatEndTime( beat ) ) {

		//			BeatCompleted( this, beatQueue.Dequeue() );
		//		} else {

		//			BeatFailed( this, beatQueue.Dequeue() );
		//		}
		//	}
		//}
	}

	public IEnumerable<Beat> GetBeats( float beatWindow ) {

		return beatQueue.Where( each => each.time - Timer <= beatWindow );
	}

	public float GetTimer() {

		return Timer;
	}

	void LateUpdate() {

		PlayTimer += Time.deltaTime;

		if ( !isEnabled || !beatQueue.Any() ) {

			return;
		}

		Timer += Time.deltaTime;

		var beat = beatQueue.Peek();

		if ( Timer >= GetBeatEndTime( beat ) ) {

			//	if ( pressTime >= beat.time && pressTime <= GetBeatEndTime( beat ) ) {

			//		if ( beat.isLong ) {

			//			BeatCompleted( this, beatQueue.Dequeue() );
			//		}
			//	} else {
			
			//Debug.LogWarning( GetBeatEndTime( beat ) + " failed at " + Timer );

			BeatFailed( this, beatQueue.Dequeue() );
			//	}
		}
	}

	public float GetBeatEndTime( Beat beat ) {

		return beat.time + ( beat.isLong ? longBeatDuration : shorBeatDuration );
	}

	public float GetDuration() {

		return GetBeatEndTime( beatmap.Last() );
	}

	public float GetRateFinished() {

		return PlayTimer / GetDuration();
	}
}