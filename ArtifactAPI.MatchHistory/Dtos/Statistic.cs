namespace ArtifactAPI.MatchHistory.Dtos
{
    public class Statistic : ListItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public double Percentage { get; set; }

        public Statistic() { }

        public Statistic(string name, object value, double percent)
        {
            Name = name;
            Value = value;
            Percentage = percent;
        }
    }
}
