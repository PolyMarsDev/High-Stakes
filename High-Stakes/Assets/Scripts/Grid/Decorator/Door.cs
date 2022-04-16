
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Vector2Int pos;

	void OnEnable() {
		if (CustomGrid.Instance != null) pos = CustomGrid.Instance.SnapCoordinate(transform.position);
		CustomGrid.Instance?.RegisterDoor(this);
	}
}
