using UnityEngine;
using System.Collections;

public class Foot : MonoBehaviour {
    [HideInInspector] public Transform Knee;
    Rigidbody Body;
    float MaxDistance;

	void Start () {
        Body = GetComponent<Rigidbody>();
        MaxDistance = Vector3.Distance(Knee.position, transform.position);
    }
	
	void FixedUpdate () {
        // check if there's ground under the foot
        RaycastHit hit;
        bool intersects = Physics.Raycast(Knee.position, Vector3.down, out hit, MaxDistance);
        Debug.DrawLine(Knee.position, Knee.position + Vector3.down*10, Color.red);
        if (!intersects) return;
        Debug.Log("yup");
        // move the foot to the ground
        Body.MoveRotation(Quaternion.LookRotation(transform.forward, hit.normal));
        Body.MovePosition(new Vector3(Knee.position.x, transform.position.y, Knee.position.z));
	}
}
