using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Enemy {
	public bool movesVertically;

	bool IsRightDir(Vector2Int dir) => dir != Vector2Int.zero && 
		(dir.x == 0 && movesVertically) || (0 == dir.y && !movesVertically);
	Vector2Int GetDelta(Vector2Int dir) =>
		dir / Mathf.Abs(dir.x + dir.y);

	public override bool CanAttackPlayer() {
		if (!Grid.Player) return false;
		return IsRightDir(Grid.Player.pos - pos) && Vector2Int.Distance(Grid.Player.pos, pos) == 1;
	}

	public override bool CanSeePlayer() {
		if (!Grid.Player) return false;
		Vector2Int displacement = Grid.Player.pos - pos;

		if (IsRightDir(displacement)) {
			Vector2Int delta = GetDelta(displacement);
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
			return GetDelta(Grid.Player.pos - pos) + pos;
		}
		return pos;
	}

	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 0, 0};
		int[] dy = {0, 0, -1, 1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 4; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}
}