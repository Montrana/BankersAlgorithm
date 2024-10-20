/// Name: Montana Nicholson
/// Date: 10/16/2024
/// Class: Intro to operating systems
/// Summary: This project is an example of how the Banker's algorithm would work in an actual computer, 
/// and verifies that deadlock is avoided at all times.
/// 
/// Sources used: https://www.javatpoint.com/bankers-algorithm-in-operating-system 
///     Used to get step by step examples of the process and what the available resources should be at each time.

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
            PrintArray(need);

            Console.WriteLine("Allocation: ");
            PrintArray(allocation);

            Console.WriteLine("Max: ");
            PrintArray(max);

            Console.WriteLine("Available: ");
            PrintArray(available);

            List<int> finished = new List<int>();

            while(!CheckComplete(need)) //Need to continue looping as long as there are processes that need to be completed
            {
                for (int i = 0; i < allocation.Length; i++)
                {
                    if(!CheckComplete(need[i]))//Preventing processes that are already complete from issuing unneeded requests for resources.
                    {
                        if (Request(need[i], available, allocation[i], out int[] newNeed, out int[] newAvailable, out int[] newAllocation))
                        {
                            int[][] tempNeed = need;
                            int[][] tempAllocation = allocation;
                            tempNeed[i] = newNeed;
                            tempAllocation[i] = newAllocation;
                            if (Safety(newAvailable, tempNeed, tempAllocation))
                            {
                                need = tempNeed;
                                allocation = tempAllocation;
                                Console.WriteLine("Available: ");
                                PrintArray(newAvailable);

                                // When processes complete, their resources need to be made available again
                                for (int j = 0; j < available.Length; j++)
                                {
                                    available[j] = newAvailable[j] + allocation[i][j];
                                }


                                Console.WriteLine("\n\nAdded Process " + i);

                                Console.WriteLine("Need: ");
                                PrintArray(need);

                                Console.WriteLine("Allocation: ");
                                PrintArray(allocation);

                                Console.WriteLine("Max: ");
                                PrintArray(max);

                                Console.WriteLine("Available: ");
                                PrintArray(available);

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

        /// <summary>
        /// Used for checking if there are any processes that still need to be completed or not
        /// </summary>
        /// <param name="need">The need matrix of all processes</param>
        /// <returns>True/False, indicating if there are still processes that need to be completed.</returns>
        static bool CheckComplete(int[][] need)
        {
            for (int i = 0; i < need.Length; i++)
            {
                for (int j = 0; j < need[i].Length; j++)
                {
                    if (need[i][j] != 0)
                    {
                        return false; //Return false if any process still has resourses that it needs allocated.
                    }
                }
            }
            return true; //Return true if any process does not have any additional resources it needs allocated.
        }
        /// <summary>
        /// Checks if the specific process has been completed
        /// </summary>
        /// <param name="need">Need Matrix of a single process</param>
        /// <returns>True/False if the process has been completed or not</returns>
        static bool CheckComplete(int[] need)
        {
            for (int i = 0; i < need.Length; i++)
            {
                if (need[i] != 0)
                {
                    return false; //Return false if the process still has resourses that it needs allocated.
                }
            }
            return true; //Return true if the process does not have any additional resources it needs allocated.
        }
        /// <summary>
        /// Request Allocation that allows processes to request resources
        /// </summary>
        /// <param name="need">The resources that the process still needs</param>
        /// <param name="available">The resources that are available</param>
        /// <param name="allocation">The resources that have been allocated to the specific process</param>
        /// <param name="newNeed">The updated need, will be permenant if the state remains safe</param>
        /// <param name="newAvailable">The updated availability, will be permenant if the state remains safe</param>
        /// <param name="newAllocation">The updated allocation, will be permenant if the state remains safe</param>
        /// <returns>True/False depending on if the request is valid or not</returns>
        static bool Request(int[] need, int[] available, int[] allocation, out int[] newNeed, out int[] newAvailable, out int[] newAllocation)
        {
            int[] requested = need;

            newNeed = new int[need.Length];
            newAvailable = new int[available.Length];
            newAllocation = new int[allocation.Length];
            
            ///Checking if any requested resource amount exceeds the process' need
            ///This is ultimately not necessary since processes are requesting all resources they need, 
            ///but would be useful if processes only requested a portion of the resources they needed.
            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > need[i])
                {
                    return false; //Returns false since the request is not valid
                }
            }

            ///Verifies that the requested resource amounts do not exceed the amount that is available
            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > available[i])
                {
                    return false; //Returns false since the request is not valid
                }
            }
            /*
            Console.WriteLine("\nRequested: ");
            PrintArray(requested);

            Console.WriteLine("\nAvailable: ");
            PrintArray(available);*/

            //Updating the availability, allocation, and need if the request is valid.
            for (int i = 0; i < requested.Length; i++)
            {
                newAvailable[i] = available[i] - requested[i];
                newAllocation[i] = allocation[i] + requested[i];
                newNeed[i] = need[i] - requested[i];
            }
            /*
            Console.WriteLine("\nRequested: ");
            PrintArray(requested);

            Console.WriteLine("\nAvailable: ");
            PrintArray(newAvailable);*/

            return true; //Returns true if the request is valid
        }

        /// <summary>
        /// Safety algorithm to determine if the cpu will remain in a safe state if the pending change will go through
        /// </summary>
        /// <param name="available">Pending availability matrix</param>
        /// <param name="need">Pending need matrix</param>
        /// <param name="allocation">Pending allocation matrix</param>
        /// <returns>True/False to indicate if the pending change will keep the CPU in a safe state</returns>
        static bool Safety(int[] available, int[][] need, int[][]allocation)
        {
            int[] work = new int[available.Length];

            /// Can't do work = available since when work gets changed, available will be updated too.
            for(int i = 0; i < available.Length; i++)
            {
                work[i] = available[i];
            }
            bool[] finish = new bool[allocation.Length];

            /*Console.WriteLine("Available: ");
            printArray(available);
            Console.WriteLine();*/
            
            // Initializing all values of finish to false.
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
                        if(need[i][j] > work[j]) // Checks if the process i can't be completed given the work available
                        {
                            work[j] = work[j] + allocation[i][j]; // Gets new resource allocation
                        }
                        /*Console.WriteLine("Work: ");
                        printArray(work);
                        Console.WriteLine();*/
                    }
                    finish[i] = true;
                }
            }

            /// Checks if the state is safe for all processes
            for (int i = 0; i < finish.Length; i++)
            {
                if (!finish[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Prints a one dimensional array
        /// </summary>
        /// <param name="array">Array to print</param>
        static void PrintArray(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.Write("]");
        }
        /// <summary>
        /// Prints a two dimensional array
        /// </summary>
        /// <param name="array">Array to print</param>
        static void PrintArray(int[][] array)
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