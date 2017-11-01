using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    public GameObject cursor;
    public GameObject fightText;
    public GameObject itemText;
    public GameObject escapeText;

    public GameObject enemy;

    private int TOTAL_OPTIONS = 3;
    private int TOTAL_ENEMY_STAGES = 3;
    private int menuPosition;
    private bool enemyTurn;
    private Enemy enemyAPI;

    void Awake() {
        menuPosition = 2;
        enemyTurn = false;
        enemyAPI = enemy.GetComponent<Enemy> ();
    }

	// Update is called once per frame
	void Update () {
        if (enemyTurn && enemyAPI.getStatus() == enemyAPI.COMPLETE) {
            Debug.Log ("Stage " + enemyAPI.getStage () + " has ended");

            if (enemyAPI.getStage () == TOTAL_ENEMY_STAGES) {
                Debug.Log ("Enemy's turn has ended");
                enemyTurn = false;
            } else {
                enemyAPI.setStage (enemyAPI.getStage() + 1);
            }
        }

        if (enemyTurn && enemyAPI.getStage () == 1) {
            enemyAPI.showProjectedPath ();
        }

        if (enemyTurn && enemyAPI.getStage () == 2) {
            enemyAPI.hideProjectedPath ();
        }

        if (enemyTurn && enemyAPI.getStage () == 3) {
            enemyAPI.move ();
        }

        checkForMenuChanges ();
        if (Input.GetKeyDown (KeyCode.Space)) {
            if (menuPosition == 0) {
                Debug.Log ("Attempt escape");
            } else if (menuPosition == 1) {
                Debug.Log ("Call unimplemented item API");
            } else {
                enemyTurn = true;
                enemyAPI.setStage (0);
                Debug.Log ("Enemy turn begins");
            }
        }
	}

    private void checkForMenuChanges() {
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            menuPosition--;
            if (menuPosition < 0) {
                menuPosition = TOTAL_OPTIONS - 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            menuPosition = (menuPosition + 1) % TOTAL_OPTIONS;
        }
        cursor.transform.position = getMenuPosition ();
    }

    private Vector3 getMenuPosition() {
        if (menuPosition == 0) {
            return new Vector3 (cursor.transform.position.x, escapeText.transform.position.y, 0);
        } else if (menuPosition == 1) {
            return new Vector3 (cursor.transform.position.x, itemText.transform.position.y, 0);
        } else {
            return new Vector3 (cursor.transform.position.x, fightText.transform.position.y, 0);
        }
    }
}
