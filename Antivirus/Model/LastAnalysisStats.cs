namespace Antivirus.Model
{
    public class LastAnalysisStats
    {
        public int malicious { get; set; }

        public int suspicious { get; set; }

        public int undetected { get; set; }

        public int harmless { get; set; }

        public int timeout { get; set; }
    }
}
