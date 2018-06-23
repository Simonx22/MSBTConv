using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSBTConv
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MSBTConv by Simonx22\n");

            if (args.Length != 1)
            {
                // Print out usage
                Console.WriteLine("Usage: dotnet MSBTConv.dll <MSBT>");

                return;
            }

            // Check that the file exists
            if (!File.Exists(args[0]))
            {
                // Print error
                Console.WriteLine("The file doesn't exist.");

                return;
            }

            Dictionary<string, string> msbt = LoadMsbt(args[0]);
            var path = Path.ChangeExtension((args[0]), ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(msbt));
        }

        private static Dictionary<string, string> LoadMsbt(string filePath)
        {
            Dictionary<string, string> textMappings = new Dictionary<string, string>();

            MSBT msbt = new MSBT(filePath);

            for (int i = 0; i < msbt.TXT2.NumberOfStrings; i++)
            {
                IEntry entry = msbt.HasLabels ? msbt.LBL1.Labels[i] : msbt.TXT2.Strings[i];
                textMappings.Add(entry.ToString(), msbt.FileEncoding.GetString(entry.Value));
            }

            return textMappings;
        }
    }
}
