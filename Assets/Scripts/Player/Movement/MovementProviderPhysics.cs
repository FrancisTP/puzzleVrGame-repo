using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MovementProviderPhysics : LocomotionProvider {

    // INITIALIZED
    public float speed = 6.0f;
    public float gravityMultiplier = 1.0f;

    // INITIALIZED AT RUNTIME
    private GameObject head = null;
    private Rigidbody thisRigidbody = null;

    // INITIALIZED IN EDITOR

    // TEMP
    // HEAD OR HAND DIRECTION
    public GameObject directionObject = null;
    // LEFT OR RIGHT HAND
    public XRController movementController = null;
    public XRController otherController = null;


    protected override void Awake() {
        //characterController = GetComponent<CharacterController>();

        // Get component for direction, right now head
        head = GetComponent<XRRig>().cameraGameObject;
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start() {
        PositionController();
    }

    // Update is called once per frame
    void FixedUpdate() {
        //PositionController();
        CheckForInput();
        //ApplyGravity();
    }

    // Positions controller based on roomscale and physical user location, not user input
    // Necessary to reposition the player based on his real life movement
    private void PositionController() {

        // Get the head in local, playspace ground
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, MovementHelper.MAX_PLAYER_HEIGHT, MovementHelper.MAX_PLAYER_HEIGHT); // Calmping height between 0.5 and 2 meters
        //characterController.height = headHeight;

        // Cut in half, add skin
        Vector3 newCenter = Vector3.zero;
        //newCenter.y = characterController.height / 2;
        //newCenter.y += characterController.skinWidth;

        // Let's move the capsule in local space as well
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;


        // Apply
        //characterController.center = newCenter;
    }

    private void CheckForInput() {
        if (movementController.enableInputActions) {
            CheckForMovement(movementController.inputDevice);
        }
    }

    private void CheckForMovement(InputDevice device) {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position)) {
            StartMove(position);
        }
    }

    private void StartMove(Vector2 position) {

        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(position.x, 0, position.y);


        // Use head or hand direction
        Vector3 rotationOffset = new Vector3(0, directionObject.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation, this means that the movement will be based on the rotation
        direction = Quaternion.Euler(rotationOffset) * direction;

        // Apply speed and move
        Vector3 movement = direction.normalized * speed;
        movement = new Vector3(movement.x, 0, movement.z);
        thisRigidbody.velocity = movement;
        //characterController.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity() {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);

        //characterController.Move(gravity * Time.deltaTime);
    }
}
