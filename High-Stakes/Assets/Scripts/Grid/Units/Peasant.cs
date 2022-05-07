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
			return Grid.VisionCast(pos, delta, Grid.Dist(displacement));
		}
		return false;
	}

	public override Vector2Int GetBestMove() {
		if (CanSeePlayer()) {
			if (CanAttackPlayer()) return Grid.Player.pos;
			Vector2Int delta = GetDelta(Grid.Player.pos - pos);
			Vector2Int desiredPosition = delta + pos;
			if (Grid.CanMoveTo(desiredPosition, DirectionExtension.Convert(delta).Reflect()) 
				&& !HasEnemy(desiredPosition)) return desiredPosition;
		}
		return pos;
	}

	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 0, 0};
		int[] dy = {0, 0, -1, 1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 4; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos, DirectionExtension.Convert(new Vector2Int(dx[k], dy[k])).Reflect())) 
				adjacents.Add(nxtPos);
		}
		return adjacents;
	}

	public override List<Vector2Int> GetVisionPositions() {
		List<Vector2Int> positions = new List<Vector2Int>();
		
		int[] dx = {1, -1, 0, 0};
		int[] dy = {0, 0, -1, 1};

		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			if (IsRightDir(dir)) {
				Vector2Int cur = pos;
				while (CustomGrid.Instance.ValidSquare(cur)) {
					cur += dir;
					if (!CustomGrid.Instance.CanSeeThrough(cur)) positions.Add(cur);
					else break;
				}
			}
		}

		return positions;
	}

	public override List<Vector2Int> GetAttackablePositions() {
		List<Vector2Int> positions = new List<Vector2Int>();
		
		int[] dx = {1, -1, 0, 0};
		int[] dy = {0, 0, -1, 1};

		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			if (IsRightDir(dir)) {
				Vector2Int cur = pos + dir;
				if (CustomGrid.Instance.ValidSquare(cur) && CustomGrid.Instance.CanMoveTo(cur)) 
					positions.Add(cur);
			}
		}

		return positions;
	}
}
