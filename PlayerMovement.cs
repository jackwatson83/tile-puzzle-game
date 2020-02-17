using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    //gameobject for player
    public GameObject player;
    //gameobject for ghost
    public GameObject ghost;
    //vector2 to adjust player gameworld position to player grid position
    public Vector2 playerTile;
    //tile for ghost
    public Vector2 ghostTile;
    //List to store player movements
    public List<Vector2> moveHistory;
    //List to store ghost action queue
    public List<Vector2> ghostQueue;

    //moves left
    public int moves;
    //moves indicator
    public Text movesLeftText;
    //check if the player has actually moved
    public bool moveSuccess = false;

    //get the map layout
    //public List<Map.Tile> map.mapGrid = new List<Map.Tile>();
    //put player above map
    public Vector3 heightAdjustment = new Vector3(0.0f, 1.0f, 0.0f);

    //reference to map.cs attached to a gameobject
    Map map;

    public MenuScript mscript;

    // Use this for initialization
    public void BeginGame ()
    {
        //find map component
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        //Debug.Log(map.name);
        SetUpPlayer();
    }
    

    public void SetUpPlayer()
    {
        moves = map.moves;
        movesLeftText.text = "Moves until Reset: " + moves.ToString();

        //put player on the starting tile - add 1 in the y axis to put player above tile
        player.transform.position = map.startTile.location + heightAdjustment;
        Debug.Log(map.startTile.location);

        //get the player position on the grid
        playerTile = new Vector2(map.startTile.row, map.startTile.column);
        Debug.Log(playerTile);

    }

    public void ActivateGhost()
    {
        SetUpPlayer();
        Debug.Log(ghostQueue.Count);
        //Put the players move history into the ghost action queue
        ghostQueue.AddRange(moveHistory);

        ghost.transform.position = map.startTile.location + heightAdjustment;
        ghostTile = new Vector2(map.startTile.row, map.startTile.column);

        //clear the move history
        moveHistory.Clear();        
    }

    /// <summary>
    /// Move in the upwards direction in 2d space
    /// </summary>
    public void MoveUp()
    {
        Move(new Vector2(1, 0));
    }
    
    //move down
    public void MoveDown()
    {        
        Move(new Vector2(-1, 0));
    }
    
    //move left
    public void MoveLeft()
    {
        Move(new Vector2(0, -1));
    }
    
    //move right
    public void MoveRight()
    {
        Move(new Vector2(0, 1));
    }
    
    //wait
    public void Wait()
    {
        Move(new Vector2(0, 0));
    }
    
    //generic move function
    public void Move(Vector2 direction)
    {
        //get player current position
        //vector for the target tile, x = column z = row
        Vector2 current = playerTile;
        Vector2 target = current + direction;

        //Find the tile in the grid where the x and y match the grid co-ordinates
        Map.Tile targetTile = map.mapGrid.Find(j => (j.row == target.x) && (j.column == target.y));

        //put player on the tile if the tile can be walked on
        if (targetTile.walkable == true)
        {
            player.transform.position = targetTile.location + heightAdjustment;
            playerTile = target;
            //add direction to list of player
            moveHistory.Add(direction);
            moveSuccess = true;
        }
        else
        {
            Debug.Log("You can't walk there");
            moveSuccess = false;
        }

        if (moveSuccess)
        {
            //only move ghost if there are actions in the queue
            if (ghostQueue.Count != 0)
            {
                //move the ghost
                MoveGhost(ghostQueue);
            }

            //check if playeris on plate
            if (targetTile.tileType == "PressurePlate")
            {
                Debug.Log("You're on a pressure plate!");

                //code to light up wires to make game look pretty
                //irrelevant for gameplay - add at end
            }
            UpdateMoveText();
            moveSuccess = false;
        }        
    }

    public void MoveGhost(List<Vector2> queue)
    {
        //get current position of ghost
        Vector2 current = ghostTile;
        //get target tile
        Vector2 target = current + queue[0];
        //Find the tile in the grid where the x and y match the grid co-ordinates
        Map.Tile targetTile = map.mapGrid.Find(x => (x.row == target.x) && (x.column == target.y));
        //The ghost shouldn't ever walk somewhere there is a wall, because it's repeating the player's moves
        //so I can be lazy and just put it wherever, without checking if the target tile is walkable
        ghost.transform.position = targetTile.location + heightAdjustment;

        ghostTile = target;

        //check if ghost is on plate
        if(targetTile.tileType == "PressurePlate")
            {
            Debug.Log("The ghost is on a pressure plate!");
            //code to light up wires to make game look pretty
            //irrelevant for gameplay - add at end
        }

        //remove this action from the list
        ghostQueue.RemoveAt(0);
    }

    //update move text box text - bad method name, am idiot
    public void UpdateMoveText()
    {
        moves = moves - 1;
        if (moves == 0)
        {
            Debug.Log("You're out of moves!");

            if (WinCheck())
            {
                //Win
                Debug.Log("You have won");

                //reset and go back to main menu
                mscript.ShowVictoryMessage();
                
                //make sure history is cleared after winning the game
                moveHistory.Clear();
            }
            else
            {
                //Lose
                ActivateGhost();
            }

            //What this will actually do is cause the "time reset"
            //do that stuff later

        }
        else
        {
            movesLeftText.text = "Moves until Reset: " + moves.ToString();
        }
    }

    public bool WinCheck()
    {
        if ((map.mapGrid.Find(x => (x.row == playerTile.x) && (x.column == playerTile.y)).tileType == "PressurePlate") &&
            (map.mapGrid.Find(x => (x.row == ghostTile.x) && (x.column == ghostTile.y)).tileType == "PressurePlate"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   
}
