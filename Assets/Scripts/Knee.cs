using UnityEngine;
using System.Collections;

public class Knee : MonoBehaviour {
    [HideInInspector] public float Speed = 20; // foot movement speed
    [HideInInspector] public float Stride = 10; // step distance
    [HideInInspector] public float Width = 2; // lateral distance between the feet
    [HideInInspector] public bool Anchored; // if the foot is currently stuck to the ground
    [HideInInspector] public Knee OtherFoot;
    Rigidbody Foot;
    Transform Parent;
    Rigidbody ParentBody;
    AudioSource StepSound;
    int Outwards; // points to the right if this is the right foot, left if left foot
    float InitialY;

	void Start () {
        Foot = GetComponent<Rigidbody>();
        InitialY = transform.position.y;
        Outwards = transform.position.x > 0 ? -1 : 1;

        // we're gonna change the hierarchy so we need to save the parent
        Parent = transform.parent;
        ParentBody = Parent.GetComponent<Rigidbody>();
        StepSound = Parent.GetComponent<AudioSource>();
        transform.SetParent(null);

        if (Anchored) Anchor();
        else Release();
	}

	public void Move () {
        // we only want to move this foot if the other one is currently grounded
        if (!OtherFoot.Anchored) return;
        if (Anchored) Release();

        // Move the foot by applying a force towards the target
        float intensity = ComputeForwardVelocity().magnitude * Speed;
        Vector3 force = Util.ForceTowards(transform.position, ComputeTarget(), intensity);
        Foot.AddForce(force);
        // The velocity needs to be more than twice the parent to make sure the feet
        // can keep up.
        //Foot.velocity.Normalize();
        //Foot.velocity *= ParentBody.velocity.magnitude;

        if (ShouldAnchor()) {
            Anchor();
            OtherFoot.Release();
        }
    }

    bool ShouldAnchor() {
        Vector3 difference = OtherFoot.transform.position - transform.position;
        //difference = Vector3.ProjectOnPlane(difference, Parent.right);
        float distance = difference.magnitude;
        bool result = distance > Stride/* ||
            Util.IsInFront(transform.position, Parent.position, Parent.right * -Outwards)*/;
        return result && IsInFront();
    }

    // calculate where we want te foot to be at the end of the step
    Vector3 ComputeTarget() {
        Vector3 target = Parent.position + ComputeForward() * Stride;
        // target position should point a bit outwards
        target += Parent.right * Outwards * Width;
        // also a bit above ground
        target.y = InitialY;
        Debug.DrawLine(Parent.position, target);
        return target;
    }

    Vector3 ComputeForward() {
        return ComputeForwardVelocity().normalized;
    }

    Vector3 ComputeForwardVelocity() {
        Vector3 velocity = ParentBody.velocity;
        return Vector3.ProjectOnPlane(velocity, Parent.up);
    }

    // true if this foot is in front of the other foot
    bool IsInFront() {
        return Util.IsInFront(transform.position, OtherFoot.transform.position, ComputeForward());
    }

    void Anchor () {
        Foot.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        Anchored = true;
        float pitchDelta = Random.Range(0.8f, 1.2f);
        StepSound.pitch = pitchDelta;
        StepSound.Play();
    }

    public void Release () {
        Foot.constraints = RigidbodyConstraints.None;
        Anchored = false;
    }
}
