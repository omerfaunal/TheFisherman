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
        { 0, new Task(0, "Find a fishing rod", new[] {1}, new Dictionary<string, int>(){ {"fishing rod",1}}, "Great! You found a fishing rod but it's broken. You need to collect 3 logs to fix it." )},
        { 1, new Task(1, "Collect 3 logs", new[] {2}, new Dictionary<string, int>(){ {"log",3}}, "Nice! You fixed your fishing rod. Now you need a bucket to put the fish you caught. Go to the hill in the deadwood forest following pink trees") },
        { 2, new Task(2, "Find a bucket (follow pink trees to reach the hill)", new[] {3}, new Dictionary<string, int>(){ {"bucket",1}}, "Good news: You are ready to catch delicious fishes!") },
        { 3, new Task(3, "Catch 1 Tuna (River Fish)", new[] {4}, new Dictionary<string, int>(){ {"tuna",1}}, "You completed river fishes.") },
        { 4, new Task(4, "Catch 1 Salmon (Sea Fish)", new[] {5,6}, new Dictionary<string, int>(){ {"salmon",1}}, "You completed sea fishes.") },
        { 5, new Task(5, "Catch 1 Carp (Lake Fish)", new int[] {}, new Dictionary<string, int>(){ {"carp",1}}, "You caught all the fishes in the game!") },
        { 6, new Task(6, "Catch 1 Largemouth (Lake Fish)", new int[] {}, new Dictionary<string, int>(){ {"largemouth",1}}, "You caught all the fishes in the game!") },
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
                if (task.id == 1) {
                    inventoryManagement.AddItem("fixed rod", CollectibleItemType.Crafting);
                }
            }
            else
            {
                if(task.id == 1 ) {
                    task.description = "Collect 3 logs" + " (" + inventoryManagement.GetItemQuantity("log") + "/3)";
                 }
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

            uiManager.DisplaySuccessMessage(activeTask.successMessage);
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
