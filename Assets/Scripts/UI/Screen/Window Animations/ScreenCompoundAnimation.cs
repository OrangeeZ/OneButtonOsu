using System.Linq;
using UnityEngine;
using System.Collections;

public class ScreenCompoundAnimation : ScreenAnimation {

	public float duration = 0.5f;

	public ScreenAnimation[] animations = null;

	public override IEnumerable GetAnimation( float? overrideDuration = null ) {

		var targetDuration = overrideDuration ?? new float?( duration );

		var animationBlocks = animations.Select( _ => _.GetAnimation( targetDuration ).GetEnumerator() ).ToArray();

		while ( animationBlocks.Aggregate( false, ( current, each ) => each.MoveNext() || current ) ) {

			yield return null;
		}
	}

	public override void Cleanup() {

		foreach ( var each in animations ) {

			each.Cleanup();
		}
	}
}
