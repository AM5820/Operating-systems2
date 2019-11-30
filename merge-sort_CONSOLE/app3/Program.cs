using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace app3
{
    class Program
    {
        static int i = 0;

        static void mergeparts(int[] arr, int l, int m, int r)
        {

            // Merges two subarrays of arr[]. 
            // First subarray is arr[l..m] 
            // Second subarray is arr[m+1..r] 
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
                int m = (l+r)/ 2;

                // Sort first and second halves
                Task t1 = new Task(() => mergeSort(arr, l, m));
                t1.Start();
                Task t2 = new Task(() => mergeSort(arr, m+1, r));
                t2.Start();


                mergeparts(arr, l, m, r);
            }
        }

        static void mergeSort2(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for 
                // large l and h 
                int m = (l + r) / 2;

                // Sort first and second halves
                mergeSort2(arr, l, m);
                mergeSort2(arr, m + 1, r);


                mergeparts(arr, l, m, r);
            }
        }

        static void printArray(int[] A, int size)
        {
            int i;
            for (i = 0; i < size; i++)
                Console.Write(A[i] + " ");
        }


        static void Main(string[] args)
        {

            
            int[] arr = new int[10000000];

            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(1, 10000);
            }

           
            int arr_size = arr.Length;
            
            

            var watch1 = Stopwatch.StartNew();
            mergeSort(arr, 0, arr_size - 1);
            watch1.Stop();

            var watch2 = Stopwatch.StartNew();
            mergeSort2(arr, 0, arr_size - 1);
            watch2.Stop();



            Console.WriteLine("parallel   processing Time = " + watch1.ElapsedMilliseconds + " milliseconds\t" + Math.Round(watch1.Elapsed.TotalSeconds,1) +" seconds");
            Console.WriteLine("sequential processing Time = " + watch2.ElapsedMilliseconds + " milliseconds\t" + Math.Round(watch2.Elapsed.TotalSeconds, 1) + " seconds");


            //printArray(arr, arr_size);


            Console.ReadKey();
        }
    }
}
