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

    public void AddTask(int id, string description, bool isCompleted)
    {
        tasks.Add(new Task(id, description) { IsCompleted = isCompleted });
        nextId = tasks.Max(t => t.Id) + 1;
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
            RenumberTasks();
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

    private void RenumberTasks()
    {
        int newId = 1;
        foreach (var task in tasks)
        {
            task.Id = newId++;
        }
        nextId = newId;
    }
}
