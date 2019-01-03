using System;
using System.Linq;
using System.Collections.Generic;
using ArtifactAPI.MatchHistory.Dtos;
using ArtifactAPI.MatchHistory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArtifactAPI.MatchHistory.Tests
{
    [TestClass]
    public class DecodeTests
    {
        private ArtifactClient m_client = null;
        private List<string> m_allHeroNames = null;
        private Random m_rnd = new Random();

        [TestMethod]
        public void MatchDecoderTest()
        {
            InitClient();

            int matchLength = 1000;
            string randomData = GetRandomMatchData(matchLength);
            List<Match> matches = MatchDecoder.DecodeMatch(randomData, m_client);

            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Count == matchLength);
        }

        private void InitClient()
        {
            if (m_client == null)
            {
                m_client = new ArtifactClient();
                m_client.GetAllCards();
            }
        }

        /// <summary>
        /// Gets a formatted string filled with random data to use in the decoder
        /// </summary>
        /// <param name="matchLimit">the amount of games to populate data for</param>
        /// <returns></returns>
        private string GetRandomMatchData(int matchLimit)
        {
            string builtString = null;

            string playerName = "DodgyDave";

            for (int i = 0; i < matchLimit; i++)
            {
                //Get a random MatchId
                int matchId = m_rnd.Next(0, 999999);

                int matchMode = GetRandomFromEnum(typeof(MatchMode));

                //Get random duration between 30 mins
                TimeSpan duration = TimeSpan.FromMinutes(m_rnd.Next(0, 30));
                int serverVersion = 250;
                //Random Outcome from enum
                int matchOutcome = GetRandomFromEnum(typeof(Outcome));
                //Random number from 0 to 10 turns
                int totalTurns = m_rnd.Next(0, 10);
                DateTime startTime = DateTime.Now;
                int clusterId = 155;
                int team = m_rnd.Next(0, 1);
                int flags = m_rnd.Next(0, 0);
                //Random Tower One health
                int towerOne = m_rnd.Next(0, 40);
                //Random Tower Two health
                int towerTwo = m_rnd.Next(0, 40);
                //Random Tower Three health
                int towerThree = m_rnd.Next(0, 40);
                //Random ancient health if towerOne Two or Three is 0
                int ancient = towerOne == 0 || towerTwo == 0 || towerThree == 0 ? m_rnd.Next(0, 80) : 80;
                //Game time between 0 and 800 seconds
                int gameClock = m_rnd.Next(0, 800);

                //Get random hero names
                string[] heroNames = GetRandomHeroNames();
                //Get a random gauntlet id
                int gauntletId = GetRandomFromEnum(typeof(GauntletType));
                //Use any deck code since not supported yet
                string deckCode = "ADCJbIGJ7kCwgSNScMFeF0CrN0BhmQBpQEmAQNBEiMBjbEBoAGtAUtCIEVjb24_";

                //Append all in format
                builtString += $"{matchId}|{playerName}|{matchMode}|{duration}|{serverVersion}|{matchOutcome}|{totalTurns}|{startTime}|{clusterId}|{team}|{flags}|{towerOne}|{towerTwo}|{towerThree}|{ancient}|{gameClock}|{heroNames[0]}|{heroNames[1]}|{heroNames[2]}|{heroNames[3]}|{heroNames[4]}|{gauntletId}|{deckCode}";

                //Add a comma if we're not at the end yet
                if (i < (matchLimit - 1))
                    builtString += ",";
            }

            return builtString;
        }

        /// <summary>
        /// Gets 5 random hero names
        /// </summary>
        /// <returns></returns>
        private string[] GetRandomHeroNames()
        {
            InitClient();

            if(m_allHeroNames == null)
            {
                List<Models.Card> cards = m_client.GetAllCards();
                List<Models.Card> heroes = cards.Where(x => x.Type == ArtifactAPI.Enums.CardType.Hero).ToList();
                m_allHeroNames = heroes.Select(x => x.Names.English).ToList();
            }
            
            List<string> randomHeroes = new List<string>();
            
            for(int i = 0; i < 5; i++)
            {
                int rndIndex = m_rnd.Next(0, m_allHeroNames.Count);
                randomHeroes.Add(m_allHeroNames[rndIndex]);
            }

            return randomHeroes.ToArray();
        }

        private int GetRandomFromEnum(Type enumType)
        {
            Array values = Enum.GetValues(enumType);
            return (int)values.GetValue(m_rnd.Next(0, values.Length));
        }
    }
}
