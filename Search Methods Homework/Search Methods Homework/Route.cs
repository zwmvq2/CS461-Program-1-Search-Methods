using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_Methods_Homework
{
    public class Route
    {
        public bool? GoalFound = null;
        public List<Location> Path = new List<Location>();

        public void Print()
        {
            if(GoalFound == null)
            {
                Console.WriteLine("This message should never appear, goal found is still null");
            }
            else if (GoalFound == false)
            {
                Console.WriteLine(" Route not found :(");
            }
            else
            {
                Console.WriteLine("Route Found: length = " + Path.Count.ToString());
                foreach (Location place in Path)
                {
                    Console.Write(place.getName() + "->");
                }
            }
        }

    }
}
