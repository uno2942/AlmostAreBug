using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiManager : MonoBehaviour
{
    private ScriptWindow scriptWindow;
    private HorizontalLayoutGroup itemPanel;
    private Inventory inventory;
    private GameObject select;
    private bool isSelectBoxOn;
    // Start is called before the first frame update
    void Start()
    {
    //    dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
        itemPanel = GameObject.Find( "ItemPanel" ).GetComponent<HorizontalLayoutGroup>();
        inventory=GameObject.Find("Inventory").GetComponent<Inventory>();
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
            inventory.CheckItemElement( itemList ).gObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
            Destroy( gObject );
        }
    }
}