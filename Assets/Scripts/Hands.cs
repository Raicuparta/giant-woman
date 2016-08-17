using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour {
    public float Speed = 10;
    public Hand RightHand;
    public Hand LeftHand;
    public Camera GameCamera;

    void Start() {
        RightHand.Speed = Speed;
        LeftHand.Speed = Speed;
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
        else RightHand.LetGo();
        if (left) LeftHand.Reach(point);
        else LeftHand.LetGo();
    }
}
