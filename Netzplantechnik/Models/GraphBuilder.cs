using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace Netzplantechnik.Models
{
    class GraphBuilder
    {

        public byte[] GetByteGraph()
        {

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph(GraphStringBuilder(ProcessRepo.Processes), Enums.GraphReturnType.Png);

            return output;
        }

        /// <summary>
        /// this method create string to create graph from the List of processes
        /// </summary>
        /// <param name="processes"></param>
        /// <returns></returns>
        private string GraphStringBuilder(List<Process> processes)
        {
            StringBuilder graphString = new StringBuilder("digraph{");
            graphString.Append("node[shape=record]\nrankdir=LR;");
            foreach (Process p in processes)
            {
                foreach (Process pNext in p.NextProcess)
                {
                    string struct1 = "struct" + p.Name;
                    string struct2 = "struct" + pNext.Name;
                    graphString.Append(struct1 + "[label=\"{ " + p.FAZ + " | " + p.FEZ + " }\"];\n");
                    graphString.Append(struct2 + "[label=\"{ " + p.FAZ + " | " + p.FEZ + " }\"];\n");
                    graphString.Append(struct1 + "->" + struct2 + ";");
                }
            }
            graphString.Append("}");

            return graphString.ToString();
        }
    }
}
