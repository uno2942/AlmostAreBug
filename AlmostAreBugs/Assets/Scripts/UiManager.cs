using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private DialogWindow dialogWindow;
    // Start is called before the first frame update
    void Start()
    {
        dialogWindow = GameObject.Find( "DialogWindow" ).GetComponent<DialogWindow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
