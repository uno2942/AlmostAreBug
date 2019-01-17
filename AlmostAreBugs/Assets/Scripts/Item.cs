using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public delegate void ClickEventHandler( object s );
    public event ClickEventHandler ClickEvent;
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}