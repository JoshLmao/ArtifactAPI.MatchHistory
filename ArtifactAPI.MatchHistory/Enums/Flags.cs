namespace ArtifactAPI.MatchHistory.Enums
{
    public enum Flags
    {
        /// <summary>
        /// No flags for the game
        /// </summary>
        None = 0,
        /// <summary>
        /// One player surrendered
        /// </summary>
        Surrendered = 1,
        /// <summary>
        /// A player abandoned the game
        /// </summary>
        Abandoned = 2,
    }
}
