using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory inventory;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    public struct ItemElement {
        public ItemManager.ItemList item;
        public int num;
        public GameObject gObject;
    }
    private ItemElement[] itemsInInventory;

    public ItemElement[] ItemsInInventory { get => itemsInInventory; }

    public static Inventory InventoryInstance {
        get
        {
            if(mShuttingDown) {
                Debug.LogWarning( "Inventory is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if(inventory==null) {
                    inventory = (Inventory) FindObjectOfType<Inventory>();
                    if(inventory==null) {
                        Debug.LogWarning( "Inventory gameObject does not exists." );
                        return null;
                    }
                }
                return inventory;
            }
        }
    }
    
    const int MAXITEMNUM = 9;
    // Start is called before the first frame update
    void Start()
    {
        itemsInInventory = new ItemElement[ MAXITEMNUM ];//아이템이 20개 이상이면 그 때 고민해보자.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemManager.ItemList itemList, ItemManager.PresentState presentStat, GameObject gObject ) {
        int i;
        for( i = 0; i < MAXITEMNUM; i++ )
            if( itemList == itemsInInventory[ i ].item )
                break;
        if( i == MAXITEMNUM ) {
            for( i = 0; i < MAXITEMNUM; i++ ) {
                if( 0 == itemsInInventory[ i ].num ) {
                    itemsInInventory[ i ].item = itemList;
                    itemsInInventory[ i ].num += 1;
                    ItemsInInventory[ i ].gObject = gObject;
                    UiManager.UiManagerInstance.AddItem( false, itemList, gObject );
                    return;
                }
            }
            throw new System.IndexOutOfRangeException();
        } else {
            if(itemList==ItemManager.ItemList.Pillow) {
                if( itemsInInventory[ i ].num < 8 )
                    itemsInInventory[ i ].num++;
                else
                    itemsInInventory[ i ].num *= 2;
            }
            else
                itemsInInventory[ i ].num += 1;
            UiManager.UiManagerInstance.AddItem( true, itemList, gObject );
        }
    }

    public bool RemoveItem( ItemManager.ItemList itemList, GameObject gObject ) {
        int i;
        for( i = 0; i < MAXITEMNUM; i++ )
            if( itemList == itemsInInventory[ i ].item )
                break;
        if( i == 20 ) {
            return false;
        } else {
            CleanitemsInInveotory( i );
            UiManager.UiManagerInstance.RemoveItem( itemList, gObject );
            return true;
            /*
            if( itemsInInventory[ i ].num < 0 )
                CleanitemsInInveotory( i );
            itemsInInventory[ i ].num -= 1;
            if( itemsInInventory[ i ].num == 0 ) {
                CleanitemsInInveotory( i );
            }
            UiManager.UiManagerInstance.RemoveItem( itemList, gObject );
            return true;
            */
        }
    }

    private void CleanitemsInInveotory(int i ) {
        itemsInInventory[ i ].item = 0;
        itemsInInventory[ i ].gObject = null;
        itemsInInventory[ i ].num = 0;
    }
    public ItemElement CheckItemElement( ItemManager.ItemList itemList ) {
        int i;
        for( i = 0; i < MAXITEMNUM; i++ )
            if( itemList == itemsInInventory[ i ].item )
                break;
        if( i == 20 )
            return default;
        else
            return itemsInInventory[ i ];
    }

    public bool CheckItem( ItemManager.ItemList itemList ) {
        if( default( ItemElement ).Equals( CheckItemElement( itemList ) ) )
            return false;
        else
            return true;
    }

    public void Burn(ItemManager.ItemList item ) {
        int i = 0;
        if( item == ItemManager.ItemList.CuttenPaperDown ) {
            for( i = 0; i < MAXITEMNUM; i++ )
                if( item == itemsInInventory[ i ].item )
                    break;
            itemsInInventory[ i ].item = ItemManager.ItemList.PasswordPaperDown;
            return;
        } else if( item == ItemManager.ItemList.CuttenPaperUp ) {
            for( i = 0; i < MAXITEMNUM; i++ )
                if( item == itemsInInventory[ i ].item )
                    break;
            itemsInInventory[ i ].item = ItemManager.ItemList.PasswordPapeUp;
            return;
        } else
            return;

    }
    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }

}
