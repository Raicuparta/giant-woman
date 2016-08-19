using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    Rigidbody Body;
    Steps CharacterSteps;
    public float MovementSpeed = 10; // character movement speed multiplier
    public float RotationSpeed = 10; // character rotation speed multiplier
    public float JumpChargeHeight = 3; // how much the body is lowered when chargind a jump
    public float JumpChargeSpeed = 10; // how fast the body is lowered when chargind a jump

    void Start() {
        Body = GetComponent<Rigidbody>();
        Body.centerOfMass = -transform.up * 5;
        CharacterSteps = GetComponent<Steps>();
    }
	
	public void Move(float h, float v) {
        // use v for forward movement
        Vector3 forwardForce = transform.rotation * Vector3.forward * v * MovementSpeed;
        forwardForce = Vector3.ProjectOnPlane(forwardForce, Vector3.up);
        Body.AddForce(forwardForce);

        // use h for y rotation
        Vector3 torque = Vector3.up * h * RotationSpeed;
        Body.AddTorque(torque);
        CharacterSteps.Move();
    }

    public void ChargeJump() {
        Vector3 target = transform.position - transform.up * JumpChargeHeight;
        Vector3 force = Util.ForceTowards(transform.position, target, JumpChargeSpeed);
        Body.AddForce(force);
    }
}
