using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
    Rigidbody Body;
    public float Speed = 5;

	void Start () {
        Body = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        Body.velocity = transform.forward * Speed;
	}
}
