using System;
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
    
   public  static Route UndirectedSearch(Location start, Location goal)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        int NodesSearched = 0;
        Route MyRoute = new Route();
        Location CurrentLocation = start;
        MyRoute.Path.Add(start);
        while(MyRoute.GoalFound == false)
        {
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
}
