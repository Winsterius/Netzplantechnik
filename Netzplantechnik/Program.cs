using Netzplantechnik.Controllers;
using Netzplantechnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzplantechnik
{
    class Program
    {

        private static string input;
        private static string output;

        static void Main(string[] args)
        {
            if (args.Length == 4)
            {

                if (args.Contains("-i"))
                {
                    int index = Array.IndexOf(args, "-i");
                    input = args[index + 1];
                }

                if (args.Contains("-o"))
                {
                    int index = Array.IndexOf(args, "-o");
                    output = args[index + 1];
                }
            }
            else
            {
                Console.WriteLine("wrong arguments");
            }

            PngOutput.CreatePngFile(input, output);

            ProcessRepo p = new ProcessRepo();

            Console.ReadKey();
        }
    }
}
