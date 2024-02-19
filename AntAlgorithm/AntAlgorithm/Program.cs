static class Program
{
    static void Main()
    {
        // Пример данных для расстояний между городами
        double[,] distances = {
            { 0, 10, 34, 16, 36, 34 },
            { 10, 0, 25, 5, 22, 45 },
            { 34, 25, 0, 43, 27, 18 },
            { 16, 5, 43, 0, 32, 50 },
            { 36, 22, 27, 32, 0, 24 },
            { 34, 45, 18, 50, 24, 0 }
        };
        
        // Параметры муравьиного алгоритма
        int numAnts = 100;
        double rho = 0.2; // Коэффициент испарения феромона
        double alpha = 1; // Параметр для феромона
        double beta = 1; // Параметр для эвристики
        int maxIterations = 100;

        // Создание объекта AntColony
        AntColony antColony = new AntColony(numAnts, distances, rho, alpha, beta);

        // Решение задачи коммивояжера
        int iteration = 0;
        while (iteration < maxIterations)
        {
            antColony.CreateAnts();
            antColony.SearchSolutions();
            antColony.UpdatePheromones();

            iteration++;
        }

        // Получение лучшего маршрута и его длины
        int[] bestTour = antColony.GetBestTour();
        double bestTourLength = antColony.GetBestTourLength();
        
        for (int i = 0; i < bestTour.Length; i++)
            bestTour[i]++;
        Console.WriteLine("Лучший путь: " + string.Join(" -> ", bestTour));
        Console.WriteLine("Длина: " + bestTourLength);
        
        antColony.PrintAllTours();
    }
}
