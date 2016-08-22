using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    Rigidbody Body;
    Rigidbody LastGrabbed;
    Transform LastParent;
    int LastLayer;
    bool Reaching;
    [HideInInspector] public float Speed = 10;
    [HideInInspector] public float ThrowStrenght = 30;
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
        Body.velocity = (point - transform.position).normalized * Speed;
        //Body.AddForce((point - transform.position).normalized * Speed);
    }

    public void ThrowTowards(Vector3 target) {
        if (Reaching) Reaching = false;
        if (!LastGrabbed) return;
        Destroy(LastGrabbed.GetComponent<FixedJoint>());
        Vector3 force = (target - transform.position).normalized * ThrowStrenght;
        LastGrabbed.AddForce(force, ForceMode.Impulse);
        LastGrabbed.gameObject.layer = LastLayer;
        LastGrabbed = null;
    }

    public void StopReaching() {
        if (Reaching) Reaching = false;
    }

    public void Grab(Rigidbody grabbedBody) {
        if (LastGrabbed || !grabbedBody) return;
        LastGrabbed = grabbedBody;
        FixedJoint joint = grabbedBody.gameObject.AddComponent<FixedJoint>();
        LastLayer = grabbedBody.gameObject.layer;
        grabbedBody.gameObject.layer = gameObject.layer;
        joint.connectedBody = Body;

        /*grabbedBody.isKinematic = true;
        grabbedBody.position = transform.position;
        LastParent = grabbedBody.transform.parent;
        grabbedBody.transform.SetParent(transform);*/
    }
}
