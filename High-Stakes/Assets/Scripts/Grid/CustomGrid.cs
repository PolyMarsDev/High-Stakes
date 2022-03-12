using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class CustomGrid : MonoBehaviour {
	public Tilemap groundGrid;
	public Tilemap UIgrid;

	public enum UIIndicator {
		None,
		Movable,
		Attackable,
	}

	// Raycast Detection
	public Vector3 GetMouseWorldPosition() {
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Plane plane = new Plane();
		plane.SetNormalAndPosition(Vector3.up, transform.position);
		float dist;
		if (plane.Raycast(ray, out dist)) {

		}
		return Vector3.zero;
	}

	public void SetUIIndicator(Vector2Int position, UIIndicator indicator) {
		if (indicator == UIIndicator.None)
			UIgrid.SetTile((Vector3Int) position, null);
	}
}