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
    private bool isWatingForButton = false;
    private bool isWatingForAnotherItemForMix = false;
    private bool isWatingForAnotherItemForUse = false;
    private bool isDrawerExists = false;
    private bool isButtonOn = false;
    private GameObject clickedGameObject = null;
    public GameObject[] DrawerControll;
    public GameObject cancelWindow;
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

    public bool IsWatingForButton { get => isWatingForButton; }
    public bool IsWatingForAnotherItemForMix { get => isWatingForAnotherItemForMix; }
    public bool IsWatingForAnotherItemForUse { get => isWatingForAnotherItemForUse;}

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


    public void WaitForAnotherItemForUse() {
        isWatingForAnotherItemForUse = true;
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        ( (RectTransform) cancelWindow.transform ).localPosition = new Vector3( BoardManager.BoardManagerInstance.nowPosForCanvas(), 0, 0 );
        cancelWindow.SetActive( true );
        StartCoroutine( WaitForAnotherItemForUseCoroutine() );
    }

    IEnumerator WaitForAnotherItemForUseCoroutine() {
        yield return new WaitWhile( () => ( item == ItemManager.ItemList.Empty && isCanceled == false ) );
        cancelWindow.SetActive( false );
        UiManager.UiManagerInstance.ResetTheColorOfBackGround();
        isCanceled = false;
        isWatingForAnotherItemForUse = false;
        if( !isCanceled )
            ItemManager.ItemManagerInstance.PutItemForUse2andUse( item, clickedGameObject );
        item = ItemManager.ItemList.Empty;
        Debug.Log( "Coroutine End: WaitForAnotherItemForUseCoroutine" );
    }



    public void WaitForButtonSelect() {
        isWatingForButton = true;
        StartCoroutine( WaitForButtonSelectCoroutine() );
    }

    IEnumerator WaitForButtonSelectCoroutine() {
        yield return new WaitWhile( () => ( isWatingForButton == true ) );
        isWatingForButton = false;
        if( !isWatingForAnotherItemForMix && !isWatingForAnotherItemForUse )
            UiManager.UiManagerInstance.ResetTheColorOfBackGround();
        Debug.Log( "Coroutine End: WaitForButtonSelectCoroutine" );
    }
    


    public void WaitForAnotherItemForMix() {
        isWatingForAnotherItemForMix = true;
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        cancelWindow.SetActive( true );
        StartCoroutine( WaitForAnotherItemForMixCoroutine() );
    }

    IEnumerator WaitForAnotherItemForMixCoroutine() {
        yield return new WaitWhile( () => (item == ItemManager.ItemList.Empty && isCanceled == false) );
        cancelWindow.SetActive( false );
        UiManager.UiManagerInstance.ResetTheColorOfBackGround();
        isCanceled = false;
        isWatingForAnotherItemForMix = false;
        if( !isCanceled )
            ItemManager.ItemManagerInstance.PutItemForMix2AndMix( item, clickedGameObject );
        item = ItemManager.ItemList.Empty;
        Debug.Log( "Coroutine End: WaitForAnotherItemCoroutine" );
        //조합 코드
    }


    public void ItemChecked( ItemManager.ItemList _item, ItemManager.PresentState presentState, GameObject gObject ) {
        this.item = _item;
        clickedGameObject = gObject;
    }

    public void Cancel() {
        isCanceled = true;
    }

    public void ButtonSelected(  ) {
        isWatingForButton = false;
    }

    public void DrawerFlagChange() {
        if( true == Inventory.InventoryInstance.CheckItem( ItemManager.ItemList.CardBox ) ) {
            DrawerControll[ 1 ] = null;
        }
        if( isDrawerExists ) {
            foreach( var gObject in DrawerControll )
                gObject?.SetActive( false );
        } 
        else {
            foreach( var gObject in DrawerControll )
                gObject?.SetActive( true );
        }
        isDrawerExists = !isDrawerExists;

    }

    public void ButtonFlagChange() {
        isButtonOn = !isButtonOn;
    }

    public bool IsDrawerExists() {
        return isDrawerExists;
        //Drawer...
    }

    public bool IsButtonOn() {
        return isButtonOn;
    }
    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
