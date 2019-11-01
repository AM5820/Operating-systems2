using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;


namespace app1
{
    class Program
    {
        public class Caesar
        {
            //private readonly Mutex m = new Mutex();

            string plaintext;
            string ciphertext;
            byte[] order;
            byte key;
            
            public void readmessege(string messege,byte key)
            {
                
                this.plaintext = messege;
                this.key = key;
                
            }

            public void getorder()
            {

                this.plaintext = this.plaintext.ToUpper();
                    this.order = Encoding.ASCII.GetBytes(plaintext);
                    for (int i = 0; i < this.order.Length; i++)
                    {
                        this.order[i] -= 65;
                    }
            }

            public void encrypt()
            {


                for (int i = 0; i < this.order.Length; i++)
                {
                    this.order[i] = (byte)(((int)this.order[i] +(int) this.key)%26);
                    string asciichar = (Convert.ToChar(this.order[i]+65)).ToString();
                    this.ciphertext += asciichar;
                }

                Console.WriteLine(this.ciphertext);
            }

            }


        static void mean(int[] dataset)
        {
            int sum = 0;
            for (int i = 0; i < dataset.Length; i++)
            {
                sum += dataset[i];
            }
            int mean = sum / dataset.Length; 
            Console.WriteLine("mean =  "+mean);
        }
        public static void categorize(int[] dataset)
        {
            int child = 0, teen = 0, youth = 0, man = 0 , oldman = 0;       //child => [1,14] , teen => [15,20] , youth => [21,35] , man=>[36,50] , old=>[51,100]

            for (int i = 0; i < dataset.Length; i++)
            {
                if (dataset[i] >= 1 && dataset[i] <= 14)
                    child++;
                else if (dataset[i] >= 15 && dataset[i] <= 20)
                    teen++;
                else if (dataset[i] >= 21 && dataset[i] <= 35)
                    youth++;
                else if (dataset[i] >= 36 && dataset[i] <= 50)
                    man++;
                else if (dataset[i] >= 51 && dataset[i] <= 100)
                    oldman++;
            }
            
            double child_prob = (double)child / dataset.Length;
            double teen_prob = (double)teen / dataset.Length;
            double youth_prob = (double)youth / dataset.Length;
            double man_prob = (double)man / dataset.Length;
            double oldman_prob = (double)oldman / dataset.Length;

            Console.WriteLine("prob. distribution = > child\t\t teen\t\t youth\t\t man\t\t oldman\n" + "\t\t\t "+child_prob+"\t "+teen_prob+"\t "+youth_prob+"\t "+man_prob+"\t "+oldman_prob);
            Console.WriteLine("\t\t\t" + child_prob*100 + "%\t " + teen_prob*100 + "%\t " + youth_prob*100 + "%\t " + man_prob*100 + "%\t " + oldman_prob*100+"%");

        }
        static void variance(int[] dataset)
        {
            int sum = 0;
            double x_sum = 0;
            int average = 0;
            for (int i = 0; i < dataset.Length; i++)
            {
                sum += dataset[i];
            }
            average = sum / dataset.Length;
            for (int i = 0; i < dataset.Length; i++)
            {
                x_sum += Math.Pow(dataset[i] - average ,2);
            }
            Console.WriteLine("variance = " + x_sum/dataset.Length);
        }

        public static object BinarySearchIterative(int[] inputArray, int key)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key == inputArray[mid])
                {
                    return ++mid;
                }
                else if (key < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return "Nil";
        }

        static int fib(int n)
        {
            if (n == 1)
                return 1;
            else if (n == 2)
                return 1;
            else
                return fib(n - 1) + fib(n - 2);
        }

        static void alg()

        {
            for (int i = 1; i <= 40; i++)
            {
                Console.WriteLine(i + " => " + fib(i));
                
            }

        }


        static void Merge(int[] input, int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        static void MergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }



