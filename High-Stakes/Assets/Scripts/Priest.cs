using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Enemy {
	public override bool CanAttackPlayer() {
		if (!Grid.Player) return false;

		Vector2Int displacement = Grid.Player.pos - pos;
		return (displacement.x == displacement.y || displacement.x == -displacement.y) && displacement.x == 1;
	}

	public override bool CanSeePlayer() {
		if (!Grid.Player) return false;

		Vector2Int displacement = Grid.Player.pos - pos;
		if (displacement != Vector2Int.zero && 
			(displacement.x == displacement.y || displacement.x == -displacement.y)) {

			Vector2Int delta = displacement / Mathf.Abs(displacement.x);

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
			Vector2Int displacement = Grid.Player.pos - pos;
			return displacement / Mathf.Abs(displacement.x) + pos;
		}
		return pos;
	}

	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, -1, 1};
		int[] dy = {1, 1, -1, -1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 4; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (Grid.CanMoveTo(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}
}
