using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridUI : MonoBehaviour
{
    public Tilemap UIGrid;

    public void addIndicator(TileBase tile, Vector2Int pos) 
    {
        UIGrid.SetTile(new Vector3Int(pos.x, 0, pos.y), tile);
    }

    public void removeIndicator(Vector2Int pos) 
    {
        UIGrid.SetTile(new Vector3Int(pos.x, 0, pos.y), null);
    }
}
