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
            /*int[][] available = [[3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 0, 2],
                                 [3, 0, 2],
                                 [3, 0, 0],
                                 [3, 0, 0],
                                 [3, 0, 0]];*/
            int [] available = [3, 3, 2]
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

            int[][] need = max - allocation;

            List<int> finished;

            for (int i = 0; i <= allocation.Length; i++)
            {
                int[][] newNeed;
                int[] newAvailable;
                int[][] newAllocation;
                if (Request(i, need[i], need, available, allocation, newNeed, newAvailable, newAllocation))
                {
                    if(Safety(newAvailable, newAllocation, allocation.length))
                    {
                        need = newNeed;
                        allocation = newAllocation;
                        for(int j = 0; j <= available[i].Length; i++)
                        {
                            available[i][j] = newAvailable[i][j] + allocation[i][j];
                        }
                        finished.add(i);
                    }
                }
            }
            console.writeline(finished);
        }
        static bool Request(int processID, int[] requested, int[][] need, int[] available, int[][] allocation, out int[][]? newNeed, out int[]? newAvailable, out int[][]? newAllocation)
        {
            newNeed = new int[need.Length][need[0].length];
            newAvailable = new int[available.Length];
            newAllocation = new int[allocation.Length];

            //int[] resourceIDs = [-1, -1, -1];

            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > need[processID][i])
                {
                    newNeed = null;
                    newAvailable = null;
                    newAllocation = null;
                    return false;
                }
            }
            for (int i = 0; i < requested.Length; i++)
            {
                if (requested[i] > available[i])
                {
                    newNeed = null;
                    newAvailable = null;
                    newAllocation = null;
                    return false;
                }
            }
            /// If we were not able to find an available resource for one of the requests
            /*if (resourceIDs[0] == -1 || resourceIDs[1] == -1 || resourceIDs[2] = -1)
            {
                newNeed = null;
                newAvailable = null;
                newAllocation = null;
                wait = true;
                return false;
            }*/
            for (int i = 0; i < requested.Length; i++)
            {
                newAvailable[i] = available[i] - requested[i];

                newAllocation[processID][i] = allocation[processID][i] + requested[i];
                newNeed[processID][i] = need[processID][i] - requested[i];
            }
            return true;
        }


        static bool Safety(int[] available, int[][] need, int numProcesses)
        {
            int[] work = Available;
            bool[] finish = new bool[numProcesses];

            for (int i = 0; i < finish.Length; i++)
            {
                finish[i] = false;
            }
            for (int i = 0; i <= finish.Length; i++)
            {
                if (!finish[i])
                {
                    for (int j = 0; j <= work.length; j++)
                    {
                        if(need[i][j] > work[j])
                        {
                            work[j] = work[j] + allocation[i][j]
                        }
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
    }
}