using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [System.Flags]
    public enum ItemList { Empty, Pillow, Quilt, Scissors, DeskKey,
        Matchbox, Match, BurningMatch, CardBox, Bullet, Gun, CutCandle,
        LoadedGun, CandleGun, EmptyPaper, CutPapaerUp, CutPaperDown,
        PasswordPapeUp, PasswordPaperDown, PasswordPaper, CardKey,
        EmptyBed, Desk, Stick, Multitab, Fax, Door };

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
