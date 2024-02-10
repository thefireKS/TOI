using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AriphmeticCoding
{
    internal class Program
    {
        private struct Segment
        {
            public char Letter;
            public double Left;
            public double Right;

            public Segment(char sLetter, double sLeft, double sRight)
            {
                Letter = sLetter;
                Left = sLeft;
                Right = sRight;
            }
        }
        
        public static void Main(string[] args)
        {
            string word = Console.ReadLine();
            int wordLength = word.Length;
            List <Segment> segs = new List<Segment>();
            
            double encodedMessage = Encode(word, ReadWord(word), segs);
            Console.WriteLine(encodedMessage);

            string decodedMessage = Decode(encodedMessage, wordLength, segs);
            Console.WriteLine(decodedMessage);
        }

        private static Dictionary<char, double> ReadWord(string word)
        {
            Dictionary<char, int> alphabet = new Dictionary<char, int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (alphabet.ContainsKey(word[i]))
                {
                    alphabet[word[i]]++;
                }
                else
                {
                    alphabet.Add(word[i],1);
                }
            }

            int quantityOfAllSymbols = alphabet.Sum(x => x.Value);
            
            Dictionary<char, double> letters = new Dictionary<char, double>();
            foreach (var symbol in alphabet)
            {
                letters.Add(symbol.Key,(double) symbol.Value / quantityOfAllSymbols);
            }

            var sortedLettersOrderedEnumerable = from entry in letters orderby letters.Values descending select entry;
            Dictionary<char, double> sortedLettersDict =
                sortedLettersOrderedEnumerable.ToDictionary(x => x.Key, x => x.Value);

            return sortedLettersDict;
        }

        private static double Encode(string word,Dictionary<char, double> sortedLettersDict, List<Segment> segs)
        {
            double left = 0;

            foreach (var l in sortedLettersDict)
            {
                segs.Add(new Segment(l.Key, left, left + l.Value));
                left += l.Value;
            }

            double leftBorder = 0, rightBorder = 1;
            
            foreach (var w in word)
            {
                Segment s = findByLetter(w, segs);
                var newLeftBorder = leftBorder + (rightBorder - leftBorder) * s.Left;
                var newRightBorder = leftBorder + (rightBorder - leftBorder) * s.Right;
                leftBorder = newLeftBorder;
                rightBorder = newRightBorder;
            }
            
            return (leftBorder + rightBorder)/2;
        }

        private static string Decode(double encodedMessage, int wordLength, List<Segment> segs)
        {
            StringBuilder decodedString = new StringBuilder();
            double currentCode = encodedMessage;
            
            for (int i = 0; i < wordLength; i++)
            {
                foreach (var s in segs.Where(s => currentCode >= s.Left && currentCode <= s.Right))
                {
                    decodedString.Append(s.Letter);
                    currentCode = (currentCode - s.Left) / (s.Right - s.Left);
                    break;
                }
            }

            return decodedString.ToString();
        }

        private static Segment findByLetter(char letter, List<Segment> segments)
        {
            return segments.FirstOrDefault(s => s.Letter.Equals(letter));
        }
    }
}