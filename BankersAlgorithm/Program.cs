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
        }
        static bool request(int[] available)
        {

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