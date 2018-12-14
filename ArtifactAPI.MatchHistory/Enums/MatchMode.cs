namespace ArtifactAPI.MatchHistory.Enums
{
    public enum MatchMode
    {
        Unknown = -1,
        /// <summary>
        /// Unknown
        /// </summary>
        GameModeOne = 1,           //ToDo: Verify
        /// <summary>
        /// Casual matchmaking against other players
        /// </summary>
        Matchmaking = 2,
        /// <summary>
        /// Casual Constructed - Pick a deck, play until you win 5 or lose 2
        /// </summary>
        CasualConstructed = 3,     //ToDo: Verify number
        /// <summary>
        /// Casual Phantom Draft - Pick 60 cards, playing until you win 5 or lose 2, can change deck inbetween games
        /// </summary>
        CasualPhantomDraft = 4,    //ToDo: Verify number
        /// <summary>
        /// Expert Constructed - Costs 1 ticket, pick a deck, play until 5 mins or 2 losses
        /// </summary>
        ExpertConstructed = 5,     //ToDo: Verify number
        /// <summary>
        /// Phantom Draft - Cost 1 ticket, pick 60 cards, play until 5 wins or 2 losses
        /// </summary>
        PhantomDraft = 6,          //ToDo: Verify number
        /// <summary>
        /// Keeper's Draft - Costs 2 tickets & 5 packs, create a deck and play until 5 wins or 2 losses. Keep cards you draft
        /// </summary>
        KeeperDraft = 7,           //ToDo: Verify number
        /// <summary>
        /// Bot Match - played against a bot
        /// </summary>
        Bot_Match = 8,
        /// <summary>
        /// Random Mode - Get a random deck from the current meta decks
        /// </summary>
        RandomMode = 9,
            
    }
}
