using ArtifactAPI.MatchHistory.Dtos;
using System.Linq;
using System.Collections.Generic;
using System;
using ArtifactAPI.MatchHistory.Enums;

namespace ArtifactAPI.MatchHistory
{
    public class StatsController
    {
        /// <summary>
        /// Get total lifetime stats for the player
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static List<ListItem> GetLifetimeStats(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();

            //Add Section Stats Title
            stats.Add(new Separator("Overview", "Matches", "Win Rate"));

            int totalGamesCount = matches.Count;
            stats.Add(new Statistic("Total Games", totalGamesCount, 0));

            List<Match> matchmakingGames = matches.Where(x => x.MatchMode == MatchMode.Matchmaking).ToList();
            int totalWonGames = matchmakingGames.Count(x => x.MatchOutcome == Outcome.Victory);
            double winRate = GetPercent(totalWonGames, matchmakingGames.Count);
            stats.Add(new Statistic("Matchmaking", matchmakingGames.Count, winRate));

            List<Match> gauntletGames = matches.Where(x => x.MatchMode == MatchMode.Gauntlet).ToList();
            totalWonGames = gauntletGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, gauntletGames.Count);
            stats.Add(new Statistic("Gauntlet", gauntletGames.Count, winRate));

            List<Match> botMatches = matches.Where(x => x.MatchMode == MatchMode.BotMatch).ToList();
            totalWonGames = botMatches.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, botMatches.Count);
            stats.Add(new Statistic("Bot Match", botMatches.Count, winRate));

            ///Add next section stats title
            stats.Add(new Separator("Gauntlet Type", "Matches", "Win Rate"));

