using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridUITesting : MonoBehaviour
{
    public TileBase tile;
    public Player player;
    public GridUI gridUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int position = gridUI.UIGrid.WorldToCell(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        gridUI.addIndicator(tile, new Vector2Int(position.x, position.y));
    }
    
}
