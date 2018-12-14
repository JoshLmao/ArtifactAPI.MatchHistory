using ArtifactAPI.MatchHistory.Dtos;
using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Collections.Generic;

namespace ArtifactAPI.MatchHistory
{
    public class MatchDecoder
    {
        const char PROPERTY_SEPARATOR = '|';
        const char MATCH_SEPARATOR = ',';

        public static List<Match> DecodeMatch(string s, ArtifactClient client)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            s = RemoveFormatting(s);

            List<Match> matches = new List<Match>();
            try
            {
                string[] matchArr = s.Split(MATCH_SEPARATOR);
                foreach (string match in matchArr)
                {
                    if (string.IsNullOrEmpty(match))
                        continue;

                    string[] properties = match.Split(PROPERTY_SEPARATOR);
                    Match m = new Match(client);

                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (string.IsNullOrEmpty(properties[i]))
                        {
                            Console.WriteLine($"Empty property at value {i}");
                            continue;
                        }

                        if (i == 0)
                        {
                            m.MatchId = int.Parse(properties[i]);
                        }
                        else if (i == 1)
                        {
                            m.AccountId = properties[i];
                        }
                        else if (i == 2)
                        {
                            m.MatchMode = (MatchMode)int.Parse(properties[i]);
                        }
                        else if (i == 3)
                        {
                            m.Duration = TimeSpan.Parse(properties[i]);
                        }
                        else if (i == 4)
                        {
                            m.ServerVersion = int.Parse(properties[i]);
                        }
                        else if (i == 5)
                        {
                            m.MatchOutcome = (Outcome)int.Parse(properties[i]);
                        }
                        else if (i == 6)
                        {
                            m.Turns = int.Parse(properties[i]);
                        }
                        else if (i == 7)
                        {
                            m.StartTime = DateTime.Parse(properties[i]);
                        }
                        else if (i == 8)
                        {
                            m.ClusterId = int.Parse(properties[i]);
                        }
                        else if (i == 9)
                        {
                            m.Team = (Teams)int.Parse(properties[i]);
                        }
                        else if (i == 10)
                        {
                            m.Flags = int.Parse(properties[i]);
                        }
                        else if (i == 11)
                        {
                            m.Tower1 = int.Parse(properties[i]);
                        }
                        else if (i == 12)
                        {
                            m.Tower2 = int.Parse(properties[i]);
                        }
                        else if (i == 13)
                        {
                            m.Tower3 = int.Parse(properties[i]);
                        }
                        else if (i == 14)
                        {
                            m.Ancient = int.Parse(properties[i]);
                        }
                        else if (i == 15)
                        {
                            m.GameClock = int.Parse(properties[i]);
                        }
                        else if (i == 16)
                        {
                            m.Hero1 = properties[i];
                        }
                        else if (i == 17)
                        {
                            m.Hero2 = properties[i];
                        }
                        else if (i == 18)
                        {
                            m.Hero3 = properties[i];
                        }
                        else if (i == 19)
                        {
                            m.Hero4 = properties[i];
                        }
                        else if (i == 20)
                        {
                            m.Hero5 = properties[i];
                        }
                        else if (i == 21)
                        {
                            m.GauntletId = int.Parse(properties[i]);
                        }
                        else if (i == 22)
                        {
                            //NOTE: Deck code currently isn't the correct deck code the player used in game
                            //More info in the ReadMe.md
                            m.DeckCode = properties[i];
                        }
                        else
                        {
                            throw new NotImplementedException("Unknown property");
                        }
                    }

                    //Only add one instance of the game Id to the list.
                    //Currently an issue with duplicate games in the history (Check ReadMe.md)
                    if (!matches.Exists(x => x.MatchId == m.MatchId))
                    {
                        matches.Add(m);
                    }
                }

                return matches;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to decode pasted content - '{e.ToString()}'");
                return null;
            }
        }

        private static string RemoveFormatting(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"\t|\n|\r", "");
        }
    }
}
