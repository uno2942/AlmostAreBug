using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    [System.Flags]
    public enum BugList { Bugs };
    Dictionary<BugList, bool> bugDic;

    void Start()
    {
        bugDic = new Dictionary<BugList, bool>();
        foreach( BugList bug in (BugList[]) System.Enum.GetValues( typeof( BugList ) ) ) 
        {
            bugDic[ bug ] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsTheBugOvercomed( BugList bug ) {
        return bugDic[ bug ];
    }
}
