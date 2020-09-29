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

    [Header("Behaviour Options")]

    [SerializeField]
    public float speed = 7f;
    [SerializeField]
    public float jumpForce = 800f;
    [SerializeField]
    public XRNode controllerNode;
    [SerializeField]
    public bool checkForGroundOnJump = true; // set to false to fly

    [Header("Capsule Collider Options")]

    [SerializeField]
    public Vector3 capsuleCenter = new Vector3(0, 1, 0);
    [SerializeField]
    public float capsuleRadius = 0.3f;
    [SerializeField]
    public float capsuleHeight = 1.6f;

    [SerializeField]
    private CapsuleDirection capsuleDirection = CapsuleDirection.YAxis;
    private InputDevice controller;
    private bool isGrounded;
    private bool buttonPressed;
    private Rigidbody rigidBodyComponent;
    private CapsuleCollider capsuleCollider;
    private List<InputDevice> devices = new List<InputDevice>();

    public enum CapsuleDirection
    {
        XAxis,
        YAxis,
        ZAxis
    }

    void OnEnable()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rigidBodyComponent.constraints = RigidbodyConstraints.FreezeRotation;
        capsuleCollider.direction = (int)capsuleDirection;
        capsuleCollider.center = capsuleCenter;
        capsuleCollider.height = capsuleHeight;
    }

    // Start is called before the first frame update
    void Start() {
        GetDevice();
    }

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            GetDevice();
        }

        UpdateMovement();
        UpdateJump();
    }

    private void UpdateMovement()
    {

        Vector2 primary2dValue;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2dValue) && primary2dValue != Vector2.zero)
        {
            float xAxis = primary2dValue.x * speed * Time.deltaTime;
            float zAxis = primary2dValue.y * speed * Time.deltaTime;

            Vector3 leftRight = transform.TransformDirection(Vector3.right) * xAxis;
            Vector3 forwardsBackwards = transform.TransformDirection(Vector3.forward) * zAxis;


        }

        /*
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position)) {
            StartMove(position);
        }
        */
    }

    private void UpdateJump()
    {

    }
}
