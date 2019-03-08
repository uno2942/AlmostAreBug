using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager boardManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private GameObject mainCamera;
    public GameObject[] Poses;
    public int Pos;

    public static BoardManager BoardManagerInstance {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "BoardManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( boardManager == null ) {
                    boardManager = (BoardManager) FindObjectOfType<BoardManager>();
                    if( boardManager == null ) {
                        Debug.LogWarning( "BoardManager gameObject does not exists." );
                        return null;
                    }
                }
                return boardManager;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find( "Main Camera" );
        Pos = 0;
        mainCamera.transform.position = Poses[Pos].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveLeft() {
        if( !( GameManager.GameManagerInstance.IsWatingForAnotherItemForMix || GameManager.GameManagerInstance.IsWatingForAnotherItemForUse || GameManager.GameManagerInstance.IsWatingForButton ) ) {
            Pos -= 1;
            if( Pos < 0 )
                Pos = 3;
            mainCamera.transform.position = Poses[ Pos ].transform.position;
        }
    }

    public void MoveRight() {
        if( !( GameManager.GameManagerInstance.IsWatingForAnotherItemForMix || GameManager.GameManagerInstance.IsWatingForAnotherItemForUse || GameManager.GameManagerInstance.IsWatingForButton ) ) {
            Pos += 1;
            if( Pos == 4 )
                Pos = 0;
            mainCamera.transform.position = Poses[ Pos ].transform.position;
        }
    }

    public float nowPosForCanvas() {
        return ( Pos * 1920f );
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
