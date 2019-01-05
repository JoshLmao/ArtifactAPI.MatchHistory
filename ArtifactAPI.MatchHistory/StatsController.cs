using ArtifactAPI.MatchHistory.Dtos;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ArtifactAPI.MatchHistory
{
    public class StatsController
    {
        public static List<ListItem> GetLifetimeStats(List<Match> matches)
        {
            List<ListItem> stats = new List<ListItem>();

            //Add Section Stats Title
            stats.Add(new Separator("Overview", "Matches", "Win Rate"));

            int totalGamesCount = matches.Count;
            stats.Add(new Statistic("Total Games", totalGamesCount, 0));

            List<Match> matchmakingGames = matches.Where(x => x.MatchMode == Enums.MatchMode.Matchmaking).ToList();
            int totalWonGames = matchmakingGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            double winRate = GetPercent(totalWonGames, matchmakingGames.Count);
            stats.Add(new Statistic("Matchmaking", matchmakingGames.Count, winRate));

            List<Match> gauntletGames = matches.Where(x => x.MatchMode == Enums.MatchMode.Gauntlet).ToList();
            totalWonGames = gauntletGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, gauntletGames.Count);
            stats.Add(new Statistic("Gauntlet", gauntletGames.Count, winRate));

            List<Match> botMatches = matches.Where(x => x.MatchMode == Enums.MatchMode.BotMatch).ToList();
            totalWonGames = botMatches.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, botMatches.Count);
            stats.Add(new Statistic("Bot Match", botMatches.Count, winRate));

            ///Add next section stats title
            stats.Add(new Separator("Gauntlet Type", "Matches", "Win Rate"));

            ///Call to Arms
            List<Match> ctaGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.RandomMeta).ToList();
            totalWonGames = ctaGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, ctaGames.Count);
            stats.Add(new Statistic("Call to Arms", ctaGames.Count, winRate));

            ///Constructed
            List<Match> constructedGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.Constructed).ToList();
            totalWonGames = constructedGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, constructedGames.Count);
            stats.Add(new Statistic("Constructed", constructedGames.Count, winRate));

            ///Phantom Draft
            List<Match> pdGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.CasualPhantomDraft).ToList();
            totalWonGames = pdGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, pdGames.Count);
            stats.Add(new Statistic("Phantom Draft", pdGames.Count, winRate));

            ///Expert Constructed
            List<Match> eConstructedGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.ConstructedExpert).ToList();
            totalWonGames = eConstructedGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, pdGames.Count);
            stats.Add(new Statistic("Expert Constructed", eConstructedGames.Count, winRate));

            ///Expert Phantom Draft
            List<Match> epdGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.ConstructedExpert).ToList();
            totalWonGames = epdGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, epdGames.Count);
            stats.Add(new Statistic("Expert Constructed", epdGames.Count, winRate));

            ///Keeper's Draft
            List<Match> keepersDraftGames = gauntletGames.Where(x => x.GauntletType == Enums.GauntletType.KeeperDraftExpert).ToList();
            totalWonGames = keepersDraftGames.Count(x => x.MatchOutcome == Enums.Outcome.Victory);
            winRate = GetPercent(totalWonGames, keepersDraftGames.Count);
            stats.Add(new Statistic("Keeper's Draft", keepersDraftGames.Count, winRate));

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
    }
}
