using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzplantechnik.Models
{
    class ProcessRepo
    {

        static public List<Process> Processes { get; set; } = new List<Process>();
        static public List<Process> CriticalPath { get; set; } = new List<Process>();



    }
}
