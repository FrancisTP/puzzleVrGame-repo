using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRotateFollow : MonoBehaviour {

    public Transform parent; // this is the object we want to follow
    private Quaternion worldRotationOffset = Quaternion.identity; // same as worldPositionOffset but for angles

    private Rigidbody thisRigidbody = null;
    public int maxNewtonForces = 300;
    public float maxOffset = 1.0f;

    public float speed = 25f;

    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();

        if (parent != null) {
            worldRotationOffset = getOffsetBetweenChildAndParent(transform.rotation, parent.transform.rotation);
        }

    }

    // Update is called once per frame
    void FixedUpdate() {
        addTorqueToMatchParentRotation();
    }


    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        Quaternion offset = Quaternion.identity;

        offset = parent * Quaternion.Inverse(child);

        return offset;
    }

    private void addTorqueToMatchParentRotation() {
        Quaternion AngleDifference = Quaternion.FromToRotation(transform.up, parent.transform.up);

        float AngleToCorrect = Quaternion.Angle(parent.transform.rotation * worldRotationOffset, transform.rotation);
        Vector3 Perpendicular = Vector3.Cross(parent.transform.up, parent.transform.forward);
        if (Vector3.Dot(transform.forward, Perpendicular) < 0)
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
