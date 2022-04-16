public enum Direction {
	LEFT = 0,
	RIGHT = 1,
	UP = 2,
	DOWN = 3
}

static class DirectionExtension {
	public static bool IsHorizontal(this Direction direction) => (int) direction <= 2;
	public static bool IsVertical(this Direction direction) => (int) direction > 2;
}