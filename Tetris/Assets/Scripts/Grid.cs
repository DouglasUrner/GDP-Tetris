using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	public static int w = 10;
	public static int h = 20;
	// Tronsforms use a 3D position, even in a 2D game.
	public static Transform[,] grid = new Transform[w, h];

	public static Vector2 RoundVector2(Vector2 v) {
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static bool insideBorder(Vector2 pos) {
		return (int) pos.x >= 0 && (int) pos.x < w && (int) pos.y >= 0;
	}

	public static void deleteFullRows() {
		for (int y = 0; y < h; y++) {
			if (isRowFull(y)) {
				deleteRow(y);
				dropRowsAbove(y + 1);
				y--;
			}
		}
	}

	static bool isRowFull(int y) {
		for (int x = 0; x < w; x++) {
			if (grid[x, y] == null) {
				return false;
			}
		}

		return true;
	}

	static void deleteRow(int y) {
		for (int x = 0; x < w; x++) {
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
	}

	static void dropRowsAbove(int y) {
		for (int i = y; i < h; i++) {
			dropRow(i);
		}
	}

	static void dropRow(int y) {
		for (int x = 0; x < w; x++) {
			if (grid[x, y] != null) {
				grid[x, y - 1] = grid[x, y];
				grid[x, y] = null;
				
				grid[x, y - 1].position += Vector3.down;
			}
		}
	}
}
