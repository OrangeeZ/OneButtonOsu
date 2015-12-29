using System.Collections.Generic;
using System.Linq;
using System.Monads;
using UnityEngine;

public class ScreenManager : AObject {

    public Screen[] screens = null;

    public Screen startScreen;

    private static ScreenManager _self = null;

    private readonly Stack<Screen> _windowStack = new Stack<Screen>();
    
    private void Awake() {

        _self = this;

		foreach ( var each in screens ) {

			each.HideImmediate();
		}

		if ( startScreen != null ) {

			startScreen.Show();
		}
    }
    
    public static Screen GetActive() {

        return _self._windowStack.FirstOrDefault();
    }

    public static void SetActive( Screen screen ) {

        if ( !screen.isPopup ) {

            _self._windowStack.Where( _ => _ != screen && _.isVisible ).MapImmediate( _ => _.Hide() );
        }

        _self._windowStack.Push( screen );
    }

    public static T GetScreen<T>() where T : Screen {

        return _self.screens.OfType<T>().FirstOrDefault();
    }

    public static Screen Back() {

        var result = default ( Screen );

        if ( !_self._windowStack.IsEmpty() ) {

            var previousWindow = _self._windowStack.Pop();
            previousWindow.Hide();

            if ( _self._windowStack.Any() && !previousWindow.isPopup ) {

                result = _self._windowStack.Pop();
            }
        }

        if ( result != null ) {

            result.Show();
        }

        return result;
    }

    public static Screen PeekBack() {

        return _self._windowStack.Skip( 1 ).FirstOrDefault();
    }

#if UNITY_EDITOR
    [ContextMenu( "Find screens" )]
    private void FindWindows() {

        screens = GetComponentsInChildren<Screen>().ToArray();
    }
#endif
}