using System.Text;

static class HuffmanCoding
{
    private class Node
    {
        public char Symbol;
        public int Frequency;
        public Node? Left;
        public Node? Right;
                
        public Node(char _symbol, int _frequency)
        {
            Symbol = _symbol;
            Frequency = _frequency;
            Left = null;
            Right = null;
        }
    }
        
    public static void Main()
    {
        Console.WriteLine("Введите сообщение, которое хотите закодировать:");
        string message = Console.ReadLine(), encodedMessage = "", decodedMessage;
        Dictionary<char, string> huffmanCodes = new Dictionary<char, string>();

        huffmanCodes = GenerateHuffmanCodes(BuildHuffmanTree(CountFrequencies(message)));
        DisplayHuffmanCodes(huffmanCodes);
        encodedMessage = EncodeMessage(message, huffmanCodes);
        decodedMessage = DecodeMessage(encodedMessage, huffmanCodes);
        
        Console.WriteLine($"Сообщение: {message}\nЗакодированное сообщение: {encodedMessage}\nДекодированное сообщение: {decodedMessage}");
    }

    private static Dictionary<char, int> CountFrequencies(string message)
    {
        Dictionary<char, int> freq = new Dictionary<char, int>();

        foreach (var symbol in message.Where(symbol => !freq.TryAdd(symbol, 1)))
        {
            freq[symbol]++;
        }

        return freq;
    }

    private static Node BuildHuffmanTree(Dictionary<char, int> frequencies)
    {
        PriorityQueue<Node, int> nodes = new PriorityQueue<Node, int>();
        
        foreach (var pair in frequencies)
        {
            nodes.Enqueue(new Node(pair.Key,pair.Value),pair.Value);
        }

        while (nodes.Count > 1)
        {
            Node left = nodes.Dequeue();
            Node right = nodes.Dequeue();

            Node parent = new Node('\0', left!.Frequency + right!.Frequency)
            {
                Left = left,
                Right = right
            };
            
            nodes.Enqueue(parent,left!.Frequency + right!.Frequency);
        }

        return nodes.Dequeue();
    }
    
    private static Dictionary<char, string> GenerateHuffmanCodes(Node? root)
    {
        Dictionary<char, string> huffmanCodes = new Dictionary<char, string>();
        GenerateHuffmanCodesRecursive(root, "", huffmanCodes);
        return huffmanCodes;
    }
    
    private static void GenerateHuffmanCodesRecursive(Node? node, string currentCode, Dictionary<char, string> huffmanCodes)
    {
        if (node.Symbol != '\0')
        {
            huffmanCodes[node.Symbol] = currentCode;
            return;
        }

        GenerateHuffmanCodesRecursive(node.Left, currentCode + "0", huffmanCodes);
        GenerateHuffmanCodesRecursive(node.Right, currentCode + "1", huffmanCodes);
    }
    
    private static string EncodeMessage(string? message, Dictionary<char, string> huffmanCodes)
    {
        return string.Join("", message.Select(symbol => huffmanCodes[symbol]));
    }

    private static string DecodeMessage(string encodedMessage, Dictionary<char, string> huffmanCodes)
    {
        StringBuilder decodedMessage = new StringBuilder();
        StringBuilder currentCode = new StringBuilder();;

        foreach (char bit in encodedMessage)
        {
            currentCode.Append(bit);

            foreach (var kvp in huffmanCodes.Where(kvp => kvp.Value == currentCode.ToString()))
            {
                decodedMessage.Append(kvp.Key);
                currentCode.Clear();
                break;
            }
        }
        return decodedMessage.ToString();
    }
    
    private static void DisplayHuffmanCodes(Dictionary<char, string> huffmanCodes)
    {
        Console.WriteLine("Символы и их коды:");

        foreach (var kvp in huffmanCodes)
        {
            Console.WriteLine($"Символ: {kvp.Key}, Код: {kvp.Value}");
        }
    }

}