using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Collider))]
public class PhysicsFinger : MonoBehaviour {

    private Rigidbody thisRigidbody;
    private HingeJoint hingeJoint;
    private Collider collider;

    private bool gripping = false;
    private float gripStoppedValue = 0.0f;

    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();
        hingeJoint = GetComponent<HingeJoint>();
        collider = GetComponent<Collider>();
    }
}