            ///Call to Arms
            List<Match> ctaGames = gauntletGames.Where(x => x.GauntletType == GauntletType.CallToArms).ToList();
            totalWonGames = ctaGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, ctaGames.Count);
            stats.Add(new Statistic("Call to Arms", ctaGames.Count, winRate));

            ///Constructed
            List<Match> constructedGames = gauntletGames.Where(x => x.GauntletType == GauntletType.Constructed).ToList();
            totalWonGames = constructedGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, constructedGames.Count);
            stats.Add(new Statistic("Constructed", constructedGames.Count, winRate));

            ///Phantom Draft
            List<Match> pdGames = gauntletGames.Where(x => x.GauntletType == GauntletType.CasualPhantomDraft).ToList();
            totalWonGames = pdGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, pdGames.Count);
            stats.Add(new Statistic("Phantom Draft", pdGames.Count, winRate));

            ///Expert Constructed
            List<Match> eConstructedGames = gauntletGames.Where(x => x.GauntletType == GauntletType.ConstructedExpert).ToList();
            totalWonGames = eConstructedGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, pdGames.Count);
            stats.Add(new Statistic("Expert Constructed", eConstructedGames.Count, winRate));

            ///Expert Phantom Draft
            List<Match> epdGames = gauntletGames.Where(x => x.GauntletType == GauntletType.ConstructedExpert).ToList();
            totalWonGames = epdGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, epdGames.Count);
            stats.Add(new Statistic("Expert Constructed", epdGames.Count, winRate));

            ///Keeper's Draft
            List<Match> keepersDraftGames = gauntletGames.Where(x => x.GauntletType == GauntletType.KeeperDraftExpert).ToList();
            totalWonGames = keepersDraftGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, keepersDraftGames.Count);
            stats.Add(new Statistic("Keeper's Draft", keepersDraftGames.Count, winRate));

            ///Faction separator and headers
            stats.Add(new Separator("Faction", "Matches", "Win Rate"));

            ///Radiant Win Rate
            List<Match> radiantGames = matches.Where(x => x.Team == Teams.Radiant).ToList();
            totalWonGames = radiantGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, radiantGames.Count);
            stats.Add(new Statistic("Radiant", totalWonGames, winRate));

            ///Dire Win Rate
            List<Match> direGames = matches.Where(x => x.Team == Teams.Dire).ToList();
            totalWonGames = direGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, direGames.Count);
            stats.Add(new Statistic("Dire", totalWonGames, winRate));

            return stats;
        }

        /// <summary>
        /// Get relevant stats related to only casual modes
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static List<ListItem> GetCasualStats(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();

            ///Total Games + Win count
            List<Match> casualGames = matches.Where(x => x.MatchMode == MatchMode.Matchmaking ||
                                                         x.MatchMode == MatchMode.Gauntlet && (x.GauntletType == GauntletType.CasualPhantomDraft || x.GauntletType == GauntletType.Constructed || x.GauntletType == GauntletType.CallToArms)
                                                         ).ToList();

            stats.AddRange(GetGeneralStats(casualGames));
            return stats;
        }

        /// <summary>
        /// Get relevant stats related to onlyy expert modes
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static List<ListItem> GetExpertStats(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();

            ///Total Games + Win count
            List<Match> expertGames = matches.Where(x => x.MatchMode == MatchMode.Gauntlet && 
                                                        (x.GauntletType == GauntletType.ConstructedExpert|| x.GauntletType == GauntletType.PhantomDraftExpert|| x.GauntletType == GauntletType.KeeperDraftExpert)
                                                         ).ToList();

            stats.AddRange(GetGeneralStats(expertGames));
            return stats;
        }

        private static List<ListItem> GetGeneralStats(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();

            stats.Add(new Separator("Overview", "Matches", "Win Rate"));

            int totalWonGames = matches.Count(x => x.MatchOutcome == Outcome.Victory);
            double winRate = GetPercent(totalWonGames, matches.Count);
            stats.Add(new Statistic("Total Games", totalWonGames, winRate));

            ///Total Radiant Games
            List<Match> radiantGames = matches.Where(x => x.Team == Teams.Radiant).ToList();
            totalWonGames = radiantGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, radiantGames.Count);
            stats.Add(new Statistic("Radiant", totalWonGames, winRate));

            ///Total Dire Games
            List<Match> direGames = matches.Where(x => x.Team == Teams.Dire).ToList();
            totalWonGames = direGames.Count(x => x.MatchOutcome == Outcome.Victory);
            winRate = GetPercent(totalWonGames, direGames.Count);
            stats.Add(new Statistic("Dire", totalWonGames, winRate));

            ///Records
            stats.Add(new Separator("Record", "Hero", "Pick Rate"));
            
            ///Most common hero card
            KeyValuePair<string, int> mostCommonCardKVP = GetMostCommonHero(matches);
            double usePercent = GetPercent((double)mostCommonCardKVP.Value, (double)matches.Count);
            stats.Add(new Statistic("Common Hero", mostCommonCardKVP.Key, usePercent));

            stats.Add(new Separator("Record", "Value", "Game Id"));

            if(matches.Count > 0)
            {
                ///Most Turns
                Match mostTurnsGame = matches.Aggregate((x, y) => x.Turns > y.Turns ? x : y);
                stats.Add(new Statistic("Most Turns in a game", mostTurnsGame.Turns, mostTurnsGame.MatchId));

                ///Shortest Game
                Match shortestGame = matches.Aggregate((x, y) => x.Flags == Flags.None && x.Duration < y.Duration? x : y);
                stats.Add(new Statistic("Shortest Game", shortestGame.Duration, shortestGame.MatchId));

                ///Longest Game
                Match longestGame = matches.Aggregate((x, y) => x.Duration > y.Duration ? x : y);
                stats.Add(new Statistic("Longest Game", longestGame.Duration.ToString(), longestGame.MatchId));
            }
            else
            {
                stats.Add(new Statistic("Most Turns in a game", "Unknown", 0));
                stats.Add(new Statistic("Shortest Game", "Unknown", 0));
                stats.Add(new Statistic("Longest Game", "Unknown", 0));
            }

            return stats;
        }

        public static List<ListItem> GetRecords(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();
            return stats;
        }

        private static double GetPercent(double amount, double total, int roundAmount = 1)
        {
            double percent = (amount / total) * 100;
            if (double.IsNaN(percent))
                return 0;
            else
                return Math.Round(percent, roundAmount);
        }

        /// <summary>
        /// Gets the most common deck in a matches list and how many times it occurs
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        private static KeyValuePair<string[], int> GetMostCommonDeck(List<Match> matches)
        {
            if (matches == null || matches.Count <= 0)
                return new KeyValuePair<string[], int>(null, 0);

            List<string[]> decks = new List<string[]>();
            foreach(Match m in matches)
            {
                string[] heroes = { m.Hero1, m.Hero2, m.Hero3, m.Hero4, m.Hero5 };
                decks.Add(heroes);
            }

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(string[] deck in decks)
            {
                //Convert to one string for the id and convert back when returning
                string id = string.Join(",", deck);
                if (dict.ContainsKey(id))
                {
                    dict[id] += 1;
                }
                else
                {
                    dict.Add(id, 1);
                }
            }

            KeyValuePair<string, int> kvp = dict.Aggregate((x, y) => x.Value > y.Value ? x : y);
            return new KeyValuePair<string[], int>(kvp.Key.Split(','), kvp.Value);
        }

        private static KeyValuePair<string, int> GetMostCommonHero(List<Match> matches)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (Match m in matches)
            {
                string mHero = m.Hero1;
                if (dict.ContainsKey(mHero))
                    dict[mHero] += 1;
                else
                    dict.Add(mHero, 1);

                mHero = m.Hero2;
                if (dict.ContainsKey(mHero))
                    dict[mHero] += 1;
                else
                    dict.Add(mHero, 1);

                mHero = m.Hero3;
                if (dict.ContainsKey(mHero))
                    dict[mHero] += 1;
                else
                    dict.Add(mHero, 1);

                mHero = m.Hero4;
                if (dict.ContainsKey(mHero))
                    dict[mHero] += 1;
                else
                    dict.Add(mHero, 1);

                mHero = m.Hero5;
                if (dict.ContainsKey(mHero))
                    dict[mHero] += 1;
                else
                    dict.Add(mHero, 1);
            }

            if (dict.Count > 0)
                return dict.Aggregate((x, y) => x.Value > y.Value ? x : y);
            else
                return new KeyValuePair<string, int>("Unknown", 0);
        }
    }
}
