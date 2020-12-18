using System;
using System.Threading;
using static MyThreads.TaskQueue;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskDelegate[] tasks = new TaskDelegate[15];

            for(int i = 0; i < tasks.Length; i++)
            {
                int num = i;
                tasks[i] = delegate { i++; i--; i = i + 1 - 1; Console.WriteLine("Task {0} completed", num); };
            }
            Parallel.WaitAll(tasks);
            Console.WriteLine("All tasks completed");
            Console.ReadLine();
            
            LogBuffer logBuffer = new LogBuffer(@"C:\Users\Владелец\source\repos\Lab4\Lab4\ListOfLogs.txt");
            for(int i = 0; i < 50; i++)
            {
                Thread.Sleep(100);
                logBuffer.Add($"Message {i}. Something happened");
            }

            Console.WriteLine("LogBuffer stopped");
            logBuffer.Dispose();
            Console.ReadLine();
        }
    }
}
