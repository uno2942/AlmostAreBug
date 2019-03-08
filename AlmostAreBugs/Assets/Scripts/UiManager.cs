﻿using System.Collections;
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
    private int pivot = 0;
    private Button[] buttons;

    public GameObject[] itemPanelBoxes;
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
        select.SetActive( false );
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
            buttons = select.GetComponentsInChildren<Button>();
            //temporary code
            buttons[ 0 ].onClick.AddListener( gObject.GetComponent<CollectableItem>().Use );
            buttons[ 0 ].onClick.AddListener( GameManager.GameManagerInstance.WaitForAnotherItemForUse );
            buttons[ 1 ].onClick.AddListener( gObject.GetComponent<CollectableItem>().Mix );
            buttons[ 1 ].onClick.AddListener( GameManager.GameManagerInstance.WaitForAnotherItemForMix );
            buttons[ 2 ].onClick.AddListener( gObject.GetComponent<CollectableItem>().Cancel );
            foreach( var button in buttons ) {
                button.onClick.AddListener( GameManager.GameManagerInstance.ButtonSelected );
                button.onClick.AddListener( CloseMessageBox );
            }
            GameManager.GameManagerInstance.WaitForButtonSelect();
        }
    }

    public void CloseMessageBox() {
        if( isSelectBoxOn ) {
            isSelectBoxOn = false;
            select.SetActive( false );
            foreach( var button in buttons )
                button.onClick.RemoveAllListeners();
        }
    }
        public void MoveLeft() {

    }

    public void MoveRight() {

    }

    public void AddItem( bool CheckItem, ItemManager.ItemList itemList, GameObject gObject ) {
        if( ItemManager.ItemList.Pillow == itemList ) {
            GameObject instantiatedGameObject;
            ( instantiatedGameObject = Instantiate( gObject, GameObject.Find( "Canvas" ).transform ) ).name = "Pillow";
            instantiatedGameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        if(ItemManager.ItemList.Gun == itemList ) {
            ((RectTransform) ( Inventory.InventoryInstance.CheckItemElement( itemList ).gObject.transform )).Rotate(new Vector3(0, 0, 90f));
        }

        if( !CheckItem ) {
            foreach( var text in gObject.GetComponentsInChildren<TextMeshProUGUI>() ) {
                text.enabled = true;
            }
        } else {
            string str = Inventory.InventoryInstance.CheckItemElement( itemList ).gObject.GetComponentInChildren<TextMeshProUGUI>().text;
            str = 'x' + ( Inventory.InventoryInstance.CheckItemElement(itemList).num ).ToString();
            Inventory.InventoryInstance.CheckItemElement( itemList ).gObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
            Destroy( gObject );
        }
        PanelUpdate();
    }

    public void PanelUpdate() {
        int i = pivot;
        foreach(var item in Inventory.InventoryInstance.ItemsInInventory ) {
            item.gObject?.SetActive( false );
        }
        foreach( var panelBox in itemPanelBoxes ) {
            if( Inventory.InventoryInstance.ItemsInInventory[ i % 10 ].gObject != null ) {
                Inventory.InventoryInstance.ItemsInInventory[ i % 10 ].gObject.SetActive( true );
                Inventory.InventoryInstance.ItemsInInventory[ i % 10 ].gObject.transform.SetParent( panelBox.transform );
                ( (RectTransform) Inventory.InventoryInstance.ItemsInInventory[ i % 10 ].gObject.transform ).sizeDelta = new Vector2( 1f, 0.9f );
                Inventory.InventoryInstance.ItemsInInventory[ i % 10 ].gObject.transform.localPosition=new Vector2( 5f, 0f );
            }
            i += 1;
        }
    }

    public void Pivotplus() {
        pivot += 1;
        if( pivot > 9 )
            pivot -= 10;
        PanelUpdate();
    }

    public void Pivotminus() {
        pivot -= 1;
        if( pivot < 0 )
            pivot += 10;
        PanelUpdate();
    }

    public void RemoveItem( ItemManager.ItemList itemList, GameObject gObject ) {
        string str = gObject.GetComponentInChildren<TextMeshProUGUI>().text;
        if( int.Parse( str.Remove( 0, 1 ).ToString() ) <= 1 ) {
            str = "x0";
            gObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
            Destroy( gObject );
        } else {
            str = 'x' + ( int.Parse( str.Remove( 0, 1 ).ToString() ) - 1 ).ToString();
            gObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
        }
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}