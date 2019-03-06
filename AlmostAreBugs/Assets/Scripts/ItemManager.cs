using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager itemManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private ItemList itemForMix1;
    private ItemList itemForMix2;

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
        LoadedGun, CandleGun, EmptyPaper, Paper, 
        PasswordPapeUp, PasswordPaperDown, PasswordPaper, CardKey, CutCandle,
        Table, Desk, Stand, TV, SangSangDo };

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
        itemForMix1 = item;
        gObject1 = gObject;
    }

    public void PutItemForMix2AndMix( ItemManager.ItemList item, GameObject gObject2 ) {
        itemForMix2 = item;
        var mixResult = MixResult( itemForMix1, itemForMix2 );
        if( mixResult[ 0 ] != ItemList.Empty ) {
            Inventory.InventoryInstance.RemoveItem( itemForMix1, gObject1 );
            Inventory.InventoryInstance.RemoveItem( itemForMix2, gObject2 );
            foreach( var mixitem in mixResult ) {
                Inventory.InventoryInstance.AddItem( mixitem, PresentState.Gotten, Instantiate( dicForPrefabs[ mixitem.ToString() ] ));
            }
        }
        
    }

    public ItemList[] MixResult(ItemList item1, ItemList item2 ) {
        if( ( item1 == ItemList.Quilt && item2 == ItemList.Scissors ) || ( item1 == ItemList.Scissors && item2 == ItemList.Quilt ) )
            return new ItemList[] { ItemList.DeskKey };
        else if( ( item1 == ItemList.EmptyPaper && item2 == ItemList.Scissors ) || ( item1 == ItemList.Scissors && item2 == ItemList.EmptyPaper ) )
            return new ItemList[] { ItemList.CuttenPaperUp, ItemList.CuttenPaperDown };
        else if( ( item1 == ItemList.Bullet && item2 == ItemList.Gun ) || ( item1 == ItemList.Gun && item2 == ItemList.Bullet ) )
            return new ItemList[] { ItemList.LoadedGun };
        else if( ( item1 == ItemList.LoadedGun && item2 == ItemList.CutCandle ) || ( item1 == ItemList.CutCandle && item2 == ItemList.LoadedGun )
            || ( item1 == ItemList.Gun && item2 == ItemList.CutCandle ) || ( item1 == ItemList.CutCandle && item2 == ItemList.Gun )
            )
            return new ItemList[] { ItemList.CandleGun };
        else if( ( item1 == ItemList.PasswordPapeUp && item2 == ItemList.PasswordPaperDown ) || ( item1 == ItemList.PasswordPaperDown && item2 == ItemList.PasswordPapeUp ) )
            return new ItemList[] { ItemList.PasswordPaper };
        else if( ( item1 == ItemList.CardBox && item2 == ItemList.PasswordPaper ) || ( item1 == ItemList.PasswordPaper && item2 == ItemList.CardBox ) )
            return new ItemList[] { ItemList.CardKey };
        else if( ( item1 == ItemList.Matchbox && item2 == ItemList.Match ) || ( item1 == ItemList.Matchbox && item2 == ItemList.Match ) )
            return new ItemList[] { ItemList.BurningMatch };
        else
            return new ItemList[] { ItemList.Empty };
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
