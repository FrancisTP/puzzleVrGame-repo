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
    private bool stopJoint = false;
    private bool gripping = false;
    private float gripStoppedValue = 0.0f;
    private float previousGripValue = 0.0f;
    private float previousTargetValue = 0.0f;
    private float targetStoppedValue = 0.0f;

    [HideInInspector]
    public bool stoppedChildJoint = false;

    private float gripThreshold = 0.01f;

    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();
        configurableJoint = GetComponent<ConfigurableJoint>();
        collider = GetComponent<Collider>();
    }

    void FixedUpdate() {
        /*
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
        */
    }

    public void updateHingeJoint(float gripValue) {

        if ((gripValue > previousGripValue || gripValue > gripStoppedValue) && gripValue > gripThreshold) {
            gripping = true;
        } else {
            gripping = false;
            resetFingerStoppedValues();
        }

        float targetAngle = configurableJoint.highAngularXLimit.limit;
        float newTargetPosition = gripValue * targetAngle;

        Quaternion rotationTargetQuaternion = GetComponent<ConfigurableJoint>().targetRotation;

        if (stopJoint || stoppedChildJoint) {
            rotationTargetQuaternion = Quaternion.Euler(targetStoppedValue, 0, 0);
            previousTargetValue = targetStoppedValue;
        } else {
            rotationTargetQuaternion = Quaternion.Euler(newTargetPosition, 0, 0);
            previousTargetValue = newTargetPosition;
        }

        GetComponent<ConfigurableJoint>().targetRotation = rotationTargetQuaternion;

        previousGripValue = gripValue;
    }

    /*
    void OnCollisionEnter(Collision collision) {
        if (gripping && !stopJoint && !stoppedChildJoint) {
            setFingerStoppedValues();

            // set children to stop as well
            foreach (PhysicsFingerConfigurableJoint physicsFinger in childJoints) {
                physicsFinger.setChildFingerStoppedValues();
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        resetFingerStoppedValues();

        foreach (PhysicsFingerConfigurableJoint physicsFinger in childJoints) {
            physicsFinger.resetChildFingerStoppedValues();
        }
    }
    */
    private void setFingerStoppedValues() {
        if (!stopJoint) {
            stopJoint = true;
            gripStoppedValue = previousGripValue;

            targetStoppedValue = previousTargetValue;
        }
    }

    private void resetFingerStoppedValues() {
        if (stopJoint) { 
            stopJoint = false;

            if (!stoppedChildJoint) {
                gripStoppedValue = 0.0f;
                targetStoppedValue = 0.0f;
            }
        }
    }

    public void setChildFingerStoppedValues() {
        if (!stoppedChildJoint && !stopJoint) {
            stoppedChildJoint = true;
            gripStoppedValue = previousGripValue;

            targetStoppedValue = previousTargetValue;
        }
    }

    public void resetChildFingerStoppedValues() {
        if (stoppedChildJoint) {
            stoppedChildJoint = false;

            if (!stopJoint) {
                gripStoppedValue = 0.0f;
                targetStoppedValue = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (gripping && !stopJoint && !stoppedChildJoint) {
            setFingerStoppedValues();

            /*
            // set children to stop as well
            foreach (PhysicsFingerConfigurableJoint physicsFinger in childJoints) {
                physicsFinger.setChildFingerStoppedValues();
            }
            */
        }
    }

    void OnTriggerExite(Collider collider) {
        resetFingerStoppedValues();

        /*
        foreach (PhysicsFingerConfigurableJoint physicsFinger in childJoints) {
            physicsFinger.resetChildFingerStoppedValues();
        }
        */
    }
}
