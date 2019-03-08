using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskKey : CollectableItem {

    protected override void Start() {
        base.Start();
        presentState = ItemManager.PresentState.Gotten;
        ClickEventHandlerReset();
        ClickEvent += UiManager.UiManagerInstance.OpenMessageBox;
        ClickEvent += UiManager.UiManagerInstance.ChangeColorOfBackground;
    }

    // Update is called once per frame
    protected override void Update() {

    }

    //아이템을 줍기 이전의 event

    //아이템을 인벤토리에 넣은 이후의 events

    public override void Clicked() {
        if( ClickEventHandlerInvoker( item, presentState, gameObject ) ) {
            switch( presentState ) {
            case ItemManager.PresentState.Dropped:
                presentState = ItemManager.PresentState.Gotten;
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
