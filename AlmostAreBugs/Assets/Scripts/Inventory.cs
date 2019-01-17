using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> itemsInInventory;

    public List<Item> ItemsInInventory { get => itemsInInventory; }

    // Start is called before the first frame update
    void Start()
    {
        itemsInInventory = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
