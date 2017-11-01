using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public Vector3[] waypoints; // TODO: Make part of projectile
	public float stageZeroDuration;
	public float stageOneDuration;
	public float stageTwoDuration;
    public GameObject[] projectiles;

    public string BLOCKING = "blocking";
    public string COMPLETE = "complete";

	private LineRenderer lineRenderer;
	private int currentWaypointIndex;
	private int currentStage;
	private float currentTime;
    private GameObject[] currentProjectiles;

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update () {
		currentTime += Time.deltaTime;
	}

	void OnCollisionEnter2D() {
        Destroy (gameObject);
	}

    public void setStage(int stage) {
        currentStage = stage;
        currentTime = 0f;

        // TODO: Break this out into an array of methods so I can do things like
        //       stagePrep[stage]();
        if (stage == 3) {
            currentProjectiles = new GameObject[projectiles.Length];
            for (int i = 0; i < projectiles.Length; i++) {
                currentProjectiles [i] = Instantiate (projectiles [i], transform);
            }
        }
    }

    public int getStage() {
        return currentStage;
    }

    public string getStatus() {
        if (currentStage == 0) {
            return (currentTime > stageZeroDuration) ? COMPLETE : BLOCKING;
        } else if (currentStage == 1) {
            return (currentTime > stageOneDuration) ? COMPLETE : BLOCKING;
        } else if (currentStage == 2) {
            return (currentTime > stageTwoDuration) ? COMPLETE : BLOCKING;
        } else {
            return (currentWaypointIndex == waypoints.Length) ? COMPLETE : BLOCKING;
        }
    }

	public void showProjectedPath() {
		lineRenderer.positionCount = waypoints.Length;
		lineRenderer.SetPositions (waypoints);
	}

	public void hideProjectedPath() {
		lineRenderer.positionCount = 0;
		lineRenderer.SetPositions(new Vector3[0]);
	}

    // TODO: This needs to move to an individual projectile script. This is ludicrous.
    public void move() {
        foreach (GameObject projectile in currentProjectiles) {
            if (currentWaypointIndex < waypoints.Length) {
                Vector3 currentWaypoint = waypoints [currentWaypointIndex];
                transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
                if (transform.position == currentWaypoint) {
                    currentWaypointIndex++;
                }
            }
        }
    }
}
