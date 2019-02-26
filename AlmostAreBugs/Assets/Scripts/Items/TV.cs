using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Item {

    private bool isOpened = false;
    private bool isValid = false;

    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
    public override void Clicked() {//Drawer 관련 코드가 필요.
        if( isValid ) {
            if( isOpened ) {
                //OpenEvent
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/TV_closed" );
            } else {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/TV_opened" );
            }
        }
    }
}
