using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRotateFollowOld : MonoBehaviour {

    public Transform parent; // this is the object we want to follow
    private Quaternion worldRotationOffset = Quaternion.identity; // same as worldPositionOffset but for angles

    private Rigidbody thisRigidbody = null;
    public int maxNewtonForces = 300;
    public float maxOffset = 1.0f;

    public float speed = 25f;

    private Vector3PIDController vector3PIDController = null;
    public float pFactor = 5f;
    public float iFactor = 0f;
    public float dFactor = 0f;

    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();

        if (parent != null) {
            worldRotationOffset = getOffsetBetweenChildAndParent(transform.rotation, parent.transform.rotation);
        }

        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    // Update is called once per frame
    void FixedUpdate() {
        addTorqueToMatchParentRotation();
    }


    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        Quaternion offset = Quaternion.identity;

        offset = child * Quaternion.Inverse(parent);

        return offset;
    }

    private void addTorqueToMatchParentRotation() {
        Quaternion AngleDifference = Quaternion.FromToRotation(transform.up, parent.transform.up);

        float AngleToCorrect = Quaternion.Angle(worldRotationOffset * parent.transform.rotation, transform.rotation);
        Vector3 Perpendicular = Vector3.Cross(parent.transform.up, parent.transform.forward);
        if (Vector3.Dot(transform.forward, Perpendicular) < 0)
            AngleToCorrect *= -1;
        Quaternion Correction = Quaternion.AngleAxis(AngleToCorrect, transform.up);

        Vector3 MainRotation = RectifyAngleDifference((AngleDifference).eulerAngles);
        Vector3 CorrectiveRotation = RectifyAngleDifference((Correction).eulerAngles);

        Vector3 torque = (MainRotation - CorrectiveRotation / 2) - thisRigidbody.angularVelocity;
        Vector3 newTorque = vector3PIDController.updatePid(torque, thisRigidbody.GetPointVelocity(transform.position), Time.fixedDeltaTime, pFactor, iFactor, dFactor);

        thisRigidbody.AddTorque(newTorque, ForceMode.VelocityChange);
    }

    private Vector3 RectifyAngleDifference(Vector3 angdiff) {
        if (angdiff.x > 180) angdiff.x -= 360;
        if (angdiff.y > 180) angdiff.y -= 360;
        if (angdiff.z > 180) angdiff.z -= 360;
        return angdiff;
    }
}
