using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // Shared variable used by all threads
    static int counter = 0;

    static void Main()
    {
        int numberOfThreads = 4;
        int incrementsPerThread = 100000;

        List<Thread> threads = new List<Thread>();

        // Create 4 threads
        for (int i = 0; i < numberOfThreads; i++)
        {
            Thread thread = new Thread(() =>
            {
                for (int j = 0; j < incrementsPerThread; j++)
                {
                    counter++;
                }
            });

            threads.Add(thread);
            thread.Start();
        }

        // Wait for all threads to finish
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        int expected = numberOfThreads * incrementsPerThread;

        Console.WriteLine($"Expected result: {expected}");
        Console.WriteLine($"Actual result:   {counter}");
    }
}