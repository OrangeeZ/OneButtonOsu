using System.Monads;
using UnityEngine;
using System.Collections;

public class ScreenPositionAnimation : ScreenAnimation {

    public Vector2 from;
    public Vector2 to;

    public AnimationCurve timeCurve;

    public bool invert = false;

    public float duration = 0.2f;

    [SerializeField]
    private RectTransform _target;

    private void Reset() {

        this.GetComponent( out _target );
    }

    private IEnumerable LerpValue( System.Action<Vector3> Setter, Vector3 from, Vector3 to, float duration ) {

        var timer = 0f;

        Setter( from );

        yield return null;

        while ( timer < duration ) {

            Setter( Vector3.Lerp( from, to, timeCurve.Evaluate( timer / duration ) ) );

            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        Setter( to );
    }

    public override IEnumerable GetAnimation( float? overrideDuration ) {

        var targetDuration = overrideDuration == null ? this.duration : overrideDuration.Value;

        var screentRect = new Vector2( UnityEngine.Screen.width, UnityEngine.Screen.height );

        var extents = _target.rect.size * 0.5f;

        var fromScreen = Vector2.Scale( screentRect, from ) - extents;
        var toScreen = Vector2.Scale( screentRect, to ) - extents;

        return LerpValue( value => _target.position = value, invert ? toScreen : fromScreen, invert ? fromScreen : toScreen, targetDuration );
    }

}