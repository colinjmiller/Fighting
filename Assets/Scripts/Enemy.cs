using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public Vector3[] waypoints;
	public float stageZeroDuration;
	public float stageOneDuration;
	public float stageTwoDuration;

	private LineRenderer lineRenderer;
	private int currentWaypointIndex;
	private int currentStage;
	private float currentTime;

	private bool inCombat;

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		inCombat = false;
		reset ();
	}

	void Update () {
		if (!inCombat) {
			return;
		}

		currentTime += Time.deltaTime;
		updateStage ();

		if (currentStage == 1) {
			showProjectedPath ();
		} else if (currentStage == 2) {
			hideProjectedPath ();
		} else if (currentStage == 3) {
			enemyMovements ();
		}
	}

	void OnCollisionEnter2D() {
        Destroy (gameObject);
	}

	public void beginCombat() {
		inCombat = true;
		reset ();
	}

	private void reset() {
		currentTime = 0f;
		currentStage = 0;
		currentWaypointIndex = 0;
	}

	private void updateStage() {
		bool moveToNextStage = (currentStage == 0 && currentTime > stageZeroDuration);
		moveToNextStage = moveToNextStage || (currentStage == 1 && currentTime > (stageZeroDuration + stageOneDuration));
		moveToNextStage = moveToNextStage || (currentStage == 2 && currentTime > (stageZeroDuration + stageOneDuration + stageTwoDuration));

		if (moveToNextStage) {
			currentStage++;
		}
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
