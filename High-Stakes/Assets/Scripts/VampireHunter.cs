using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireHunter : Enemy {
	[SerializeField] int VisionRange = 5;

	bool IsRightDir(Vector2Int dir) {
		return Mathf.Abs(dir.x * dir.y) == 2;
	}

	public override bool CanAttackPlayer() {
		if (!Grid.Player) return false;
		Vector2Int displacement = Grid.Player.pos - pos;
		return IsRightDir(displacement);
	}

	public override bool CanSeePlayer() {
		if (!Grid.Player) return false;
		Vector2Int displacement = Grid.Player.pos - pos;
		return Mathf.Abs(displacement.x) + 
			Mathf.Abs(displacement.y) <= VisionRange;
	}

	public override Vector2Int GetBestMove() {
		if (CanSeePlayer()) {
			if (CanAttackPlayer()) return Grid.Player.pos;

			// Can be made more efficient (by only runnning it once instead of once per vampire hunter) but I don't think we need to given the grid size
			Queue<Vector2Int> pq = new Queue<Vector2Int>();
			pq.Enqueue(Grid.Player.pos);
			int[,] dist = new int[Grid.Size.x, Grid.Size.y];
			int INFTY = (int) (1e9);
			for (int i = 0; i < Grid.Size.x; i++)
			for (int j = 0; j < Grid.Size.y; j++)
				dist[i,j] = INFTY;
			dist[0,0] = 0;
			
			int[] dx = {1, -1, 1, -1, 2, -2, 2, -2};
			int[] dy = {2, 2, -2, -2, 1, 1, -1, -1};

			while (pq.Count > 0) {
				Vector2Int current = pq.Dequeue();

				int x = current.x, y = current.y;

				for (int k = 0; k < 8; k++) {
					Vector2Int nxt = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
					if (Grid.ValidSquare(nxt) && dist[nxt.x,nxt.y] == INFTY && Grid.CanMoveTo(nxt) && !HasEnemy(nxt)) {
						dist[nxt.x, nxt.y] = dist[x,y] + 1;
						pq.Enqueue(nxt);
					}
				}
			}

			Vector2Int candidate = new Vector2Int(-INFTY,-INFTY);

			foreach (Vector2Int nxtpos in GetMoveTo())
				if (candidate.x == -INFTY || dist[nxtpos.x, nxtpos.y] < dist[candidate.x, candidate.y])
					candidate = nxtpos;
			if (candidate.x != -INFTY)
				return candidate;
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
