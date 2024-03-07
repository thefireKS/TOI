class SimulatedAnnealing
{
    static readonly Random Random = new();

    static readonly double InitialTemperature = 10000;
    static readonly double MinTemperature = 1;
    static readonly double Alpha = 0.99;

    static int[] GenerateRandomRoute(int numCities)
    {
        int[] route = new int[numCities];
        for (int i = 0; i < numCities; i++)
        {
            route[i] = i;
        }

        for (int i = numCities - 1; i > 0; i--)
        {
            int j = Random.Next(i + 1);
            (route[i], route[j]) = (route[j], route[i]);
        }

        return route;
    }

    static double CalculateEnergy(int[] route, double[,] distances)
    {
        double energy = 0;
        for (int i = 0; i < route.Length - 1; i++)
        {
            energy += distances[route[i], route[i + 1]];
        }
        
        energy += distances[route[route.Length - 1], route[0]];

        return energy; 
    }

    static void Main()
    {
        int numCities = 6; 
        double[,] distances = {
            { 0, 21, 36, 8, 12, 44 },
            { 49, 0, 16, 38, 22, 31 },
            { 51, 25, 0, 13, 2, 38 },
            { 48, 39, 36, 0, 18, 52 },
            { 17, 6, 42, 11, 0, 15 },
            { 10, 18, 35, 28, 31, 0 }
        };
        
        double temperature = InitialTemperature;
        
        int[] currentRoute = GenerateRandomRoute(numCities);

        while (temperature > MinTemperature)
        {
            int[] newRoute = (int[])currentRoute.Clone();
            int index1 = Random.Next(numCities);
            int index2 = Random.Next(numCities);
            (newRoute[index1], newRoute[index2]) = (newRoute[index2], newRoute[index1]);
            
            double deltaEnergy = CalculateEnergy(newRoute, distances) - CalculateEnergy(currentRoute, distances);
            
            if (deltaEnergy < 0 || Random.NextDouble() < Math.Exp(-deltaEnergy / temperature))
            {
                currentRoute = newRoute;
            }
            
            temperature *= Alpha;
        }

        double finalDistance = CalculateEnergy(currentRoute, distances);
        for (int i = 0; i < currentRoute.Length; i++)
            currentRoute[i]++;
        Console.WriteLine("Финальный путь: " + string.Join(" -> ", currentRoute));
        Console.WriteLine("Итоговая длина пути: " + finalDistance);
    }
}
