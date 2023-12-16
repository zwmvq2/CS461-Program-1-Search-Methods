using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_Methods_Homework
{
    public class Route
    {
        public bool GoalFound = false;
        public List<Location> Path = new List<Location>();

        public void Print()
        {
            if (!GoalFound)
            {
                Console.WriteLine(" Route not found :(");
            }
            else
            {
                Console.WriteLine("Route Found:");
                foreach (Location place in Path)
                {
                    Console.Write(place.getName() + "->");
                }
            }
        }
    }
}
