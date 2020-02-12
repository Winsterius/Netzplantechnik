using Netzplantechnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzplantechnik.Controllers
{
    class PngOutput
    {
        private static ProcessRepo pr = new ProcessRepo();
        private static Process p = new Process();
        public static void CreatePngBild(string inputFile, string outputFile)
        {
            TxtReader.CreateProcesses(inputFile);
            p.SetPreviewProcess();
            p.SetNextProcesses();
            p.SetFAZ_FEZ();
            p.SetSAZ_SEZ();
            p.SetGP_FP();
            p.SetCriticalPath();


        }

    }
}
