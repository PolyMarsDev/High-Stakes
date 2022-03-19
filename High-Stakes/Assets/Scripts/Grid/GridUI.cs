using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridUI : MonoBehaviour
{
    public Tilemap UIGrid;
	public static GridUI Instance;

	void Awake() {
		Instance = this;
	}

	public void addIndicator(Indicator indicator, Vector2Int pos) {
		Tile tile = null;
		if (indicator == Indicator.MOVABLE)	tile = movableIndicator;
		addIndicator(tile, pos);
	}

    public void addIndicator(TileBase tile, Vector2Int pos) 
    {
        UIGrid.SetTile(new Vector3Int(pos.x, pos.y, 0), tile);
    }

    public void removeIndicator(Vector2Int pos) 
    {
        UIGrid.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
    }

	public enum Indicator {
		MOVABLE
	}

	public Tile movableIndicator; 
}
