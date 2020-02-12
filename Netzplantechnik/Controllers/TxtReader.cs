using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using Netzplantechnik.Models;

namespace Netzplantechnik.Controllers
{
    class TxtReader
    {

		static string path;
		static string inputText;
		
		/// <summary>
		/// This method create list of processes in ProcessRepo class from text file in CSV format 
		/// </summary>
		/// <param name="inputFile">name of file in same directory</param>
        internal static void CreateProcesses(string inputFile)
        {
			path = Directory.GetCurrentDirectory();

			try
			{
				inputText = File.ReadAllText(path + "\\" + inputFile);
							
			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);
			}

			if (!string.IsNullOrEmpty(inputText))
			{
				string[] processes = inputText.Split('\n');

				for (int i = 1; i < processes.Length; i++)
				{
					string[] data = processes[i].Split(';');
					if (!string.IsNullOrWhiteSpace(processes[i]))
					{
						ProcessRepo.Processes.Add(new Process(data[0], data[1], int.Parse(data[2]), data[3].Replace("\r", "").Split(',')));

					}
				}

			}

        }
    }
}
