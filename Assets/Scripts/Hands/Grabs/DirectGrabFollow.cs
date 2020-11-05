using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectGrabFollow {

    public Transform hand;
    private Vector3 handLocalOffset = Vector3.zero;

    private Rigidbody thisRigidbody = null;
    private Vector3PIDController vector3PIDController = null;

    public float pFactor = 60;
    public float iFactor = 0.05f;
    public float dFactor = 1f;

    public DirectGrabFollow(Rigidbody rigidbody, Transform hand) {
        this.thisRigidbody = rigidbody;
        this.hand = hand;
        handLocalOffset = hand.InverseTransformPoint(thisRigidbody.position);
        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    // Update is called once per frame
    public void UpdateFollow() {
        if (hand != null) {
            Vector3 pidVector = hand.TransformPoint(handLocalOffset);
            Vector3 newVelocity = vector3PIDController.updatePid(pidVector, thisRigidbody.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

            thisRigidbody.AddForce(newVelocity, ForceMode.VelocityChange);

            Debug.Log("Trying to update object");
        }
    }
}
