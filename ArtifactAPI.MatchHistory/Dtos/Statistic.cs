namespace ArtifactAPI.MatchHistory.Dtos
{
    public class Statistic : ListItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double Percentage { get; set; }

        public Statistic() { }

        public Statistic(string name, double value, double percent)
        {
            Name = name;
            Value = value;
            Percentage = percent;
        }
    }
}
