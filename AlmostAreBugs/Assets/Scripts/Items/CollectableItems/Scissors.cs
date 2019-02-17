using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : CollectableItem {
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Clicked() {
        ClickEventHandlerInvoker( item, presentState, gameObject );
        switch( presentState ) {
        case ItemManager.PresentState.Dropped:
            presentState = ItemManager.PresentState.Gotten;
            ClickEventHandlerReset();
            ClickEvent += UiManager.UiManagerInstance.OpenMessageBox;
            return;
        case ItemManager.PresentState.Gotten:
            break;
        }
    }
}
