using System.Collections.Generic;

public class Task
{
    public int id;
    public string description;
    public bool checkedState= false;
    public int[] nextTasks = null;
    public Dictionary<string, int> requirements;
    public string successMessage;

    public Task(int id, string description,int[] next,Dictionary<string, int> requirements, string successMessage)
    {
        this.id = id;
        this.description = description;
        this.nextTasks = next;
        this.requirements = requirements;
        this.successMessage = successMessage;
    }

}