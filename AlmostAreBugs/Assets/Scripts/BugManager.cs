using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class BugManager : MonoBehaviour
{
    private static BugManager bugManager;
    private static bool mShuttingDown = false;
    private static object mLock = new object();
    public AudioClip[] audioClip;
    int nowAudio = 0;
    [System.Flags]
    public enum BugList { Pillow, TV, LoadedGun, FireMatch, Paper};
    Dictionary<BugList, bool> bugDic;

    private List<BugList> bugList;
    public static BugManager BugManagerInstance
    {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "SceneManager is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( bugManager == null ) {
                    bugManager = (BugManager) FindObjectOfType<BugManager>();
                    if( bugManager == null ) {
                        Debug.LogWarning( "SceneManager does not exists." );
                        return null;
                    }
                }
                return bugManager;
            }
        }
    }
    public static string BugName(BugList bug)
    {
        switch (bug)
        {
            case BugList.Pillow: return "배게 생성 버그";
            case BugList.TV: return "서랍 버그";
            case BugList.LoadedGun: return "총 버그";
            case BugList.FireMatch: return "불타는 성냥 버그";
            case BugList.Paper: return "종이 버그";
            default: return "";
        }
    }
    private void Awake() {
        GetComponent<AudioSource>().clip = audioClip[ 0 ];
        gameObject.GetComponent<AudioSource>().volume = 0.8f;
        gameObject.GetComponent<AudioSource>().Play();
    }


    void Start()
    {
        bugList = new List<BugList>();
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

    public void BugOvercomed(BugList bug ) {
        if (bugDic[bug] == false)
        {
            bugList.Remove(bug);
            ScriptWindow.ScriptWindowInstance.BugOvercome(bug);
        }
        bugDic[ bug ] = true;
        ChangeAudio();
    }
    public bool IsBugOvercomed( BugList bug ) {
        return bugDic[ bug ];
    }
    public void BugOccured(BugList bug) {
        if( bugDic[ bug ] == false && !(bugList.Exists(x => (x == bug))))
            bugList.Add( bug );
        ChangeAudio();
    }

    private void ChangeAudio() {
        if( bugList.Count > 0 && nowAudio == 0 ) {
            GetComponent<AudioSource>().clip = audioClip[ 1 ];
            gameObject.GetComponent<AudioSource>().volume = 0.6f;
            gameObject.GetComponent<AudioSource>().Play();
            nowAudio = 1;
        } else if( bugList.Count == 0 && nowAudio == 1 ) {
            GetComponent<AudioSource>().clip = audioClip[ 0 ];
            gameObject.GetComponent<AudioSource>().volume = 0.7f;
            gameObject.GetComponent<AudioSource>().Play();
            nowAudio = 0;
        }
    }
    public bool IsThereLeftBug() {
        if( bugList.Count > 0 )
            return true;
        return false;
    }
    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}
