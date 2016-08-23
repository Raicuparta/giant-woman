using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    Rigidbody Body;
    Rigidbody GrabbedBody;
    Destructible GrabbedDestructible;
    int LastLayer;
    bool Reaching;
    float MaxArmLength;
    [HideInInspector] public float Speed = 10;
    [HideInInspector] public float ThrowStrenght = 30;
    void Start() {
        Body = GetComponent<Rigidbody>();
        MaxArmLength = Vector3.Distance(transform.parent.position, transform.position);
    }

    void Update() {
        PullDestructible();
    }

    void PullDestructible() {
        if (!GrabbedDestructible || !GrabbedBody) return;
        float distance = Vector3.Distance(transform.parent.position, transform.position);
        if (distance > MaxArmLength) {
            GrabbedDestructible.Destroy();
            LetGo();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (!Reaching) return;
        Grab(collision.rigidbody);
    }

    public void Reach(Vector3 point) {
        if (GrabbedBody) return;
        if (!Reaching) Reaching = true;
        Body.velocity = (point - transform.position).normalized * Speed;
        //Body.AddForce((point - transform.position).normalized * Speed);
    }

    public void LetGo() {
        StopReaching();
        if (!GrabbedBody) return;
        Destroy(GrabbedBody.GetComponent<FixedJoint>());
        GrabbedBody.gameObject.layer = LastLayer;
        GrabbedBody = null;
    }

    public void ThrowTowards(Vector3 target) {
        if (!GrabbedBody) return;
        Vector3 force = (target - transform.position).normalized * ThrowStrenght;
        GrabbedBody.AddForce(force, ForceMode.Impulse);
        LetGo();
    }

    public void StopReaching() {
        if (Reaching) Reaching = false;
    }

    public void Grab(Rigidbody grabbedBody) {
        if (GrabbedBody || !grabbedBody) return;
        GrabbedBody = grabbedBody;
        GrabbedDestructible = grabbedBody.GetComponent<Destructible>();
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
