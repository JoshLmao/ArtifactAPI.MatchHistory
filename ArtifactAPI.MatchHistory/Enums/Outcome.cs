namespace ArtifactAPI.MatchHistory.Enums
{
   public enum Outcome
    {
        Unknown = -1,
        Loss = 0,
        Victory = 1,
        Draw = 2,
        Draw_ = 8, //ToDo Verify in game how 8 occurs, apparently another draw case
    }
}
