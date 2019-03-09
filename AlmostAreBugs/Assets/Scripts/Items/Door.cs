using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : Item {
    private bool isOpenable;
    private bool isOpened=false;
    public bool IsOpenable { get => isOpenable; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isOpenable = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Clicked()
    {
        base.Clicked();
        OpenDoor();
    }
    private void OpenDoor() {
        if( !( GameManager.GameManagerInstance.IsWatingForAnotherItemForMix || GameManager.GameManagerInstance.IsWatingForAnotherItemForUse || GameManager.GameManagerInstance.IsWatingForButton ) ) {
            if( isOpenable ) {
                Debug.Log( "openAble" );
               
                if( isOpened ) {
                    Debug.Log( "Scene try to Load" );
                    UnityEngine.SceneManagement.SceneManager.LoadScene( "gameEnd" );
                    
                    //OpenEvent
                    //gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/door_closed" );
                    //isOpened = false;
                } else {
                    gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>( "Image/door_opened" );
                    isOpened = true;
                    TaskList.TaskListInstance.AddStrikethrough(13);
                }
            }
        }
    }
    private IEnumerator frameDelay() {
        yield return null;
    }

        public void OpenableTheDoor() {
        isOpenable = true;
        OpenDoor();
    }
}
