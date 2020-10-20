using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsDirectGrab : MonoBehaviour {

    // Snap grab settings
    public bool snapGrab = false;
    private Transform snapPosition;

    private Transform hand = null; // player hand (left ir right)

    private Rigidbody thisRigidbody = null;

    private Vector3PIDController vector3PIDController = null;
    private float pFactor = 60;
    private float iFactor = 0.05f;
    private float dFactor = 1f;

    private bool handHovering = false;

    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>();
        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    private void OnSelect() {

    }

    // Start is called before the first frame update
    private void UpdatePosition() {

    }

    private void UpdateRotation() {

    }

    private void OnRelease() {

    }


    // Automatic triggers
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Hand") {
            handHovering = true;
            hand = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Hand") {
            handHovering = false;
            hand = null;
        }
    }

}
