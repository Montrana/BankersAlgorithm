/// https://www.javatpoint.com/bankers-algorithm-in-operating-system

using System;
using System.Collections.Generic;
using System.Numerics;

namespace BankersAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] available = [3, 3, 2];
            int[][] max = [[7, 5, 3],
                           [3, 2, 2],
                           [9, 0, 2],
                           [2, 2, 2],
                           [4, 3, 3]];
            int[][] allocation = [[0, 1, 0],
                                  [2, 0, 0],
                                  [3, 0, 2],
                                  [2, 1, 1],
                                  [0, 0, 2]];

            int[][] need = new int[allocation.Length][];
            for (int i = 0; i < need.Length; i++)
            {
                need[i] = new int[available.Length];
            }
            for (int i = 0; i < max.Length; i++)
            {
                for (int j = 0; j < allocation[i].Length; j++)
                {
                    need[i][j] = max[i][j] - allocation[i][j];
                }
            }
            
            Console.WriteLine("Need: ");
            printArray(need);

            Console.WriteLine("Allocation: ");
            printArray(allocation);

            Console.WriteLine("Max: ");
            printArray(max);

            Console.WriteLine("Available: ");
            printArray(available);

            List<int> finished = new List<int>();

            while(!CheckComplete(need))
            {
                for (int i = 0; i < allocation.Length; i++)
                {
                    if(!CheckComplete(need[i]))
                    {
                        if (Request(need[i], available, allocation[i], out int[] newNeed, out int[] newAvailable, out int[] newAllocation))
                        {
                            int[][] tempNeed = need;
                            int[][] tempAllocation = allocation;
                            tempNeed[i] = newNeed;
                            tempAllocation[i] = newAllocation;
                            if (Safety(newAvailable, tempNeed, tempAllocation, allocation.Length))
                            {
                                need = tempNeed;
                                allocation = tempAllocation;
                                Console.WriteLine("Available: ");
                                printArray(newAvailable);
                                for (int j = 0; j < available.Length; j++)
                                {
                                    available[j] = newAvailable[j] + allocation[i][j];
                                }
                                Console.WriteLine("\n\nAdded Process " + i);

                                Console.WriteLine("Need: ");
                                printArray(need);

                                Console.WriteLine("Allocation: ");
                                printArray(allocation);

                                Console.WriteLine("Max: ");
                                printArray(max);

                                Console.WriteLine("Available: ");
                                printArray(available);

                                finished.Add(i + 1);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("<");
            foreach (int i in finished)
            {
                Console.Write("P" + i + ", ");
            }
            Console.Write(">");
        }
        static bool CheckComplete(int[][] need)
        {
            for (int i = 0; i < need.Length; i++)
            {
                for (int j = 0; j < need[i].Length; j++)
                {
                    if (need[i][j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static bool CheckComplete(int[] need)
        {
            for (int i = 0; i < need.Length; i++)
            {
                if (need[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        static bool Request(int[] need, int[] available, int[] allocation, out int[] newNeed, out int[] newAvailable, out int[] newAllocation)
        {
            int[] requested = need;

            newNeed = new int[need.Length];
            newAvailable = new int[available.Length];
            newAllocation = new int[allocation.Length];

            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > need[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > available[i])
                {
                    return false;
                }
            }
            
            Console.WriteLine("\nRequested: ");
            printArray(requested);

            Console.WriteLine("\nAvailable: ");
            printArray(available);
            for (int i = 0; i < requested.Length; i++)
            {
                newAvailable[i] = available[i] - requested[i];
                newAllocation[i] = allocation[i] + requested[i];
                newNeed[i] = need[i] - requested[i];
            }
            Console.WriteLine("\nRequested: ");
            printArray(requested);

            Console.WriteLine("\nAvailable: ");
            printArray(newAvailable);

            return true;
        }


        static bool Safety(int[] available, int[][] need, int[][]allocation, int numProcesses)
        {
            int[] work = new int[available.Length];
            for(int i = 0; i < available.Length; i++)
            {
                work[i] = available[i];
            }
            bool[] finish = new bool[numProcesses];

            /*Console.WriteLine("Available: ");
            printArray(available);
            Console.WriteLine();*/

            for (int i = 0; i < finish.Length; i++)
            {
                finish[i] = false;
            }
            for (int i = 0; i < finish.Length; i++)
            {
                if (!finish[i])
                {
                    for (int j = 0; j < work.Length; j++)
                    {
                        if(need[i][j] > work[j])
                        {
                            work[j] = work[j] + allocation[i][j];
                        }
                        /*Console.WriteLine("Work: ");
                        printArray(work);
                        Console.WriteLine();*/
                    }
                    finish[i] = true;
                }
            }
            for (int i = 0; i < finish.Length; i++)
            {
                if (!finish[i])
                {
                    return false;
                }
            }
            return true;
        }
        static void printArray(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.Write("]");
        }
        static void printArray(int[][] array)
        {
            Console.Write("[");
            for(int i = 0;i < array.Length;i++)
            {
                Console.Write("[");
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + ", ");
                }
                Console.Write("]");
            }
            Console.Write("]");
            Console.WriteLine();
        }
    }
}