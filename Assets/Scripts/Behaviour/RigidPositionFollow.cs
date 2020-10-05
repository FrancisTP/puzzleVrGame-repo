using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidPositionFollow : MonoBehaviour {

    public Transform parent; //  object we want to follow

    private Vector3 parentPositionOffset = Vector3.zero; // this is the offset of the parent, to keep correct positioning

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

        parentPositionOffset = new Vector3(parent.position.x - transform.position.x, parent.position.y - transform.position.y, parent.position.z - transform.position.z);

        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    void FixedUpdate() {
        if (parent != null) {
            //float currentDist = Vector3.Distance((parent.transform.position + parentPositionOffset), this.transform.position);

            //
            float pidX = parent.position.x - parentPositionOffset.x;
            float pidY = parent.position.y - parentPositionOffset.y;
            float pidZ = parent.position.z - parentPositionOffset.z;

            Vector3 pidVector = new Vector3(pidX, pidY, pidZ);
            Vector3 newVelocity = vector3PIDController.updatePid(pidVector, transform.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

            thisRigidbody.AddForce(newVelocity, ForceMode.VelocityChange);


            // debug
           // if (((parent.transform.position - parentPositionOffset) - this.transform.position).magnitude > biggestDistance && ((parent.transform.position - parentPositionOffset) - this.transform.position).magnitude < 1.5f) {
                //biggestDistance = ((parent.position - parentPositionOffset) - this.transform.position).magnitude;
                //Debug.Log("Biggest distance: " + biggestDistance);
           // }
        }
    }
}
