using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {

	public static Vector2[] vector3ArrayToVector2Array(Vector3[] v3Array) {
		return v3Array.Select (v => new Vector2 (v.x, v.y)).ToArray ();
	}
}
