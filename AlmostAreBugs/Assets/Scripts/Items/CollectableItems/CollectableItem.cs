using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Item
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        presentState = ItemManager.PresentState.Dropped;
        ClickEvent += ImageChange;
        ClickEvent += GameObject.Find( "Inventory" ).GetComponent<Inventory>().AddItem;
        
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {
        ClickEventHandlerInvoker( item, presentState, gameObject );
        switch(presentState) {
        case ItemManager.PresentState.Dropped:
            presentState = ItemManager.PresentState.Gotten;
            ClickEventHandlerReset();
            ClickEvent += uiManager.OpenMessageBox;
            return;
        case ItemManager.PresentState.Gotten:
            break;
        }        
    }

    public virtual void Discard() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }

    public virtual void Mix() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }

    public virtual void Use() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }
}