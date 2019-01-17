using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Parser parser;
    private BoardManager boardManager;
    private DialogWindow dialogWindow;
    private UiManager uiManager;
    private BugManager bugManager;
    private SceneManager sceneManager;
    private ItemManager itemManager;
    // Start is called before the first frame update
    void Start()
    {
        parser = new Parser();
        boardManager = GameObject.Find( "BoardManager" ).GetComponent<BoardManager>();
        dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
        uiManager = GameObject.Find( "UiManager" ).GetComponent<UiManager>();
        bugManager = GameObject.Find( "BugManager" ).GetComponent<BugManager>();
        sceneManager = GameObject.Find( "SceneManager" ).GetComponent<SceneManager>();
        itemManager = GameObject.Find( "ItemManager" ).GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
