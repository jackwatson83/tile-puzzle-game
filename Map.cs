using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TileType
{
    Tile,
    StartTile,
    Wall,
    PressurePlate
}

public class Map : MonoBehaviour {
    
    //struct for Tile
    public struct Tile
    {
        public Vector3 location;
        public int row;
        public int column;
        public bool walkable;
        public string tileType; //could use enum here to help prevent typos        
    }

    //array for all the tiles - all maps will be the same dimension
    public List<Tile> mapGrid;
    public Tile startTile;
    //int for number of moves
    public int moves;

    
    public void CreateMap()
    {
        Debug.Log("Map created");
        //temp list for tiles before putting in array
        List<GameObject> t = new List<GameObject>();
        //look at all children of the map
        foreach (Transform child in transform)
        {
            //find the ones with relevant tags
            if (child.tag == "Tile" || child.tag == "StartTile" || child.tag == "Wall" || child.tag == "PressurePlate")
            {

                //if correct tag, add to list of tiles
                t.Add(child.gameObject);
            }
            //catch if something is a child that shouldn't be
            else
            {
                Debug.Log("Incorrect Tag");
            }
        }
        //create the mapGrid array
        mapGrid = GetMapLayout(t);
        //print map list - debug
        PrintMapList();
    }

    public List<Tile> GetMapLayout(List<GameObject> grid)
    {
        //extract location, row, column, walkable from list of gameobjects
        Tile temp;
        List<Tile> tempList = new List<Tile>();
        foreach (GameObject g in grid)
        {
            //get the name of the gameobject, and split at the -
            //each gameobject will be named x-y, where x is the row and y is the column
            string s = g.name;
            string[] w = s.Split('-');
            //get row
            temp.row = Convert.ToInt32(w[0]);
            //get column
            temp.column = Convert.ToInt32(w[1]);
            //get tag for walkable
            if (g.tag == "Wall")
            {
                temp.walkable = false;
            }
            else
            {
                temp.walkable = true;
            }
            //get location vector3
            temp.location = g.transform.position;
            //get tag
            temp.tileType = g.tag;
            //put in temp array            
            tempList.Add(temp);

            if (temp.tileType == "StartTile")
            {
                startTile = temp;
            }

            //check to see if list is being populated
            //Debug.Log("Row: " + temp.row.ToString() + " Column: " + temp.column.ToString());
        }
        return tempList;
    }

    public int GetMapMoves()
    {
        return moves;
    }

    private void PrintMapList()
    {
        foreach (Tile t in mapGrid)
        {
            Debug.Log("Row: " + t.row.ToString() + " Column: " + t.column.ToString());
            Debug.Log("Walkable? " + t.walkable.ToString());
            Debug.Log("Location: " + t.location.x.ToString() + ", " + t.location.y.ToString() + ", " + t.location.z.ToString());
        }
    }
}
