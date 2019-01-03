using System;
using System.Collections.Generic;
using ArtifactAPI.MatchHistory.Dtos;
using ArtifactAPI.MatchHistory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArtifactAPI.MatchHistory.Tests
{
    [TestClass]
    public class DecodeTests
    {
        [TestMethod]
        public void MatchDecoderTest()
        {
            ArtifactClient client = new ArtifactClient();
            client.GetAllCards();

            int matchLength = 1000;
            string randomData = GetRandomMatchData(matchLength);
            List<Match> matches = MatchDecoder.DecodeMatch(randomData, client);

            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Count == matchLength);
        }

        private string GetRandomMatchData(int matchLimit)
        {
            string builtString = null;

            string playerName = "DodgyDave";
            Array allModes = Enum.GetValues(typeof(MatchMode));

            Random rnd = new Random();
            for (int i = 0; i < matchLimit; i++)
            {
                //Get a random MatchId
                int matchId = rnd.Next(0, 999999);

                int matchMode = (int)MatchMode.Matchmaking;

                //Get random duration between 30 mins
                TimeSpan duration = TimeSpan.FromMinutes(rnd.Next(0, 30));
                int serverVersion = 250;
                int matchOutcome = rnd.Next(0, 2);
                int totalTurns = rnd.Next(0, 10);
                DateTime startTime = DateTime.Now;
                int clusterId = 155;
                int team = rnd.Next(0, 1);
                int flags = rnd.Next(0, 0);
                int towerOne = rnd.Next(0, 40);
                int towerTwo = rnd.Next(0, 40);
                int towerThree = rnd.Next(0, 40);
                int ancient = towerOne == 0 || towerTwo == 0 || towerThree == 0 ? rnd.Next(0, 80) : 80;
                int gameClock = rnd.Next(0, 800);

                string[] heroNames = new string[] { "Venomancer", "Necrophos", "Zeus", "Ogre Magi", "Tinker" };
                int gauntletId = (int)GauntletType.Constructed;
                string deckCode = "ADCJbIGJ7kCwgSNScMFeF0CrN0BhmQBpQEmAQNBEiMBjbEBoAGtAUtCIEVjb24_";
                builtString += $"{matchId}|{playerName}|{matchMode}|{duration}|{serverVersion}|{matchOutcome}|{totalTurns}|{startTime}|{clusterId}|{team}|{flags}|{towerOne}|{towerTwo}|{towerThree}|{ancient}|{gameClock}|{heroNames[0]}|{heroNames[1]}|{heroNames[2]}|{heroNames[3]}|{heroNames[4]}|{gauntletId}|{deckCode}";

                if(i < (matchLimit - 1))
                    builtString += ",";
            }

            return builtString;
        }
    }
}
