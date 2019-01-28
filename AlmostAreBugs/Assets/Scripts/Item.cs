using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemManager.PresentState presentState;
    public ItemManager.ItemList item;
    public delegate void ClickEventHandler( ItemManager.ItemList item, ItemManager.PresentState presentState );
    public event ClickEventHandler ClickEvent;
    private UiManager uiManager;
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        uiManager = GameObject.Find( "UiManager" ).GetComponent<UiManager>();
        presentState = ItemManager.PresentState.Dropped;
        ClickEvent += ItemGet;
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Clicked() {
        if( presentState == ItemManager.PresentState.Dropped )  {
            presentState = ItemManager.PresentState.Gotten;
            ClickEvent?.Invoke( item, presentState );
            ClickEvent = null;
            ClickEvent += uiManager.OpenMessageBox;
            return;
        }
    }

    public virtual void ItemGet(ItemManager.ItemList item, ItemManager.PresentState presentState) {

    }
}