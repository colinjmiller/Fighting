using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public Vector3[] waypoints;

	private LineRenderer lineRenderer;
	private int currentWaypointIndex;

	private int currentStage;

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		currentWaypointIndex = 0;
		currentStage = 0;
	}

	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			currentStage++;
		}

		switch (currentStage) {
			case 1:
				showProjectedPath ();
				break;
			case 2:
				hideProjectedPath ();
				break;
			case 3:
				enemyMovements ();
				break;
		}
	}

	void OnCollisionEnter2D() {
        Destroy (gameObject);
	}

	private void showProjectedPath() {
		lineRenderer.positionCount = waypoints.Length;
		lineRenderer.SetPositions (waypoints);
	}

	private void hideProjectedPath() {
		lineRenderer.positionCount = 0;
		lineRenderer.SetPositions(new Vector3[0]);
	}

    private void enemyMovements() {
        if (currentWaypointIndex < waypoints.Length) {
            Vector3 currentWaypoint = waypoints [currentWaypointIndex];
            transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
            if (transform.position == currentWaypoint) {
                currentWaypointIndex++;
            }
        }
    }
}
