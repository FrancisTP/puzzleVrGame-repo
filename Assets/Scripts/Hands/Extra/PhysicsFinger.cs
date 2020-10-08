using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
public class PhysicsFinger : MonoBehaviour {

    [SerializeField]
    public List<PhysicsFinger> childJoints = new List<PhysicsFinger>();

    private Rigidbody thisRigidbody;
    private HingeJoint hingeJoint;
    private Collider collider;

    [HideInInspector]
    public bool stopJoint = false;
    private bool gripping = false;
    private float gripStoppedValue = 0.0f;
    private float previousGripValue = 0.0f;
    private float previousTargetValue = 0.0f;
    private float targetStoppedValue = 0.0f;

    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();
        hingeJoint = GetComponent<HingeJoint>();
        collider = GetComponent<Collider>();
    }

    void FixedUpdate() {
        if (!stopJoint) {
            bool childStopped = false;
            foreach (PhysicsFinger physicsFinger in childJoints) {
                if (physicsFinger.stopJoint) {
                    setFingerStoppedValues();
                    childStopped = true;
                    break;
                }
            }

            if (!childStopped) {
                resetFingerStoppedValues();
            }
        }
    }

    public void updateHingeJoint(float gripValue) {

        if (gripValue > previousGripValue || gripValue > gripStoppedValue) {
            gripping = true;
        } else {
            gripping = false;
            resetFingerStoppedValues();
        }

        float targetAngle = hingeJoint.limits.max;
        float newTargetPosition = gripValue * targetAngle;

        JointSpring hingeSpring = hingeJoint.spring;

        if (stopJoint) {
            hingeSpring.targetPosition = targetStoppedValue;
            previousTargetValue = targetStoppedValue;
        } else {
            hingeSpring.targetPosition = newTargetPosition;
            previousTargetValue = newTargetPosition;
        }

        hingeJoint.spring = hingeSpring;

        previousGripValue = gripValue;
    }

    void OnCollisionEnter(Collision collision) {
        if (gripping) {
            setFingerStoppedValues();
        }
    }

    void OnCollisionExit(Collision collision) {
        resetFingerStoppedValues();
    }

    private void setFingerStoppedValues() {
        stopJoint = true;
        gripStoppedValue = previousGripValue;

        targetStoppedValue = previousTargetValue;
    }

    private void resetFingerStoppedValues() {
        stopJoint = false;
        gripStoppedValue = 0.0f;
        targetStoppedValue = 0.0f;
    }
}
