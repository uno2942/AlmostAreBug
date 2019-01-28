using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [System.Flags]
    public enum PresentState { Dropped, Gotten, Discarded};
    public PresentState presentState;
    public ItemManager.ItemList itemList;
    public delegate void ClickEventHandler( object s );
    public event ClickEventHandler ClickEvent;
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        presentState = PresentState.Dropped;
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Clicked() {
        if( presentState == PresentState.Dropped )  {
            presentState = PresentState.Gotten;
            if( ClickEvent != null )
                ClickEvent( itemList );
            return;
        }
    }
}