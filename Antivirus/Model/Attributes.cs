namespace Antivirus.Model
{
    public class Attributes
    {
        public LastAnalysisStats last_analysis_stats { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
        public string meaningful_name { get; set; }
        public string type_description { get; set; }
        public long size { get; set; }
        public long last_analysis_date { get; set; }
    }
}
