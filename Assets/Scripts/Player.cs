using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

	public Tilemap tilemap;
	public TileBase testTile;
	// List<Vector3> specialDestinations;
	// GameObject[] debugLines = new GameObject[8];
	// public Camera cam;

	public void Start() {
		// specialDestinations = new List<Vector3>();
		// for (int i = 0; i < 8; i++)
		// {
		// 	specialDestinations.Add(Vector3.zero);
		// 	debugLines[i] = new GameObject();
		// 	debugLines[i].transform.position = transform.position;
		// 	debugLines[i].AddComponent<LineRenderer>();
        //     LineRenderer lr = debugLines[i].GetComponent<LineRenderer>();
        //     lr.SetColors(Color.white, Color.white);
        //     lr.SetWidth(0.1f, 0.1f);
        //     lr.SetPosition(0, transform.position);
        //     lr.SetPosition(1, specialDestinations[i]);
		// }
		// setSpecialDestinations();
	}

	public void OnMovement(InputValue value) {
		Vector2 mvmt = value.Get<Vector2>();
		if (mvmt.x == 0 || mvmt.y == 0) {
			
			Vector3Int nextPosition = tilemap.WorldToCell(new Vector3(transform.position.x + mvmt.x, 0, transform.position.z + mvmt.y));
			//tilemap.SetTile(nextPosition, testTile);
			if (tilemap.GetTile(nextPosition) != null) {
				transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
			}
		}
		//setSpecialDestinations();
	}

	void setSpecialDestinations() {
		// specialDestinations[0] = new Vector3(transform.position.x - 2, 1, transform.position.z + 1);
		// specialDestinations[1] = new Vector3(transform.position.x + 2, 1, transform.position.z - 1);
		// specialDestinations[2] = new Vector3(transform.position.x - 2, 1, transform.position.z - 1);
		// specialDestinations[3] = new Vector3(transform.position.x + 2, 1, transform.position.z + 1);
		// specialDestinations[4] = new Vector3(transform.position.x - 1, 1, transform.position.z + 2);
		// specialDestinations[5] = new Vector3(transform.position.x + 1, 1, transform.position.z - 2);
		// specialDestinations[6] = new Vector3(transform.position.x - 1, 1, transform.position.z - 2);
		// specialDestinations[7] = new Vector3(transform.position.x + 1, 1, transform.position.z + 2);
	
		// for (int i = 0; i < 8; i++)
		// {
        //     LineRenderer lr = debugLines[i].GetComponent<LineRenderer>();
        //     lr.SetPosition(0, transform.position);
        //     lr.SetPosition(1,specialDestinations[i]);
		// }
	}
}