using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct ItemElement {
        public ItemManager.ItemList item;
        public int num;
    }
    private ItemElement[] itemsInInventory;

    public ItemElement[] ItemsInInventory { get => itemsInInventory; }

    const int MAXITEMNUM = 20;
    // Start is called before the first frame update
    void Start()
    {
        itemsInInventory = new ItemElement[ MAXITEMNUM ];//아이템이 20개 이상이면 그 때 고민해보자.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(ItemManager.ItemList itemList ) {
        int i;
        for( i = 0; i < MAXITEMNUM; i++ )
            if( itemList == itemsInInventory[ i ].item )
                break;
        if( i == MAXITEMNUM ) {
            for( i = 0; i < MAXITEMNUM; i++ ) {
                if( 0 == itemsInInventory[ i ].num ) {
                    itemsInInventory[ i ].item = itemList;
                    itemsInInventory[ i ].num += 1;
                    return true;
                }
            }
            throw new System.IndexOutOfRangeException();
        } else {
            itemsInInventory[ i ].num += 1;
            return true;
        }
    }

    public bool RemoveItem( ItemManager.ItemList itemList ) {
        int i;
        for( i = 0; i < MAXITEMNUM; i++ )
            if( itemList == itemsInInventory[ i ].item )
                break;
        if( i == 20 ) {
            return false;
        } else {
            itemsInInventory[ i ].num -= 1;
            if( itemsInInventory[ i ].num == 0 ) {
                itemsInInventory[ i ].item = 0;
            }
            return true;
        }
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
}
