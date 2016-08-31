using UnityEngine;
using System.Collections;

public class Person : Grabbable {
    Rigidbody Body;
    Quaternion TargetRotation;
    public Renderer BodyRenderer;
    public float Speed = 5;
    public float RotationInterval = 3; // how long to wait before changing direction
    public float RotationSpeed = 5;

	void Start() {
        Body = GetComponent<Rigidbody>();

        // Lower the center of gravity to keep the little guy standing up
        Body.centerOfMass = Vector3.down;

        // Change the direction of movement every so often
        InvokeRepeating("ChangeDirection", 0, RotationInterval);
    }

    void ChangeDirection() {
        float yRot = Random.Range(0f, 360f);
        TargetRotation = Quaternion.Euler(0, yRot, 0);
    }
	
	void FixedUpdate () {
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        Body.AddForce(forward, ForceMode.VelocityChange);

        // Change the X and Z components of the velocity to the given speed,
        // but keep the Y component as to not affect gravity.
        Vector2 velocityXZ = new Vector2(Body.velocity.x, Body.velocity.z);
        velocityXZ = velocityXZ.normalized * Speed;
        Body.velocity = new Vector3(velocityXZ.x, Body.velocity.y, velocityXZ.y);

        // Update the body rotation
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, RotationSpeed);
        Body.MoveRotation(rotation);
	}

    public override void Grab() {
        // TODO
    }
}
