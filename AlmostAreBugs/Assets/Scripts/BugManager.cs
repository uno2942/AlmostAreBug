using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    private static BugManager bugManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    [System.Flags]
    public enum BugList { Bugs };
    Dictionary<BugList, bool> bugDic;

    public static BugManager BugManagerInstance
    {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "SceneManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( bugManager == null ) {
                    bugManager = (BugManager) FindObjectOfType<BugManager>();
                    if( bugManager == null ) {
                        Debug.LogWarning( "SceneManager does not exists." );
                        return null;
                    }
                }
                return bugManager;
            }
        }
    }

    void Start()
    {
        bugDic = new Dictionary<BugList, bool>();
        foreach( BugList bug in (BugList[]) System.Enum.GetValues( typeof( BugList ) ) ) 
        {
            bugDic[ bug ] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BugOvercomed(BugList bug ) {
        bugDic[ bug ] = true;
    }
    public bool IsBugOvercomed( BugList bug ) {
        return bugDic[ bug ];
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
