class Program
{
    static readonly Random Random = new();
    static readonly int[,] Distances = {
        { 0, 21, 36, 8, 12, 44 },
        { 49, 0, 16, 38, 22, 31 },
        { 51, 25, 0, 13, 2, 38 },
        { 48, 39, 36, 0, 18, 52 },
        { 17, 6, 42, 11, 0, 15 },
        { 10, 18, 35, 28, 31, 0 }
    };

    static readonly int PopulationSize = 10;
    static readonly int Generations = 1000;
    static readonly double MutationRate = 0.02;

    static void Main()
    {
        int citiesCount = Distances.GetLength(0);
        int[] bestRoute = GeneticAlgorithm(citiesCount);

        int finalDistance = CalculateRouteLength(bestRoute);
        
        for (int i = 0; i < bestRoute.Length; i++)
        {
            bestRoute[i]++;
        }
        
        Console.WriteLine("Лучший маршрут: " + string.Join(" -> ", bestRoute));
        Console.WriteLine("Длина лучшего маршрута: " + finalDistance);
    }

    static int[] GeneticAlgorithm(int citiesCount)
    {
        int[][] population = InitializePopulation(citiesCount);

        for (int generation = 0; generation < Generations+1; generation++)
        {
            population = population.OrderBy(CalculateRouteLength).ToArray();
            
            int[][] newPopulation = new int[PopulationSize][];

            for (int i = 0; i < PopulationSize; i++)
            {
                int[] parent1 = SelectParent(population);
                int[] parent2 = SelectParent(population);
                int[] child = Crossover(parent1, parent2);
                child = Mutate(child);
                newPopulation[i] = child;
            }

            population = newPopulation;
        }

        return population.OrderBy(CalculateRouteLength).First();
    }

    static int[][] InitializePopulation(int citiesCount)
    {
        int[][] population = new int[PopulationSize][];

        for (int i = 0; i < PopulationSize; i++)
        {
            population[i] = Enumerable.Range(0, citiesCount).OrderBy(x => Random.Next()).ToArray();
        }

        return population;
    }

    static int[] SelectParent(int[][] population)
    {
        int tournamentSize = 5;
        int[][] tournament = new int[tournamentSize][];

        for (int i = 0; i < tournamentSize; i++)
        {
            tournament[i] = population[Random.Next(PopulationSize)];
        }

        return tournament.OrderBy(CalculateRouteLength).First();
    }

    static int[] Crossover(int[] parent1, int[] parent2)
    {
        int[] child = new int[parent1.Length];
        int startPos = Random.Next(parent1.Length);
        int endPos = Random.Next(startPos, parent1.Length);

        for (int i = startPos; i < endPos; i++)
        {
            child[i] = parent1[i];
        }

        int currentIndex = endPos % parent1.Length;

        foreach (var parent in parent2)
        {
            if (!child.Contains(parent))
            {
                child[currentIndex] = parent;
                currentIndex = (currentIndex + 1) % parent1.Length;
            }
        }

        return child;
    }

    static int[] Mutate(int[] route)
    {
        if (Random.NextDouble() < MutationRate)
        {
            int swapIndex1 = Random.Next(route.Length);
            int swapIndex2 = Random.Next(route.Length);

            (route[swapIndex1], route[swapIndex2]) = (route[swapIndex2], route[swapIndex1]);
        }

        return route;
    }

    static int CalculateRouteLength(int[] route)
    {
        int length = 0;

        for (int i = 0; i < route.Length - 1; i++)
        {
            length += Distances[route[i], route[i + 1]];
        }

        length += Distances[route[^1], route[0]];

        return length;
    }
}