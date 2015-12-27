using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Beatmap : MonoBehaviour {

	public System.Action<Beatmap, Beat> BeatAvailable = delegate { };
	public System.Action<Beatmap, Beat> BeatCompleted = delegate { };
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

	public float EndTime { get { return beatmap.Last().time;} }

	private Queue<Beat> beatQueue = new Queue<Beat>();

	private float pressTime;
	private float releaseTime;

	void OnEnable() {

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

	public void OnPress() {

		if ( !isEnabled ) {

			return;
		}

		pressTime = Timer;

		if ( !beatQueue.Any() ) {

			return;
		}

		var beat = beatQueue.Peek();

		if ( Timer >= beat.time && Timer <= GetBeatEndTime( beat ) ) {

			if ( !beat.isLong ) {

				BeatCompleted( this, beatQueue.Dequeue() );
			}
		} else {

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

		var beat = beatQueue.Peek();
		if ( beat.isLong ) {

			if ( pressTime >= beat.time && pressTime <= GetBeatEndTime( beat ) ) {

				if ( releaseTime >= GetBeatEndTime( beat ) ) {

					BeatCompleted( this, beatQueue.Dequeue() );
				} else {

					BeatFailed( this, beatQueue.Dequeue() );
				}
			}
		}
	}

	public IEnumerable<Beat> GetBeats( float beatWindow ) {

		return beatQueue.Where( each => each.time - Timer <= beatWindow );
	}

	public float GetTimer() {

		return Timer;
	}

	void LateUpdate() {

		//PlayTimer += Time.deltaTime;

		if ( !isEnabled || !beatQueue.Any() ) {

			return;
		}

		Timer += Time.deltaTime;

		var beat = beatQueue.Peek();

		if ( Timer >= GetBeatEndTime( beat ) ) {

			if ( pressTime >= beat.time && pressTime <= GetBeatEndTime( beat ) ) {

				if ( beat.isLong ) {

					BeatCompleted( this, beatQueue.Dequeue() );
				}
			} else {

				BeatFailed( this, beatQueue.Dequeue() );
			}
		}
	}

	public float GetBeatEndTime( Beat beat ) {

		return beat.time + ( beat.isLong ? longBeatDuration : shorBeatDuration );
	}
}