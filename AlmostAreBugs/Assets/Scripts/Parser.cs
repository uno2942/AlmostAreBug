using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Parser
{
    private readonly string[] fileNames = new string[] {"" };
    public Parser() {
        foreach( var fileName in fileNames ) {
            string filePath = Path.Combine( Application.streamingAssetsPath, fileName );
            if( File.Exists( filePath ) ) {
                string dataAsJson = File.ReadAllText( filePath );
            } else
                Debug.LogError( "File Not Exsits" );
        }

    }
}
