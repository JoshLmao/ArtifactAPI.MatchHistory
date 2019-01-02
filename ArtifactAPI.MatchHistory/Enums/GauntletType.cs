namespace ArtifactAPI.MatchHistory.Enums
{
    /// <summary>
    /// The type of gauntlets in Artifact
    /// </summary>
    public enum GauntletType
    {
        /// <summary>
        /// No gauntlet type
        /// </summary>
        None = 0,
        /// <summary>
        /// Current meta Gauntlet (Featured tab)
        /// </summary>
        RandomMeta = 5,
        /// <summary>
        /// Constructed, Expert - Costs 1 ticket, pick a deck, play until 5 mins or 2 losses
        /// </summary>
        ConstructedExpert = 6,     //ToDo: Verify number
        /// <summary>
        /// Phantom Draft, Expert - Cost 1 ticket, pick 60 cards, play until 5 wins or 2 losses
        /// </summary>
        PhantomDraftExpert = 7,          //ToDo: Verify number
        /// <summary>
        /// Keeper's Draft, Expert - Costs 2 tickets & 5 packs, create a deck and play until 5 wins or 2 losses. Keep cards you draft
        /// </summary>
        KeeperDraftExpert = 8,           //ToDo: Verify number
        /// <summary>
        /// Constructed, Casual - Pick a deck, play until you win 5 or lose 2
        /// </summary>
        Constructed = 10,
        /// <summary>
        /// Phantom Draft, Casual - Pick 60 cards, playing until you win 5 or lose 2, can change deck inbetween games
        /// </summary>
        CasualPhantomDraft = 11,
    }
}
