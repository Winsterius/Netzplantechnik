using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzplantechnik.Models
{
    class Process
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string[] PreviewProcessName { get; set; }
        public List<Process> PreviewProcess { get; set; } = new List<Process>();
        public List<Process> NextProcess { get; set; } = new List<Process>();
        public int FAZ { get; set; }
        public int FEZ { get; set; }
        public int GP { get; set; }
        public int FP { get; set; }
        public int SAZ { get; set; }
        public int SEZ { get; set; }
        public bool CriticalPath { get; set; }

        //Constructors
        public Process()
        {

        }
        public Process(string name, string description, int duration, string[] preview)
        {
            Name = name;
            Description = description;
            Duration = duration;
            PreviewProcessName = preview;


            FAZ = 0;
            FEZ = FAZ + Duration;
            GP = 0;
            FP = 0;
            SAZ = 0;
            SEZ = 0;
            
            //FEZ = FAZ + duration;
            //SEZ = SAZ + duration;
            //GP = SAZ - FAZ;
        }


        //Methods to set Processes data (Preview Process, Next Process, Critical Path and another calculated data)
        public void SetPreviewProcess()
        {
            foreach (Process p in ProcessRepo.Processes)
            {
                foreach(string pr in p.PreviewProcessName)
                {
                    p.PreviewProcess.AddRange(ProcessRepo.Processes.Where(c => c.Name == pr));
                }
            }
        }
        public void SetNextProcesses()
        {
            foreach (Process p in ProcessRepo.Processes)
            {
                foreach (Process p2 in p.PreviewProcess)
                {
                    foreach (Process p3 in ProcessRepo.Processes)
                    {
                        if(p3.Name == p2.Name)
                        {
                            p2.NextProcess.Add(p);
                        }
                    }
                }
            }
            
        }
        public void SetFAZ_FEZ()
        {
            foreach (Process p in ProcessRepo.Processes)
            {
                if(p.PreviewProcess.Count != 0)
                {
                    p.FAZ = p.PreviewProcess.Max(pr => pr.FEZ);
                    p.FEZ = p.FAZ + p.Duration;
                    p.SEZ = p.FEZ;
                    p.SAZ = p.SEZ - p.Duration;
                }
            }
        }
        public void SetSAZ_SEZ()
        {

            for (int i = ProcessRepo.Processes.Count - 1; i >= 0; i--)
            {
                if (ProcessRepo.Processes[i].NextProcess.Count != 0)
                {
                    ProcessRepo.Processes[i].SEZ = ProcessRepo.Processes[i].NextProcess.Min(pr => pr.SAZ);
                    ProcessRepo.Processes[i].SAZ = ProcessRepo.Processes[i].SEZ - ProcessRepo.Processes[i].Duration;

                }
            }
        }

        public void SetGP_FP()
        {

            //for (int i = ProcessRepo.Processes.Count - 1; i >= 0; i--)
            //{
            //    if (ProcessRepo.Processes[i].NextProcess.Count != 0)
            //    {
            //        ProcessRepo.Processes[i].FP = ProcessRepo.Processes[i].NextProcess.Min(pr => pr.FAZ) - ProcessRepo.Processes[i].FEZ;
            //        ProcessRepo.Processes[i].GP = ProcessRepo.Processes[i].SEZ - ProcessRepo.Processes[i].FEZ;

            //    }
            //}

            foreach (Process p in ProcessRepo.Processes)
            {
                if (p.NextProcess.Count != 0)
                {
                    p.FP = p.NextProcess.Min(pr => pr.FAZ) - p.FEZ;
                    p.GP = p.SEZ - p.FEZ;
                }
            }
        }
        public void SetCriticalPath()
        {
            foreach (Process p in ProcessRepo.Processes)
            {
                if(p.GP == 0 && p.FP == 0)
                {
                    p.CriticalPath = true;
                }
            }
        }
    }
}
