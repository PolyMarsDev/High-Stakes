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

	public abstract List<Vector2Int> GetMoveTo();

	public virtual IEnumerator MoveTo(Vector2Int pos) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		yield return null;
	}
	public virtual IEnumerator Capture(Unit unit) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		// Kills the game obsject
        Destroy (unit.gameObject);
        // Removes this script instance from the game object
        Destroy (this);
        // Removes the rigidbody from the game object
        Destroy(GetComponent<Rigidbody>());
		yield return null;
	}
}