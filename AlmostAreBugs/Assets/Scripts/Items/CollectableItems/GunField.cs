using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunField : CollectableItem {

    protected override void Start() {
        base.Start();
        ClickEvent += Inventory.InventoryInstance.AddItem;
        ClickEvent += ImageChange;
    }

    // Update is called once per frame
    protected override void Update() {

    }

    //아이템을 줍기 이전의 event
    public override void ImageChange( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/EmptyGun_inven" );
    }


    //아이템을 인벤토리에 넣은 이후의 events

    public override void Clicked() {
        if( ClickEventHandlerInvoker( item, presentState, gameObject ) ) {
            switch( presentState ) {
            case ItemManager.PresentState.Dropped:
                presentState = ItemManager.PresentState.Gotten;
                TaskList.TaskListInstance.AddStrikethrough(2);
                ClickEventHandlerReset();
                ClickEvent += UiManager.UiManagerInstance.OpenMessageBox;
                ClickEvent += UiManager.UiManagerInstance.ChangeColorOfBackground;
                return;
            case ItemManager.PresentState.Gotten:
                break;
            }
        }
    }

    public override void Use() {
        base.Use();
    }
}
