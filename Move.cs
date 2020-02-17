using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public void SetUpMap()
    {
        //put player in start location

        //display the number of moves allowed for this map

        //put the echo in the start location
        //but NOT on first playthrough
        //since there won't be a queue of echo actions
    }

    public void MoveCharacter(Vector2 direction, GameObject character)
    {
        //take in a vector for which direction to move
        //as well as which character is moving
    }

    public void MovesHandler()
    {
        //check if the player has run out of moves
        //if they have, has the player won?
        //if victorious, then end the level
        //if not, reset

        //if the player has moves left
        //decrement movesleft by 1
        //Update the moves left text box
    }
}
