using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnGame : Item {

    private bool isOn = false;
    private bool isValid = false;
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {
        ClickEventHandlerInvoker( item, presentState, gameObject );
    }
    private void Change() {
        if( isValid ) {
            if( isOn ) {
                //OpenEvent
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/button_off" );
                isOn = false;
                GameManager.GameManagerInstance.ButtonFlagChange();
            } else {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/button_on" );
                isOn = true;
                GameManager.GameManagerInstance.ButtonFlagChange();
            }
        }
    }
    public void OnTheButton() {
        Validiate();
    }

    public void Validiate() {
        if(!isValid)
            ItemManager.ItemManagerInstance.PutPaperOnFax();
        isValid = true;
        Change();
    }
}
