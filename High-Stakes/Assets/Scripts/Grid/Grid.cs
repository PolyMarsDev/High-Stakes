using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public Tilemap groundGrid;

	public Vector2Int Size;
	List<Unit>[,] units;

	public enum UIIndicator {
		None,
		Movable,
		Attackable,
	}

	// Prioritized being called before anything else
	async void Awake() {
		units = new List<Unit>[Size.x, Size.y];
		for (int i = 0; i < Size.x; i++)
		for (int j = 0; j < Size.y; j++)
			units[i,j] = new List<Unit>();
	}

	public Unit GetUnitAt(int x, int y) => units[x,y][0];
	public Unit GetUnitAt(Vector2Int pos) => GetUnitAt(pos.x, pos.y);

	public void AddUnit(Vector2Int pos) {

	}

	// Raycast Detection
	public Vector3 GetMouseWorldPosition() {
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Plane plane = new Plane();
		plane.SetNormalAndPosition(Vector3.up, transform.position);
		float dist;
		if (plane.Raycast(ray, out dist)) {
			Vector3 hitPoint = ray.GetPoint(dist);
			return hitPoint;
		}
		return Vector3.zero;
	}

	public Vector3Int SnapCoordinate(Vector3 position) {
		return groundGrid.WorldToCell(position);
	}
}