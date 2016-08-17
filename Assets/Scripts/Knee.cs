﻿using UnityEngine;
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
    int Outwards; // points to the right if this is the right foot, left if left foot
    float InitialY;

	void Start () {
        Foot = GetComponent<Rigidbody>();
        InitialY = transform.position.y;
        Outwards = transform.position.x > 0 ? 1 : -1;

        // we're gonna change the hierarchy so we need to save the parent
        Parent = transform.parent;
        ParentBody = Parent.GetComponent<Rigidbody>();
        transform.SetParent(null);

        if (Anchored) Anchor();
        else Release();
	}

	public void Move () {
        // we only want to move this foot if the other one is currently grounded
        if (!OtherFoot.Anchored) return;
        if (Anchored) Release();

        // Move the foot by changing its velocity.
        // The velocity needs to be more than twice the parent to make sure the feet
        // can keep up.
        Foot.velocity = ParentBody.velocity * 3;

        // To calculate the distance between the feet, we use projection so that
        // we only take into account the forward axis
        Vector3 difference = OtherFoot.transform.position - transform.position;
        difference = Vector3.ProjectOnPlane(difference, Parent.right);
        float distance = difference.magnitude;
        if (distance > Stride && IsInFront()) Anchor();
	}

    // calculate where we want te foot to be at the end of the step
    Vector3 ComputeTarget() {
        Vector3 target = Parent.position + Parent.forward * Stride;
        // target position should point a bit outwards
        target += Parent.right * Outwards * Width;
        // also a bit above ground
        target.y = InitialY;
        Debug.DrawLine(Parent.position, target);
        return target;
    }

    // true if this foot is in front of the other foot
    bool IsInFront() {
        Vector3 heading = OtherFoot.transform.position - transform.position;
        heading.Normalize();
        float dot = Vector3.Dot(heading, Parent.forward);
        return dot < 0;
    }

    void Anchor () {
        Foot.constraints = RigidbodyConstraints.FreezePosition;
        Anchored = true;
    }

    public void Release () {
        Foot.constraints = RigidbodyConstraints.None;
        Anchored = false;
    }
}