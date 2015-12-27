using UnityEngine;
using System.Collections;
using System.Linq;

public class UIScreen : Screen {

    [SerializeField]
    private ScreenAnimation _inAnimation;

    [SerializeField]
    private ScreenAnimation _outAnimation;

    public override void Show() {

        base.Show();

        new PMonad().Add( NotifyWillAppear )
                    .Add( _inAnimation == null ? Enumerable.Empty<object>() : _inAnimation.GetAnimation() )
                    .Add( NotifyDidAppear )
                    .Execute();

        gameObject.SetActive( true );
    }

    public override void Hide() {

        base.Hide();

        new PMonad().Add( NotifyWillDisappear )
                    .Add( _inAnimation == null ? Enumerable.Empty<object>() : _outAnimation.GetAnimation() )
                    .Add( NotifyDidDisappear )
                    .Execute();

        gameObject.SetActive( false );
    }

    public override void HideImmediate() {

        base.HideImmediate();

        gameObject.SetActive( false );
    }

}