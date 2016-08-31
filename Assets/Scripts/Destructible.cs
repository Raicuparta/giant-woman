using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (MeshCollider))]
public class Destructible : Grabbable {
    Destructible Bellow; // building under this one, if any
    public float DownRayHeight = 6; // size of the ray that checks if there's another building bellow
    [HideInInspector] public bool Destroyed;

    void Start() {
        SetupCells();
        CheckBellow();
    }

    void Update() {
        if (Bellow && Bellow.Destroyed) Destroy();
    }

    // check if there's another building under this one
    void CheckBellow() {
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, -Vector3.up, out hit, DownRayHeight);
        if (!ray) return;
        Bellow = hit.transform.GetComponent<Destructible>();
    }

    void SetupCells() {
        foreach (Transform child in transform) {
            child.gameObject.AddComponent<Fracture>();
            child.gameObject.AddComponent<Rigidbody>();
            child.GetComponent<MeshCollider>().convex = true;
            child.gameObject.SetActive(false);
        }
    }

    public void Destroy() {
        Destroyed = true;
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

    public override void Grab() {
        // TODO
    }
}
