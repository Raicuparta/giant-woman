using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterInput : MonoBehaviour {
    CharacterMovement Movement;

    void Start () {
        Movement = GetComponent<CharacterMovement>();
	}
	
	void Update () {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Movement.Move(h, v);
    }
}
