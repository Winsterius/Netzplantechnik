using Netzplantechnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Netzplantechnik.Controllers
{
    class PngOutput
    {
        private static ProcessRepo pr = new ProcessRepo();
        private static Process p = new Process();
        private static GraphBuilder gb = new GraphBuilder();
        
        //total process to create Png picture
        public static void CreatePngFile(string inputFile, string outputFile)
        {
            TxtReader.CreateProcesses(inputFile);
            p.SetPreviewProcess();
            p.SetNextProcesses();
            p.SetFAZ_FEZ();
            p.SetSAZ_SEZ();
            p.SetGP_FP();
            p.SetCriticalPath();
            CreatePicture(outputFile);

        }


        /// <summary>
        /// This method will create graph from the Process repository
        /// </summary>
        /// <param name="outputFile">Name of the File</param>
        public static void CreatePicture (string outputFile)
        {
            byte[] graph = gb.GetByteGraph();

            string path = Directory.GetCurrentDirectory();

            try
            {
                File.WriteAllBytes(path + "\\" + outputFile, graph);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            
        }

    }
}
