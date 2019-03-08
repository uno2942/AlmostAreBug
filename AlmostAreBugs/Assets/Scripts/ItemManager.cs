using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager itemManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private ItemList item1;
    private ItemList item2;

    public GameObject[] prefabs;
    private Dictionary<string, GameObject> dicForPrefabs;
    private GameObject gObject1;
    public static ItemManager ItemManagerInstance
    {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "ItemManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( itemManager == null ) {
                    itemManager = (ItemManager) FindObjectOfType<ItemManager>();
                    if( itemManager == null ) {
                        Debug.LogWarning( "ItemManager does not exists." );
                        return null;
                    }
                }
                return itemManager;
            }
        }
    }

    [System.Flags]
    public enum ItemList { Empty, Pillow, Quilt, Scissors, DeskKey,
        Matchbox, Match, BurningMatch, CardBox, Door, Bullet, Gun, Closet, Candle, CuttenPaperUp, CuttenPaperDown,
        LoadedGun, CandleGun, Paper, 
        PasswordPaperUp, PasswordPaperDown, PasswordPaper, CardKey, CutCandle,
        Table, Desk, Stand, TV, SangSangDo, ButtonOnGame, LightingCandle, Bed, Fax};

    [System.Flags]
    public enum PresentState { Default, Dropped, Gotten, Discarded };
    void Start()
    {
        dicForPrefabs = new Dictionary<string, GameObject>();
        foreach( var prefab in prefabs )
            dicForPrefabs.Add( prefab.name, prefab );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutItemForMix1( ItemManager.ItemList item, GameObject gObject ) {
        item1 = item;
        gObject1 = gObject;
    }

    public void PutItemForUse1( ItemManager.ItemList item, GameObject gObject ) {
        item1 = item;
        gObject1 = gObject;
    }

    public void PutItemForMix2AndMix( ItemManager.ItemList item, GameObject gObject2 ) {
        item2 = item;
        var mixResult = MixResult( item1, item2 );
        if( mixResult[ 0 ] != ItemList.Empty ) {
            if( item1 != ItemList.Scissors )
                Inventory.InventoryInstance.RemoveItem( item1, gObject1 );
            if( item2 != ItemList.Scissors )
                Inventory.InventoryInstance.RemoveItem( item2, gObject2 );
            foreach( var mixitem in mixResult ) {
                Inventory.InventoryInstance.AddItem( mixitem, PresentState.Gotten, Instantiate( dicForPrefabs[ mixitem.ToString() ] ));
            }
        }
    }

    public void PutItemForUse2andUse( ItemManager.ItemList item, GameObject gObject2 ) {
        item2 = item;
        UseResult( item1, gObject1, item2, gObject2);
    }

    public void UseResult( ItemManager.ItemList item1, GameObject gObject1, ItemManager.ItemList item2, GameObject gObject2 ) {
        if( item1 == ItemManager.ItemList.Pillow && item2 == ItemManager.ItemList.Pillow
            && Inventory.InventoryInstance.CheckItemElement( item1 ).num < 0 ) {
            BugManager.BugManagerInstance.BugOvercomed( BugManager.BugList.Pillow );
            Inventory.InventoryInstance.RemoveItem( item1, gObject1 );
            Destroy( gObject2 );
        } else if( item1 == ItemManager.ItemList.DeskKey && item2 == ItemManager.ItemList.TV ) {
            BugManager.BugManagerInstance.BugOccured( BugManager.BugList.TV );
            Inventory.InventoryInstance.RemoveItem( item1, gObject1 );
            GameObject.Find( "TV" ).GetComponent<TV>().OpenableDesk();
        } else if( item1 == ItemManager.ItemList.LoadedGun ) {
            TMPro.TextMeshProUGUI temp = GameObject.Find( "BulletNum" ).GetComponent<TMPro.TextMeshProUGUI>();
            string text = temp.text;
            Vector3 mouse = Input.mousePosition;
            Instantiate( Instantiate( dicForPrefabs[ "Pillow" ], Camera.main.ScreenToWorldPoint( mouse ) + new Vector3( 0, 0, 10 ), Quaternion.identity, GameObject.Find( "PlayerCanvas" ).transform ) );
            BugManager.BugManagerInstance.BugOccured( BugManager.BugList.LoadedGun );
            if( temp.text == "" )
                temp.text = "-1발";
            else {
                if( temp.text[ 0 ] == '-' )
                    temp.text = $"{int.Parse( ( string.Concat( text[ 0 ].ToString() + text[ 1 ].ToString() ) ) ) - 1}발";
                else
                    temp.text = $"{int.Parse( text[ 0 ].ToString() ) - 1}발";
            }
            if( temp.text == "1발" ) {
                temp.text = "";
                gObject1.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/EmptyGun_inven" );
            }

        } else if( item1 == ItemList.CandleGun ) {
            TMPro.TextMeshProUGUI temp = GameObject.Find( "BulletNum" ).GetComponent<TMPro.TextMeshProUGUI>();
            string text = temp.text;
            if( temp.text == "" )
                temp.text = "-1발";
            else {
                if( temp.text[ 0 ] == '-' )
                    temp.text = $"{int.Parse( ( string.Concat( text[ 0 ].ToString() + text[ 1 ].ToString() ) ) ) - 1}발";
                else
                    temp.text = $"{int.Parse( text[ 0 ].ToString() ) - 1}발";
            }
            if( temp.text == "1발" ) {
                temp.text = "";
                gObject1.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/EmptyGun_inven" );
            }
            if( item2 == ItemList.ButtonOnGame ) {
                GameObject.Find( "ButtonOnGame" ).GetComponent<ButtonOnGame>().OnTheButton();
                BugManager.BugManagerInstance.BugOvercomed( BugManager.BugList.LoadedGun );
            }
        } else if( item1 == ItemList.BurningMatch && ( item2 == ItemList.Candle ) ) {
            gObject2.GetComponent<CandleStick>().LitTheCandle();
            BugManager.BugManagerInstance.BugOvercomed( BugManager.BugList.FireMatch );
            Inventory.InventoryInstance.RemoveItem( item1, gObject1 );
        } else if( item1 == ItemList.Scissors && ( item2 == ItemList.Candle || item2 == ItemList.LightingCandle ) ) {
            gObject2.GetComponent<CandleStick>().CutCandle();
        } else if( item1 == ItemList.Scissors && item2 == ItemList.Quilt ) {
            Inventory.InventoryInstance.RemoveItem( ItemList.Quilt, gObject2 );
            Inventory.InventoryInstance.AddItem( ItemList.DeskKey, PresentState.Gotten, Instantiate( dicForPrefabs[ "DeskKey" ] ) );
        } else if( item1 == ItemList.Scissors && item2 == ItemList.Paper ) {
            Inventory.InventoryInstance.RemoveItem( ItemList.Paper, gObject2 );
            Inventory.InventoryInstance.AddItem( ItemList.CuttenPaperDown, PresentState.Gotten, Instantiate( dicForPrefabs[ "CuttenPaperDown" ] ) );
            Inventory.InventoryInstance.AddItem( ItemList.CuttenPaperUp, PresentState.Gotten, Instantiate( dicForPrefabs[ "CuttenPaperUp" ] ) );
        } else if( item1 == ItemList.CuttenPaperUp && item2 == ItemList.LightingCandle ) {
            gObject1.GetComponent<CutPaperUp>().Burn();
        } else if( item1 == ItemList.CuttenPaperDown && item2 == ItemList.LightingCandle ) {
            gObject1.GetComponent<CutPaperDown>().Burn();
        } else if( item1 == ItemList.CardKey && item2 == ItemList.Door ) {
            Inventory.InventoryInstance.RemoveItem( item1, gObject1 );
            gObject2.GetComponent<Door>().OpenableTheDoor();
        }
    }



    public ItemList[] MixResult(ItemList item1, ItemList item2 ) {
        if( ( item1 == ItemList.Quilt && item2 == ItemList.Scissors ) || ( item1 == ItemList.Scissors && item2 == ItemList.Quilt ) )
            return new ItemList[] { ItemList.DeskKey };
        else if( ( item1 == ItemList.Paper && item2 == ItemList.Scissors ) || ( item1 == ItemList.Scissors && item2 == ItemList.Paper ) )
            return new ItemList[] { ItemList.CuttenPaperUp, ItemList.CuttenPaperDown };
        else if( ( item1 == ItemList.Bullet && item2 == ItemList.Gun ) || ( item1 == ItemList.Gun && item2 == ItemList.Bullet ) )
            return new ItemList[] { ItemList.LoadedGun };
        else if( ( item1 == ItemList.LoadedGun && item2 == ItemList.CutCandle ) || ( item1 == ItemList.CutCandle && item2 == ItemList.LoadedGun )
            || ( item1 == ItemList.Gun && item2 == ItemList.CutCandle ) || ( item1 == ItemList.CutCandle && item2 == ItemList.Gun )
            )
            return new ItemList[] { ItemList.CandleGun };
        else if( ( item1 == ItemList.PasswordPaperUp && item2 == ItemList.PasswordPaperDown ) || ( item1 == ItemList.PasswordPaperDown && item2 == ItemList.PasswordPaperUp ) )
            {
            BugManager.BugManagerInstance.BugOvercomed( BugManager.BugList.Paper );
            return new ItemList[] { ItemList.PasswordPaper };
        }
        else if( ( item1 == ItemList.CardBox && item2 == ItemList.PasswordPaper ) || ( item1 == ItemList.PasswordPaper && item2 == ItemList.CardBox ) )
            return new ItemList[] { ItemList.CardKey };
        else if( ( item1 == ItemList.Matchbox && item2 == ItemList.Match ) || ( item1 == ItemList.Match && item2 == ItemList.Matchbox ) ) {
            BugManager.BugManagerInstance.BugOccured( BugManager.BugList.FireMatch );
            return new ItemList[] { ItemList.BurningMatch };
        } else
            return new ItemList[] { ItemList.Empty };
    }

    public void PutPaperOnFax() {
        Instantiate( dicForPrefabs[ "Paper" ], GameObject.Find( "Canvas" ).GetComponent<Transform>() );
    }

    public void PutCutCandle() {
        Instantiate( dicForPrefabs[ "CutCandle" ], GameObject.Find("Canvas").GetComponent<Transform>());
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
