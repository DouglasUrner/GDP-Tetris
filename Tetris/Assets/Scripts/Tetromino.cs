using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour {
	float lastFall = 0;

	// Use this for initialization
	private void Start () {
		if (!isValidGridPosition()) {
			Debug.Log("Game over.");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	private void Update () {		
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			// Try to move.
			transform.position += Vector3.left;
			
			// Check if it is a valid position.
			if (isValidGridPosition()) {
				moveTetromino();
			} else {
				transform.position += Vector3.right;
			}
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			// Try to move.
			transform.position += Vector3.right;

			// Check if it is a valid position.
			if (isValidGridPosition()) {
				moveTetromino();
			} else {
				transform.position += Vector3.left;
			}
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			// Try to move.
			transform.Rotate(0, 0, -90);

			// Check if it is a valid position.
			if (isValidGridPosition()) {
				moveTetromino();
			} else {
				transform.Rotate(0, 0, 90);
			}
		}
		
		// Move downwards on each tick and Fall
		else if (Input.GetKeyDown(KeyCode.DownArrow) ||
		           Time.time - lastFall >= 1) {
			// Try to move.
			transform.position += Vector3.down;

			// Check if it is a valid position.
			if (isValidGridPosition()) {
				moveTetromino();
			} else {
				transform.position += Vector3.up;
				
				// We've hit bottom, clear any full rows.
				Grid.DeleteFullRows();
				
				// Spawn a new Tetromino.
				FindObjectOfType<Spawner>().spawnNext();
				
				// Disable the script.
				enabled = false;
			}
			lastFall = Time.time;
		}
	}

	bool isValidGridPosition() {
		foreach (Transform child in transform) {
			Vector2 v = Grid.RoundVector2(child.position);

			if (!Grid.InsideBorder(v))
				return false;
			
			// Check for a block in the Well cell that is not part of this Tetromino
			if (Grid.Well[(int) v.x, (int) v.y] != null && 
			    Grid.Well[(int) v.x, (int) v.y].parent != transform) {
				return false;
			}
		}
		return true;
	}

	void moveTetromino() {
		// Remove old children (blocks).
		for (var y = 0; y < Grid.h; y++) {
			for (int x = 0; x < Grid.w; x++) {
				if (Grid.Well[x, y] != null) {
					if (Grid.Well[x,y].parent == transform) {
						Grid.Well[x, y] = null;
					}
				}
			}
		}
		
		// Move, by adding new children (blocks) at the next location of the piece.
		foreach (Transform child in transform) {
			Vector2 v = Grid.RoundVector2(child.position);
			Grid.Well[(int) v.x, (int) v.y] = child;
		}
	}
}
