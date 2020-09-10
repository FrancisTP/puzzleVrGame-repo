using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowRigid : MonoBehaviour {

    public Transform parent; //  object we want to follow

    private Vector3 parentPositionOffset = Vector3.zero; // this is the offset of the parent, to keep correct positioning

    private Rigidbody thisRigidbody = null;
    public int strenght = 15;
    private PIDController pidControllerX = null;
    private PIDController pidControllerY = null;
    private PIDController pidControllerZ = null;

    public float pFactor = 60;
    public float iFactor = 0.05f;
    public float dFactor = 1f;

    private float biggestDistance = 0.0f;

    // Start is called before the first frame update
    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>();

        parentPositionOffset = new Vector3(parent.position.x - transform.position.x, parent.position.y - transform.position.y, parent.position.z - transform.position.z);

        pidControllerX = new PIDController(pFactor, iFactor, dFactor);
        pidControllerY = new PIDController(pFactor, iFactor, dFactor);
        pidControllerZ = new PIDController(pFactor, iFactor, dFactor);
    }

    void FixedUpdate() {
        if (parent != null) {
            //float currentDist = Vector3.Distance((parent.transform.position + parentPositionOffset), this.transform.position);

            //
            float pidX = pidControllerX.updatePid((parent.position.x - parentPositionOffset.x), this.transform.position.x, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
            float pidY = pidControllerY.updatePid((parent.position.y - parentPositionOffset.y), this.transform.position.y, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
            float pidZ = pidControllerZ.updatePid((parent.position.z - parentPositionOffset.z), this.transform.position.z, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
            Vector3 newVector = new Vector3(pidX, pidY, pidZ);

            Vector3 newVelocity = newVector;//.normalized * ((float)(strenght));
            newVelocity.y = this.thisRigidbody.velocity.y;

            thisRigidbody.velocity = newVelocity;


            // debug
           // if (((parent.transform.position - parentPositionOffset) - this.transform.position).magnitude > biggestDistance && ((parent.transform.position - parentPositionOffset) - this.transform.position).magnitude < 1.5f) {
                biggestDistance = ((parent.position - parentPositionOffset) - this.transform.position).magnitude;
                Debug.Log("Biggest distance: " + biggestDistance);
           // }
        }
    }
}
