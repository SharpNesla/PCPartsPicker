namespace PCPartsPicker
{
    public class Rule
    {
        public string CaseID { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public bool IsUpperBoundSet { get { return UpperBound != decimal.MaxValue; } }
        public string OutputCPUID { get; set; }
        public string OutputGPUID { get; set; }
    }
}
