using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Events;
using NaughtyAttributes;
using Effects;

[RequireComponent(typeof(Animator))]
public class Player : LiveUnit {

	[Header("Particle Combos")]
	[HorizontalLine(color: EColor.Red)]
	[SerializeField] ParticleCombo DashCombo;

	public override List<Vector2Int> GetMoveTo() {
		int[] dx = {1, -1, 0, 0};
		int[] dy = {0, 0, -1, 1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 4; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			Direction entranceDir = DirectionExtension.Convert(dx[k], dy[k]).Reflect();
			// Debug.Log(entranceDir.ToString());
			if (CustomGrid.Instance.CanMoveTo(nxtPos, entranceDir)) 
				adjacents.Add(nxtPos);
		}
		return adjacents;
	}

	public List<Vector2Int> GetSpecialMoveTo() {
		int[] dx = {1, -1, 1, -1, 2, -2, 2, -2};
		int[] dy = {2, 2, -2, -2, 1, 1, -1, -1};

		List<Vector2Int> adjacents = new List<Vector2Int>();
		for (int k = 0; k < 8; k++) {
			Vector2Int nxtPos = pos + Vector2Int.right * dx[k] + Vector2Int.up * dy[k];
			if (CustomGrid.Instance.CanMoveTo(nxtPos)) adjacents.Add(nxtPos);
		}
		return adjacents;
	}

	public override IEnumerator Capture(Unit unit) {
		Vector2Int dist = unit.pos - this.pos;
		bool dashing = (Mathf.Abs(dist.x) > 1 || Mathf.Abs(dist.y) > 1);
		if (dashing) DashCombo.Activate();
		yield return base.Capture(unit);
		if (dashing) DashCombo.Stop();
	}

	public override IEnumerator MoveTo(Vector2Int pos) {
		Vector2Int dist = pos - this.pos;
		bool dashing = (Mathf.Abs(dist.x) > 1 || Mathf.Abs(dist.y) > 1);
		if (dashing) DashCombo.Activate();
		yield return base.MoveTo(pos);
		if (dashing) DashCombo.Stop();
	}
}