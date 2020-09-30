using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class XRPlayerController : MonoBehaviour {
    private bool DEBUG = false;


    [Header("Behaviour Options")]

    [SerializeField]
    public float speed = 50f;
    [SerializeField]
    public float maxVelocityChange = 0.8f;
    [SerializeField]
    public float maxVelocity = 3.5f;
    [SerializeField]
    public float jumpForce = 800f;
    [SerializeField]
    public bool checkForGroundOnJump = true; // set to false to fly
    [SerializeField]
    public XRNode mainControllerNode;
    [SerializeField]
    public XRNode secondaryControllerNode;
    [SerializeField]
    public Transform head;

    [Header("Capsule Collider Options")]

    private InputDevice mainController;
    private InputDevice secondaryController;
    private bool isGrounded;
    private float isGroundedCheckLength = 0.1f;
    private bool buttonPressed = false;
    private Rigidbody rigidBodyComponent;
    private CapsuleCollider capsuleCollider;

    // What to use for direction (head, hands, etc)
    private GameObject directionDevice;
    

    public static readonly float MIN_PLAYER_HEIGHT = 0.20f; // in meters
    public static readonly float MAX_PLAYER_HEIGHT = 2.0f; // in meters

    public enum CapsuleDirection {
        XAxis,
        YAxis,
        ZAxis
    }

    void OnEnable() {
        rigidBodyComponent = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rigidBodyComponent.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Start is called before the first frame update
    void Start() {
        GetDevices();
    }

    private void GetDevices() {
        List<InputDevice> devices;
        if (mainController == null) {
            devices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(mainControllerNode, devices);
            mainController = devices.FirstOrDefault();
        }

        if (secondaryController == null) {
            devices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(secondaryControllerNode, devices);
            secondaryController = devices.FirstOrDefault();
        }

    }

    // Handle button presses
    void FixedUpdate() {
        if (mainController == null || secondaryController == null) {
            GetDevices();
        }

        // check if player is grounded before checking for any movement
        Vector3 colliderWorldSpace = transform.TransformPoint(capsuleCollider.center);
        Vector3 startingRaycastPoint = new Vector3(colliderWorldSpace.x, colliderWorldSpace.y, colliderWorldSpace.z);
        Vector3 endingRaycastPoint = new Vector3(colliderWorldSpace.x, colliderWorldSpace.y - ((capsuleCollider.height / 2f) + isGroundedCheckLength), colliderWorldSpace.z);

        isGrounded = Physics.Raycast(startingRaycastPoint, Vector3.down, (capsuleCollider.height / 2f) + isGroundedCheckLength);

        if (DEBUG) {
            Debug.DrawLine(startingRaycastPoint, endingRaycastPoint, Color.red, 1f);
            Debug.Log("isGrounded: " + isGrounded);
        }

        SetCapsuleColliderToHead();
        UpdateMovement();
        UpdateJump();
    }

    private void UpdateMovement() {

        if (!isGrounded && checkForGroundOnJump) {
            return;
        }

        Vector2 primary2dValue;
        mainController.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2dValue);

        if (mainController.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2dValue) && primary2dValue != Vector2.zero) {
            Debug.Log("qweqwewqe");
            float xAxis = Mathf.Clamp(primary2dValue.x * speed * Time.deltaTime, -maxVelocityChange, maxVelocityChange);
            float zAxis = Mathf.Clamp(primary2dValue.y * speed * Time.deltaTime, -maxVelocityChange, maxVelocityChange);


            Vector3 leftRight = transform.TransformDirection(Vector3.right) * xAxis;
            Vector3 forwardsBackwards = transform.TransformDirection(Vector3.forward) * zAxis;

            Vector3 velocityForceVector = leftRight + forwardsBackwards;
            rigidBodyComponent.AddForce(velocityForceVector, ForceMode.VelocityChange);
            ClampVelocity();
        }
    }

    private void ClampVelocity() {
        float clampedXVelocity = Mathf.Clamp(rigidBodyComponent.velocity.x, -maxVelocity, maxVelocity);
        float clampedZVelocity = Mathf.Clamp(rigidBodyComponent.velocity.z, -maxVelocity, maxVelocity);

        Vector3 newVelocity = new Vector3(clampedXVelocity, rigidBodyComponent.velocity.y, clampedZVelocity);
        rigidBodyComponent.velocity = newVelocity;
    }

    private void UpdateJump() {
       
        if (!isGrounded && checkForGroundOnJump) {
            return;
        }

        bool buttonValue;
        if (secondaryController.TryGetFeatureValue(CommonUsages.primaryButton, out buttonValue) && buttonValue) {
            if (!buttonPressed) {
                buttonPressed = true;

                rigidBodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        } else if (buttonPressed) {
            buttonPressed = false;
        }
    }

    private void SetCapsuleColliderToHead() {
        Vector3 headLocalSpace = transform.InverseTransformPoint(head.position);

        Vector3 capsuleColliderNewCenter = new Vector3(headLocalSpace.x, capsuleCollider.center.y, headLocalSpace.z);
        //capsuleCollider.center = capsuleColliderNewCenter;
    }
}
