using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public Transform Pelvis; // used to check what direction we're facing
    public float Speed = 20; // foot movement speed
    public float Stride = 10; // step distance
    public bool Anchored; // if this foot is currently stuck to the ground
    Rigidbody Body;

	void Start () {
        Body = GetComponent<Rigidbody>();
        if (Anchored) Anchor();
        else Release();
	}
	
	void FixedUpdate () {
        if (Anchored) return;
        transform.Rotate(0, Pelvis.rotation.y, 0);
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag != "StepLimit") return;
        Anchor();
        other.GetComponentInParent<Steps>().Release();
    }

    void Anchor () {
        Body.constraints = RigidbodyConstraints.FreezeAll;
        Anchored = true;
    }

    public void Release () {
        Body.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionY;
        Anchored = false;
    }
}
