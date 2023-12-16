// See https://aka.ms/new-console-template for more information
using Search_Methods_Homework;
using System.IO;

Dictionary<string,Location> myMap = new Dictionary<string,Location>();
LoadLocationsData();
UserLoop();

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

void UserLoop()
{
    Console.WriteLine("**********************************************************************************");
    bool validStart = false;
  
    string input;
    Location Start = new Location("placeholder", 0, 0);
    Location Goal = new Location("placeholder", 0, 0);
    while (!validStart)
    {
        Console.WriteLine("Enter starting location ");
        input = Console.ReadLine();
        if (input == "random")
        {
            Start = RandomLocation();
            Console.WriteLine(Start.getName());
            validStart = true;
        }
        else if (myMap.ContainsKey(input))
        {
            Start= myMap[input];
            validStart = true;
        }
        else
        {
            Console.WriteLine("Location Invalid");
        }
    }
    bool validGoal = false;
    while (!validGoal)
    {
        Console.WriteLine("Enter goal location ");
        input = Console.ReadLine();
        if (input == "random")
        {
            Goal = RandomLocation();
            Console.WriteLine(Goal.getName());
            validGoal = true;
        }
        else if (myMap.ContainsKey(input))
        {
            Goal = myMap[input];
            validGoal = true;
        }
        else
        {
            Console.WriteLine("Location Invalid");
        }
    }

    Route r;
    Console.WriteLine("Select search Algorithim");
    Console.WriteLine("1) Undirected");
    Console.WriteLine("2) Breadth-First Search");
    Console.WriteLine("3) Depth-First Search");
    Console.WriteLine("4) ID-DFS Search");
    Console.WriteLine("5) Best-first Search");
    Console.WriteLine("Enter anything aside from 1,2,3,4,or 5 to exit the program");
    input = Console.ReadLine();
    switch(input)
    {
        case "1":
            r = SearchMethods.UndirectedSearch(Start, Goal);
            r.Print();
            break;
        case "2":
            r = SearchMethods.BreadthFirstSearch(Start, Goal);
            r.Print();
            break;
        case "3":
            r = SearchMethods.DepthFirstSearch(Start, Goal);
            r.Print();
            break;
        case "4":
            r = SearchMethods.IDDFSSearch(Start, Goal);
            r.Print();
            break;
        case "5":
            r = SearchMethods.BestFirstSearch(Start, Goal);
            r.Print();
            break;
        default:
            Console.WriteLine("Goodbye!");
            return;
    }
    UserLoop();
}
Location RandomLocation()
{
    return myMap[myMap.Keys.ElementAt(Location.RNG.Next(myMap.Count))];
}

