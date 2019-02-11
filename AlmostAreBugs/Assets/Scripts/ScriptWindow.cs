using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ScriptWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private GameObject scriptWindow;
    private const float DELAYTIME=5.0f;
    private float passedTime=0;
    private bool IsWriteEventTriggered = true;
    private bool IsMouseOnScriptWindow = false;
    // Start is called before the first frame update
    void Start() {
        scriptWindow = GameObject.Find( "ScriptWindow" );
    }

    // Update is called once per frame
    void Update() {
        if( IsMouseOnScriptWindow )
            ScriptWindowOn();
        if( IsWriteEventTriggered ) {
            passedTime += Time.deltaTime;
            if( passedTime > DELAYTIME ) {
                StartCoroutine( "FadeOut" );
                IsWriteEventTriggered = false;
            }
        }
    }

    public void WriteALine( string str )
    {
        try {
            if( str == null )
                throw new System.ArgumentNullException();
            if( str.Split( '\n' ).Length > 1 )
                throw new System.ArgumentException();
            // '\n'+gameObject의 Text(mesh pro)에 한 줄을 넣는 부분이 들어가야함.
            ScriptWindowOn();
        } catch(System.Exception ex) {

            throw;
        }
    }

    public void ScriptWindowOn() {
        IsWriteEventTriggered = true;
        Color color1 = scriptWindow.GetComponent<Image>().color;
        color1.a = 1.0f;
        scriptWindow.GetComponent<Image>().color = color1;

        Color color2 = scriptWindow.GetComponentInChildren<TextMeshProUGUI>().color;
        color2.a = 1.0f;
        scriptWindow.GetComponentInChildren<TextMeshProUGUI>().color = color2;
        passedTime = 0;
    }

    IEnumerator FadeOut() {
        Color color1=scriptWindow.GetComponent<Image>().color;
        Color color2= scriptWindow.GetComponentInChildren<TextMeshProUGUI>().color;
        while( color1.a>0f) {
            color1.a -= 0.05f;
            scriptWindow.GetComponent<Image>().color = color1;
            color2.a -= 0.05f;
            scriptWindow.GetComponentInChildren<TextMeshProUGUI>().color = color2;
            yield return new WaitForSeconds( 0.02f );
        }
    }

    public void OnPointerEnter( PointerEventData eventData ) {
        IsMouseOnScriptWindow = true;
    }
    public void OnPointerExit(PointerEventData eventData ) {
        IsMouseOnScriptWindow = false;
    }
}
