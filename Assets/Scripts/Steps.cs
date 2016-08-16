using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float Speed = 20; // foot movement speed
    public float Stride = 10; // step distance
    public bool Anchored; // if the foot is currently stuck to the ground
    public Steps OtherFoot;
    Rigidbody Foot;
    Transform Parent;
    Vector3 PreviousStep; // position the player was in the previous step

	void Start () {
        Foot = GetComponent<Rigidbody>();
        // we're gonna change the hierarchy so we need to save the parent
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
        // let's apply a force towards that target position to get the foot there
        Vector3 force = (target - transform.position).normalized * Speed;
        Foot.AddForce(force);

        float distance = Vector3.Distance(OtherFoot.transform.position, transform.position);
        if (distance > 2 * Stride && IsInFront()) Anchor();
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
        Foot.constraints = RigidbodyConstraints.FreezeAll;
        Anchored = true;
    }

    public void Release () {
        Foot.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionY;
        Anchored = false;
    }
}
