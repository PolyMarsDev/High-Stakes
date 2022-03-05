using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public void OnMovement(InputValue value) {
		Vector2 mvmt = value.Get<Vector2>();
		if (mvmt.x == 0 || mvmt.y == 0)
			transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
	}

	
}