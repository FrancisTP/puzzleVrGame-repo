using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpringRotation : MonoBehaviour {
    public bool active = true;

    public Rigidbody target;
    private new Rigidbody rigidbody;

    private Vector3 torque;

    private Quaternion worldRotationOffset = Quaternion.identity; // same as worldPositionOffset but for angles

    private Vector3PIDController vector3PIDController = null;
    public float pFactor = 5f;
    public float iFactor = 0f;
    public float dFactor = 0f;

    private void Start() {
        this.rigidbody = this.GetComponent<Rigidbody>();
        if (target != null) {
            worldRotationOffset = getOffsetBetweenChildAndParent(transform.rotation, target.transform.rotation);
        }

        vector3PIDController = new Vector3PIDController(pFactor, iFactor, dFactor);
    }

    /*
    private void OnGUI() {
        Quaternion diff = Quaternion.Inverse(rigidbody.rotation) * target.rotation;
        Vector3 eulers = OrientTorque(diff.eulerAngles);
        GUI.TextArea(new Rect(50, 50, 200, 50), eulers.ToString());
    }
    */

    private void FixedUpdate() {
        if (active) {
            //Find the rotation difference in eulers
            Quaternion diff = (worldRotationOffset * Quaternion.Inverse(rigidbody.rotation)) * target.rotation;
            Vector3 eulers = OrientTorque(diff.eulerAngles);
            Vector3 torque = eulers;
            //put the torque back in body space
            torque = rigidbody.rotation * torque;

            //just zero out the current angularVelocity so it doesnt interfere
            //rigidbody.angularVelocity = Vector3.zero;

            Vector3 newTorque = vector3PIDController.updatePid(torque, rigidbody.angularVelocity, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
            rigidbody.AddTorque(newTorque, ForceMode.VelocityChange);
        }
    }

    private Vector3 OrientTorque(Vector3 torque) {
        // Quaternion's Euler conversion results in (0-360)
        // For torque, we need -180 to 180.

        return new Vector3
        (
        torque.x > 180f ? torque.x - 360f : torque.x,
        torque.y > 180f ? torque.y - 360f : torque.y,
        torque.z > 180f ? torque.z - 360f : torque.z
        );
    }

    private Quaternion getOffsetBetweenChildAndParent(Quaternion child, Quaternion parent) {
        Quaternion offset = Quaternion.identity;

        offset = child * Quaternion.Inverse(parent);

        return offset;
    }
}