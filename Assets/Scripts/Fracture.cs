using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.layer != 10) return;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
