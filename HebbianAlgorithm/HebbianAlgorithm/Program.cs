class HebbianNetwork
{
    static void Main()
    {
        double[] weights = InitializeWeights(2);
        
        double[][] inputs = {
            [1, 0],
            [0, 1]
        };

        foreach (var input in inputs)
        {
            TrainHebbian(input, weights);
        }
        
        Console.WriteLine("Вывод для всех возможных входных векторов:");

        for (double x = 0; x <= 1; x += 0.5)
        {
            for (double y = 0; y <= 1; y += 0.5)
            {
                double[] testInput = { x, y };
                double output = CalculateOutput(testInput, weights);
                Console.WriteLine($"Вывод для входа {string.Join(", ", testInput)}: {output}");
            }
        }
    }
    
    static double[] InitializeWeights(int size)
    {
        double[] weights = new double[size];
        for (int i = 0; i < size; i++)
        {
            weights[i] = 0;
        }
        return weights;
    }
    
    static void TrainHebbian(double[] input, double[] weights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += input[i] * input[i];
        }
        
        NormalizeWeights(weights);
    }
    
    static void NormalizeWeights(double[] weights)
    {
        double sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * weights[i];
        }

        double magnitude = Math.Sqrt(sum);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] /= magnitude;
        }
    }
    
    static double CalculateOutput(double[] input, double[] weights)
    {
        double output = 0;
        for (int i = 0; i < input.Length; i++)
        {
            output += input[i] * weights[i];
        }

        return output;
    }
}