using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager sceneManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    public static SceneManager SceneManagerInstance
    {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "SceneManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( sceneManager == null ) {
                    sceneManager = (SceneManager) FindObjectOfType<SceneManager>();
                    if( sceneManager == null ) {
                        Debug.LogWarning( "SceneManager does not exists." );
                        return null;
                    }
                }
                return sceneManager;
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
        
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
