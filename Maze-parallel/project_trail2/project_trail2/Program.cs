using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace project_trail2
{
    //By AhmedRamadan

    class Program

    {
        int cs;//n of call of explore nei
        int size = 0;   //counter 
        int turn,f=0 ; //flage
       
        int R = 9, C = 9;                                       // size = R   
        public int sr = -1, sc = -1;


        int move_count = 1;                                    // counting nodes visited 
        int nodes_left_in_layer = 1;                            // how many nodes in the layer/level ==> in BFS
        int nodes_in_next_layer = 0;                            // how many nodes in the next layer/level ==> in BFS

        bool reached_end = false;
        int state = 1;



        // NORTH , EAST , SOUTH , WEST
        int[] dr = new int[4] { -1, 0, 1, 0 };                       // direction row
        int[] dc = new int[4] { 0, 1, 0, -1 };                       // direction col


        // check for goal state

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


        char[,] maze = new char[9, 9] {
                {'b','w','b','w','w','w','b','b','b'},
                {'g','w','g','w','w','b','b','w','w'},
                {'w','b','b','w','w','w','w','w','w'},
                {'w','w','b','b','b','b','w','w','w'},
                {'w','w','w','w','w','b','b','w','w'},
                {'w','b','b','w','w','g','b','w','w'},
                {'w','w','w','w','w','w','w','w','w'},
                {'b','b','w','w','b','b','w','w','w'},
                {'b','w','w','s','w','w','w','w','b'},
                                    /*--*/
        };


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


        



        List<int> si = new List<int>();//list of sizes

        //r,c,state
        //static IList<int> rq = new List<int>();
        //static IList<int> cq = new List<int>();
        //static IList<int> sq = new List<int>();
        Queue rq = new Queue();
        Queue cq = new Queue();
        Queue sq = new Queue();

        List<Tuple<int, int, int,int>> path = new List<Tuple<int, int, int, int>>();
        List<List<Tuple<int, int, int, int>>> path2 = new List<List<Tuple<int, int, int, int>>>();


        Tuple<int, int, int> g;


        public object Tupple { get; private set; }

        public int solve()
        {

            string fileContent = File.ReadAllText("D:/FCAI/Level 4/Os2/C#/Mazefinal2/Mazfinal2/Maze7.txt", System.Text.Encoding.UTF8);
            char[] linearMaze = fileContent.ToCharArray();


            int j = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    maze[i, k] = linearMaze[j];
                    j++;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    Console.Write(maze[i,k] + " ");
                }
                Console.WriteLine();
            }



            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (maze[i, k] == 's')

                    {
                        sr = i;
                        sc = k;
                    }
                }
            }


            //add sorc    
            rq.Enqueue(sr);
            cq.Enqueue(sc);
            sq.Enqueue(state);
      
            path.Add(Tuple.Create (sr, sc, state,move_count));
            path2.Add(path);

            while (rq.Count > 0) //or cq.Count > 0
            {





                //int r = rq[0];
                //rq.RemoveAt(0);
                //int c = cq[0];
                //cq.RemoveAt(0);
                //int s = sq[0];
                //sq.RemoveAt(0);
                int r = (int)rq.Dequeue();
                int c = (int)cq.Dequeue();
                int s = (int)sq.Dequeue();


                if (maze[r, c] == 'g')
                {
                    reached_end = true;
                    move_count++;
                    break;
                }
                int cs = 0;
                explore_neighbours(r, c, s);

                //Console.WriteLine("-" + cs + "-");
                cs++;

                    //track number of steps to reach goal
                    nodes_left_in_layer--;
                if (nodes_left_in_layer == 0)
                {
                    nodes_left_in_layer = nodes_in_next_layer;
                    nodes_in_next_layer = 0;
                    //Console.WriteLine();
                   move_count++;
                    //Console.Write(move_count);
                    
                    //if(move_count%3==0)
                    //    Thread.Sleep(5000);


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





        static void Main(string[] args)
        {


          



            Program o = new project_trail2.Program();
            int gr = 0, gc = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (o.maze[i, k] == 'g')

                    {
                        gr = i;
                        gc = k;
                    }
                }
            }
            //add source
            o.g = Tuple.Create(gr, gc, 0);

            //start t
            var watch1 = Stopwatch.StartNew();
            //solve
            int y = o.solve();
            //print path
            o.CreatePath();
            //end t
            var watch2 = Stopwatch.StartNew();


            Console.WriteLine("parallel   processing Time = " + watch1.ElapsedMilliseconds + " milliseconds\t" + Math.Round(watch1.Elapsed.TotalSeconds, 1) + " seconds");

            Console.WriteLine("parallel   processing Time = " + watch2.ElapsedMilliseconds + " milliseconds\t" + Math.Round(watch2.Elapsed.TotalSeconds, 1) + " seconds");

            Console.WriteLine("time" + (Math.Round(watch2.Elapsed.TotalSeconds, 1) - Math.Round(watch1.Elapsed.TotalSeconds, 1))) ;

                      Console.ReadKey();
        }








        void CreatePath() {

            List<int> sols = new List<int>();

            int CoPath = 0;
            bool ff = false;

            for (int k = path2.Count - 1; k > 0; k--)
            {
                size = 0;
                foreach (Tuple<int, int, int, int> l in path2[k])
                    size++;
                if (path2[k][size - 1].Item1 == g.Item1 && path2[k][size - 1].Item2 == g.Item2)
                {
                    ff = true;
                    CoPath = k;
                    sols.Add(k);

                }

            }

            if (ff == false)
                Console.WriteLine("No sol");
            bool f = true;
            int sol = 0;
            for (int i = 0; i < sols.Count; i++)
            {

                int ind= sols[i];

                for (int j= 0;j < path2[ind].Count;j++)
                {
                    if(j<path2[ind].Count-1  )
                        if (  ((path2[ind][j+1].Item3+1)== path2[ind][j].Item3) || ((path2[ind][j + 1].Item3 + 0) == path2[ind][j].Item3)|| ((path2[ind][j ].Item3 + 1) == path2[ind][j+1].Item3))
                        {
                            f = true;
                            sol = ind;
                        }
                    else { f = false; break; }

                    //Console.WriteLine(path2[ind][j].Item1 + "  " + path2[ind][j].Item2 + "  " + path2[ind][j].Item3);

                  
                }//end f
                if (f == true)
                {
                    Console.WriteLine(sol+ "  g = " +g.Item1+" " +g.Item2);
                    for(int l = 0; l < path2[sol].Count; l++)
                    {
                        Console.WriteLine(path2[sol][l].Item1 + "  " + path2[sol][l].Item2 + "  " + path2[sol][l].Item3);


                    }

                }
                f = true;



            }
        }



        // BFS algorithm
        void explores0(int s,int r,int  c)
        {
       
            turn = 0;
                
            int rr1=0, cc1=0,st=0;

          
                if (s == 1)//up
            {
                    //"up"
                    rr1 = r + dr[0];
                    cc1 = c + dc[0];

                    

                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {

                    
                    lock (this)
                    {


                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue(1);
                        st = 1;

                        Tuple<int, int, int,int> parents;

                        parents = Tuple.Create(r, c, s,move_count-1);
                        for (int i = path2.Count - 1; i >= 0; i--)
                        {
                          
                            foreach ( List<Tuple<int,int,int,int>> l in path2)
                            {
                                si.Add( l.Count);
                                size++;
                            }

                            if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                            {
                                List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                path3.AddRange(path2[i]);
                                path3.Add(Tuple.Create(rr1, cc1, st, move_count));
                                path2.Add(path3);
                            }
                            si = new List<int>();
                            size = 0;
                        }



                    }


                    nodes_in_next_layer++;
                    visited[rr1, cc1] = 1;
                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }

            }
            else if (s == 2)
                { //"right"
                  //r0


                    //"right"

                    rr1 = r + dr[1];
                    cc1 = c + dc[1];
                

                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {

                    lock (this)
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue(2); //"right"
                        st = 2;

                        Tuple<int, int, int,int> parents;

                        parents = Tuple.Create(r, c, s,move_count-1);
                        for (int i = path2.Count - 1; i >= 0; i--)
                        {

                            foreach (List<Tuple<int, int, int,int>> l in path2)
                            {
                                si.Add(l.Count);
                                size++;
                            }

                            if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                            {
                                List<Tuple<int, int, int,int>> path3 = new List<Tuple<int, int, int,int>>();
                                path3.AddRange(path2[i]);
                                path3.Add(Tuple.Create(rr1, cc1, st, move_count));
                                path2.Add(path3);
                            }
                            si = new List<int>();

                            size = 0;
                        }


                    }
                    nodes_in_next_layer++;
                    visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }
            }
            else if (s ==3 )
                {//"down"

                    //"down"

                    rr1 = r + dr[2];
                    cc1 = c + dc[2];


                    //skip out of bounds locations
                    //d0
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {

                    lock (this)
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue(3);//"down"
                        st = 3;

                        Tuple<int, int, int, int> parents;


                        parents = Tuple.Create(r, c, s, move_count - 1);
                        for (int i = path2.Count - 1; i >= 0; i--)
                        {

                            foreach (List<Tuple<int, int, int, int>> l in path2)
                            {
                                si.Add(l.Count);
                                size++;
                            }

                            if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                            {
                                List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                path3.AddRange(path2[i]);
                                path3.Add(Tuple.Create(rr1, cc1, st,move_count));
                                path2.Add(path3);
                            }
                            si = new List<int>();

                            size = 0;
                        }


                    }
                    nodes_in_next_layer++;
                    visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);
                }

            }
            else if (s ==4) {
                    //l0 
                    //left 4
                    //"left"

                    rr1 = r + dr[3];
                    cc1 = c + dc[3];

                  
                    if (!(rr1 < 0 || cc1 < 0) && !(rr1 >= R || cc1 >= C) && !(maze[rr1, cc1] == 'b'))
                {

                    lock (this)
                    {
                        rq.Enqueue(rr1);
                        cq.Enqueue(cc1);
                        sq.Enqueue(4);//"left"
                        st = 4;



                        Tuple<int, int, int, int> parents;


                        parents = Tuple.Create(r, c, s, move_count - 1); for (int i = path2.Count - 1; i >= 0; i--)
                        {

                            foreach (List<Tuple<int, int, int, int>> l in path2)
                            {
                                si.Add(l.Count);
                                size++;
                            }

                            if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                            {
                                List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                path3.AddRange(path2[i]);
                                path3.Add(Tuple.Create(rr1, cc1, st, move_count));
                                path2.Add(path3);
                            }

                            si = new List<int>();
                            size = 0;
                        }



                    }

                    nodes_in_next_layer++;
                    visited[rr1, cc1] = 1;

                    if (tup[rr1, cc1] == null) tup[rr1, cc1] = new Tuple<int, int>(r, c);

                }




            }







        }

        void explores1(int s,int r,int  c)
        {

            int rr1=0, cc1=0,  rr2=0, cc2=0,st2=0;
            turn =1;
            lock (this)
            {

                if (s == 1)//"up"


                {
                    //"up"
                   
                    rr2 = r + dr[1];
                    cc2 = c + dc[1];



                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {
                        lock (this)
                        {
                            rq.Enqueue(rr2);
                            cq.Enqueue(cc2);
                            sq.Enqueue(2);//"right"
                            st2 = 2;

                            Tuple<int, int, int, int> parents;


                            parents = Tuple.Create(r, c, s, move_count - 1); for (int i = path2.Count - 1; i >= 0; i--)
                            {

                                foreach (List<Tuple<int, int, int,int>> l in path2)
                                {
                                    si.Add(l.Count)  ;
                                    size++;
                                }

                                if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                                {
                                    List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                    path3.AddRange(path2[i]);
                                    path3.Add(Tuple.Create(rr2, cc2, st2, move_count));
                                    path2.Add(path3);
                                }

                                si = new List<int>();
                                size = 0;
                            }

                        }

                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }


                }
                else if (s == 2)//"right"
                {

                    //"right"

                    
                    rr2 = r + dr[2];
                    cc2 = c + dc[2];


                    //r1
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {

                        lock (this)
                        {
                            rq.Enqueue(rr2);
                            cq.Enqueue(cc2);
                            sq.Enqueue(3);//"down"
                            st2 = 3;
                            Tuple<int, int, int, int> parents;


                            parents = Tuple.Create(r, c, s, move_count - 1); for (int i = path2.Count - 1; i >= 0; i--)
                            {

                                foreach (List<Tuple<int, int, int, int>> l in path2)
                                {
                                    si.Add(l.Count);
                                    size++;
                                }

                                if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                                {
                                    List<Tuple<int, int, int,int>> path3 = new List<Tuple<int, int,int ,int>>();
                                    path3.AddRange(path2[i]);
                                    path3.Add(Tuple.Create(rr2, cc2, st2, move_count));
                                    path2.Add(path3);
                                }

                                si = new List<int>();
                                size = 0;
                            }
                        }

                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }

                }
                else if (s == 3)//"down"
                {

                    //"down"

                 
                    rr2 = r + dr[3];
                    cc2 = c + dc[3];


                    //d1
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {

                        lock (this)
                        {
                            rq.Enqueue(rr2);
                            cq.Enqueue(cc2);
                            sq.Enqueue(4);//"left"
                            st2 = 4;
                            Tuple<int, int, int, int> parents;


                            parents = Tuple.Create(r, c, s, move_count - 1); for (int i = path2.Count - 1; i >= 0; i--)
                            {

                                foreach (List<Tuple<int, int, int, int>> l in path2)
                                {
                                    si.Add(l.Count);
                                    size++;
                                }

                                if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                                {
                                    List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                    path3.AddRange(path2[i]);
                                    path3.Add(Tuple.Create(rr2, cc2, st2, move_count));
                                    path2.Add(path3);
                                }
                                si = new List<int>();

                                size = 0;
                            }

                        }
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }
                }
                else if (s == 4)//"left"
                {
                    //"left"

               
                    rr2 = r + dr[0];
                    cc2 = c + dc[0];

                    //skip out of bounds locations
                    //l1
                    if (!(rr2 < 0 || cc2 < 0) && !(rr2 >= R || cc2 >= C) && !(maze[rr2, cc2] == 'b'))
                    {

                        lock (this)
                        {
                            rq.Enqueue(rr2);
                            cq.Enqueue(cc2);
                            sq.Enqueue(1);//"up"
                            st2 = 1;
                            Tuple<int, int, int, int> parents;

                            parents = Tuple.Create(r, c, s,move_count-1);
                            for (int i = path2.Count - 1; i >= 0; i--)
                            {

                                foreach (List<Tuple<int, int, int, int>> l in path2)
                                {
                                    si.Add(l.Count);
                                    size++;
                                }

                                if ((path2[i][si[i] - 1].Item1 == parents.Item1) && (path2[i][si[i] - 1].Item2 == parents.Item2))
                                {
                                    List<Tuple<int, int, int, int>> path3 = new List<Tuple<int, int, int, int>>();
                                    path3.AddRange(path2[i]);
                                    path3.Add(Tuple.Create(rr2, cc2, st2, move_count));
                                    path2.Add(path3);
                                }

                                si = new List<int>();
                                size = 0;
                            }

                        }
                        nodes_in_next_layer++;
                        visited[rr2, cc2] = 1;

                        if (tup[rr2, cc2] == null) tup[rr2, cc2] = new Tuple<int, int>(r, c);
                    }
                }

            }



        }
        
        void explore_neighbours(int r, int c, int s)
        {
            
            int x = 0;
            if (x == 0)
            {
                Thread th = new Thread(new ThreadStart(() => explores0(s, r, c)));

                Thread th2 = new Thread(new ThreadStart(() => explores1(s, r, c)));

               th.Start();
               th2.Start();
               th.Join();
                //explores0(s, r, c);
                //   explores1(s, r, c);

                th2.Join();



                //Thread  th3 = new Thread(Fc);

                if (f != 0)
                {


                    if (turn == 1)
                    {
                        int re, ce, se;
                        if (rq.Count >= 2)
                        {


                                




                 


                            //re = rq[rq.Count - 2];
                            //ce = cq[cq.Count - 2];
                            //se = sq[sq.Count - 2];

                            //rq[rq.Count - 2] = rq[rq.Count - 1];
                            //cq[cq.Count - 2] = cq[cq.Count - 1];
                            //sq[sq.Count - 2] = sq[sq.Count - 1];



                            //rq[rq.Count - 1] = re;
                            //cq[cq.Count - 1] = ce;
                            //sq[sq.Count - 1] = se;


                        }

                    }
                }


                f++;


            }
            else if (x == 1)
            {

                explores0(s, r, c);
                explores1(s, r, c);
                Console.WriteLine();
                Console.WriteLine();
                       
            }
            //Console.WriteLine(turn);
        }

        void Fc() { }




    }
}

