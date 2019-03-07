using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
public class TaskList : MonoBehaviour
{
    private static TaskList taskList;
    private static bool mShuttingDown = false;
    private static object mLock = new object();

    private StringBuilder strBuilder;
    private TextMeshProUGUI taskListText;

    public static TaskList TaskListInstance
    {
        get
        {
            if( mShuttingDown ) {
                Debug.LogWarning( "TaskList is already destroyed." );
                return null;
            }
            lock( mLock ) {
                if( taskList == null ) {
                    taskList = (TaskList) FindObjectOfType<TaskList>();
                    if( taskList == null ) {
                        Debug.LogWarning( "TaskList gameObject does not exists." );
                        return null;
                    }
                }
                return taskList;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        taskListText = TaskListInstance.GetComponentInChildren<TextMeshProUGUI>();
        strBuilder = new StringBuilder( taskListText.text );
    }

    // Update is called once per frame
    
    private void EditTaskList() {
        taskListText.text = strBuilder.ToString();
    }

    private void AddStrikethrough(int line) {
        string[] strarray = strBuilder.ToString().Split( '\n' );
        int len = 0;
        int len2 = 0;
        int i;
        for( i = 0; i < line; i++ )
            len += strarray[ i ].Length;
        strBuilder.Insert( len+1, "<s>" );
        len2 = len + 1 + strarray[ i ].Length + "<s>".Length;
        strBuilder.Insert( len2 , "</s>" );
        taskListText.text = strBuilder.ToString();
    }

    private void OnApplicationQuit() {
        mShuttingDown = true;
    }


    private void OnDestroy() {
        mShuttingDown = true;
    }
}