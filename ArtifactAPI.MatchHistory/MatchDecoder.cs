using ArtifactAPI.MatchHistory.Dtos;
using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ArtifactAPI.MatchHistory
{
    public class MatchDecoder
    {
        const char PROPERTY_SEPARATOR = '|';
        const char MATCH_SEPARATOR = ',';

        public static List<Match> ParseStringToMatches(string s, ArtifactClient client)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            s = RemoveFormatting(s);

            List<Match> matches = new List<Match>();
            string[] matchArr = s.Split(MATCH_SEPARATOR);
            foreach (string match in matchArr)
            {
                try
                {
                    if (string.IsNullOrEmpty(match))
                        continue;

                    string[] properties = match.Split(PROPERTY_SEPARATOR);
                    Match m = new Match(client);
                    int lastMatchOutcome = -1;
                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (string.IsNullOrEmpty(properties[i]))
                        {
                            Logger.OutputError($"Empty property at value {i}");
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
                            m.MatchMode = (MatchMode)IntToEnum<MatchMode>(int.Parse(properties[i]), MatchMode.Matchmaking);
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
                            ///Store winning team outcome to set later
                            lastMatchOutcome = int.Parse(properties[i]);
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
                            m.Team = (Teams)IntToEnum<Teams>(int.Parse(properties[i]), Teams.Radiant);
                        }
                        else if (i == 10)
                        {
                            m.Flags = (Flags)IntToEnum<Flags>(int.Parse(properties[i]), Flags.None);
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
                            m.GauntletType = (GauntletType)IntToEnum<GauntletType>(int.Parse(properties[i]), GauntletType.None);
                        }
                        else if (i == 22)
                        {
                            //NOTE: Deck code currently isn't the correct deck code the player used in game
                            //More info in the ReadMe.md
                            m.DeckCode = properties[i];
                        }
                        else
                        {
                            throw new NotImplementedException($"Unknown property - '{properties[i]}'");
                        }
                    }

                    ///Set win outcome by seeing if the player's team matches the outcome team
                    object value = IntToEnum<Teams>(lastMatchOutcome, -1);
                    if(value is int)
                    {
                        ///The value doesn't exist in Teams - Meaning outcome isn't from a Team winning
                        switch (lastMatchOutcome)
                        {
                            case 8:
                                m.Flags = Flags.Abandoned;
                                break;
                            default:
                                Logger.OutputError($"Unknown Match outcome '{lastMatchOutcome}'");
                                break;
                        }
                    }
                    else
                    {
                        ///If Outcome is Radiant or Dire, determine win or loss
                        m.MatchOutcome = (Teams)lastMatchOutcome == m.Team ? Outcome.Victory : Outcome.Loss;
                    }
                    

                    ///Only add one instance of the game Id to the list.
                    ///Currently an issue with duplicate games in the history (Check ReadMe.md)
                    if (!matches.Exists(x => x.MatchId == m.MatchId))
                    {
                        matches.Add(m);
                    }
                }
                catch (Exception e)
                {
                    Logger.OutputError($"Unable to decode pasted content - '{e.ToString()}'");
                    return null;
                }
            }

            return matches;
        }

        /// <summary>
        /// Removes all formatting from a string
        /// </summary>
        /// <param name="s">The string to remove formatting from</param>
        /// <returns></returns>
        private static string RemoveFormatting(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"\t", "");
        }

        /// <summary>
        /// Converts an integer to the int index of an Enum. Will return the default value if it's not valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number"></param>
        /// <param name="defaultResult">The result to return if the int does not exist</param>
        /// <returns></returns>
        private static object IntToEnum<T>(int number, object defaultResult) where T : struct, IConvertible
        {
            bool isDefined = Enum.IsDefined(typeof(T), number);
            if (isDefined)
            {
                var list = GetEnumList<T>();
                T result = (T)Enum.Parse(typeof(T), list.Where(x => x.Value == number).FirstOrDefault().Key);
                return result;
            }
            else
            {
                Logger.OutputError($"Undefined MatchOutcome of value '{number}'");
                return defaultResult;
            }
        }

        public static List<KeyValuePair<string, int>> GetEnumList<T>()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }
    }
}
