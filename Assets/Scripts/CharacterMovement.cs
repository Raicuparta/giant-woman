using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    Rigidbody Body;
    Steps CharacterSteps;
    SphereCollider ColliderBase;
    float DefaultBaseY;
    public float MovementSpeed = 10; // character movement speed multiplier
    public float RotationSpeed = 10; // character rotation speed multiplier
    public float JumpChargeHeight = 1; // how much the body is lowered when chargind a jump
    public float JumpStrength = 10; // how fast the body is lowered when chargind a jump

    void Start() {
        Body = GetComponent<Rigidbody>();
        Body.centerOfMass = -transform.up * 5;
        CharacterSteps = GetComponent<Steps>();
        ColliderBase = GetComponent<SphereCollider>();
        DefaultBaseY = ColliderBase.center.y;
    }
	
	public void Move(float h, float v) {
        // use v for forward movement
        Vector3 forwardForce = transform.rotation * Vector3.forward * v * MovementSpeed;
        forwardForce = Vector3.ProjectOnPlane(forwardForce, Vector3.up);
        Body.AddForce(forwardForce);

        // use h for y rotation
        /*Vector3 torque = Vector3.up * h * RotationSpeed;
        Body.AddTorque(torque);*/
        CharacterSteps.Move(h * RotationSpeed);
    }

    public void ChargeJump() {
        ColliderBase.center = Vector3.up * (DefaultBaseY + JumpChargeHeight);
    }

    public void Jump() {
        ColliderBase.center = Vector3.up * DefaultBaseY;
        Vector3 force = transform.up * JumpStrength;
        Body.AddForce(force, ForceMode.Impulse);
    }
}
