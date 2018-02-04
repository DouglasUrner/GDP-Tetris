using UnityEngine;

public class Grid : MonoBehaviour {
	public static int w = 10;
	public static int h = 20;
	// Tronsforms use a 3D position, even in a 2D game.
	public static readonly Transform[,] Well = new Transform[w, h];

	public static Vector2 RoundVector2(Vector2 v) {
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static bool InsideBorder(Vector2 pos) {
		return (int) pos.x >= 0 && (int) pos.x < w && (int) pos.y >= 0;
	}

	public static void DeleteFullRows() {
		for (int y = 0; y < h; y++) {
			if (IsRowFull(y)) {
				DeleteRow(y);
				DropRowsAbove(y + 1);
				y--;
			}
		}
	}

	static bool IsRowFull(int y) {
		for (int x = 0; x < w; x++) {
			if (Well[x, y] == null) {
				return false;
			}
		}

		return true;
	}

	static void DeleteRow(int y) {
		for (int x = 0; x < w; x++) {
			Destroy(Well[x, y].gameObject);
			Well[x, y] = null;
		}
	}

	static void DropRowsAbove(int y) {
		for (int i = y; i < h; i++) {
			DropRow(i);
		}
	}

	static void DropRow(int y) {
		for (int x = 0; x < w; x++) {
			if (Well[x, y] != null) {
				Well[x, y - 1] = Well[x, y];
				Well[x, y] = null;
				
				Well[x, y - 1].position += Vector3.down;
			}
		}
	}
}
