public class Neuron
{
    public double[] Weights { get; }
    private double _bias;
    public double Output { get; private set; }

    public Neuron(int inputSize)
    {
        Weights = new double[inputSize];
        InitializeWeights();
        _bias = 0;
        Output = 0;
    }

    private void InitializeWeights()
    {
        Random random = new Random();

        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = random.NextDouble() - 0.5; // случайные значения в диапазоне [-0.5, 0.5]
        }
    }

    private double Sigmoid(double x)
    {
        return 1.0 / (1.0 + Math.Exp(-x));
    }

    public double Activate(double[] inputs)
    {
        double sum = 0;

        for (int i = 0; i < Weights.Length; i++)
        {
            sum += inputs[i] * Weights[i];
        }

        sum += _bias;
        Output = Sigmoid(sum);
        return Output;
    }

    public void UpdateWeights(double learningRate, double errorGradient, double[] inputs)
    {
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] += learningRate * errorGradient * inputs[i];
        }

        _bias += learningRate * errorGradient;
    }
}