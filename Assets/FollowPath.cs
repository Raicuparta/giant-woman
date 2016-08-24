using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {
    public float Speed = 5;
    public Transform Lookahead; // used to see where we're going
    int Index = 0;
    Rigidbody Body;

    bool LastRight;
    bool LastLeft;

	void Start () {
        Body = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {

        bool roadForward = CheckIfRoad(transform.forward);
        bool roadRight = CheckIfRoad(transform.right);
        bool roadLeft = CheckIfRoad(-transform.right);

        if (!roadForward) Turn();
        else if (roadRight && roadRight != LastRight) MaybeTurn(true);
        else if (roadLeft && roadLeft != LastLeft) MaybeTurn(false);

        LastRight = roadRight;
        LastLeft = roadLeft;

        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        Body.velocity = forward * Speed;
        Vector3 position = transform.position;
        position.y = 1;
        Body.MovePosition(position);
	}

    // checks if there is road in a given direction
    bool CheckIfRoad(Vector3 direction) {
        RaycastHit hit;
        Vector3 origin = Lookahead.position;
        float slope = 3;
        Vector3 forward = -transform.up + direction * slope;
        bool ray = Physics.Raycast(origin, forward, out hit);
        Debug.DrawLine(origin, origin + forward * 5);
        return ray && (hit.transform.tag == "Road");
    }

    void MaybeTurn(bool right) {
        float random = Random.value;
        Debug.Log("maybe turn?");
        if (random < 0.5) {
            Debug.Log("maybe turned!");
            Turn(right ? 90 : -90);
        }
    }

    void Turn(float degrees) {
        Debug.Log("turned");
        float rotY = Body.transform.eulerAngles.y;
        Body.MoveRotation(Quaternion.Euler(0, rotY + degrees, 0));
    }

    void Turn() {
        float rotY = Body.transform.eulerAngles.y;
        float random = Random.value;
        int direction = random > 0.5 ? 1 : -1;
        Turn(90 * direction);
    }
}
