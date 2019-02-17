using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiManager : MonoBehaviour
{
    private static UiManager uiManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private HorizontalLayoutGroup itemPanel;
    private GameObject select;
    private bool isSelectBoxOn;

    public static UiManager UiManagerInstance {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "UiManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( uiManager == null ) {
                    uiManager = (UiManager) FindObjectOfType<UiManager>();
                    if( uiManager == null ) {
                        Debug.LogWarning( "UiManager gameObject does not exists." );
                        return null;
                    }
                }
                return uiManager;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    //    dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
        itemPanel = GameObject.Find( "ItemPanel" ).GetComponent<HorizontalLayoutGroup>();
        select = GameObject.Find( "select" );
        isSelectBoxOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessageBox( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        if( !isSelectBoxOn ) {
            isSelectBoxOn = true;
            select.SetActive( true );
        }
    }

    public void CloseMessageBox( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        if( isSelectBoxOn ) {
            isSelectBoxOn = false;
            select.SetActive( false );
        }
    }
        public void MoveLeft() {

    }

    public void MoveRight() {

    }

    public void AddItem( bool CheckItem, ItemManager.ItemList itemList, GameObject gObject ) {
        if( !CheckItem ) {
            if(ItemManager.ItemList.Pillow==itemList) {
                Instantiate( gObject, GameObject.Find("Canvas").transform );
                gObject.transform.SetParent( itemPanel.transform );
                gObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            gObject.transform.SetParent( itemPanel.transform );
            gObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        } else {
            string str = gObject.GetComponentInChildren<TextMeshProUGUI>().text;
            str = 'x' + ( int.Parse( str.Remove( 0, 1 ).ToString() ) + 1 ).ToString();
            Inventory.InventoryInstance.CheckItemElement( itemList ).gObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
            Destroy( gObject );
        }
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}