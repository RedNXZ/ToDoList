using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    private static TaskManager taskManager = new TaskManager();
    private static FileHandler fileHandler = new FileHandler("tasks.json");

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // Поддержка кириллицы
        var tasks = fileHandler.LoadTasks();
        foreach (var task in tasks)
        {
            taskManager.AddTask(task.Id, task.Description, task.IsCompleted);
        }

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== Список задач ==========");
            Console.ResetColor();
            Console.WriteLine("\n1. Добавить задачу");
            Console.WriteLine("2. Просмотр задач");
            Console.WriteLine("3. Завершить задачу");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Фильтрация задач");
            Console.WriteLine("6. Сохранить и выйти");
            Console.WriteLine("7. Выйти без сохранения");
            Console.WriteLine("==================================");
            Console.WriteLine("Выберите опцию:");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    FilterTasks();
                    break;
                case "6":
                    SaveAndExit();
                    return;
                case "7":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неверная опция, попробуйте снова.");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    private static void AddTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var description = Console.ReadLine();
        taskManager.AddTask(description);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Задача добавлена!");
        Console.ResetColor();
    }

    private static void ViewTasks()
    {
        Console.WriteLine("\nВсе задачи:");
        foreach (var task in taskManager.GetTasks())
        {
            if (task.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(task);
        }
        Console.ResetColor();
    }

    private static void CompleteTask()
    {
        Console.WriteLine("Введите ID задачи для завершения:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            taskManager.CompleteTask(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задача завершена!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неверный ID");
        }
        Console.ResetColor();
    }

    private static void DeleteTask()
    {
        Console.WriteLine("Введите ID задачи для удаления:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            taskManager.DeleteTask(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задача удалена!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неверный ID");
        }
        Console.ResetColor();
    }

    private static void FilterTasks()
    {
        Console.WriteLine("\n1. Все задачи");
        Console.WriteLine("2. Завершенные задачи");
        Console.WriteLine("3. Незавершенные задачи");
        Console.WriteLine("Выберите опцию:");

        var choice = Console.ReadLine();

        List<Task> tasks;
        switch (choice)
        {
            case "1":
                tasks = taskManager.GetTasks();
                break;
            case "2":
                tasks = taskManager.GetTasksByStatus(true);
                break;
            case "3":
                tasks = taskManager.GetTasksByStatus(false);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверная опция");
                return;
        }

        Console.WriteLine("\nОтфильтрованные задачи:");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    private static void SaveAndExit()
    {
        fileHandler.SaveTasks(taskManager.GetTasks());
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Задачи сохранены. Выход...");
        Console.ResetColor();
    }
}

