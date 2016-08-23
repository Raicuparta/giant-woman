using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (MeshCollider))]
public class Destructible : MonoBehaviour {
    void Start() {
        foreach (Transform child in transform) {
            child.gameObject.AddComponent<Fracture>();
            child.gameObject.AddComponent<Rigidbody>();
            child.GetComponent<MeshCollider>().convex = true;
            child.gameObject.SetActive(false);
        }
    }

    public void Destroy() {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision col) {
        /*if (col.gameObject.layer == 11) return;

        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }*/
    }
}
