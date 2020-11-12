using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Runtime.ConstrainedExecution;

public class PhysicsFingerConfigurableJointsController : MonoBehaviour {


    // the joints list have the lowest part of the finger at location [0], middle part at [1], and tip of the finger at [2]
    [SerializeField]
    List<GameObject> thumbJoints = null;

    [SerializeField]
    List<GameObject> indexJoints = null;

    [SerializeField]
    List<GameObject> middleJoints = null;

    [SerializeField]
    List<GameObject> ringJoints = null;

    [SerializeField]
    List<GameObject> pinkyJoints = null;

    [SerializeField]
    public XRNode controllerNode;
    [SerializeField]
    private InputDevice controller;

    private List<GameObject> allJoints = new List<GameObject>(); // list that will have all joint objects for a hand, if we need to apply something to alll will be easier

    void Start() {
        GetDevices();
        ConfigurableJoint con;
        foreach (GameObject fingerJointObject in thumbJoints) {
            allJoints.Add(fingerJointObject);
        }

        foreach (GameObject fingerJointObject in indexJoints) {
            allJoints.Add(fingerJointObject);
        }

        foreach (GameObject fingerJointObject in middleJoints) {
            allJoints.Add(fingerJointObject);
        }

        foreach (GameObject fingerJointObject in ringJoints) {
            allJoints.Add(fingerJointObject);
        }

        foreach (GameObject fingerJointObject in pinkyJoints) {
            allJoints.Add(fingerJointObject);
        }


        foreach (GameObject fingerJointObject in allJoints) {

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
        foreach (GameObject thumbJointObject in thumbJoints) {
            PhysicsFingerConfigurableJoint thumbJoint = thumbJointObject.GetComponentInChildren<PhysicsFingerConfigurableJoint>();
            thumbJoint.updateHingeJoint(gripValue);
        }
    }
    private void updateIndex(float gripValue) {
        foreach (GameObject indexJointObject in indexJoints) {
            PhysicsFingerConfigurableJoint indexJoint = indexJointObject.GetComponentInChildren<PhysicsFingerConfigurableJoint>();
            indexJoint.updateHingeJoint(gripValue);
        }
    }
    private void updateMiddle(float gripValue) {
        foreach (GameObject middleJointObject in middleJoints) {
            PhysicsFingerConfigurableJoint middleJoint = middleJointObject.GetComponentInChildren<PhysicsFingerConfigurableJoint>();
            middleJoint.updateHingeJoint(gripValue);
        }
    }

    private void updateRing(float gripValue) {
        foreach (GameObject ringJointObject in ringJoints) {
            PhysicsFingerConfigurableJoint ringJoint = ringJointObject.GetComponentInChildren<PhysicsFingerConfigurableJoint>();
            ringJoint.updateHingeJoint(gripValue);
        }
    }

    private void updatePinky(float gripValue) {
        foreach (GameObject pinkyJointObject in pinkyJoints) {
            PhysicsFingerConfigurableJoint pinkyJoint = pinkyJointObject.GetComponentInChildren<PhysicsFingerConfigurableJoint>();
            pinkyJoint.updateHingeJoint(gripValue);
        }
    }
}
