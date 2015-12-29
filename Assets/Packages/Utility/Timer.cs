using UnityEngine;
using System.Collections;

public class StepTimer {

    public float Value {
        get { return timerValue; }
    }

    public float ValueNormalized {
        get { return timerValue / duration; }
    }

    private float timerValue;

    private readonly float duration;

    public StepTimer( float duration ) {

        this.duration = duration;
    }

    public void Step() {

        Step( Time.deltaTime );
    }

    public void Step( float deltaTime ) {

        timerValue = ( timerValue + deltaTime ).Clamped( 0, duration );
    }

}

public class AutoTimer {

    public float Value {
        get { return Time.time - startTime; }
    }

    public float ValueNormalized {
        get { return ( Value / duration ).Clamped( 0, 1 ); }
    }

    private readonly float duration;
    private float startTime;

    public AutoTimer( float duration ) {

        this.duration = duration;
        startTime = Time.time;
    }

    public void Reset() {

        startTime = Time.time;
    }

}