using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapsule : MonoBehaviour {

    public Transform playerHeadTransform = null;
    public CapsuleCollider capsuleCollider = null;

    private Rigidbody rigidbody = null;

    // Start is called before the first frame update
    void Start() {

        //capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // called once per physics update
    void FixedUpdate() {
        // make the heigh the same as headset + radius
        capsuleCollider.height = playerHeadTransform.localPosition.y;
        capsuleCollider.center = new Vector3(0, capsuleCollider.height / 2, 0);

        // make capsule follow head on x,z axis
    }
}