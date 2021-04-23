namespace PCPartsPicker
{
    public class Rule
    {
        public string CaseID { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public string UpperBoundFinite { get { return UpperBound != decimal.MaxValue ? UpperBound.ToString() : "∞"; } }
        public string OutputCPUID { get; set; }
        public string OutputGPUID { get; set; }
        public bool IsSuggestion { get; set; }
    }
}
