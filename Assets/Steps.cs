using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour {
    public float Speed = 20; // movement speed
    public float Stride = 10; // step distance
    public float Width = 2; // lateral distance between the feet
    public Step Right;
    public Step Left;

    void Start () {
        Right.Speed = Speed;
        Left.Speed = Speed;
        Right.Stride = Stride;
        Left.Stride = Stride;
        Right.Width = Width;
        Left.Width = Width;
        Right.OtherFoot = Left;
        Left.OtherFoot = Right;
    }
}
