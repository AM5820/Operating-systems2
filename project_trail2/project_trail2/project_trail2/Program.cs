using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_trail2
{
    class Program
    {
        
       



        static void Main(string[] args)
        {
            
            char[,] maze = new char[9, 9] {
                {'b','w','b','w','w','w','b','b','b'},
                {'w','w','g','w','w','b','b','w','w'},
                {'w','b','b','w','w','w','w','w','w'},
                {'w','w','b','b','b','b','w','w','w'},
                {'w','w','w','w','w','b','b','w','w'},
                {'w','b','b','w','w','w','b','w','w'},
                {'w','w','w','w','w','w','w','w','w'},
                {'b','b','w','w','b','b','w','w','w'},
                {'b','w','w','w','w','w','w','w','b'},
            };

            
            int R = 9, C = 9;                                       // size = R   
            int sr = 8, sc = 3;                                     // start postion
            Queue rq = new Queue();                                 // row queue for storing row index while traversing
            Queue cq = new Queue();                                 //  column queue for storing coloumn index while traversing
            Queue sq = new Queue();                                 //  state queue for storing state of car while traversing state =>car direction (up,down,right,left)


            int move_count = 0 ;                                    // counting nodes visited 
            int nodes_left_in_layer = 1;                            // how many nodes in the layer/level ==> in BFS
            int nodes_in_next_layer = 0;                            // how many nodes in the next layer/level ==> in BFS

            bool reached_end = false;                               // check for goal state

            int[,] visited = new int[9, 9] {
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

            

            Tuple<int, int>[,] tup = new Tuple<int, int>[9, 9]
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

            //Tuple<int>[,] cat = new Tuple<int>[9, 9];

            

            // NORTH , EAST , SOUTH , WEST
            int[] dr = new int[4] {-1,0,1,0};                       // direction row
            int[] dc = new int[4] {0,1,0,-1};                       // direction col


            string[] direction = new string[4] { "up", "right", "down", "left" };    // directions vector
            string state = direction[0];                                             // state of car => direction of car

            Console.WriteLine(solve());
            


            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (maze[i, k] == 'g')
                    {
                        Console.Write("g" + " ");
                        continue;
                    }
                    else
                    {
                        Console.Write(visited[i, k] + " ");
                    }
                }
                Console.WriteLine();
            }


            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (tup[i, k] == null)
                    {
                        Console.Write("(--,--)");
                    }
                    else
                    {
                        Console.Write(tup[i, k] + " ");
                    }
                    
                }
                Console.WriteLine();
            }


            Console.ReadKey();

            // BFS algorithm
            int solve()
            {
                rq.Enqueue(sr);
                cq.Enqueue(sc);
                sq.Enqueue(state);

                List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                List<IList> path2 = new List<IList>();

                //visited[sr, sc] = true;

                

                while (rq.Count > 0) //or cq.Count > 0
                {
                    int r = (int)rq.Dequeue();
                    int c = (int)cq.Dequeue();
                    string s = (string)sq.Dequeue();

                    //path.Add(Tuple.Create(r, c));
                    //path2.Add(path);

                    
                    Console.Write(r+""+c);
                    Console.WriteLine();

                    if (maze[r, c] == 'g')
                    {
                        reached_end = true;
                        move_count++;
                        break;
                    }
                    
                    explore_neighbours(r, c, s);

                    //track number of steps to reach goal
                    nodes_left_in_layer --;
                    if (nodes_left_in_layer == 0)
                    {
                        nodes_left_in_layer = nodes_in_next_layer;
                        nodes_in_next_layer = 0;
                        move_count++;
                        
                    }
                }
                if (reached_end)
                {
                    return move_count;
                }
                else
                {
                    return -1;
                }
            }

            void explore_neighbours(int r, int c,string s)
            {


                int rr1 = 0;
                int cc1 = 0;

                int rr2 = 0;
                int cc2 = 0;


                if (s == "up")
                {
                    rr1 = r + dr[0];
                    cc1 = c + dc[0];

                    rr2 = r + dr[1];
                    cc2 = c + dr[1];

                    

                    //skip out of bounds locations
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue("up");
                        nodes_in_next_layer++;
                        visited[rr1, cc1] = 1;

                        if(tup[rr1, cc1] == null) tup[rr1,cc1] = new Tuple<int, int>(r,c);
                    }
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {
                        rq.Enqueue(rr2);
                        cq.Enqueue(cc2);
                        sq.Enqueue("right");
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }


                    
                }
                else if (s == "right")
                {
                    rr1 = r + dr[1];
                    cc1 = c + dc[1];

                    rr2 = r + dr[2];
                    cc2 = c + dr[2];
                    

                    //skip out of bounds locations
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue("right");
                        nodes_in_next_layer++;
                        visited[rr1, cc1] = 1;

                        if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                    }
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {
                        rq.Enqueue(rr2);
                        cq.Enqueue(cc2);
                        sq.Enqueue("down");
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }



                }
                else if (s == "down")
                {
                    rr1 = r + dr[2];
                    cc1 = c + dc[2];

                    rr2 = r + dr[3];
                    cc2 = c + dr[3];

                    
                    //skip out of bounds locations
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue("down");
                        nodes_in_next_layer++;
                        visited[rr1, cc1] = 1;

                        if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                    }
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {
                        rq.Enqueue(rr2);
                        cq.Enqueue(cc2);
                        sq.Enqueue("left");
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }


                }
                else if (s == "left")
                {
                    rr1 = r + dr[3];
                    cc1 = c + dc[3];

                    rr2 = r + dr[0];
                    cc2 = c + dr[0];


                    //skip out of bounds locations
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue("left");
                        nodes_in_next_layer++;
                        visited[rr1, cc1] = 1;

                        if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                    }
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {
                        rq.Enqueue(rr2);
                        cq.Enqueue(cc2);
                        sq.Enqueue("up");
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }



                }

            }




        }
    }
}
