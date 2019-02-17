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
        ClickEvent += Inventory.InventoryInstance.AddItem;
        
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
    }

    public virtual void Mix() {
        UiManager.UiManagerInstance.GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }

    public virtual void Use() {
        UiManager.UiManagerInstance.CloseMessageBox( item, presentState, gameObject );
    }

}