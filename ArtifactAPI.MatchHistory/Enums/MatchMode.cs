namespace ArtifactAPI.MatchHistory.Enums
{
    /// <summary>
    /// All different modes inside Artifact
    /// </summary>
    public enum MatchMode
    {
        Unknown = -1,
        /// <summary>
        /// Casual matchmaking against other players
        /// </summary>
        Matchmaking = 2,
        /// <summary>
        /// Gauntlet type game mode
        /// </summary>
        Gauntlet = 3,
        /// <summary>
        /// Bot Match - played against a bot
        /// </summary>
        Bot_Match = 8,
    }
}
