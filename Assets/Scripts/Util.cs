using UnityEngine;
using System.Collections;

public class Util {
	public static Vector3 ForceTowards(Vector3 origin, Vector3 target, float intensity) {
        return (target - origin).normalized * intensity;
    }
}
