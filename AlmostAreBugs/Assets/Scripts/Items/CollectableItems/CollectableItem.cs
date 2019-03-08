using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Item
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        presentState = ItemManager.PresentState.Dropped;
        
        //ClickEvent에 Subscriber를 붙여줍시다.
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked() {

    }

    public virtual void Cancel() {
        GameManager.GameManagerInstance.Cancel();
    }

    public virtual void Mix() {
        ItemManager.ItemManagerInstance.PutItemForMix1( item, gameObject );
    }

    public virtual void Use() {
        ItemManager.ItemManagerInstance.PutItemForUse1( item, gameObject );
    }

    protected override void ClickEventHandlerReset() {
        base.ClickEventHandlerReset();
        if( presentState == ItemManager.PresentState.Gotten )
            ClickEvent += GameManager.GameManagerInstance.ItemChecked; //GameManager에서 필요로 하고 있는지 체크함.
    }
}