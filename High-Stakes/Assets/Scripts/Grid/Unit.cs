using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public abstract class Unit : MonoBehaviour {
	[HideInInspector]
	public Vector2Int pos;

	void OnEnable() {
		pos = CustomGrid.Instance.SnapCoordinate(transform.position);
		CustomGrid.Instance?.AddUnit(pos, this);
	}

	void OnDisable() {
		CustomGrid.Instance?.RemoveUnit(this);
	}

	public abstract List<Vector2Int> GetAdjacent();

	public virtual IEnumerator MoveTo(Vector2Int pos) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		yield return null;
	}
	public virtual IEnumerator Capture(Unit unit) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		Destroy(unit.gameObject);
		yield return null;
	}
}