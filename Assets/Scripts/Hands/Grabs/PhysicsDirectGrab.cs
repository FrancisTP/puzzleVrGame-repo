using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsDirectGrab : MonoBehaviour {

    // Snap grab settings
    public bool snapGrab = false;
    private Transform snapPosition;

    private Transform hand = null; // player hand (left ir right)

    private Rigidbody thisRigidbody = null;

    private Vector3PIDController vector3PIDController = null;
    private float pFactor = 60;
    private float iFactor = 0.05f;
    private float dFactor = 1f;

    private bool handHovering = false;
    private bool isGrabbed = false;

    private InputDevice handController;
    private InputDevice dummyInputDevice = new InputDevice();

    private DirectGrabFollow directGrabFollow = null;

    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody>(); 
        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    void Update() {
        if (handHovering) { // A hand is within intaraction reach of this object
            float grip;
            if (handController.TryGetFeatureValue(CommonUsages.grip, out grip) && grip != 0.0f) {
                // hand is close enough to interact with the object and the user is trying to grab it


                if (directGrabFollow == null) {
                    directGrabFollow = new DirectGrabFollow(thisRigidbody, hand);
                }

                isGrabbed = true;
                Debug.Log("Trying to grab with: " + handController.name + " grip: " + grip + " directGrabFollow: " + directGrabFollow);
            } else {
                Debug.Log("LETTING GO");
                isGrabbed = false;
                directGrabFollow = null;
            }
        }
    }

    void FixedUpdate() {
        Debug.Log("isGrabbed: " + isGrabbed + "   -    directGrabFollow: " + directGrabFollow);
        if (isGrabbed && directGrabFollow != null) {
            // hand (palm to match position and rotation)
            // handController (inputDevice)
            directGrabFollow.UpdateFollow();
            Debug.Log("1231321");
        }
    }

    private void OnSelect() {

    }

    // Start is called before the first frame update
    private void UpdatePosition() {

    }

    private void UpdateRotation() {

    }

    private void OnRelease() {

    }


    // Automatic triggers
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Hand") {
            handHovering = true;
            hand = other.transform;

            RigidPositionFollow rigidPositionFollow = other.gameObject.GetComponent<RigidPositionFollow>();
            Transform trackedControllerTransform = rigidPositionFollow.parent;
            UnityEngine.XR.Interaction.Toolkit.XRController xrController = trackedControllerTransform.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>();
            handController = xrController.inputDevice;

            Debug.Log("Hand entered grab area: " + handController.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Hand") {
            handHovering = false;
            hand = null;
            handController = dummyInputDevice;
        }
    }

}
