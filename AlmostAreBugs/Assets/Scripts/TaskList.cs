using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
public class TaskList : MonoBehaviour
{
    private StringBuilder strBuilder;
    private TextMeshProUGUI taskListText;
    // Start is called before the first frame update
    void Start()
    {
        taskListText = GameObject.Find( "TaskListText" ).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    
    private void EditTaskList() {
        taskListText.text = strBuilder.ToString();
    }

    private void AddStrikethrough(int line) {
        string[] strarray = strBuilder.ToString().Split( '\n' );
        int len = 0;
        int i;
        for( i = 0; i < line; i++ )
            len += strarray[ i ].Length;
        strBuilder.Insert( len, "<s>" );
        strBuilder.Insert( len + strarray[ i ].Length, "</s>" );
    }
}