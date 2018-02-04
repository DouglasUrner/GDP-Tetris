using UnityEngine;

public class Spawner : MonoBehaviour {

  // Tetrominos
	public GameObject[] Tetrominos;

	// Use this for initialization
	void Start () {
		SpawnNext();
	}

	public void SpawnNext() {
		int i = Random.Range(0, Tetrominos.Length);
		
		// Spawn a Tetromino at the current location.
		Instantiate(Tetrominos[i], transform);
	}
}
