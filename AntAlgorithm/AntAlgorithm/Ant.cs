public class Ant
{
    private readonly List<int> _visitedCities = new();

    public int CurrentCity => _visitedCities.Count > 0 ? _visitedCities.Last() : -1;

    public List<int> VisitedCities => _visitedCities.ToList();

    public void VisitCity(int city)
    {
        _visitedCities.Add(city);
    }

    public void Clear()
    {
        _visitedCities.Clear();
    }

    public double TourLength(double[,] distances)
    {
        double length = 0.0;
        for (int i = 0; i < _visitedCities.Count - 1; i++)
        {
            length += distances[_visitedCities[i], _visitedCities[i + 1]];
        }
        length += distances[_visitedCities.Last(), _visitedCities.First()]; // Возвращение в начальный город
        return length;
    }
    
    
}