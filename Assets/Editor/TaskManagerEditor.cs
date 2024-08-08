using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(TaskManager))]
public class TaskManagerEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    TaskManager taskManager = FindFirstObjectByType<TaskManager>();//(TaskManager)target;

    //    GUILayout.Label("Task Queue", EditorStyles.boldLabel);
    //    if(taskManager.availableTasks != null)
    //    {
    //        for (int i = 0; i < taskManager.availableTasks.Count; i++)
    //        {
    //            Task task = taskManager.availableTasks[i];
    //            GUILayout.Label($"Task {i + 1}: {task.Description}");
    //        }
    //    }
        
    //    // Save any changes made to the task manager
    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(taskManager);
    //    }
    //}
}