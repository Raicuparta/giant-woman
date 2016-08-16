using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float Speed = 20; // foot movement speed
    public float Stride = 10; // step distance
    public float Width = 2; // lateral distance between the feet
    public bool Anchored; // if the foot is currently stuck to the ground
    public Steps OtherFoot;
    Rigidbody Foot;
    Transform Parent;
    Vector3 PreviousStep; // position the player was in the previous step
    int Outwards; // points to the right if this is the right foot, left if left foot

	void Start () {
        Foot = GetComponent<Rigidbody>();
        // we're gonna change the hierarchy so we need to save the parent

        Outwards = transform.position.x > 0 ? 1 : -1;

        Parent = transform.parent;
        transform.SetParent(null);

        if (Anchored) Anchor();
        else Release();
	}
	
	void FixedUpdate () {
        // we only want to move this foot if the other one is currently grounded
        if (!OtherFoot.Anchored) return;
        if (Anchored) Release();
        // this is where we want te foot to be at the end of the step
        Vector3 target = Parent.position + Parent.forward * Stride;
        // target position should point a bit outwards
        target += Parent.right * Outwards * Width;
        // also a bit above ground
        target += Vector3.up;
        Debug.DrawLine(Parent.position, target, Color.red);
        Debug.DrawLine(Parent.position + Vector3.up, Parent.position + Parent.forward * 10, Color.black);
        // let's apply a force towards that target position to get the foot there
        Vector3 force = (target - transform.position).normalized * Speed;
        Debug.DrawLine(transform.position, transform.position + force, Color.green);
        Foot.AddForce(force);

        // To calculate the distance between the feet, we use projection so that
        // we only take into account the forward axis
        Vector3 difference = OtherFoot.transform.position - transform.position;
        difference = Vector3.ProjectOnPlane(difference, Parent.right);
        float distance = difference.magnitude;
        if (distance > Stride && IsInFront()) Anchor();
	}

    // true if this foot is in front of the other foot
    bool IsInFront() {
        Vector3 heading = OtherFoot.transform.position - transform.position;
        heading.Normalize();
        float dot = Vector3.Dot(heading, Parent.forward);
        return dot < 0;
    }

    void OnTriggerEnter(Collider other) {
    }

    void Anchor () {
        Foot.constraints = ~RigidbodyConstraints.FreezeRotation;
        Anchored = true;
    }

    public void Release () {
        Foot.constraints = RigidbodyConstraints.FreezePositionY;
        Anchored = false;
    }
}
