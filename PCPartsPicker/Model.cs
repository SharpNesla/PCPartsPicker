using System;
using System.Collections.Generic;

namespace PCPartsPicker
{
    public class PartPickResult
    {
        public CPU SelectedCPU { get; set; }
        public GPU SelectedGPU { get; set; }
        public Rule Rule { get; set; }
        public decimal SumCost {
            get { return SelectedCPU.Cost + (SelectedGPU != null ? SelectedGPU.Cost : 0); }
        }

    }
    public class Model
    {
        public List<CPU> CPUs { get; set; }
        public List<GPU> GPUs { get; set; }
        public List<Rule> Rules { get; set; }
        public string[] Variants { get; set; }
        public PartPickResult PickByCase(string CaseID, decimal maxPrice)
        {
            foreach (var rule in Rules)
            {
                if (maxPrice >= rule.LowerBound &&
                    maxPrice < rule.UpperBound &&
                    rule.CaseID == CaseID)
                {
                    return new PartPickResult()
                    {
                        SelectedCPU = CPUs.Find(x => x.ID == rule.OutputCPUID),
                        SelectedGPU = GPUs.Find(x => x.ID == rule.OutputGPUID),
                        Rule = rule
                    };
                }
            }
            return null;
        }
    }
}
