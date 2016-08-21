using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour {
    public float Speed = 10;
    public float MinGrabHeight = 3; // maximum height difference between player and grabbed object
    public float LoweringSpeed = 2; // speed at which the body rotates when grabbing low objects
    public Hand RightHand;
    public Hand LeftHand;
    public Camera GameCamera;
    Rigidbody ParentBody;

    void Start() {
        RightHand.Speed = Speed;
        LeftHand.Speed = Speed;
        ParentBody = GetComponent<Rigidbody>();
    }

    public void Grab(bool right, bool left, Vector3 mouse) {
        // Shoot a ray to determine what object was clicked
        Ray ray = GameCamera.ScreenPointToRay(mouse);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (!hit) return;
        Vector3 point = hitInfo.point;
        // Grab that object
        if (right) RightHand.Reach(point);
        //else RightHand.LetGo();
        if (left) LeftHand.Reach(point);
        //else LeftHand.LetGo();

        if (!right && !left) return;

        // rotate the body if the object we wanna grab is too low
        if (transform.position.y + MinGrabHeight > point.y) {
            Vector3 forward = point - (ParentBody.position + Vector3.up * 5);
            Quaternion target = Quaternion.LookRotation(Vector3.down, transform.up);
            Quaternion rotation = Quaternion.RotateTowards(ParentBody.rotation, target, LoweringSpeed);
            ParentBody.MoveRotation(rotation);
        }
    }

    public void Press(bool right, bool left) {
        if (right) RightHand.LetGo();
        if (left) LeftHand.LetGo();
    }
}
