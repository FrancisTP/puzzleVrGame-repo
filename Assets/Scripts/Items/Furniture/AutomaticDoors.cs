using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoors : MonoBehaviour
{

    public GameObject leftDoor = null;
    public GameObject rightDoor = null;
    public float openWidth = 0.0f;

    private Vector3 leftDoorStartingLocPos = Vector3.zero;
    private Vector3 rightDoorStartingLocPos = Vector3.zero;

    private bool sensorTriggered = false;
    private float varianceThreshold = 0.005f;

    private readonly string triggerTag = "Player";

    private Rigidbody leftDoorRigidbody = null;
    private Rigidbody rightDoorRigidbody = null;


    void Start() {
        if (leftDoor != null && rightDoor != null) {
            leftDoorRigidbody = leftDoor.GetComponent<Rigidbody>();
            leftDoorStartingLocPos = new Vector3 (leftDoor.transform.localPosition.x, leftDoor.transform.localPosition.y, leftDoor.transform.localPosition.z);
            
            rightDoorRigidbody = rightDoor.GetComponent<Rigidbody>();
            rightDoorStartingLocPos = new Vector3(rightDoor.transform.localPosition.x, rightDoor.transform.localPosition.y, rightDoor.transform.localPosition.z);
        }
    }



    /*
     Rigidbody rb = target.attachedRigidbody;
     localVelocity = rb.transform.InverseTransformDirection(rb.velocity);
     localVelocity.x *= 0.5f; // cut sideways speed in half
     rb.velocity = rb.transform.TransformDirection (localVelocity);
     */

    //thisRigidbody.velocity = ((parent.transform.position - worldPositionOffset) - this.transform.position).normalized * ((float)(0.25));


    // Update is called once per frame
    void FixedUpdate() {
        if (leftDoor != null && rightDoor != null && openWidth != 0.0f) {

            if (!sensorTriggered) { // doors closed or closing
                // doors move on z axis in local space
                // left door
               


                // right door

            } else if (sensorTriggered) { // doors opened or opening

                // left doors
                Vector3 localLeftDoorPosition = leftDoor.transform.localPosition;
                Vector3 localTargetLeftDoorPosition = leftDoorStartingLocPos - new Vector3(0, 0, openWidth);
                Vector3 worldLeftDoorPosition = leftDoor.transform.position;
                Vector3 worldTargetLeftDoorPosition = this.transform.TransformPoint(localTargetLeftDoorPosition);

                // right door
                Vector3 localRightDoorPosition = rightDoor.transform.localPosition;
                Vector3 localTargetRightDoorPosition = rightDoorStartingLocPos + new Vector3(0, 0, openWidth);
                Vector3 worldRightDoorPosition = rightDoor.transform.position;
                Vector3 worldTargetRightDoorPosition = this.transform.TransformPoint(localTargetRightDoorPosition);

                // check if doors are already fully opened
                // left door
                bool leftDoorOpened = false;
                if (Mathf.Abs(worldLeftDoorPosition.x - worldTargetLeftDoorPosition.x) < varianceThreshold && Mathf.Abs(worldLeftDoorPosition.y - worldTargetLeftDoorPosition.y) < varianceThreshold && Mathf.Abs(worldLeftDoorPosition.z - worldTargetLeftDoorPosition.z) < varianceThreshold) {
                    leftDoorOpened = false; // true;
                }

                if (!leftDoorOpened) {
                    leftDoorRigidbody.velocity = (worldTargetLeftDoorPosition - worldLeftDoorPosition).normalized * (2f);
                }


                // right door
                rightDoorRigidbody.velocity = (worldTargetRightDoorPosition - worldRightDoorPosition).normalized * (2f);
            }
        }
    }



    void OnTriggerEnter(Collider other) {

        if (other.tag == triggerTag) {
            // someone is triggering a sensor
            sensorTriggered = true;
            Debug.Log("SENSOR TRIGGERED");
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.tag == triggerTag) {
            // person left sensor area
            sensorTriggered = false;
            Debug.Log("SENSOR UN-TRIGGERED");
        }
    }
}
