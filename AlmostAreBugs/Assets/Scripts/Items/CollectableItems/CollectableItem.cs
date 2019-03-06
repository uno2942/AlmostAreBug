using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Item
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        presentState = ItemManager.PresentState.Dropped;
        
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {

    }

    public virtual void Discard() {
        UiManager.UiManagerInstance.GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
        Inventory.InventoryInstance.RemoveItem( item, gameObject );
    }

    public virtual void Mix() {
        UiManager.UiManagerInstance.GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
        ItemManager.ItemManagerInstance.PutItemForMix1( item, gameObject );
        GameManager.GameManagerInstance.WaitForAnotherItem();
    }

    public virtual void Use() {
        Debug.Log( "1" );
        UiManager.UiManagerInstance.CloseMessageBox( item, presentState, gameObject );
    }

    protected override void ClickEventHandlerReset() {
        base.ClickEventHandlerReset();
        if( presentState == ItemManager.PresentState.Gotten )
            ClickEvent += GameManager.GameManagerInstance.CollectableItemChecked; //GameManager에서 필요로 하고 있는지 체크함.
    }
}