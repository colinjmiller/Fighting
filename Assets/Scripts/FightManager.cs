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
    private int cursorPosition;
    private bool enemyTurn;
    private Enemy enemyAPI;

    void Awake() {
        cursorPosition = 2;
        enemyTurn = false;
        enemyAPI = enemy.GetComponent<Enemy> ();
    }

	// Update is called once per frame
	void Update () {
        if (enemyTurn && enemyAPI.getStatus() == enemyAPI.COMPLETE) {
            Debug.Log ("Stage " + enemyAPI.getStage () + " has ended");
            enemyAPI.setStage (enemyAPI.getStage() + 1);
            enemyAPI.hideProjectedPath ();
        }

        if (enemyTurn && enemyAPI.getStage () == 1) {
            enemyAPI.showProjectedPath ();
        }

        if (enemyTurn && enemyAPI.getStage () == 3) {
            enemyAPI.move ();
        }

        checkForMenuChanges ();
        if (Input.GetKeyDown (KeyCode.Space)) {
            if (cursorPosition == 0) {
                Debug.Log ("Attempt escape");
            } else if (cursorPosition == 1) {
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
            cursorPosition--;
            if (cursorPosition < 0) {
                cursorPosition = TOTAL_OPTIONS - 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            cursorPosition = (cursorPosition + 1) % TOTAL_OPTIONS;
        }
        cursor.transform.position = getCursorPosition ();
    }

    private Vector3 getCursorPosition() {
        if (cursorPosition == 0) {
            return new Vector3 (cursor.transform.position.x, escapeText.transform.position.y, 0);
        } else if (cursorPosition == 1) {
            return new Vector3 (cursor.transform.position.x, itemText.transform.position.y, 0);
        } else {
            return new Vector3 (cursor.transform.position.x, fightText.transform.position.y, 0);
        }
    }
}
