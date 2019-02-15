using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemManager.PresentState presentState;
    public ItemManager.ItemList item;
    public delegate void ClickEventHandler( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject );
    public event ClickEventHandler ClickEvent;
    protected UiManager uiManager;
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        presentState = ItemManager.PresentState.Default;
        uiManager = GameObject.Find( "UiManager" ).GetComponent<UiManager>();
        ClickEvent += GameObject.Find( "ScriptWindow" ).GetComponent<ScriptWindow>().ScriptPrinterForClickItem;
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Clicked() {
        ClickEventHandlerInvoker( item, presentState, gameObject );
    }
    public virtual void ImageChange( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject) {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/KafuuChino" );
    }

    protected void ClickEventHandlerInvoker( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {
        ClickEvent?.Invoke( item, presentState, gObject );
    }

    protected void ClickEventHandlerReset() {
        ClickEvent = null;
        ClickEvent += GameObject.Find( "ScriptWindow" ).GetComponent<ScriptWindow>().ScriptPrinterForClickItem;
    }
}