using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class Parser {
    private readonly string[] fileNames = new string[] { @"ItemScript.json", @"ScriptOnclick.json" };
    public readonly ItemScripts loadedDataForItemScripts;
    public readonly ScriptOnclicks loadedDataForScriptOnclicks;
    public Parser() {
        foreach( var fileName in fileNames ) {
            string filePath = Path.Combine( Application.streamingAssetsPath, fileName );
            if( File.Exists( filePath ) ) {
                string dataAsJson;

                switch( fileName ) {
                case @"ItemScript.json":
                    dataAsJson = File.ReadAllText( filePath );
                    loadedDataForItemScripts = JsonUtility.FromJson<ItemScripts>( dataAsJson );
                    break;
                case @"ScriptOnclick.json":
                    dataAsJson = File.ReadAllText( filePath );
                    loadedDataForScriptOnclicks = JsonUtility.FromJson<ScriptOnclicks>( dataAsJson );
                    break;
                }
            } else
                Debug.LogError( "File Not Exsits" );
        }
    }
}

[System.Serializable]
public class ItemScriptData {
    public string codeName;
    public string stringName;
    public string boxScript;
}

[System.Serializable]
public class ItemScripts {
    public ItemScriptData[] ItemScript;
}

[System.Serializable]
public class ScriptOnclickData {
    public string target;
    public string hangulName;
    public string objectOnclick;
}

[System.Serializable]
public class ScriptOnclicks {
    public ScriptOnclickData[] ScriptOnclick;
}