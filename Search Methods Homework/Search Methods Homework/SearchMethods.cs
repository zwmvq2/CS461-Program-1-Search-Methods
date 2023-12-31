﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Search_Methods_Homework;

/// <summary>
/// Each function represents a distinct search approach.
/// Each function should a Route Object, 
///     - contains a list of nodes visited and boolean stating if goal was found
/// 
/// As a sideeffect, each function should print  time taken and number of nodes searched
/// </summary>
 static class SearchMethods
{
    
   public  static Route UndirectedSearch(Location start, Location goal, float TimeLimit = 1000000 )
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        MyRoute.Path.Add(start);
        while(MyRoute.GoalFound == null)
        {
            if(timer.ElapsedTicks > TimeLimit)
            {
                MyRoute.GoalFound = false;
                Console.WriteLine("Algorithim force stopped due to reaching time limit");
            }
            if(CurrentLocation == goal)
            {
                MyRoute.GoalFound = true;
            }
            else
            {
                CurrentLocation = CurrentLocation.RandomNeighbor();
                MyRoute.Path.Add(CurrentLocation);
                NodesSearched++;
            }
        }


        timer.Stop();
        Console.WriteLine("Execution Time: " + timer.ElapsedTicks.ToString() + " ticks ");
        Console.WriteLine("Nodes Searched: " + NodesSearched.ToString());
        return MyRoute;
    }

   public static Route  BreadthFirstSearch(Location start,Location goal, float TimeLimit = 1000000)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        start.MyParent = null;
        Queue<Location> Open = new Queue<Location>();
        List<Location> Closed = new List<Location>();
        while (MyRoute.GoalFound == null)
        {
            if (timer.ElapsedTicks > TimeLimit)
            {
                MyRoute.GoalFound = false;
                Console.WriteLine("Algorithim force stopped due to reaching time limit");
            }
            if (CurrentLocation == goal)
            {
                MyRoute.GoalFound = true;
            }
            else
            {
                NodesSearched++;
                foreach(Location edge in CurrentLocation.getAdjacencies())
                {
                    if(!Open.Contains(edge) && !Closed.Contains(edge))
                    {
                        Open.Enqueue(edge);
                        edge.MyParent = CurrentLocation;
                    }
                }
                Closed.Add(CurrentLocation);
                if (Open.Count < 1)
                {
                    MyRoute.GoalFound = false;
                }
                else
                {
                    CurrentLocation = Open.Dequeue();
                }
            }
        }

        //Trace from goal node to construct path
        bool RootFound = false;
        while (!RootFound)
        {
            MyRoute.Path.Insert(0, CurrentLocation);
            if (CurrentLocation.MyParent == null)
            {
                RootFound = true;
            }
            else
            {
                CurrentLocation = CurrentLocation.MyParent;
            }
        }

        timer.Stop();
        Console.WriteLine("Execution Time: " + timer.ElapsedTicks.ToString() + " ticks ");
        Console.WriteLine("Nodes Searched: " + NodesSearched.ToString());
        return MyRoute;
    }
    public static Route DepthFirstSearch(Location start, Location goal, float TimeLimit = 1000000)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        start.MyParent = null;
        Stack<Location> Open = new Stack<Location>();
        List<Location> Closed = new List<Location>();
        while (MyRoute.GoalFound == null)
        {
            if (timer.ElapsedTicks > TimeLimit)
            {
                MyRoute.GoalFound = false;
                Console.WriteLine("Algorithim force stopped due to reaching time limit");
            }
            if (CurrentLocation == goal)
            {
                MyRoute.GoalFound = true;
            }
            else
            {
                NodesSearched++;
                foreach (Location edge in CurrentLocation.getAdjacencies())
                {
                    if (!Open.Contains(edge) && !Closed.Contains(edge))
                    {
                        Open.Push(edge);
                        edge.MyParent = CurrentLocation;
                    }
                }
                Closed.Add(CurrentLocation);
                if (Open.Count < 1)
                {
                    MyRoute.GoalFound = false;
                }
                else
                {
                    CurrentLocation = Open.Pop();
                }
            }
        }

        //Trace from goal node to construct path
        bool RootFound = false;
        while (!RootFound)
        {
            MyRoute.Path.Insert(0, CurrentLocation);
            if (CurrentLocation.MyParent == null)
            {
                RootFound = true;
            }
            else
            {
                CurrentLocation = CurrentLocation.MyParent;
            }
        }

        timer.Stop();
        Console.WriteLine("Execution Time: " + timer.ElapsedTicks.ToString() + " ticks ");
        Console.WriteLine("Nodes Searched: " + NodesSearched.ToString());
        return MyRoute;
    }
    public static Route IDDFSSearch(Location start, Location goal, float TimeLimit = 1000000)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        for (int MaxDepth = 0; timer.ElapsedTicks < TimeLimit; MaxDepth++)
        {
            MyRoute = DepthFirstButForIDDFS(start, goal, ref  NodesSearched, MaxDepth);
            if(MyRoute.GoalFound == true)
            {
               break;
            }
        }
        timer.Stop();
        Console.WriteLine("Execution Time: " + timer.ElapsedTicks.ToString() + " ticks ");
        Console.WriteLine("Nodes Searched: " + NodesSearched.ToString());
        return MyRoute;

    }


    public static Route BestFirstSearch(Location start, Location goal, float TimeLimit = 1000000)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        start.MyParent = null;
        PriorityQueue<Location,float> Open = new PriorityQueue<Location,float>();
        List<Location> Closed = new List<Location>();
        while (MyRoute.GoalFound == null)
        {
            if (timer.ElapsedTicks > TimeLimit)
            {
                MyRoute.GoalFound = false;
                Console.WriteLine("Algorithim force stopped due to reaching time limit");
            }
            if (CurrentLocation == goal)
            {
                MyRoute.GoalFound = true;
            }
            else
            {
                NodesSearched++;
                foreach (Location edge in CurrentLocation.getAdjacencies())
                {
                    if (!Closed.Contains(edge))
                    {
                        Open.Enqueue(edge,Heuristic(edge,goal,CurrentLocation));
                        edge.MyParent = CurrentLocation;
                    }
                }
                RemoveDuplicatesInPriorityQueue(ref Open);
                Closed.Add(CurrentLocation);
                if (Open.Count < 1)
                {
                    MyRoute.GoalFound = false;
                }
                else
                {
                    CurrentLocation = Open.Dequeue();
                }
            }
        }

        //Trace from goal node to construct path
        bool RootFound = false;
        while (!RootFound)
        {
            MyRoute.Path.Insert(0, CurrentLocation);
            if (CurrentLocation.MyParent == null)
            {
                RootFound = true;
            }
            else
            {
                CurrentLocation = CurrentLocation.MyParent;
            }
        }

        timer.Stop();
        Console.WriteLine("Execution Time: " + timer.ElapsedTicks.ToString() + " ticks ");
        Console.WriteLine("Nodes Searched: " + NodesSearched.ToString());
        return MyRoute;
    }
   //TODO: Implement A*
    public static Route AStarSearch(Location start, Location goal)
    {
        return new Route();
    }

   private static Route DepthFirstButForIDDFS(Location start, Location goal, ref int NodesSearched,int DepthLimit)
    {
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        start.MyParent = null;

        Stack<Location> Open = new Stack<Location>();
        List<Location> Closed = new List<Location>();
        while (MyRoute.GoalFound == null)
        {
            if (CurrentLocation == goal)
            {
                MyRoute.GoalFound = true;
            }
            else
            {
                NodesSearched++;
                if (CurrentLocation.getDepth() < DepthLimit)
                {
                    foreach (Location edge in CurrentLocation.getAdjacencies())
                    {
                        if (!Open.Contains(edge) && !Closed.Contains(edge))
                        {
                            Open.Push(edge);
                            edge.MyParent = CurrentLocation;
                        }
                    }
                }
                Closed.Add(CurrentLocation);
                if (Open.Count < 1)
                {
                    MyRoute.GoalFound = false;
                }
                else
                {
                    CurrentLocation = Open.Pop();
                }
            }
        }

        //Trace from goal node to construct path
        bool RootFound = false;
        while (!RootFound)
        {
            MyRoute.Path.Insert(0, CurrentLocation);
            if (CurrentLocation.MyParent == null)
            {
                RootFound = true;
            }
            else
            {
                CurrentLocation = CurrentLocation.MyParent;
            }
        }
        return MyRoute;
    }

   private static float Heuristic (Location canidate,Location goal, Location current)
    {
        return Location.Distance(canidate, goal) + (0.70f * Location.Distance(current, canidate));
    }
    
    //Helper function for best first search. It's tragic that PriorityQueue does not have a Contains method :(
    private static void RemoveDuplicatesInPriorityQueue(ref PriorityQueue<Location,float> q)
    {
        //These 2 lists must be exactly alligned.
        //When an element is added to one list, the matching element should be added to the other list
        List<Location> Locations = new List<Location>();
        List<float> Priorities = new List<float>();
        while(q.Count > 0)
        {
            Location next;
            float priority;
            q.TryDequeue(out next, out priority);
            if(!Locations.Contains(next))
            {
                Locations.Add(next);
                Priorities.Add(priority);
            }
        }
        for (int i =0; i< Locations.Count;i++)
        {
            q.Enqueue(Locations[i], Priorities[i]);
        }
    }
}
