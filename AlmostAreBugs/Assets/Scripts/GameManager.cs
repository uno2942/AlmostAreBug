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
    private bool isSelected = false;
    private bool isWating = false;
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

    public bool IsWating { get => isWating; }

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

    public void WaitForSelect() {
        isWating = true;
        isSelected = false;
        StartCoroutine( WaitForSelectCoroutine() );
    }

    IEnumerator WaitForSelectCoroutine() {
        yield return new WaitWhile( () => ( isSelected == false ) );
        isSelected = false;
        isWating = false;
    }

    public void WaitForAnotherItem() {
        isWating = true;
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        StartCoroutine( WaitForAnotherItemCoroutine() );
    }

    IEnumerator WaitForAnotherItemCoroutine() {
        Debug.Log( item );
        yield return new WaitWhile( () => (item == ItemManager.ItemList.Empty && isCanceled == false) );
        if( !isCanceled )
            ItemManager.ItemManagerInstance.PutItemForMix2AndMix( item );
        item = ItemManager.ItemList.Empty;
        isCanceled = false;
        isWating = false;
        Debug.Log( item );
        //조합 코드
    }

    public void CollectableItemChecked( ItemManager.ItemList _item, ItemManager.PresentState presentState, GameObject gObject ) {
        this.item = _item;
    }

    public void CollectableItemSelected(  ) {
        isSelected = true;
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
