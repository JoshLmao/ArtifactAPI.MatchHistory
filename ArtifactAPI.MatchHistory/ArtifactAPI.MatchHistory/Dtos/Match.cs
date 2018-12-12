using System;

namespace ArtifactAPI.MatchHistory.Dtos
{
    public class Match
    {
        public string AccountId { get; set; }
        public int MatchId { get; set; }
        public int MatchMode { get; set; }
        public TimeSpan Duration { get; set; }
        public int ServerVersion { get; set; }
        public int MatchOutcome { get; set; }
        public int Turns { get; set; }
        public DateTime StartTime { get; set; }
        public int ClusterId { get; set; }
        public int Team { get; set; }
        public int Flags { get; set; }
        public int Tower1 { get; set; }
        public int Tower2 { get; set; }
        public int Tower3 { get; set; }
        public int Ancient { get; set; }
        public int GameClock { get; set; }
        public string Hero1 { get; set; }
        public string Hero2 { get; set; }
        public string Hero3 { get; set; }
        public string Hero4 { get; set; }
        public string Hero5 { get; set; }
        public int GauntletId { get; set; }
        public string DeckCode { get; set; }
    }
}
