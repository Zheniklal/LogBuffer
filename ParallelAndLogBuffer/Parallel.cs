using MyThreads;
using System;
using System.Collections.Generic;
using System.Text;
using static MyThreads.TaskQueue;


namespace Lab4
{
    static class Parallel
    {
        public static void WaitAll(TaskDelegate[] tasks)
        {
            TaskQueue taskQueue = new TaskQueue(tasks.Length);
            foreach(var task in tasks)
            {
                taskQueue.EnqueueTask(task);
            }

            taskQueue.Interrupt();
        }
    }
}
