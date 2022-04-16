using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireHunter : Enemy {
	[SerializeField] int VisionRange = 5;

	bool IsHorizontal(Vector2Int dir) => dir != Vector2Int.zero && 
		((dir.x == 0) || (dir.y == 0));

	bool IsDiagonal(Vector2Int dir) => dir != Vector2Int.zero &&
		(dir.x == dir.y || dir.x == -dir.y);

	Vector2Int GetDelta() {
		Vector2Int displacement = Grid.Player.pos - pos;

		Vector2Int delta = displacement /
			Mathf.Max((Mathf.Abs(displacement.x)), Mathf.Abs(displacement.y));
		return delta;
	}

	public override bool CanAttackPlayer() {
		if (!Grid.Player) return false;
		Vector2Int displacement = Grid.Player.pos - pos;
		return Mathf.Max(
			Mathf.Abs(displacement.x),
			Mathf.Abs(displacement.y)
		) == 1;
	}

	public override bool CanSeePlayer() {
		if (!Grid.Player) return false;

		Vector2Int displacement = Grid.Player.pos - pos;

		if (IsHorizontal(displacement) || IsDiagonal(displacement)) {
			Vector2Int delta = GetDelta();

			for (int dist = 1; ; dist++) {
				Vector2Int nxt = delta * dist + pos;
				if (!Grid.ValidSquare(nxt)) return false; // Shouldn't ever run but I put it here just in case
				if (Grid.GetUnitAt(nxt) is Player) return true;
				if (!Grid.CanSeeThrough(nxt)) return false;
			}
		}

		return false;
	}

	public override Vector2Int GetBestMove() {
		if (CanSeePlayer()) {
			if (CanAttackPlayer()) return Grid.Player.pos;
			Vector2Int delta = GetDelta();
			Vector2Int desiredPosition = pos + delta;
			if (Grid.CanMoveTo(desiredPosition) && !HasEnemy(desiredPosition)) 
				return desiredPosition;
		}
		return pos;
	}
	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 1, -1, 2, -2, 2, -2};
		int[] dy = {2, 2, -2, -2, 1, 1, -1, -1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 8; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos) && !HasEnemy(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}
}
