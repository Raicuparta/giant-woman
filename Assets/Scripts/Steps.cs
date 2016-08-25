using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float KneeSpeed = 20;
    public float Stride = 10; // step distance
    public float Width = 2; // lateral distance between the feet
    float GroundRayHeight = 7;
    public LayerMask GroundLayers;
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

    public void Move() {
        // check if grounded
        bool grounded = Physics.Raycast(transform.position, Vector3.down, GroundRayHeight, GroundLayers);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * GroundRayHeight, Color.blue);

        if (grounded) {
            if (!RightKnee.Anchored && !LeftKnee.Anchored) RightKnee.Anchor();
            RightKnee.Move();
            LeftKnee.Move();
        } else {
            RightKnee.Release();
            LeftKnee.Release();
        }
    }
}
