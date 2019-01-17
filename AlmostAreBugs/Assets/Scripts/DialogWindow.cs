using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWindow : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void WriteALine( string str )
    {
        try {
            if( str == null )
                throw new System.ArgumentNullException();
            if( str.Split( '\n' ).Length > 1 )
                throw new System.ArgumentException();
            // '\n'+gameObject의 Text(mesh pro)에 한 줄을 넣는 부분이 들어가야함.

        } catch(System.Exception ex) {

            throw;
        }
    }
}
