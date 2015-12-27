using UnityEngine;
using System.Collections;

public class ScreenAnimation : AObject, IEnumerable {

    [SerializeField]
    protected Screen screen = null;

    private void Reset() {

        GetComponent( out screen );
    }

    public virtual IEnumerable GetAnimation( float? overrideDuration = null ) {

        return new Object[0];
    }

    public virtual void Cleanup() {

    }

    public IEnumerator GetEnumerator() {

        return GetAnimation().GetEnumerator();
    }

}