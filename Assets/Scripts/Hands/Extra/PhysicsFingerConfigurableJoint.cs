using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PhysicsFingerConfigurableJoint : MonoBehaviour {

    [SerializeField]
    public List<PhysicsFingerConfigurableJoint> childJoints = new List<PhysicsFingerConfigurableJoint>();

    private Rigidbody thisRigidbody;
    private ConfigurableJoint configurableJoint;
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
        configurableJoint = GetComponent<ConfigurableJoint>();
        collider = GetComponent<Collider>();
    }

    void FixedUpdate() {
        if (!stopJoint) {
            bool childStopped = false;
            foreach (PhysicsFingerConfigurableJoint physicsFinger in childJoints) {
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

        float targetAngle = configurableJoint.highAngularXLimit.limit;
        float newTargetPosition = gripValue * targetAngle;

        Quaternion rotationTarget = GetComponent<ConfigurableJoint>().targetRotation;

        if (stopJoint) {
            rotationTarget = Quaternion.Euler(targetStoppedValue, 0, 0);
            previousTargetValue = targetStoppedValue;
        } else {
            rotationTarget = Quaternion.Euler(newTargetPosition, 0, 0);
            previousTargetValue = newTargetPosition;
        }

        GetComponent<ConfigurableJoint>().targetRotation = rotationTarget;

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
