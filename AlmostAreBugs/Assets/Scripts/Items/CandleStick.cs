using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleStick : Item {
    // Start is called before the first frame update
    private bool isLit = false;
    private bool isCut = false;

    public bool IsCut { get => isCut; set => isCut = value; }

    public bool IsLit { get => isLit; set => isLit = value; }


    // Update is called once per frame
    protected override void Update()
    {
        
    }
    private void Change() {
    }

    public override void Clicked() {
        base.Clicked();
    }

    public void CutCandle() {
        if( !isCut ) {
            ItemManager.ItemManagerInstance.PutCutCandle();
            isCut = true;
            if( !isLit )
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/candlestick_off_cut" );
        }
    }

    public void LitTheCandle() {
        if( !isLit ) {
            isLit = true;
            item = ItemManager.ItemList.LightingCandle;
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/candlestick_on" );
        }
    }
}
