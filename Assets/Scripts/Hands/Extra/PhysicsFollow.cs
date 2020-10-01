using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsFollow : MonoBehaviour {

    public Transform parent; // this is the object we want to follow

    private Vector3 worldPositionOffset = Vector3.zero; // this is the offset of the parent, to keep correct positioning
    private Quaternion worldRotationOffset = Quaternion.identity; // same as worldPositionOffset but for angles

    // for simplicity 
    private Rigidbody thisRigidbody = null;
    public int maxNewtonForces = 300;
    public float maxOffset = 1.0f;

    public float speed = 25f;



    // Start is called before the first frame update
    void Start() {

        thisRigidbody = this.GetComponent<Rigidbody>();

        if (parent != null) {
            worldPositionOffset = getOffsetBetweenChildAndParent(this.transform.position, parent.transform.position);
            worldRotationOffset = getOffsetBetweenChildAndParent(this.transform.rotation, parent.transform.rotation);
        }
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate() {
        if (parent != null) {
            float currentDist = Vector3.Distance((parent.transform.position + worldPositionOffset), this.transform.position);

            thisRigidbody.AddForce(((parent.transform.position + worldPositionOffset) - transform.position) * speed);
            /*
            addTorqueToMatchParentRotation();
            /*
            if (currentDist < 0.05f) {
                // changing velocity still allows the forces and physics to be calculated correctly
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(0.25));
            } else if (currentDist < 0.075f) {
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(0.45));
            } else if (currentDist < 0.15f) {
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(1.75));
            } else if (currentDist < 0.35f) {
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(4.0));
            } else if (currentDist < 0.5f || true) {
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(9.0));
            } else if (currentDist < 1.0f) {
                thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(Mathf.Pow(currentDist, 3) * 5));
            } else {
                // far away, move physics hand to where it needs to be ignoring forces (teleport)
                this.transform.position = parent.transform.position - worldPositionOffset;
            }
            */
        }
    }




    // Private methods
    private Vector3 getOffsetBetweenChildAndParent(Vector3 child, Vector3 parent) {
        Vector3 offset = Vector3.zero;
        float xOffset = 0.0f;
        float yOffset = 0.0f;
        float zOffset = 0.0f;

        // get x offset
        xOffset = parent.x - child.x;
        yOffset = parent.y - child.y;
        zOffset = parent.z - child.z;

        // update new offset Vector3
        offset = new Vector3(xOffset, yOffset, zOffset);

        return offset;
    }


    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        Quaternion offset = Quaternion.identity;

        offset = parent * Quaternion.Inverse(child);

        return offset;
    }

    private void addTorqueToMatchParentRotation() {
        Quaternion AngleDifference = Quaternion.FromToRotation(this.transform.up, parent.transform.up);

        float AngleToCorrect = Quaternion.Angle(parent.transform.rotation * worldRotationOffset, this.transform.rotation);
        Vector3 Perpendicular = Vector3.Cross(parent.transform.up, parent.transform.forward);
        if (Vector3.Dot(this.transform.forward, Perpendicular) < 0)
            AngleToCorrect *= -1;
        Quaternion Correction = Quaternion.AngleAxis(AngleToCorrect, transform.up);

        Vector3 MainRotation = RectifyAngleDifference((AngleDifference).eulerAngles);
        Vector3 CorrectiveRotation = RectifyAngleDifference((Correction).eulerAngles);
        thisRigidbody.AddTorque((MainRotation - CorrectiveRotation / 2) - thisRigidbody.angularVelocity, ForceMode.VelocityChange);
    }

    private Vector3 RectifyAngleDifference(Vector3 angdiff) {
        if (angdiff.x > 180) angdiff.x -= 360;
        if (angdiff.y > 180) angdiff.y -= 360;
        if (angdiff.z > 180) angdiff.z -= 360;
        return angdiff;
    }
}
