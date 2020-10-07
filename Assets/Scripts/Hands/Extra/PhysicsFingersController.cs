using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;



public class PhysicsFingersController : MonoBehaviour {


    // the joints list have the lowest part of the finger at location [0], middle part at [1], and tip of the finger at [2]
    [SerializeField]
    List<HingeJoint> thumbJoints = null;

    [SerializeField]
    List<HingeJoint> indexJoints = null;

    [SerializeField]
    List<HingeJoint> middleJoints = null;

    [SerializeField]
    List<HingeJoint> ringJoints = null;

    [SerializeField]
    List<HingeJoint> pinkyJoints = null;

    [SerializeField]
    public XRNode controllerNode;
    [SerializeField]
    private InputDevice controller;

    private List<HingeJoint> allJoints = new List<HingeJoint>();

    void Start() {
        GetDevices();
        ConfigurableJoint con;
        foreach (HingeJoint hingeJoint in thumbJoints) {
            allJoints.Add(hingeJoint);
        }

        foreach (HingeJoint hingeJoint in indexJoints) {
            allJoints.Add(hingeJoint);
        }

        foreach (HingeJoint hingeJoint in middleJoints) {
            allJoints.Add(hingeJoint);
        }

        foreach (HingeJoint hingeJoint in ringJoints) {
            allJoints.Add(hingeJoint);
        }

        foreach (HingeJoint hingeJoint in pinkyJoints) {
            allJoints.Add(hingeJoint);
        }

        
        foreach (HingeJoint hingeJoint in allJoints) {
            
        }
    }

    private void GetDevices() {
        List<InputDevice> devices;
        devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }

        // Update is called once per frame
    void FixedUpdate() {
        if (controller == null) {
            GetDevices();
        }

        bool thumbRest = false;
        if (controller.TryGetFeatureValue(CommonUsages.primaryTouch, out thumbRest)) {
            if (thumbRest) {
                updateThumb(1.0f);
            } else {
                updateThumb(0.0f);
            }
        }

        float triggerValue = 0;
        if (controller.TryGetFeatureValue(CommonUsages.trigger, out triggerValue)) {
            updateIndex(triggerValue);
        }

        float gripValue = 0;
        if (controller.TryGetFeatureValue(CommonUsages.grip, out gripValue)) {
            updateMiddle(gripValue);
            updateRing(gripValue);
            updatePinky(gripValue);
        }

    }

    private void updateThumb(float gripValue) {
        foreach (HingeJoint hingeJoint in thumbJoints) {
            float targetAngle = hingeJoint.limits.max;
            float newTargetPosition = gripValue * targetAngle;

            JointSpring hingeSpring = hingeJoint.spring;
            hingeSpring.targetPosition = newTargetPosition;

            hingeJoint.spring = hingeSpring;
        }
    }
    private void updateIndex(float gripValue) {
        foreach (HingeJoint hingeJoint in indexJoints) {
            float targetAngle = hingeJoint.limits.max;
            float newTargetPosition = gripValue * targetAngle;

            JointSpring hingeSpring = hingeJoint.spring;
            hingeSpring.targetPosition = newTargetPosition;

            hingeJoint.spring = hingeSpring;
        }
    }
    private void updateMiddle(float gripValue) {
        foreach (HingeJoint hingeJoint in middleJoints) {
            float targetAngle = hingeJoint.limits.max;
            float newTargetPosition = gripValue * targetAngle;

            JointSpring hingeSpring = hingeJoint.spring;
            hingeSpring.targetPosition = newTargetPosition;

            hingeJoint.spring = hingeSpring;
        }
    }

    private void updateRing(float gripValue) {
        foreach (HingeJoint hingeJoint in ringJoints) {
            float targetAngle = hingeJoint.limits.max;
            float newTargetPosition = gripValue * targetAngle;

            JointSpring hingeSpring = hingeJoint.spring;
            hingeSpring.targetPosition = newTargetPosition;

            hingeJoint.spring = hingeSpring;
        }
    }

    private void updatePinky(float gripValue) {
            foreach (HingeJoint hingeJoint in pinkyJoints) {
                float targetAngle = hingeJoint.limits.max;
                float newTargetPosition = gripValue * targetAngle;

                JointSpring hingeSpring = hingeJoint.spring;
                hingeSpring.targetPosition = newTargetPosition;

                hingeJoint.spring = hingeSpring;
            }
    }
}