        static void Main(string[] args)
        {

            int[] Arr = new int[10] { 10, 22, 30, 44, 50, 69, 72, 83, 94, 100 };
            int[] arr = new int[25]
                    {
                        1, 5, 4, 11, 20, 8, 2, 98, 90, 16,100,200,150,250,300,28,411,530,890,1100,2000,999,123,433,456
                    };


            int[] dataset = new int[10000000];

            Random random = new Random();
            for (int i = 0; i < dataset.Length; i++)
            {
                dataset[i] = random.Next(1,100);
            }



            var watch1 = Stopwatch.StartNew();
            
            mean(dataset);
            categorize(dataset);
            variance(dataset);
            watch1.Stop();
            

            //------------------parallel------------------
            
            Thread task1 = new Thread(()=>mean(dataset));
            Thread task2 = new Thread(()=>categorize(dataset));
            Thread task3 = new Thread(() => variance(dataset));

            var watch2 = Stopwatch.StartNew();
            Parallel.Invoke(()=>mean(dataset),()=>categorize(dataset),()=>variance(dataset));

            //task1.Start();
            //task2.Start();
            //task3.Start();

            watch2.Stop();
            
            Console.WriteLine("sequential processing Time = " + watch1.ElapsedMilliseconds + " milliseconds");
            Console.WriteLine("parallel processing Time = " + watch2.ElapsedMilliseconds + " milliseconds");
            Console.ReadKey();


            /*
            
            Caesar s1 = new Caesar();

            var watch2 = System.Diagnostics.Stopwatch.StartNew();

            s1.readmessege("I am student in FICH blablabla",3);
            s1.getorder();
            s1.encrypt();

            watch2.Stop();

            Caesar s2 = new Caesar();
            Thread task1 = new Thread(() => s2.readmessege("I am student in FICH blablabla",3));
            Thread task2 = new Thread(() => s2.getorder());
            Thread task3 = new Thread(() => s2.encrypt());


            

            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            task1.Start();
            task2.Start();
            task3.Start();
            watch1.Stop();

            Console.WriteLine("sequential processing Time = " + watch2.ElapsedMilliseconds + " milliseconds");
            Console.WriteLine("parallel processing Time = " + watch1.ElapsedMilliseconds + " milliseconds");

            Console.ReadKey();
            */


            /*
            while (true)
            {
                Console.WriteLine("1) seq \n2) parallel \n3)exit");
                string key = Console.ReadLine();
                if (int.Parse(key) == 1)
                {
                    var watch2 = System.Diagnostics.Stopwatch.StartNew();
                    alg();
                    watch2.Stop();
                    Console.WriteLine("sequential processing Time = " + watch2.Elapsed.TotalSeconds + " seconds");
                    
                }
                else if (int.Parse(key) == 2)
                {
                    
                    Task t3 = new Task(() => alg());
                    var watch1 = System.Diagnostics.Stopwatch.StartNew();
                    t3.Start();
                    watch1.Stop();
                    Console.WriteLine("parallel processing Time = " + watch1.Elapsed.TotalSeconds + " seconds");
                    
                }
                else if(int.Parse(key) == 3)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            */



            /*
            Console.WriteLine("parallel processing , sequential processing ...");

            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            Task t3 = new Task(() => alg());
            
            t3.Start();
            watch1.Stop();
            */
            /*
            Task t3 = new Task(() => MergeSort(arr, 0, arr.Length - 1));
            var watch1 = System.Diagnostics.Stopwatch.StartNew();

            watch1.Start();
            Parallel.Invoke(alg);
            watch1.Stop();
            
            
            //----------------------------------------------------

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            MergeSort(arr, 0, arr.Length - 1);
            watch2.Stop();
            

            Console.WriteLine("sequential processing Time = " + watch2.Elapsed.TotalSeconds + " seconds");
            Console.WriteLine("parallel processing Time = " + watch1.Elapsed.TotalSeconds + " seconds");

            Console.ReadKey();
            */


        }
    }
}