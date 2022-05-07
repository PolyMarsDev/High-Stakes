using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Enemy {
	public override bool CanAttackPlayer() {
		if (!Grid.Player) return false;

		Vector2Int displacement = Grid.Player.pos - pos;
		Debug.Log(displacement);
		return (displacement.x == displacement.y || displacement.x == -displacement.y) && displacement.x == 1;
	}

	public override bool CanSeePlayer() {
		if (!Grid.Player) return false;

		Vector2Int displacement = Grid.Player.pos - pos;
		if (Grid.IsDiagonal(displacement)) {
			Vector2Int delta = displacement / Mathf.Abs(displacement.x);
			return Grid.VisionCast(pos, delta, Grid.Dist(displacement));
		}
		// if (displacement != Vector2Int.zero && 
		// 	(displacement.x == displacement.y || displacement.x == -displacement.y)) {

		// 	Vector2Int delta = displacement / Mathf.Abs(displacement.x);

		// 	for (int dist = 1; ; dist++) {
		// 		Vector2Int nxt = delta * dist + pos;
		// 		if (!Grid.ValidSquare(nxt)) return false; // Shouldn't ever run but I put it here just in case
		// 		if (Grid.GetUnitAt(nxt) is Player) return true;
		// 		if (!Grid.CanSeeThrough(nxt)) return false;
		// 	}
		// }
		return false;
	}

	public override Vector2Int GetBestMove() {
		if (CanSeePlayer()) {
			if (CanAttackPlayer()) return Grid.Player.pos;
			Vector2Int displacement = Grid.Player.pos - pos;
			Vector2Int desiredPosition = displacement / Mathf.Abs(displacement.x) + pos;
			Debug.Log(desiredPosition + " " + Grid.CanMoveTo(desiredPosition));
			if (Grid.CanMoveTo(desiredPosition) && !HasEnemy(desiredPosition)) 
				return desiredPosition;
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

	public override List<Vector2Int> GetVisionPositions() {
		List<Vector2Int> positions = new List<Vector2Int>();

		int[] dx = {1, -1, 1, -1};
		int[] dy = {1, 1, -1, -1};
		
		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			Vector2Int cur = pos;
			while (CustomGrid.Instance.ValidSquare(cur)) {
				cur += dir;
				if (CustomGrid.Instance.CanSeeThrough(cur)) positions.Add(cur);
				else break;
			}
		}

		return positions;
	}
	public override List<Vector2Int> GetAttackablePositions() {
		List<Vector2Int> positions = new List<Vector2Int>();
		
		int[] dx = {1, -1, 1, -1};
		int[] dy = {1, 1, -1, -1};

		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			Vector2Int cur = pos + dir;
			if (CustomGrid.Instance.ValidSquare(cur) && CustomGrid.Instance.CanMoveTo(cur)) 
				positions.Add(cur);
		}

		return positions;
	}
}
