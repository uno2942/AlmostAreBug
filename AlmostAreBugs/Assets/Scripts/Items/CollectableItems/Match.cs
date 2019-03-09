using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : CollectableItem {
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ClickEvent += Inventory.InventoryInstance.AddItem;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {
        if( ClickEventHandlerInvoker( item, presentState, gameObject ) ) {
            switch( presentState ) {
            case ItemManager.PresentState.Dropped:
                presentState = ItemManager.PresentState.Gotten;
                    TaskList.TaskListInstance.AddStrikethrough(5);
                ClickEventHandlerReset();
                ClickEvent += UiManager.UiManagerInstance.OpenMessageBox;
                ClickEvent += UiManager.UiManagerInstance.ChangeColorOfBackground;
                return;
            case ItemManager.PresentState.Gotten:
                break;
            }
        }
    }
}
