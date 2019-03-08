using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : Item {

    public GameObject ClosetFront;
    public bool isOpen = false;
    // Start is called before the first frame update
    protected override void Start() {

    }

    // Update is called once per frame
    protected override void Update() {

    }

    public override void Clicked() {
        if( !( GameManager.GameManagerInstance.IsWatingForAnotherItemForMix || GameManager.GameManagerInstance.IsWatingForAnotherItemForUse || GameManager.GameManagerInstance.IsWatingForButton ) ) {

            if( !isOpen ) {
                ClosetFront.SetActive( false );
            } else
                ClosetFront.SetActive( true );
            isOpen = !isOpen;
        }
    }
}
