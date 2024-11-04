using System;

namespace sitic_gtp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks tasks = new Tasks();

            foreach (var task in tasks.tasks) {
                Console.WriteLine($"id: {task.id}, nombre:{task.name}, " +
                    $"priority:{task.priority.id}, {task.priority.priority}, {task.priority.code}, " +
                    $"state: {task.state.id}, {task.state.status}, {task.state.code}");
            }
        }
    }
}
