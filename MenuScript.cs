using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    //all of the maps for the game
    public GameObject map1;
    public GameObject map2;
    GameObject selectedMap;

    public GameObject player;
    public GameObject ghost;
    Vector3 offScreen = new Vector3(10.0f, 10.0f, 10.0f);
    Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

    public GameObject menu;
    public GameObject controls;
    public GameObject victory;

    public PlayerMovement pmscript;
    public Map m;

    // Use this for initialization
    void Start() {
        //pm = this.gameObject.GetComponent<PlayerMovement>();
    }

    public void LoadGame(GameObject map)
    {

        //activate map - the map depends on which game object is set
        //map.SetActive(true);
        //Instantiate(map, origin, Quaternion.identity);
        selectedMap = map;

        m = map.GetComponent<Map>();
        m.CreateMap();

        //put the player at the start position
        pmscript.BeginGame();

        //hide menu
        menu.SetActive(false);

        //bring up controls
        controls.SetActive(true);

    }


    public void ShowVictoryMessage()
    {
        //display victory message
        victory.SetActive(true);
    }
    public void EndGame()
    {
        //show menu, hide other UI elements
        victory.SetActive(false);
        controls.SetActive(false);
        menu.SetActive(true);

        //move player and ghost out of the way
        player.transform.position = offScreen;
        ghost.transform.position = offScreen;

        //selectedMap.SetActive(false);
        Destroy(selectedMap);
    }

    public void UseMap1()
    {
        //map1 = Resources.Load("Prefabs/TestMap") as GameObject;
        //Instantiate(map1, origin, Quaternion.identity);
        //map1.GetComponent<Map>().CreateMap();
        //selectedMap = map1;
        LoadGame(Instantiate(map1, origin, Quaternion.identity));
    }

    public void UseMap2()
    {
        //map2 = Resources.Load("Prefabs/TestMap2") as GameObject;
        //Instantiate(map2, origin, Quaternion.identity);
        //selectedMap = map2;
        LoadGame(Instantiate(map2, origin, Quaternion.identity));
    }
}