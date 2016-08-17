using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float Speed = 20; // movement speed
    public float Stride = 10; // step distance
    public float Width = 2; // lateral distance between the feet
    public Transform Right;
    public Transform Left;

    void Start () {
        Step rStep = Right.gameObject.AddComponent<Step>();
        Step lStep = Left.gameObject.AddComponent<Step>();
        rStep.Speed = Speed;
        lStep.Speed = Speed;
        rStep.Stride = Stride;
        lStep.Stride = Stride;
        rStep.Width = Width;
        lStep.Width = Width;
        rStep.OtherFoot = lStep;
        lStep.OtherFoot = rStep;
        rStep.Anchored = true;
        lStep.Anchored = false;
    }
}
