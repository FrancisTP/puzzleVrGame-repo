using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleRotateFollow : MonoBehaviour {

    public Transform parent;
    private Rigidbody thisRigidbody;
    private Quaternion worldOffset = Quaternion.identity;

    // Start is called before the first frame update
    void Start() {
        thisRigidbody = GetComponent<Rigidbody>();

        if (parent != null) {
            worldOffset = getOffsetBetweenChildAndParent(thisRigidbody.rotation, parent.rotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        thisRigidbody.rotation = parent.rotation;
    }

    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        Quaternion offset = Quaternion.identity;

        offset = child * Quaternion.Inverse(parent);

        return offset;
    }
}
