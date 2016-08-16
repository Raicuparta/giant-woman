using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    Rigidbody Body;
    public float MovementSpeed = 10; // character movement speed multiplier
    public float RotationSpeed = 10;

    void Start () {
        Body = GetComponent<Rigidbody>();
    }
	
	public void Move (float h, float v) {
        Vector3 forwardForce = transform.rotation * Vector3.forward * v * MovementSpeed;
        forwardForce = Vector3.ProjectOnPlane(forwardForce, Vector3.up);
        Body.AddForce(forwardForce);

        Vector3 torque = Vector3.up * h * RotationSpeed;
        Body.AddTorque(torque);
	}
}
