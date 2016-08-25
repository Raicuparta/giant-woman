﻿using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
    Rigidbody Body;
    Quaternion TargetRotation;
    public Renderer BodyRenderer;
    public float Speed = 5;
    public float RotationInterval = 3; // how long to wait before changing direction
    public float RotationSpeed = 5;

	void Start() {
        Body = GetComponent<Rigidbody>();
        InvokeRepeating("ChangeDirection", 0, RotationInterval);
        foreach (Material mat in BodyRenderer.materials) {
            if (mat.name == "Sin (Instance)") continue; // TODO ugly code here
            Util.ChangeColorRandom(mat);
        }

    }

    void ChangeDirection() {
        float yRot = Random.Range(0f, 360f);
        TargetRotation = Quaternion.Euler(0, yRot, 0);
    }
	
	void FixedUpdate () {
        Body.velocity = transform.forward * Speed;
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, RotationSpeed);
        Body.MoveRotation(rotation);
	}
}
