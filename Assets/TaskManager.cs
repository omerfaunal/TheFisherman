using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public InventoryManagement inventoryManagement;
    public UIManager uiManager;
    public Task[] activeTasks;
    public Dictionary<int, Task> tasks =new ()
    {
        { 0, new Task("Find a fishing rod", new[] {1}, new Dictionary<string, int>(){ {"fishing rod",1}} )},
        { 1, new Task("Collect 3 logs", new[] {2}, new Dictionary<string, int>(){ {"log",3}}) },
        { 2, new Task("Find a bucket (follow pink trees to reach the hill)", new[] {3}, new Dictionary<string, int>(){ {"bucket",1}}) },
        { 3, new Task("Catch 1 Tuna", new[] {4,5}, new Dictionary<string, int>(){ {"tuna",1}}) },
        { 4, new Task("Catch 1 Salmon", new[] {6}, new Dictionary<string, int>(){ {"salmon",1}}) },
        { 5, new Task("Catch 1 Carp", new[] {6}, new Dictionary<string, int>(){ {"carp",1}}) },
        { 6, new Task("Catch 1 Largemouth", new int[] {}, new Dictionary<string, int>(){ {"largemouth",1}}) },
    };
    
    // Start is called before the first frame update
    void Start()
    {
        activeTasks = new[]{ tasks[0] };
        uiManager.DisplayTasks(activeTasks);
    }

    // Update is called once per frame
    public void CheckTaskState()
    {
        bool flag = true;
        foreach (Task task in activeTasks)
        {
            if ( isRequirementFulfilled(task.requirements))
            {
                task.checkedState = true;
            }
            else
            {
                flag = false;
            }
        }

        if (flag)
        {
            Task activeTask = activeTasks[0];
            activeTasks = new Task[activeTask.nextTasks.Length];
            
            for (var i = 0 ; i < activeTask.nextTasks.Length ; i++ )
            {
                activeTasks[i] = tasks[activeTask.nextTasks[i]];
            }
        }
        
        uiManager.DisplayTasks(activeTasks);
    }

    public bool isRequirementFulfilled(Dictionary<string,int> req)
    {
        foreach (KeyValuePair<string, int> kvp in req)
        {
            if (inventoryManagement.GetItemQuantity(kvp.Key) < kvp.Value)
            {
                return false;
            }
        }

        return true;
    }
}
