using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();
    

    public static GameManager GameManagerInstance {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "GameManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( gameManager == null ) {
                    gameManager = (GameManager) FindObjectOfType<GameManager>();
                    if( gameManager == null ) {
                        Debug.LogWarning( "GameManager gameObject does not exists." );
                        return null;
                    }
                }
                return gameManager;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey( KeyCode.Tab ) ) {
            TaskList.TaskListInstance.gameObject.SetActive( true );
        } 
        else
            TaskList.TaskListInstance.gameObject.SetActive( false );
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
