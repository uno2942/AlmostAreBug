using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private ItemManager.ItemList item = ItemManager.ItemList.Empty;
    private bool isCanceled = false;
    private bool isButtonSelected = false;
    private bool isWatingForSelect = false;
    private bool isWatingForAnotherItem = false;
    private GameObject clickedGameObject = null;

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

    public bool IsWating { get => isWatingForSelect || isWatingForAnotherItem; }

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

    public void WaitForButtonSelect() {
        isWatingForSelect = true;
        isButtonSelected = false;
        StartCoroutine( WaitForButtonSelectCoroutine() );
    }

    IEnumerator WaitForButtonSelectCoroutine() {
        yield return new WaitWhile( () => ( isButtonSelected == false ) );
        isButtonSelected = false;
        isWatingForSelect = false;
        Debug.Log( "Coroutine End" );
    }

    public void WaitForAnotherItem() {
        isWatingForAnotherItem = true;
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        StartCoroutine( WaitForAnotherItemCoroutine() );
    }

    IEnumerator WaitForAnotherItemCoroutine() {
        yield return new WaitWhile( () => (item == ItemManager.ItemList.Empty && isCanceled == false) );
        if( !isCanceled )
            ItemManager.ItemManagerInstance.PutItemForMix2AndMix( item, clickedGameObject );
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        isWatingForAnotherItem = false;
        Debug.Log( "Coroutine End" );
        //조합 코드
    }

    public void ItemChecked( ItemManager.ItemList _item, ItemManager.PresentState presentState, GameObject gObject ) {
        this.item = _item;
        clickedGameObject = gObject;
    }

    public void ButtonSelected(  ) {
        isButtonSelected = true;
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
