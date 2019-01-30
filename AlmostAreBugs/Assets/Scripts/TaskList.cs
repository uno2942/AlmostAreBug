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
        strBuilder = new StringBuilder( taskListText.text );
        AddStrikethrough( 1 );
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
}