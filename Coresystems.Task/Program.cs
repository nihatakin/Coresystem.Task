using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Coresystems.Task
{
    class Program
    {
        private const string TxtFile = @"FileContainer/Text.txt";

        static void Main(string[] args)
        {
            var txtPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, TxtFile);
            
            var dictionary = GetWordCountFromFile(txtPath);
            
            var ordered = dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            foreach (var kvp in ordered)
                Console.WriteLine("Key: {0}, Count: {1}", kvp.Key, kvp.Value);
        }

        private static Dictionary<string, int> GetWordCountFromFile(string txtPath)
        {
            var dictionary = new Dictionary<string, int>();
            var lines = File.ReadAllLines(txtPath);
            
            foreach (string line in lines)
            {
                foreach (string word in line.Split(' '))
                {
                    if (string.IsNullOrWhiteSpace(word))
                        continue;

                    var replacedWord = TrimCharacterAndToLower(word);

                    if (dictionary.ContainsKey(replacedWord))
                    {
                        dictionary[replacedWord] += 1;
                    }
                    else
                    {
                        dictionary[replacedWord] = 1;
                    }
                }
            }

            return dictionary;
        }
        private static string TrimCharacterAndToLower(string word)
        {
            return word.TrimEnd(".,-[!@#$%&/{()}=?+]".ToCharArray()).ToLower();
        }
    }
}