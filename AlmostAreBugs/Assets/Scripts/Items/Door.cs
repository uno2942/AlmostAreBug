﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpened;

    public bool IsOpened { get => isOpened;}

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickEvent() {
        if(isOpened) {
            //OpenEvent
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/OpenedDoor" );
        }
    }

    public void OpenTheDoor() {
        isOpened = true;
    }
}