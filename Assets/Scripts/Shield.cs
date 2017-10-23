using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	private LineRenderer lineRenderer;
	private EdgeCollider2D edgeCollider;

	private List<Vector3> points;

	void Awake () {
		points = new List<Vector3> ();
		lineRenderer = GetComponent<LineRenderer> ();
		edgeCollider = GetComponent<EdgeCollider2D> ();
	}

	void OnCollisionEnter2D() {
		Debug.Log ("Collided!");
	}
	
	public void addPoint(Vector3 point) {
		points.Add (point);
		updateShield ();
	}

	private void updateShield() {
		lineRenderer.positionCount = points.Count;
		lineRenderer.SetPositions (points.ToArray ());

		if (points.Count >= 2) {
			edgeCollider.points = Util.vector3ArrayToVector2Array (points.ToArray ());
		}

	}
}
