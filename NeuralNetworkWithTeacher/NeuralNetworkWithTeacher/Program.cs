class Program
{
    static void Main()
    {
        int inputSize = 2;
        int hiddenSize = 2;

        NeuralNetwork neuralNetwork = new NeuralNetwork(inputSize, hiddenSize);

        double[][] inputs = 
        {
            new double[] {0, 0},
            new double[] {0, 1},
            new double[] {1, 0},
            new double[] {1, 1}
        };

        double[] targets = { 0, 1, 1, 0 };

        double learningRate = 0.1;
        int epochs = 10000;

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] input = inputs[i];
                double target = targets[i];

                double output = neuralNetwork.Train(input, target, learningRate, epochs);

                Console.WriteLine($"Input: [{string.Join(", ", input)}], Target: {target}, Output: {output}");
            }
        }

        // Пример 
        Console.WriteLine("Predictions:");

        for (int i = 0; i < inputs.Length; i++)
        {
            double[] testInput = inputs[i];
            double prediction = neuralNetwork.Predict(testInput);

            Console.WriteLine($"Input: [{string.Join(", ", testInput)}], Prediction: {prediction}");
        }

        Console.ReadLine();
    }
}