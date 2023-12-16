// See https://aka.ms/new-console-template for more information
using Search_Methods_Homework;
using System.IO;
Console.WriteLine("Hello, World!");

Dictionary<string,Location> myMap = new Dictionary<string,Location>();
LoadLocationsData();
PrintAdjacencies();

void LoadLocationsData()
{
    //Reads through coordinates csv to populate myMap
    try
    {

        StreamReader InputCordsAndNames = new StreamReader("coordinates.csv");
        while (!InputCordsAndNames.EndOfStream)
        { 
        string[] entries = InputCordsAndNames.ReadLine().Split(',');
        myMap.Add(entries[0], new Location(entries[0],float.Parse(entries[1]), float.Parse(entries[2])));
        }
        InputCordsAndNames.Close();
    }
    catch (IOException e)
    {
        Console.WriteLine("File reading skill issue");
        Console.WriteLine(e.ToString());
    }

    //Reads through Adjacencies.txt to to populate adjances in each location in myMap
    try
    {
        StreamReader InputAdjacencies = new StreamReader("Adjacencies.txt");
        while (!InputAdjacencies.EndOfStream) 
        {
            string[] entries = InputAdjacencies.ReadLine().Split(' ');
            Location loc1 = myMap[entries[0]];
            Location loc2 = myMap[entries[1]];
            loc1.AddAdjacency(loc2);
            loc2.AddAdjacency(loc1);
        }
        InputAdjacencies.Close();
    }
    catch (IOException e)
    {
        Console.WriteLine("File reading skill issue");
        Console.WriteLine(e.ToString());
    }
}

void PrintAdjacencies()
{
    foreach (KeyValuePair<string, Location> place in myMap)
    {
        Console.WriteLine(place.Key + ":");
        foreach (Location adj in place.Value.getAdjacencies())
        {
            Console.WriteLine(adj.getName());
        }
        Console.WriteLine("*************************************************************************************");
    }
}