using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FileHandler
{
    private readonly string filePath;

    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public void SaveTasks(List<Task> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(filePath, json);
    }

    public List<Task> LoadTasks()
    {
        if (!File.Exists(filePath))
        {
            return new List<Task>();
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Task>>(json);
    }
}
