using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoardManager boardManager;
    private ScriptWindow scriptWIndow;
    private UiManager uiManager;
    private BugManager bugManager;
    private SceneManager sceneManager;
    private ItemManager itemManager;
    private GameObject taskList;
    // Start is called before the first frame update
    void Start()
    {
        boardManager = GameObject.Find( "BoardManager" ).GetComponent<BoardManager>();
        uiManager = GameObject.Find( "UiManager" ).GetComponent<UiManager>();
        bugManager = GameObject.Find( "BugManager" ).GetComponent<BugManager>();
        sceneManager = GameObject.Find( "SceneManager" ).GetComponent<SceneManager>();
        itemManager = GameObject.Find( "ItemManager" ).GetComponent<ItemManager>();
        taskList = GameObject.Find( "TaskList" );
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey( KeyCode.Tab ) ) {
            taskList.SetActive( true );
        } 
        else
            taskList.SetActive( false );
    }
}
