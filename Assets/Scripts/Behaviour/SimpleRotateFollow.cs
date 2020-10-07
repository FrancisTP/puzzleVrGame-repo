using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleRotateFollow : MonoBehaviour {

    public Transform parent;
    private Rigidbody thisRigidbody;
    private Quaternion localOffset = Quaternion.identity;

    // Start is called before the first frame update
    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();



        if (parent != null) {
            localOffset = getOffsetBetweenChildAndParent(thisRigidbody.rotation, parent.rotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        thisRigidbody.rotation = parent.rotation * localOffset;
    }

    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        //Quaternion LocalRotation = Quaternion.Inverse(Target.transform.rotation) * WorldRotation;
        Quaternion offset = Quaternion.identity;

        offset = Quaternion.Inverse(parent) * child;

        return offset;
    }
}
