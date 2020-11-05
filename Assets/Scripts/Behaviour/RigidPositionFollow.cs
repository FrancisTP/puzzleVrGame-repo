using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RigidPositionFollow : MonoBehaviour {

    public Transform parent; //  object we want to follow

    private Vector3 parentLocalOffset = Vector3.zero; // this is the offset of the parent, to keep correct positioning

    private Rigidbody thisRigidbody = null;
    public int strenght = 15;
    private Vector3PIDController vector3PIDController = null;

    public float pFactor = 60;
    public float iFactor = 0.05f;
    public float dFactor = 1f;

    private float biggestDistance = 0.0f;

    // Start is called before the first frame update
    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>();
        parentLocalOffset = parent.InverseTransformPoint(thisRigidbody.position);
        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    void FixedUpdate() {
        if (parent != null) {
            Vector3 pidVector = parent.TransformPoint(parentLocalOffset);
            Vector3 newVelocity = vector3PIDController.updatePid(pidVector, thisRigidbody.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

            thisRigidbody.AddForce(newVelocity, ForceMode.VelocityChange);
        }
    }
}
