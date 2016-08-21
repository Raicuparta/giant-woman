using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    Rigidbody Body;
    Rigidbody LastGrabbed;
    Transform LastParent;
    bool Reaching;
    [HideInInspector] public float Speed = 10;

	void Start () {
        Body = GetComponent<Rigidbody>();
	}

    void OnCollisionEnter(Collision collision) {
        if (!Reaching) return;
        Grab(collision.rigidbody);
    }

    public void Reach(Vector3 point) {
        if (LastGrabbed) return;
        if (!Reaching) Reaching = true;
        Body.AddForce((point - transform.position).normalized * Speed);
    }

    public void LetGo() {
        if (Reaching) Reaching = false;
        if (!LastGrabbed) return;
        Destroy(LastGrabbed.GetComponent<FixedJoint>());
        LastGrabbed.AddForce(transform.parent.forward * 30, ForceMode.Impulse);
        LastGrabbed = null;
    }

    public void Grab(Rigidbody grabbedBody) {
        if (LastGrabbed || !grabbedBody) return;
        LastGrabbed = grabbedBody;
        FixedJoint joint = grabbedBody.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = Body;

        /*grabbedBody.isKinematic = true;
        grabbedBody.position = transform.position;
        LastParent = grabbedBody.transform.parent;
        grabbedBody.transform.SetParent(transform);*/
    }
}
