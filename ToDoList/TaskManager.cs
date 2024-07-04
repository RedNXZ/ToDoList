using System.Collections.Generic;
using System.Linq;

public class TaskManager
{
    private List<Task> tasks;
    private int nextId;

    public TaskManager()
    {
        tasks = new List<Task>();
        nextId = 1;
    }

    public void AddTask(string description)
    {
        tasks.Add(new Task(nextId++, description));
    }

    public void CompleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsCompleted = true;
        }
    }

    public void DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
        }
    }

    public List<Task> GetTasks(bool? isCompleted = null)
    {
        if (isCompleted == null)
        {
            return tasks;
        }
        return tasks.Where(t => t.IsCompleted == isCompleted.Value).ToList();
    }

    public List<Task> GetTasksByStatus(bool isCompleted)
    {
        return tasks.Where(t => t.IsCompleted == isCompleted).ToList();
    }
}
