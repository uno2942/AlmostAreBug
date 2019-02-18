using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item {
    private bool isOpened;

    public bool IsOpened { get => isOpened;}

    // Start is called before the first frame update
    protected override void Start()
    {
        isOpened = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {
        if(isOpened) {
            //OpenEvent
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/OpenedDoor" );
        }
    }

    public void OpenTheDoor() {
        isOpened = true;
    }
}
