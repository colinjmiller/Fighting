using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAPI : MonoBehaviour {

	public Camera mainCamera;
	public GameObject shieldPrefab;

	private GameObject currentShieldDrawing;

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 point = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			point.z = 0;
			currentShieldDrawing = Instantiate (shieldPrefab);
			currentShieldDrawing.GetComponent<Shield> ().addPoint (point);
		} else if (Input.GetMouseButton (0)) {
			Vector3 point = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			point.z = 0;
			currentShieldDrawing.GetComponent<Shield> ().addPoint (point);
		}
	}
}
