using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        // Create a list to store our threads
        List<Thread> threads = new List<Thread>();

        // Create 3 threads
        for (int i = 1; i <= 3; i++)
        {
            int threadNumber = i;

            Thread thread = new Thread(() => CountNumbers(threadNumber));

            threads.Add(thread);
            thread.Start();
        }

        // Main thread waits for all 3 threads to finish
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("All threads have finished.");
    }

    static void CountNumbers(int threadNumber)
    {
        Random random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(
                $"Worker {threadNumber} - Thread ID: " +
                $"{Thread.CurrentThread.ManagedThreadId} - Number: {i}"
            );

            // Wait a random amount of time
            Thread.Sleep(random.Next(100, 500));
        }
    }
}