using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public abstract class Enemy : LiveUnit {
	public abstract bool CanSeePlayer();
	public abstract bool CanAttackPlayer();
	public abstract Vector2Int GetBestMove();

	public bool HasEnemy(Vector2Int pos) => Grid.GetUnitAt(pos) is Enemy;

    // public Tilemap tilemap;
	// public TileBase testTile;
	// public void OnMovement(InputValue value) {
	// 	Vector2 mvmt = value.Get<Vector2>();
	// 	if (mvmt.x == 0 || mvmt.y == 0)
	// 		transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
	// 		this.die(value);

	// }
	// // public void OnMovement(InputValue value) {
	// // 	Vector2 mvmt = value.Get<Vector2>();
	// // 	if (mvmt.x == 0 || mvmt.y == 0)
	// // 		transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
	// // }
    

// Nani?
    // public void OnMovement(InputValue value) {
	// 	Vector2 mvmt = value.Get<Vector2>();
	// 	if (mvmt.x == 0 || mvmt.y == 0) {
	// 		Vector3Int nextPosition = tilemap.WorldToCell(new Vector3(transform.position.x + mvmt.x, 0, transform.position.z + mvmt.y));
	// 		//tilemap.SetTile(nextPosition, testTile);
	// 		if (tilemap.GetTile(nextPosition) != null) {
	// 			transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
    //             // this.die(value);
	// 		}
	// 	}
	// 	//setSpecialDestinations();
	// }

    public void die(InputValue value) {
        // Kills the game object
        Destroy (gameObject);
        // Removes this script instance from the game object
        Destroy (this);
        // Removes the rigidbody from the game object
        Destroy(GetComponent<Rigidbody>());
	}

	public abstract List<Vector2Int> GetVisionPositions();
	public abstract List<Vector2Int> GetAttackablePositions();
}