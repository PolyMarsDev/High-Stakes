using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour {
	[HideInInspector]
	public Vector2Int pos;

	protected CustomGrid Grid => CustomGrid.Instance;

	void OnEnable() {
		if (CustomGrid.Instance != null)
			pos = CustomGrid.Instance.SnapCoordinate(transform.position);
		CustomGrid.Instance?.AddUnit(pos, this);
	}

	void OnDisable() {
		CustomGrid.Instance?.RemoveUnit(this);
	}

	public abstract List<Vector2Int> GetMoveTo();

	public virtual IEnumerator MoveTo(Vector2Int pos) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		this.pos = pos;
		yield return null;
	}
	public virtual IEnumerator Capture(Unit unit) {
		transform.position = CustomGrid.Instance.GridToWorld((Vector3Int) unit.pos);
		// Kills the game other obsject
		unit.Kill();

		// An : Not sure what the following 4 lines does
        // Removes this script instance from the game object
        // Destroy (this);
        // Removes the rigidbody from the game object
        // Destroy(GetComponent<Rigidbody>());
		yield return null;
	}

	public virtual void Kill() => Destroy(gameObject);

	
}