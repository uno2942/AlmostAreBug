using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager itemManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

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
        Matchbox, Match, BurningMatch, CardBox, Door, Bullet, Gun, Closet, Candle,
        LoadedGun, CandleGun, EmptyPaper, Paper, 
        PasswordPapeUp, PasswordPaperDown, PasswordPaper, CardKey,
        Table, Desk, Stand, TV, SangSangDo };

    [System.Flags]
    public enum PresentState { Default, Dropped, Gotten, Discarded };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
