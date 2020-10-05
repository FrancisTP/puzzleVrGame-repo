using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRotateFollow : MonoBehaviour {

    private Rigidbody thisRigidbody = null;
    public Transform parent; //  object we want to match rotation

    private Vector3 parentRotationOffset = Vector3.zero;
    private Vector3PIDController vector3PIDController = null;

    public float pFactor = 60;
    public float iFactor = 0.05f;
    public float dFactor = 1f;

    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>();

        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Quaternion thisRotation = transform.rotation;
        Quaternion targetRotation = parent.rotation;
        Quaternion forceToApply = targetRotation * Quaternion.Inverse(thisRotation);

        float pidX = forceToApply.x;
        float pidY = forceToApply.y;
        float pidZ = forceToApply.z;

        Vector3 pidVector = new Vector3(pidX, pidY, pidZ);
        Vector3 newForce = vector3PIDController.updatePid(pidVector, transform.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

        thisRigidbody.AddTorque(newForce.x, newForce.y, newForce.z, ForceMode.Force);
    }
}
