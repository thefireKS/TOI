public class AntColony
{
    private double minTour = 999;
    
    private readonly int _numAnts;
    private readonly double[,] _distances;
    private readonly double[,] _pheromones;
    private readonly double[,] _heuristicValues;
    private readonly List<Ant> _ants;
    private readonly Random _rand;
    private readonly double _rho; // коэффициент испарения феромона
    private readonly double _alpha; // параметр для феромона
    private readonly double _beta; // параметр для эвристики

    public AntColony(int numAnts, double[,] distances, double rho, double alpha, double beta)
    {
        _numAnts = numAnts;
        _distances = distances;
        _pheromones = new double[distances.GetLength(0), distances.GetLength(1)];
        _heuristicValues = CalculateHeuristicValues();
        _ants = new List<Ant>();
        _rand = new Random();
        _rho = rho;
        _alpha = alpha;
        _beta = beta;

        // Инициализация муравьев
        for (int i = 0; i < numAnts; i++)
        {
            _ants.Add(new Ant());
        }
    }

    public void CreateAnts()
    {
        _ants.Clear();
        for (int i = 0; i < _numAnts; i++)
        {
            _ants.Add(new Ant());
        }
    }

    public void SearchSolutions()
    {
        foreach (var ant in _ants)
        {
            ant.Clear();
            ant.VisitCity(_rand.Next(_distances.GetLength(0)));
        }

        for (int i = 0; i < _distances.GetLength(0) - 1; i++)
        {
            foreach (var ant in _ants)
            {
                ant.VisitCity(SelectNextCity(ant));
            }
        }
    }

    private int SelectNextCity(Ant ant)
    {
        int currentCity = ant.CurrentCity;
        var unvisitedCities = Enumerable.Range(0, _distances.GetLength(0)).Where(c => !ant.VisitedCities.Contains(c)).ToArray();

        double[] probabilities = new double[_distances.GetLength(0)];

        foreach (var city in unvisitedCities)
        {
            double pheromone = Math.Pow(_pheromones[currentCity, city], _alpha);
            double heuristic = Math.Pow(_heuristicValues[currentCity, city], _beta);
            probabilities[city] = pheromone * heuristic;
        }

        double sumProbabilities = probabilities.Sum();

        double[] normalizedProbabilities = probabilities.Select(p => p / sumProbabilities).ToArray();

        double randomValue = _rand.NextDouble();
        double cumulativeProbability = 0.0;

        for (int i = 0; i < _distances.GetLength(0); i++)
        {
            cumulativeProbability += normalizedProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        return unvisitedCities.Last();
    }

    public void UpdatePheromones()
    {
        for (int i = 0; i < _distances.GetLength(0); i++)
        {
            for (int j = 0; j < _distances.GetLength(0); j++)
            {
                if (i != j)
                {
                    _pheromones[i, j] *= (1.0 - _rho); // Испарение феромона

                    foreach (var ant in _ants)
                    {
                        double deltaPheromone = 1.0 / ant.TourLength(_distances);
                        _pheromones[i, j] += deltaPheromone;
                    }
                }
            }
        }
    }

    private double[,] CalculateHeuristicValues()
    {
        double[,] values = new double[_distances.GetLength(0), _distances.GetLength(1)];

        for (int i = 0; i < _distances.GetLength(0); i++)
        {
            for (int j = 0; j < _distances.GetLength(1); j++)
            {
                if (i != j)
                {
                    values[i, j] = 1.0 / _distances[i, j];
                }
            }
        }

        return values;
    }

    public int[]? GetBestTour()
    {
        double bestTourLength = double.MaxValue;
        int[]? bestTour = null;

        foreach (var ant in _ants)
        {
            double tourLength = ant.TourLength(_distances);
            if (tourLength < bestTourLength)
            {
                bestTourLength = tourLength;
                bestTour = ant.VisitedCities.ToArray();
            }
        }

        return bestTour;
    }
    
    public double GetBestTourLength()
    {
        double bestTourLength = double.MaxValue;

        foreach (var ant in _ants)
        {
            double tourLength = ant.TourLength(_distances);
            if (tourLength < bestTourLength)
            {
                bestTourLength = tourLength;
            }
        }

        return bestTourLength;
    }
    
    public void PrintAllTours()
    {
        for (int i = 0; i < _ants.Count; i++)
        {
            int[] tour = _ants[i].VisitedCities.ToArray();
            double tourLength = _ants[i].TourLength(_distances);

            if (tourLength < minTour)
                minTour = tourLength;

            Console.WriteLine($"Tour {i + 1}: {string.Join(" -> ", tour)}, Length: {tourLength}");
        }
    }

    public void getMinTour()
    {
        Console.WriteLine(minTour);
    }
}