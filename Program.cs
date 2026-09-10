using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        // The words that can appear in our list.
        string[] possibleWords =
        {
            "æble",
            "banan",
            "citron",
            "appelsin",
            "pære"
        };

        // Create a list containing 1,000 random words.
        List<string> words = new List<string>();

        Random random = new Random();

        for (int i = 0; i < 1000; i++)
        {
            int index = random.Next(possibleWords.Length);
            words.Add(possibleWords[index]);
        }

        // Thread-safe dictionary.
        ConcurrentDictionary<string, int> wordCounter =
            new ConcurrentDictionary<string, int>();

        // We use 4 threads.
        int numberOfThreads = 4;

        // Each thread gets 250 words.
        int wordsPerThread = words.Count / numberOfThreads;

        List<Thread> threads = new List<Thread>();

        // Create the four threads.
        for (int i = 0; i < numberOfThreads; i++)
        {
            int threadNumber = i;

            Thread thread = new Thread(() =>
            {
                // Calculate where this thread starts.
                int startIndex = threadNumber * wordsPerThread;

                // Calculate where this thread stops.
                int endIndex = startIndex + wordsPerThread;

                // Count this thread's words.
                for (int j = startIndex; j < endIndex; j++)
                {
                    string word = words[j];

                    // Add the word if it does not exist.
                    // Otherwise increase its existing value.
                    wordCounter.AddOrUpdate(
                        word,
                        1,
                        (key, oldValue) => oldValue + 1
                    );
                }
            });

            threads.Add(thread);
            thread.Start();
        }

        // Wait for all threads to finish.
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        // Display the result.
        Console.WriteLine("CONCURRENT DICTIONARY");
        Console.WriteLine("----------------------------");

        foreach (KeyValuePair<string, int> entry in wordCounter)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        // Calculate the total number of counted words.
        int actualTotal = 0;

        foreach (int count in wordCounter.Values)
        {
            actualTotal += count;
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine($"Expected total: {words.Count}");
        Console.WriteLine($"Actual total:   {actualTotal}");

        if (actualTotal == words.Count)
        {
            Console.WriteLine("Result is correct!");
        }
        else
        {
            Console.WriteLine("Result is WRONG!");
        }
    }
}