using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [System.Flags]
    public enum ItemList { Empty, Pillow };
    [System.Flags]
    public enum PresentState { Default, Dropped, Gotten, Discarded };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
