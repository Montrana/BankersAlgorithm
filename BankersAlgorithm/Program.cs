// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Numerics;

namespace BankersAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] available = [[3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 3, 2],
                                 [3, 0, 2],
                                 [3, 0, 2],
                                 [3, 0, 0],
                                 [3, 0, 0],
                                 [3, 0, 0]];
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

            int instancesA = 10;
            int[] resourceA = new int[instancesA];

            int instancesB = 5;
            int[] resourceB = new int[instancesB];

            int instancesC = 7;
            int[] resourceC = new int[instancesC];
        }
        static bool request(int[] request, int[] need, int[] available, int[] allocation, out int[]? newNeed, out int[]? newAvailable, out int[]? newAllocation, out bool wait)
        {
            newNeed = new int[need.Length];
            newAvailable = new int[available.Length];
            newAllocation = new int[allocation.Length];
            for (int i = 0; i < request.Length; i++)
            {
                if (request[i] > need[i])
                {
                    newNeed = null;
                    newAvailable = null;
                    newAllocation = null;
                    wait = false;
                    return false;
                }
            }
            for (int i = 0; i < request.Length; i++)
            {
                if (request[i] > available[i])
                {
                    newNeed = null;
                    newAvailable = null;
                    newAllocation = null;
                    wait = true;
                    return false;
                }
            }
            for (int i = 0; i < request.Length; i++)
            {
                newAvailable[i] = available[i] - request[i];
                newAllocation[i] = allocation[i] + request[i];
                newNeed[i] = need[i] - request[i];
            }
            wait = false;
            return true;
        }
        static bool Safety(int[] work, int[] need)
        {
            bool[] finish = new bool[work.Length];

            for (int i = 0; i < finish.Length; i++)
            {
                finish[i] = false;
            }
            for (int i = 0; i <= work.Length; i++)
            {
                if (!finish[i] && need[i] <= work[i])
                {
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