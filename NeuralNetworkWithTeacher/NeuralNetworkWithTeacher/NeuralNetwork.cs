public class NeuralNetwork
{
    private readonly Neuron[] _inputLayer;
    private readonly Neuron[] _hiddenLayer;
    private readonly Neuron _outputNeuron;

    public NeuralNetwork(int inputSize, int hiddenSize)
    {
        _inputLayer = new Neuron[inputSize];
        _hiddenLayer = new Neuron[hiddenSize];
        InitializeLayers();

        _outputNeuron = new Neuron(hiddenSize);
    }

    private void InitializeLayers()
    {
        for (int i = 0; i < _inputLayer.Length; i++)
        {
            _inputLayer[i] = new Neuron(1); // каждый нейрон входного слоя принимает по одному входу
        }

        for (int i = 0; i < _hiddenLayer.Length; i++)
        {
            _hiddenLayer[i] = new Neuron(_inputLayer.Length);
        }
    }

    public double Train(double[] inputs, double target, double learningRate, int epochs)
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            // Прямое распространение (feedforward)
            double[] hiddenOutputs = new double[_hiddenLayer.Length];

            for (int i = 0; i < _hiddenLayer.Length; i++)
            {
                hiddenOutputs[i] = _hiddenLayer[i].Activate(inputs);
            }

            double output = _outputNeuron.Activate(hiddenOutputs);

            // Вычисление ошибки
            double error = target - output;

            // Обратное распространение ошибки (backpropagation)
            double outputGradient = output * (1 - output) * error;

            for (int i = 0; i < _hiddenLayer.Length; i++)
            {
                double hiddenGradient = hiddenOutputs[i] * (1 - hiddenOutputs[i]) * _outputNeuron.Weights[i] * outputGradient;
                _hiddenLayer[i].UpdateWeights(learningRate, hiddenGradient, inputs);
            }

            _outputNeuron.UpdateWeights(learningRate, outputGradient, hiddenOutputs);
        }

        return _outputNeuron.Output;
    }

    public double Predict(double[] inputs)
    {
        double[] hiddenOutputs = new double[_hiddenLayer.Length];

        for (int i = 0; i < _hiddenLayer.Length; i++)
        {
            hiddenOutputs[i] = _hiddenLayer[i].Activate(inputs);
        }

        return _outputNeuron.Activate(hiddenOutputs);
    }
}