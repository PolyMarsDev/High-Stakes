using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireHunter : Enemy {
	[SerializeField] int VisionRange = 5;

	Vector2Int GetDelta() {
		Vector2Int displacement = Grid.Player.pos - pos;

		Vector2Int delta = displacement / Grid.Dist(displacement);
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

		if (Grid.IsHorizontal(displacement) || Grid.IsDiagonal(displacement)) {
			Vector2Int delta = GetDelta();
			return Grid.VisionCast(pos, delta, Grid.Dist(delta));
		}

		return false;
	}

	public override Vector2Int GetBestMove() {
		if (CanSeePlayer()) {
			if (CanAttackPlayer()) return Grid.Player.pos;
			Vector2Int delta = GetDelta();
			Vector2Int desiredPosition = pos + delta;
			if (Grid.CanMoveTo(desiredPosition, DirectionExtension.Convert(delta).Reflect()) && !HasEnemy(desiredPosition)) 
				return desiredPosition;
		}
		return pos;
	}
	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 1, -1, 1, -1, 1, -1};
		int[] dy = {1, 1, -1, -1, 1, 1, -1, -1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 8; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos, DirectionExtension.Convert(dx[k], dy[k]).Reflect())
				&& !HasEnemy(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}

	public override List<Vector2Int> GetVisionPositions() {
		List<Vector2Int> positions = new List<Vector2Int>();

		int[] dx = {1, -1, 1, -1, 1, -1, 1, -1};
		int[] dy = {1, 1, -1, -1, 1, 1, -1, -1};
		
		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			Vector2Int cur = pos;
			while (CustomGrid.Instance.ValidSquare(cur)) {
				cur += dir;
				if (!CustomGrid.Instance.CanSeeThrough(cur)) positions.Add(cur);
				else break;
			}
		}

		return positions;
	}
	public override List<Vector2Int> GetAttackablePositions() {
		List<Vector2Int> positions = new List<Vector2Int>();
		
		int[] dx = {1, -1, 1, -1, 1, -1, 1, -1};
		int[] dy = {1, 1, -1, -1, 1, 1, -1, -1};

		for (int k = 0; k < 4; k++) {
			Vector2Int dir = new Vector2Int(dx[k], dy[k]);
			Vector2Int cur = pos + dir;
			if (CustomGrid.Instance.ValidSquare(cur) && CustomGrid.Instance.CanMoveTo(cur)) 
				positions.Add(cur);
		}

		return positions;
	}
}
