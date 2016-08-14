using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
    public float UpStrenght = 100; // strenght of the force pushing the head up

    Rigidbody Body;
    float InitialY;

	void Start () {
        Body = GetComponent<Rigidbody>();
        InitialY = Body.position.y;
	}

	void FixedUpdate () {
        if (Body.position.y < InitialY)
            Body.AddForce(Vector3.up * UpStrenght);
	}
}
