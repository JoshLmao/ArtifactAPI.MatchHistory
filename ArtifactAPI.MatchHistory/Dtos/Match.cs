using ArtifactAPI.Enums;
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
        /// <summary>
        /// The outcome of the game for the player
        /// </summary>
        public Outcome MatchOutcome { get; set; }
        /// <summary>
        /// The total turns the game had
        /// </summary>
        public int Turns { get; set; }
        /// <summary>
        /// The start time of the game
        /// </summary>
        public DateTime StartTime { get; set; }
        public int ClusterId { get; set; }
        /// <summary>
        /// The team the player is on
        /// </summary>
        public Teams Team { get; set; }
        /// <summary>
        /// Set of flags that have been triggered in game - E.g, abandoned
        /// </summary>
        public Flags Flags { get; set; }
        /// <summary>
        /// Health of the first tower
        /// </summary>
        public int Tower1 { get; set; }
        /// <summary>
        /// Health of the second tower
        /// </summary>
        public int Tower2 { get; set; }
        /// <summary>
        /// Health of the third tower
        /// </summary>
        public int Tower3 { get; set; }
        /// <summary>
        /// Health of the one exposed ancient, if a tower was destroyed
        /// </summary>
        public int Ancient { get; set; }
        /// <summary>
        /// The amount of seconds to complete the game
        /// </summary>
        public int GameClock { get; set; }
        /// <summary>
        /// The type of Gauntlet being played
        /// </summary>
        public GauntletType GauntletType { get; set; }
        /// <summary>
        /// The deck code of the deck used by the player
        /// </summary>
        public string DeckCode { get; set; }

        public List<string> Heroes { get; set; }

        private string m_heroOne;
        public string Hero1
        {
            get { return m_heroOne; }
            set
            {
                m_heroOne = value;
                LoadHeroUrl(m_heroOne, 0);
            }
        }

        private string m_heroTwo;
        public string Hero2
        {
            get { return m_heroTwo; }
            set
            {
                m_heroTwo = value;
                LoadHeroUrl(m_heroTwo, 1);
            }
        }

        private string m_heroThree;
        public string Hero3
        {
            get { return m_heroThree; }
            set
            {
                m_heroThree = value;
                LoadHeroUrl(m_heroThree, 2);
            }
        }

        private string m_heroFour;
        public string Hero4
        {
            get { return m_heroFour; }
            set
            {
                m_heroFour = value;
                LoadHeroUrl(m_heroFour, 3);
            }
        }

        private string m_heroFive;
        public string Hero5
        {
            get { return m_heroFive; }
            set
            {
                m_heroFive = value;
                LoadHeroUrl(m_heroFive, 4);
            }
        }

        ArtifactClient m_client;

        public Match(ArtifactClient client)
        {
            m_client = client;
        }

        private async System.Threading.Tasks.Task LoadHeroUrl(string heroName, int heroIndex)
        {
            string url = await m_client.GetCardArtUrlAsync(heroName, ArtType.Ingame);
            if (string.IsNullOrEmpty(url))
            {
                Logger.OutputError($"Cannot find URL for '{heroName}'");
                return;
            }

            if (Heroes == null)
                Heroes = new List<string>();

            if(Heroes.Count - 1 > heroIndex)
            {
                Heroes.Insert(heroIndex, url);
            }
            else
            {
                Heroes.Add(url);
            }
        }
    }
}
