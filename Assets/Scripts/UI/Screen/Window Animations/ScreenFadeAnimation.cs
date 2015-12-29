using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFadeAnimation : ScreenAnimation {

    public float duration = 0.4f;

    public bool isReverse = false;

    [SerializeField]
    private Graphic[] _graphics;

    private void Reset() {

        this.GetComponent( out screen );
        this.GetComponentsInChildren( out _graphics );
    }

    private IEnumerable LerpValue( System.Action<float> setter, float from, float to, float duration ) {

        var timer = 0f;

        setter( from );

        yield return null;

        while ( timer < duration ) {

            setter( Mathf.Lerp( from, to, timer / duration ) );

            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        setter( to );
    }

    public override IEnumerable GetAnimation( float? overrideDuration ) {

        var targetDuration = overrideDuration ?? duration;

	    var animationMonad = new PMonad();
		//var initialAlphaValues = _graphics.Select(_ => _.color.a).ToArray();

		//System.Action<float> updateAlphaValues = opacityValue =>
		//{
		//	for (var i = 0; i < _graphics.Length; ++i)
		//	{
			    
		//	}
		//};

		//foreach (var each in _graphics)
		//{
		//	var targetValue = each.color.a;
		//	var target = each;
		//	animationMonad.Add( LerpValue( opacityValue => target.SetColor( a: opacityValue ), isReverse ? targetValue : 0f, isReverse ? 0f : targetValue, targetDuration ) );
		//}

	    var coroutines = _graphics.Select(each =>
	    {
		    var targetValue = each.color.a;
		    var target = each;

		    return LerpValue(opacityValue => target.SetColor(a: opacityValue), isReverse ? targetValue : 0f,
			    isReverse ? 0f : targetValue, targetDuration).GetEnumerator();
	    }).ToArray();

	    System.Action updateAlphaValues = () =>
	    {
			foreach ( var each in coroutines)
			{
				each.MoveNext();
			}
	    };

		return new PMonad().Add( UpdateTimed( updateAlphaValues, targetDuration ) ).ToEnumerable();
    }

	private IEnumerable UpdateTimed(System.Action action, float duration)
	{
		var timer = 0f;
		while ((timer += Time.unscaledDeltaTime) < duration)
		{
			action();

			yield return null;
		}

		action();
	}
}