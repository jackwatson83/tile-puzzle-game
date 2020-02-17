using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    
    //can the player walk on the tile
    public bool walkable;

    //is the player on this tile
    public bool isPlayerHere = false;

    //the row this tile is in
    public int row;

    //the column this map is in
    public int column;

    //vector to represent the grid co-ordinate of the tile
    public Vector2 MapPosition()
    {
        Vector2 Position = new Vector2(row, column);
        return Position;
    }
}
