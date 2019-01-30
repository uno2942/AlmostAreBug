using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private ScriptWindow scriptWindow;
    private HorizontalLayoutGroup itemPanel;
    // Start is called before the first frame update
    void Start()
    {
    //    dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
        itemPanel = GameObject.Find( "ItemPanel" ).GetComponent<HorizontalLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessageBox( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        //MessageBox를 띄움.
    }
    public void MoveLeft() {

    }

    public void MoveRight() {

    }

    public void AddItem( bool CheckItem, GameObject gObject) {
        if( !CheckItem ) 
        gObject.transform.SetParent( itemPanel.transform );
        else { Destroy( gObject ); }
    }
}