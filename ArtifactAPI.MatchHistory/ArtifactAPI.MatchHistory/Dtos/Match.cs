using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Collections.Generic;

namespace ArtifactAPI.MatchHistory.Dtos
{
    public class Match
    {
        /// <summary>
        /// The display name of the player
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// The ID of the match
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// Type of mode that was played
        /// </summary>
        public MatchMode MatchMode { get; set; }
        /// <summary>
        /// Total duration of the game
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// The version of the server the game was played on
        /// </summary>
        public int ServerVersion { get; set; }
        public Outcome MatchOutcome { get; set; }
        public int Turns { get; set; }
        public DateTime StartTime { get; set; }
        public int ClusterId { get; set; }
        public Teams Team { get; set; }
        public int Flags { get; set; }
        public int Tower1 { get; set; }
        public int Tower2 { get; set; }
        public int Tower3 { get; set; }
        public int Ancient { get; set; }
        public int GameClock { get; set; }
        public int GauntletId { get; set; }
        public string DeckCode { get; set; }

        private string m_heroOne;
        public string Hero1
        {
            get { return m_heroOne; }
            set
            {
                m_heroOne = value;
                LoadHeroUrl(m_heroOne);
            }
        }

        private string m_heroTwo;
        public string Hero2
        {
            get { return m_heroTwo; }
            set
            {
                m_heroTwo = value;
                LoadHeroUrl(m_heroTwo);
            }
        }

        private string m_heroThree;
        public string Hero3
        {
            get { return m_heroThree; }
            set
            {
                m_heroThree = value;
                LoadHeroUrl(m_heroThree);
            }
        }

        private string m_heroFour;
        public string Hero4
        {
            get { return m_heroFour; }
            set
            {
                m_heroFour = value;
                LoadHeroUrl(m_heroFive);
            }
        }

        private string m_heroFive;
        public string Hero5
        {
            get { return m_heroFive; }
            set
            {
                m_heroFive = value;
                LoadHeroUrl(m_heroFive);
            }
        }


        public List<string> Heroes { get; set; }

        //private ArtifactAPI.ArtifactClient m_client;

        private void LoadHeroUrl(string heroName)
        {
        }
    }
}
