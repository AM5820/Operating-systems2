using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public static int size_show = 200;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Merge Sort";
            label7.Text = "First n = " + size_show+ " items"; 
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static int size = 1000000;

        public static Stopwatch s1;
        public static Stopwatch s2;
        public static int[] unsorted_arr = new int[size];
        public static int[] sorted_arr;

        static void start()
        {
            

            int[] arr = new int[size];

            

            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                int rand = random.Next(1, 10000);
                arr[i] = rand;
                unsorted_arr[i] = rand;
            }

            int arr_size = arr.Length;



            var watch1 = Stopwatch.StartNew();
            mergeSort(arr, 0, arr_size - 1);
            watch1.Stop();

            var watch2 = Stopwatch.StartNew();
            mergeSort2(arr, 0, arr_size - 1);
            watch2.Stop();

            s1 = watch1;
            s2 = watch2;

            sorted_arr = arr;

        }

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
                int m = (l + r) / 2;

                // Sort first and second halves
                Task t1 = new Task(() => mergeSort(arr, l, m));
                t1.Start();
                Task t2 = new Task(() => mergeSort(arr, m + 1, r));
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

        private void button1_Click(object sender, EventArgs e)
        {

            start();

            textBox1.Text = " " + Math.Round(s1.Elapsed.TotalSeconds, 5) + "  sec.";
            textBox2.Text = " " + Math.Round(s2.Elapsed.TotalSeconds, 5) + "  sec.";

            textBox3.Text = " " + s1.ElapsedMilliseconds + "  Millisec.";
            textBox4.Text = " " + s2.ElapsedMilliseconds + "  Millisec.";

            label4.Text = "~";
            label5.Text = "~";


            textBox1.Font = new Font(textBox1.Font.FontFamily, 16);
            textBox2.Font = new Font(textBox2.Font.FontFamily, 16);
            textBox3.Font = new Font(textBox3.Font.FontFamily, 16);
            textBox4.Font = new Font(textBox4.Font.FontFamily, 16);

            label4.Font = new Font(label4.Font.FontFamily, 14);
            label5.Font = new Font(label4.Font.FontFamily, 14);

            label6.Text = "Sorted !!";


            for (int i = 0; i < size_show; i++)
            {
                textBox5.AppendText(unsorted_arr[i] + "\t");
                textBox6.AppendText(sorted_arr[i] + "\t");
            }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
    }
}
