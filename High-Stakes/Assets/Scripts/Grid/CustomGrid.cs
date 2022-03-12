using UnityEngine;
using UnityEngine.Tilemaps;

enum UIIndicator {
	Movable,
	Attackable
}

public class CustomGrid : MonoBehaviour {
	public Tilemap groundGrid;
	public Tilemap UIgrid;

	// Raycast Detection
	public Vector3 GetMouseWorldPosition() {
		return Vector3.zero;
	}

	public void SetUIIndicator() {
	}
}