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

    void FixedUpdate() {
        // check if there's ground under the foot
        RaycastHit hit;
        bool intersects = Physics.Raycast(Knee.position, Vector3.down, out hit, MaxDistance * 2, LayerMask.GetMask("Ground"));
        Debug.DrawLine(Knee.position, Knee.position + Vector3.down*10, Color.red);
        if (!intersects) return;
        // move the foot to the ground
        Vector3 forward = Vector3.Cross(hit.normal, -transform.parent.right).normalized;
        Body.MoveRotation(Quaternion.LookRotation(forward, hit.normal));
        Body.MovePosition(hit.point);
	}
}
