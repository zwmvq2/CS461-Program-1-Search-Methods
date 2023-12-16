﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_Methods_Homework
{
    public class Location

    {
         public Location(string name, float xcord, float ycord) 
        {
            Name = name;
            Coordinates = new Tuple<float,float>(xcord, ycord);
            Adjacencies = new List<Location>();
        }
        private string Name;
        public string getName() { return Name; }
        private Tuple<float, float> Coordinates;
        public Tuple<float,float> getCoordinates() { return Coordinates; }

        private List<Location> Adjacencies;
        public List<Location> getAdjacencies() { return Adjacencies; }
        public void AddAdjacency(Location neighbor )
        {
            //A new adjacency is only added if it's not a duplicate
            if(Adjacencies.Contains(neighbor))
            {
                return;
            }
            Adjacencies.Add( neighbor );
        }
    }
}
