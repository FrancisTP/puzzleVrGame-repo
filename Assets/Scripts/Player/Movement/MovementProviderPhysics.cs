using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MovementProviderPhysics : LocomotionProvider {

    // For physics and collisions
    public Transform collisionTransform; //  object we want to follow
    private Vector3 collisionTransformPositionOffset = Vector3.zero;
    public float collisionDistance = 0.5f;
    private Vector3PIDController pidController = null;

    public float pFactor = 60;
    public float iFactor = 0.1f;
    public float dFactor = 0.0f;

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

        // physics
        pidController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {

        Vector3 addedVelocity = CheckForCollision();

        CheckForInput(addedVelocity);
    }

    private Vector3 CheckForCollision() {
        Vector3 collisionVector = Vector3.zero;

        if (collisionTransform != null) {
            float currentDist = Vector3.Distance((collisionTransform.position - collisionTransformPositionOffset), this.transform.position);

            if (currentDist > collisionDistance) {
                Vector3 newVector = pidController.updatePID((collisionTransform.position - collisionTransformPositionOffset), this.transform.position, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

                collisionVector = newVector;
            }
        }

        return collisionVector;
    }

    private void CheckForInput(Vector3 addedVelocity) {
        if (movementController.enableInputActions) {
            CheckForMovement(movementController.inputDevice, addedVelocity);
        }
    }

    private void CheckForMovement(InputDevice device, Vector3 addedVelocity) {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position)) {
            StartMove(position, addedVelocity);
        }
    }

    private void StartMove(Vector2 position, Vector3 addedVelocity) {

        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(position.x, 0, position.y);


        // Use head or hand direction
        Vector3 rotationOffset = new Vector3(0, directionObject.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation, this means that the movement will be based on the rotation
        direction = Quaternion.Euler(rotationOffset) * direction;

        // Apply speed and move
        Vector3 movement = direction.normalized * speed * direction.magnitude;
        movement = new Vector3(movement.x, 0, movement.z);
        thisRigidbody.velocity = (movement + addedVelocity);
    }

}
