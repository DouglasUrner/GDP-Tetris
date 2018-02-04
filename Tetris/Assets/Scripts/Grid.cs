using UnityEngine;

public class Grid : MonoBehaviour {
	public const int Width = 10;
	public const int Height = 20;
	// Tronsforms use a 3D position, even in a 2D game.
	public static readonly Transform[,] Well = new Transform[Width, Height];

	public static Vector2 RoundVector2(Vector2 v) {
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static bool InsideBorder(Vector2 pos) {
		return (int) pos.x >= 0 && (int) pos.x < Width && (int) pos.y >= 0;
	}

	public static void DeleteFullRows() {
		for (var y = 0; y < Height; y++) {
			if (!IsRowFull(y)) {
				continue;
			}

			DeleteRow(y);
			DropRowsAbove(y + 1);
			y--;
		}
	}

	private static bool IsRowFull(int y) {
		for (var x = 0; x < Width; x++) {
			if (Well[x, y] == null) {
				return false;
			}
		}

		return true;
	}

	private static void DeleteRow(int y) {
		for (var x = 0; x < Width; x++) {
			Destroy(Well[x, y].gameObject);
			Well[x, y] = null;
		}
	}

	private static void DropRowsAbove(int y) {
		for (var i = y; i < Height; i++) {
			DropRow(i);
		}
	}

	private static void DropRow(int y) {
		for (var x = 0; x < Width; x++) {
			if (Well[x, y] == null) {
				continue;
			}

			Well[x, y - 1] = Well[x, y];
			Well[x, y] = null;

			Well[x, y - 1].position += Vector3.down;
		}
	}
}
