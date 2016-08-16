using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    public float Speed = 10;
    Rigidbody Body;
    Rigidbody LastGrabbed;
    Transform LastParent;
    bool Reaching;

	void Start () {
        Body = GetComponent<Rigidbody>();
	}

    void OnCollisionEnter(Collision collision) {
        if (!Reaching) return;
        Grab(collision.rigidbody);
    }

    public void Reach(Vector3 point) {
        if (!Reaching) Reaching = true;
        Body.AddForce((point - transform.position).normalized * Speed);
    }

    public void LetGo() {
        if (Reaching) Reaching = false;
        if (!LastGrabbed) return;
        LastGrabbed.transform.SetParent(LastParent);
        LastGrabbed.isKinematic = false;
        LastGrabbed = null;
    }

    public void Grab(Rigidbody grabbedBody) {
        if (LastGrabbed) return;
        LastGrabbed = grabbedBody;
        grabbedBody.isKinematic = true;
        grabbedBody.position = transform.position;
        LastParent = grabbedBody.transform.parent;
        grabbedBody.transform.SetParent(transform);
    }
}
