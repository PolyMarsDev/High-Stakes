using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireHunter : Unit
{
    public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 0, 0, 1, -1, -1, 1};
		int[] dy = {0, 0, -1, 1, 1, 1, -1, -1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 8; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}
}
