using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Unit {
	public bool IsTransparent = false;
	public override List<Vector2Int> GetMoveTo() => new List<Vector2Int>();
}
