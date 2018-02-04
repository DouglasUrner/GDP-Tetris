using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

  // Tetrominos
	public GameObject[] tetrominos;

	// Use this for initialization
	void Start () {
		spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnNext() {
		int i = Random.Range(0, tetrominos.Length);
		
		// Spawn a Tetromino at the current location.
		Instantiate(tetrominos[i], transform);
	}
}
