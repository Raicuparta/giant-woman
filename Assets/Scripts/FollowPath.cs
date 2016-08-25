using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {
    public float Speed = 5;
    Rigidbody Body;
    Road CurrentRoad;
    bool Grounded;
    public Vector3 Target;
    float MaxGroundDistance = 0.5f;
    float RotationSpeed = 10;

	void Start() {
        Body = GetComponent<Rigidbody>();
        Body.centerOfMass = Vector3.down;
	}
	
	void FixedUpdate() {
        Road road = GetCurrentRoad();
        if (road != CurrentRoad && road != null) {
            CurrentRoad = road;
            Target = PickTarget();
        }
        if (Grounded) MoveTowardsTarget();
	}

    void MoveTowardsTarget() {
        Vector3 forward = Vector3.ProjectOnPlane((Target - transform.position), Vector3.up);
        Body.velocity = forward.normalized * Speed;
        //Body.AddForce(forward.normalized )
        bool targetBehind = Util.IsInFront(transform.position + transform.forward * 2, Target, transform.forward);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 2);
        Quaternion rotation;
        if (targetBehind) {
            Vector3 angle = transform.eulerAngles;
            angle.y -= RotationSpeed;
            //Body.MovePosition(transform.position + transform.forward * 0.3f);
            Body.velocity = Vector3.zero;
            rotation = Quaternion.Euler(angle);
        } else {
            rotation = Quaternion.RotateTowards(Body.rotation, Quaternion.LookRotation(forward), RotationSpeed);
        }
        Body.MoveRotation(rotation);
    }

    // pick one of the connections randomly
    Vector3 PickTarget() {
        List<Vector3> connections = CurrentRoad.Connections;
        if (connections.Count == 1) return CurrentRoad.Connections[0];

        List<Vector3> targets = new List<Vector3>();
        foreach (Vector3 connection in CurrentRoad.Connections) {
            bool inFront = Util.IsInFront(connection, transform.position - transform.forward * 2, transform.forward);
            if (inFront) targets.Add(connection);
        }
        int index = Random.Range(0, targets.Count);
        return targets[index];
    }

    // get the road that's currently under the car
    Road GetCurrentRoad() {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = -transform.up;
        Grounded = Physics.Raycast(origin, direction, out hit, MaxGroundDistance);
        Debug.DrawLine(origin, origin + direction * MaxGroundDistance);
        if (!Grounded) return null;
        return hit.transform.GetComponent<Road>();
    }
}
