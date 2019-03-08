using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPaperUp : CollectableItem {
    // Start is called before the first frame update
    public bool isBurn = false;
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

    public override void ImageChange( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/code_paper_top" );
    }
    public void ImageChange() {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/code_paper_top" );
    }
    public override void Clicked() {
        if( ClickEventHandlerInvoker( item, presentState, gameObject ) ) {
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

    public void Burn() {
        if( !isBurn ) {
            isBurn = true;
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/code_paper_top" );
            Inventory.InventoryInstance.Burn( item );
            item = ItemManager.ItemList.PasswordPapeUp;
        }
    }
}
