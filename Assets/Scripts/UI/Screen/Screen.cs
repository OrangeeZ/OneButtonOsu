using System;
using Object = UnityEngine.Object;

public class Screen : AObject {

    public static event Action<Screen> WillAppear = delegate { };
    public static event Action<Screen> DidAppear = delegate { };
    public static event Action<Screen> WillDisappear = delegate { };
    public static event Action<Screen> DidDisappear = delegate { };

    public bool isPopup = false;

    public ScreenEventListener[] listeners = null;

    public bool isVisible { get; protected set; }

    private Action _onHide = null;

    protected void NotifyWillAppear() {

        ScreenManager.SetActive( this );

        foreach ( var each in listeners ) {

            each.OnWillAppear();
        }

        WillAppear( this );
    }

    protected void NotifyDidAppear() {

        foreach ( var each in listeners ) {

            each.OnDidAppear();
        }

        DidAppear( this );
    }

    protected void NotifyWillDisappear() {

        foreach ( var each in listeners ) {

            each.OnWillDisappear();
        }

        WillDisappear( this );
    }

    protected void NotifyDidDisappear() {

        foreach ( var each in listeners ) {

            each.OnDidDisappear();
        }

        if ( _onHide != null ) {

            _onHide();
        }

        DidDisappear( this );
    }

    public void Show( Action OnHide ) {

        this._onHide = OnHide;
        this.Show();
    }

    public virtual void Show() {

        ScreenManager.SetActive( this );

        isVisible = true;
    }

    public virtual void Hide() {

        if ( !isVisible ) {

            return;
        }

        isVisible = false;
    }

    public virtual void HideImmediate() {

        isVisible = false;
    }

}