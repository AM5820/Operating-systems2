using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace app3
{
    class Program
    {
        static int i = 0;

        static void merge(int[] arr, int l, int m, int r)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            /* create temp arrays */
            int[] L = new int[n1], R = new int[n2];

            /* Copy data to temp arrays L[] and R[] */
            for (i = 0; i < n1; i++)
                L[i] = arr[l + i];
            for (j = 0; j < n2; j++)
                R[j] = arr[m + 1 + j];

            /* Merge the temp arrays back into arr[l..r]*/
            i = 0; // Initial index of first subarray 
            j = 0; // Initial index of second subarray 
            k = l; // Initial index of merged subarray 
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            /* Copy the remaining elements of L[], if there 
               are any */
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            /* Copy the remaining elements of R[], if there 
               are any */
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        /* l is for left index and r is right index of the 
           sub-array of arr to be sorted */
        static void mergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for 
                // large l and h 
                int m = l + (r - l) / 2;

                // Sort first and second halves
                Thread thread = new Thread(new ThreadStart(()=>mergeSort(arr, l, m)));
                thread.Start();
                Thread thread2 = new Thread(new ThreadStart(() => mergeSort(arr, m+1, r)));
                thread2.Start();

                merge(arr, l, m, r);
            }
        }

        static void mergeSort2(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for 
                // large l and h 
                int m = l + (r - l) / 2;

                // Sort first and second halves
                Thread thread = new Thread(new ThreadStart(() => mergeSort(arr, l, m)));
                thread.Start();
                Thread thread2 = new Thread(new ThreadStart(() => mergeSort(arr, m + 1, r)));
                thread2.Start();

                merge(arr, l, m, r);
            }
        }

        static void printArray(int[] A, int size)
        {
            int i;
            for (i = 0; i < size; i++)
                Console.WriteLine(A[i]);
        }


        static void Main(string[] args)
        {

          
                
            


            
            int[] arr = new int[100];

            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(1, 10000);
            }

            //int[] arr = { 12, 11, 13, 5, 6, 7 ,20,123,324,34,45,5,67,79,34,666,777,333,23,54,77,798,1020,1232,345,3433,4335,459,887,908,707,303,1024};
            int arr_size = arr.Length;
            
            

            var watch1 = Stopwatch.StartNew();
            mergeSort(arr, 0, arr_size - 1);
            watch1.Stop();

            var watch2 = Stopwatch.StartNew();
            mergeSort2(arr, 0, arr_size - 1);
            watch2.Stop();


            Console.WriteLine("parallel processing Time = " + watch1.ElapsedMilliseconds + " milliseconds");
            Console.WriteLine("sequential processing Time = " + watch2.ElapsedMilliseconds + " milliseconds");
            
            Console.ReadKey();
        }
    }
}
