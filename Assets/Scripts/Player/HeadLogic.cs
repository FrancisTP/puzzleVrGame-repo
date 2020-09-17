using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLogic : MonoBehaviour {

    public Transform headCamera = null;
    public Transform body = null;
    private Rigidbody rigidbody = null;
    private Transform transform = null;

    void Start() {

        //capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    // called once per physics update
    void FixedUpdate() {
        // make head go directly on MainCamera, if collision occurs set on collision body
        transform.position = headCamera.position;
    }
}
