using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed = 3f;

    public Vector3 rotationAxis = Vector3.forward;

    private void Update() {

        transform.localRotation *= Quaternion.AngleAxis( speed * Time.deltaTime, rotationAxis);
    }

}