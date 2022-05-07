using UnityEngine;

public enum Direction {
	LEFT = 0,
	RIGHT = 1,
	UP = 2,
	DOWN = 3
}

static class DirectionExtension {
	static int[] dx = {-1, 1, 0, 0};
	static int[] dy = {0, 0, 1, -1};

	public static bool IsHorizontal(this Direction direction) => (int) direction < 2;
	public static bool IsVertical(this Direction direction) => (int) direction >= 2;
	public static Direction Reflect(this Direction direction) => (Direction) (((int) direction) ^ 1);
	public static Direction Convert(Vector2Int dir) {
		for (int k = 1; k < 4; k++) {
			Vector2Int point = new Vector2Int(dx[k], dy[k]);
			if (point == dir) return (Direction) k;
		}
		return Direction.LEFT;
	}
	public static Direction Convert(int x, int y) => Convert(new Vector2Int(x,y));
	public static string ToString(this Direction direction) {
		if (direction == Direction.LEFT) return "LEFT";
		if (direction == Direction.RIGHT) return "RIGHT";
		if (direction == Direction.UP) return "UP";
		if (direction == Direction.DOWN) return "DOWN";
		return "WHAT";
	}
	public static Vector2Int GetRealDir(this Direction direction) {
		return new Vector2Int(dx[(int) direction], dy[(int) direction]);
	}
}