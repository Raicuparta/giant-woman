using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour {
    public float Speed = 10;
    public float MinGrabHeight = 3; // maximum height difference between player and grabbed object
    public float LoweringSpeed = 2; // speed at which the body rotates when grabbing low objects
    public float MaxGrabDistance = 100;
    public Hand RightHand;
    public Hand LeftHand;
    public Camera GameCamera;
    public LayerMask GrabbableLayers;
    Rigidbody ParentBody;

    void Start() {
        RightHand.Speed = Speed;
        LeftHand.Speed = Speed;
        ParentBody = GetComponent<Rigidbody>();
    }

    // convert mouse coordinate to 3D world coordinates
    Vector3 MouseToWorld(Vector3 mouse) {
        Ray ray = GameCamera.ScreenPointToRay(mouse);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(ray, out hitInfo, MaxGrabDistance, GrabbableLayers.value);
        Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
        return hitInfo.point;
    }

    public void Grab(bool right, bool left, Vector3 mouse) {
        Vector3 point = MouseToWorld(mouse);
        // Grab that object
        if (right) RightHand.Reach(point);
        else RightHand.StopReaching();
        if (left) LeftHand.Reach(point);
        else LeftHand.StopReaching();

        if (!right && !left) return;

        // rotate the body if the object we wanna grab is too low
        if (transform.position.y + MinGrabHeight > point.y) {
            Vector3 forward = point - (ParentBody.position);
            Quaternion target = Quaternion.LookRotation(forward, transform.up);
            Quaternion rotation = Quaternion.RotateTowards(ParentBody.rotation, target, LoweringSpeed);
            ParentBody.MoveRotation(rotation);
        }
    }

    public void Press(bool right, bool left, Vector3 mouse) {
        Vector3 target = MouseToWorld(mouse);
        if (right) RightHand.ThrowTowards(target);
        if (left) LeftHand.ThrowTowards(target);
    }
}
