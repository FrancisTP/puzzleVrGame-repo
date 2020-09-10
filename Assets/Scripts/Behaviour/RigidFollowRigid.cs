using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowRigid : MonoBehaviour {

    public Transform parent; //  object we want to follow

    private Vector3 parentPositionOffset = Vector3.zero; // this is the offset of the parent, to keep correct positioning

    private Rigidbody thisRigidbody = null;

    public float distance = 0.0f;

    private Vector3PIDController pidController = null;

    public float pFactor = 60;
    public float iFactor = 0.1f;
    public float dFactor = 0.0f;

    private float biggestDistance = 0.0f;

    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    // Start is called before the first frame update
    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>();

        parentPositionOffset = new Vector3(parent.position.x - transform.position.x, parent.position.y - transform.position.y, parent.position.z - transform.position.z);

        pidController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    void FixedUpdate() {
        if (parent != null) {
            float currentDist = Vector3.Distance((parent.position - parentPositionOffset), this.transform.position);

            if (currentDist > distance) {
                Vector3 newVector = pidController.updatePID((parent.position - parentPositionOffset), this.transform.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

                Vector3 newVelocity = newVector;

                if (!followX) {
                    newVelocity.x = this.thisRigidbody.velocity.x;
                }
                if (!followY) {
                    newVelocity.y = this.thisRigidbody.velocity.y;
                }
                if (!followZ) {
                    newVelocity.z = this.thisRigidbody.velocity.z;
                }

                thisRigidbody.velocity = newVelocity;
            }


            Debug.Log("currentDist distance: " + currentDist);
        }
    }
}
