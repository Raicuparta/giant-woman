using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float KneeSpeed = 20;
    public float Stride = 10; // step distance
    public float Width = 2; // lateral distance between the feet
    public Knee RightKnee;
    public Knee LeftKnee;
    public Foot RightFoot;
    public Foot LeftFoot;

    void Awake() {
        RightKnee.Speed = KneeSpeed;
        LeftKnee.Speed = KneeSpeed;
        RightKnee.Stride = Stride;
        LeftKnee.Stride = Stride;
        RightKnee.Width = Width;
        LeftKnee.Width = Width;
        RightKnee.OtherFoot = LeftKnee;
        LeftKnee.OtherFoot = RightKnee;
        RightKnee.Anchored = true;
        LeftKnee.Anchored = false;
        RightFoot.Knee = RightKnee.transform;
        LeftFoot.Knee = LeftKnee.transform;
    }

    public void Move(float h) {
        RightKnee.Move();
        LeftKnee.Move();

        Transform foot = RightKnee.Anchored ? RightFoot.transform : LeftFoot.transform;

        Rigidbody Body = transform.GetComponent<Rigidbody>();
        Quaternion q = Quaternion.AngleAxis(h, Vector3.up);
        Body.MovePosition(q * (Body.transform.position - foot.position) + foot.position);
        Body.MoveRotation(Body.transform.rotation * q);
    }
}
