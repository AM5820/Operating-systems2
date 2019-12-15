using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mazfinal2
{
    public partial class Form1 : Form
    {

        public char[,] maze1 = new char[9, 9] {
                {'w','w','w','w','b','w','w','b','w'},
                {'w','b','w','w','w','b','w','b','w'},
                {'w','w','b','w','b','w','w','b','w'},
                {'w','b','w','w','w','w','w','w','w'},
                {'b','w','s','b','w','b','w','w','w'},
                {'w','b','w','w','w','w','w','w','w'},
                {'w','w','b','b','w','w','w','b','b'},
                {'b','w','w','w','w','w','b','w','w'},
                {'g','w','w','w','w','w','w','w','w'},
            };
        public char[,] maze = new char[9, 9];

        public int R = 9, C = 9;                                       // size = R   
        public int sr = -1, sc = -1;                                   // start postion
        public Queue rq = new Queue();                                 // row queue for storing row index while traversing
        public Queue cq = new Queue();                                 //  column queue for storing coloumn index while traversing
        public Queue sq = new Queue();                                 //  state queue for storing state of car while traversing state =>car direction (up,down,right,left)


        public int move_count = 0;                                    // counting nodes visited 
        public int nodes_left_in_layer = 1;                            // how many nodes in the layer/level ==> in BFS
        public int nodes_in_next_layer = 0;                            // how many nodes in the next layer/level ==> in BFS

        public bool reached_end = false;                               // check for goal state

        public int[,] visited = new int[9, 9] {
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0}
            };                      // used for viewing maze with all visited nodes



        public Tuple<int, int>[,] tup = new Tuple<int, int>[9, 9]
        {
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null},
        }; // initiailized null nodes where it will hold null and position of previous node i came from to it.

        public int solved = 0;


        // NORTH , EAST , SOUTH , WEST
        public int[] dr = new int[4] { -1, 0, 1, 0 };                       // direction row
        public int[] dc = new int[4] { 0, 1, 0, -1 };                       // direction col


        public static string[] direction = new string[4] { "up", "right", "down", "left" };    // directions vector
        public string state = direction[0];                                             // state of car => direction of car




        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public Form1()
        {
            InitializeComponent();


            

            /*
            
             // saving maze in textfile
            using (TextWriter tw = new StreamWriter("D:/FCAI/Level 4/Os2/C#/Mazefinal2/Mazfinal2/Maze8.txt"))
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j <9; j++)
                    {
                        tw.Write(maze1[i, j]);
                    }
                    //tw.WriteLine();
                }
            }
            
            */

        }

        int solve()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (maze[i, k] == 's')

                    {
                        this.sr = i;
                        this.sc = k;
                    }
                }
            }

            this.visited = new int[9, 9] {
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0}
            };


            this.solved = 0;
            this.move_count = 0;
            this.reached_end = false;
            this.rq = new Queue();
            this.cq = new Queue();
            this.sq = new Queue();
            this.nodes_left_in_layer = 1;
            this.nodes_in_next_layer = 0;
            



            this.rq.Enqueue(this.sr);
            this.cq.Enqueue(this.sc);
            this.sq.Enqueue(this.state);

     



            while (this.rq.Count > 0 && move_count<30) //or cq.Count > 0
            {
                int r = (int)this.rq.Dequeue();
                int c = (int)this.cq.Dequeue();
                string s = (string)this.sq.Dequeue();



                if (this.maze[r, c] == 'g')
                {
                    this.reached_end = true;
                    this.move_count++;
                    break;
                }

                explore_neighbours(r, c, s);

                //track number of steps to reach goal
                this.nodes_left_in_layer--;
                if (this.nodes_left_in_layer == 0)
                {
                    this.nodes_left_in_layer = nodes_in_next_layer;
                    this.nodes_in_next_layer = 0;
                    this.move_count++;

                }
            }
            if (this.reached_end)
            {
                solved = 1;
                return this.move_count;
            }
            else
            {
                solved = 0;
                return -1;
            }
            
        }

        void explore_neighbours(int r, int c, string s)
        {


            int rr1 = 0;
            int cc1 = 0;

            int rr2 = 0;
            int cc2 = 0;


            if (s == "up")
            {
                rr1 = r + this.dr[0];
                cc1 = c + this.dc[0];

                rr2 = r + this.dr[1];
                cc2 = c + this.dc[1];



                //skip out of bounds locations
                if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {
                    this.rq.Enqueue(rr1);
                    this.cq.Enqueue(cc1);
                    this.sq.Enqueue("up");
                    this.nodes_in_next_layer++;
                    this.visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }
                if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                {
                    this.rq.Enqueue(rr2);
                    this.cq.Enqueue(cc2);
                    this.sq.Enqueue("right");
                    this.nodes_in_next_layer++;
                    this.visited[rr2, cc2] = 1;

                    if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                }



            }
            else if (s == "right")
            {
                rr1 = r + dr[1];
                cc1 = c + dc[1];

                rr2 = r + dr[2];
                cc2 = c + dc[2];


                //skip out of bounds locations
                if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {
                    this.rq.Enqueue(rr1);
                    this.cq.Enqueue(cc1);
                    this.sq.Enqueue("right");
                    this.nodes_in_next_layer++;
                    this.visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }
                if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                {
                    this.rq.Enqueue(rr2);
                    this.cq.Enqueue(cc2);
                    this.sq.Enqueue("down");
                    this.nodes_in_next_layer++;
                    this.visited[rr2, cc2] = 1;

                    if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                }



            }
            else if (s == "down")
            {
                rr1 = r + dr[2];
                cc1 = c + dc[2];

                rr2 = r + dr[3];
                cc2 = c + dc[3];


                //skip out of bounds locations
                if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {
                    this.rq.Enqueue(rr1);
                    this.cq.Enqueue(cc1);
                    this.sq.Enqueue("down");
                    this.nodes_in_next_layer++;
                    this.visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }
                if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                {
                    this.rq.Enqueue(rr2);
                    this.cq.Enqueue(cc2);
                    this.sq.Enqueue("left");
                    this.nodes_in_next_layer++;
                    this.visited[rr2, cc2] = 1;

                    if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                }


            }
            else if (s == "left")
            {
                rr1 = r + dr[3];
                cc1 = c + dc[3];

                rr2 = r + dr[0];
                cc2 = c + dc[0];


                //skip out of bounds locations
                if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {
                    this.rq.Enqueue(rr1);
                    this.cq.Enqueue(cc1);
                    this.sq.Enqueue("left");
                    this.nodes_in_next_layer++;
                    this.visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }
                if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                {
                    this.rq.Enqueue(rr2);
                    this.cq.Enqueue(cc2);
                    this.sq.Enqueue("up");
                    this.nodes_in_next_layer++;
                    this.visited[rr2, cc2] = 1;

                    if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                }



            }

        }


        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "solving";
            Thread t1 = new Thread(() => solve());
            var watch = Stopwatch.StartNew();

            t1.Start();
            t1.Join();

            watch.Stop();
            label3.Text = watch.ElapsedMilliseconds+ "ms";
            if (solved == 1)
            {
                print();
                label4.Text = "explored levels = "+move_count;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No solution");
            }
            label1.Text = "Done";
        }

        public void print()
        {
            Label[,] labels = new Label[9, 9]{
                { cell1,cell2,cell3,cell4,cell5,cell6,cell7,cell8,cell9},
                { cell10,cell11,cell12,cell13,cell14,cell15,cell16,cell17,cell18},
                { cell19,cell20,cell21,cell22,cell23,cell24,cell25,cell26,cell27},
                { cell28,cell29,cell30,cell31,cell32,cell33,cell34,cell35,cell36},
                { cell37,cell38,cell39,cell40,cell41,cell42,cell43,cell44,cell45},
                { cell46,cell47,cell48,cell49,cell50,cell51,cell52,cell53,cell54},
                { cell55,cell56,cell57,cell58,cell59,cell60,cell61,cell62,cell63},
                { cell64,cell65,cell66,cell67,cell68,cell69,cell70,cell71,cell72},
                { cell73,cell74,cell75,cell76,cell77,cell78,cell79,cell80,cell81},


            };


            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if(this.maze[i,k] == 'g' || this.maze[i, k] == 's')
                    {
                        continue;
                    }
                    else if (this.visited[i, k]==1)
                    {
                        labels[i, k].BackColor = Color.GreenYellow;
                    }
                        
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {

        Label[,] labels = new Label[9, 9]{
                { cell1,cell2,cell3,cell4,cell5,cell6,cell7,cell8,cell9},
                { cell10,cell11,cell12,cell13,cell14,cell15,cell16,cell17,cell18},
                { cell19,cell20,cell21,cell22,cell23,cell24,cell25,cell26,cell27},
                { cell28,cell29,cell30,cell31,cell32,cell33,cell34,cell35,cell36},
                { cell37,cell38,cell39,cell40,cell41,cell42,cell43,cell44,cell45},
                { cell46,cell47,cell48,cell49,cell50,cell51,cell52,cell53,cell54},
                { cell55,cell56,cell57,cell58,cell59,cell60,cell61,cell62,cell63},
                { cell64,cell65,cell66,cell67,cell68,cell69,cell70,cell71,cell72},
                { cell73,cell74,cell75,cell76,cell77,cell78,cell79,cell80,cell81},


            };
            
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.ShowDialog();

            filename.Text = openFileDialog.SafeFileName;
            string filepath = openFileDialog.FileName;


            var fileStream = openFileDialog.OpenFile();
            var fileContent = string.Empty;

             
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContent = reader.ReadToEnd();
               
            }
            

            char[] linearMaze = fileContent.ToCharArray();

            int j = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    this.maze[i, k] = linearMaze[j];
                    j++;
                }
            }
            

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (maze[row, col].Equals('b'))
                    {
                        labels[row, col].BackColor = Color.Black;
                    }
                    if (maze[row, col].Equals('w'))
                    {
                        labels[row, col].BackColor = Color.White;
                    }
                    if (maze[row, col].Equals('g'))
                    {
                        labels[row, col].BackColor = Color.Red;
                    }

                    if (maze[row, col].Equals('s'))
                    {
                        labels[row, col].BackColor = Color.Yellow;
                    }

                }

            }


            





        }
    }
}
