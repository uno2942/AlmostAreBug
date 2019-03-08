using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Item {

    private bool isOpened = false;
    private bool isValid = false;

    public bool IsOpened { get => isOpened; set => isOpened = value; }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
    public override void Clicked() {//Drawer 관련 코드가 필요.
        if( ClickEventHandlerInvoker( item, presentState, gameObject ) ) {
            Change();
        }
    }
    
    private void Change() {
        if( !( GameManager.GameManagerInstance.IsWatingForAnotherItemForMix || GameManager.GameManagerInstance.IsWatingForAnotherItemForUse || GameManager.GameManagerInstance.IsWatingForButton ) ) {
            if( isValid ) {
                if( isOpened ) {
                    //OpenEvent
                    gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/TV_closed" );
                    isOpened = false;
                    GameManager.GameManagerInstance.DrawerFlagChange();
                } else {
                    gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/TV_opened" );
                    isOpened = true;
                    GameManager.GameManagerInstance.DrawerFlagChange();
                }
            }
        }
    }
    public void OpenDesk() {
        OpenableDesk();
    }

    public void OpenableDesk() {
        isValid = true;
        Change();
    }
}
