using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterInput : MonoBehaviour {
    Hands CharacterHands;
    CharacterMovement Movement;

    void Start () {
        Movement = GetComponent<CharacterMovement>();
        CharacterHands = GetComponent<Hands>();
	}
	
	void FixedUpdate () {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        Movement.Move(h, v);


        bool left = CrossPlatformInputManager.GetButton("LB");
        bool right = CrossPlatformInputManager.GetButton("RB");

        Vector3 mouse = CrossPlatformInputManager.mousePosition;
        CharacterHands.Grab(right, left, mouse);
        
    }
}
