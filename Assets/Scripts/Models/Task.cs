using System.Collections.Generic;

public class Task
{
    public string description;
    public bool checkedState= false;
    public int[] nextTasks = null;
    public Dictionary<string, int> requirements;

    public Task(string description,int[] next,Dictionary<string, int> requirements)
    {
        this.description = description;
        this.nextTasks = next;
        this.requirements = requirements;
    }

}