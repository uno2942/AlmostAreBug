using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow :CollectableItem
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }
    
    //아이템을 줍기 이전의 event
    public override void ImageChange( ItemManager.ItemList item, ItemManager.PresentState presentState, GameObject gObject ) {

    }


    //아이템을 인벤토리에 넣은 이후의 events
    public override void Clicked() {
        base.Clicked();
    }

    //3rd button
    public override void Discard() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }

    //2nd button
    public override void Mix() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }

    //1st button
    public override void Use() {
        GameObject.Find( "UiManager" ).GetComponent<UiManager>().CloseMessageBox( item, presentState, gameObject );
    }
}
