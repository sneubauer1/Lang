using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace grid
{
    class Program
    {
        public static int NUM_ROW = 12;
        public static int NUM_COL = 20;
        public static int NUM_GEN = 10;    

        static void fillGrid(int[,] G, int high)
        {
            var rand = new Random(2);
            //srand(2);

            for (int i = 0; i < NUM_ROW; i++)
            {
                for(int j = 0; j < NUM_COL; j++)
                {
                    G[i,j] = rand.Next(0, high);
                    //G[i, j] = rand()% high;
                }
            }

        }
        static void printGrid( int[,] G )
        {
            for (int i = 0; i < NUM_ROW; i++)
            {
                for (int j = 0; j < NUM_COL; j++)
                {
                    Console.Write($"{G[i, j], 4:d}");
                }
                Console.Write("\n");
            }
        }

        static int checker(int[,] arr, int x, int y)
        {
            int currentBlock = arr[x,y];

            int sum = currentBlock;

            if (x - 1 >= 0)
            {
                sum += arr[x - 1,y];
                if (y - 1 >= 0)
                {
                    sum += arr[x - 1, y - 1];
                }
                if (y + 1 < NUM_COL)
                {
                    sum += arr[x - 1, y + 1];
                }
            }
            if (y - 1 >= 0)
            {
                sum += arr[x, y - 1];
            }
            if (y + 1 < NUM_COL)
            {
                sum += arr[x, y + 1];
            }
            if (x + 1 < NUM_ROW)
            {
                sum += arr[x + 1, y];
                if (y - 1 >= 0)
                {
                    sum += arr[x + 1, y - 1];
                }
                if (y + 1 < NUM_COL)
                {
                    sum += arr[x + 1, y + 1];
                }
            }
            //Console.WriteLine("calculated sum is: %d\n", sum);
            //Thread.Sleep(sum % 11 * 1500);
            if (sum % 10 == 0)
            {
                return 0;
            }
            else if (sum < 50)
            {
                return currentBlock + 3;
            }
            else if (sum > 150)
            {
                return 1;
            }
            else
            {
                if (currentBlock - 3 > 0)
                {
                    return currentBlock - 3;
                }
                else
                    return 0;
            }
        }

        static void Main(string[] args)
        {
            int numRows = Program.NUM_ROW;
            int numCols = Program.NUM_COL;
            int numGens = Program.NUM_GEN;

            int[,] A = new int [numRows, numCols];
            int[,] B = new int[numRows, numCols];
            int checks = 0;
            long time = 0;
            fillGrid(A, 20);
            int source;
            Console.WriteLine("-----------------Generation 1---------------------\n");
            printGrid(A);
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            List<long> ticks = new List<long>();
            for (int i = 2; i <= numGens; ++i)
            {
                if (i % 2 == 0)
                    source = 0; // populate array B based on A
                else
                    source = 1; // populate array A based on B
                Stopwatch elapsed = new Stopwatch();
                elapsed.Start();
                for (int j = 0; j < numRows * numCols; j++)
                {
                    int x = j / numCols;
                    int y = j % numCols;

                    if (source == 0)
                    {
                        B[x,y] = checker(A, x, y);
                    }
                    else
                    {
                        A[x,y] = checker(B, x, y);
                    }
                    checks++;
                }
                elapsed.Stop();
                ticks.Add(elapsed.ElapsedTicks);
                //time = stopwatch.ElapsedTicks;
                Console.WriteLine($"-----------------Generation {i}---------------------\n", i);
                if (i % 2 == 0)
                {
                    printGrid(B);
                }
                else
                {
                    printGrid(A);
                }
            }
            
            Console.WriteLine($"Total checks: {checks}");

            //stopwatch.Stop();
            double avg = ticks.Average();
            TimeSpan averageTimeSpan = new TimeSpan((long)avg);
            Console.WriteLine($"Time: {averageTimeSpan}");
        }
    }
}
