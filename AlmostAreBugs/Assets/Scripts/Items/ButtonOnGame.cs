using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnGame : Item {

    private bool isOn = false;
    private bool isValid = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Clicked() {
        if( isValid ) {
            if( isOn ) {
                //OpenEvent
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/button_on" );
            } else {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/button_off" );
            }
        }
    }

    public void Validiate() {
        isValid = true;
    }
}
