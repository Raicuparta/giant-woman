using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    Rigidbody Body;
    Steps CharacterSteps;
    public float MovementSpeed = 10; // character movement speed multiplier
    public float RotationSpeed = 10;

    void Start () {
        Body = GetComponent<Rigidbody>();
        CharacterSteps = GetComponent<Steps>();
    }
	
	public void Move (float h, float v) {
        // use v for forward movement
        Vector3 forwardForce = transform.rotation * Vector3.forward * v * MovementSpeed;
        forwardForce = Vector3.ProjectOnPlane(forwardForce, Vector3.up);
        Body.AddForce(forwardForce);

        // use h for y rotation
        Vector3 torque = Vector3.up * h * RotationSpeed;
        Body.AddTorque(torque);
        if (v != 0) CharacterSteps.Move();
    }
}
