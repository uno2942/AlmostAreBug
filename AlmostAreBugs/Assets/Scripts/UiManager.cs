using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private DialogWindow dialogWindow;
    private HorizontalLayoutGroup itemPanel;
    // Start is called before the first frame update
    void Start()
    {
        dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
        itemPanel = GameObject.Find( "ItemPanel" ).GetComponent<HorizontalLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessageBox( ItemManager.ItemList item, ItemManager.PresentState presentState ) {
        //MessageBox를 띄움.
    }
    public void MoveLeft() {

    }

    public void MoveRight() {

    }
}