﻿using ArtifactAPI.MatchHistory.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ArtifactAPI.MatchHistory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArtifactClient m_client = null;
        private List<Match> m_matches = null;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnViewLoaded;
            outputBox.TextChanged += OnOutputPasted;
            tb_javascriptCopy.GotFocus += OnHaveGotFocus;

            t_invalidCode.Visibility = Visibility.Collapsed;

            m_client = new ArtifactClient();
            //Load since bug with not loading cards
            m_client.GetAllCards();
        }

        private void OnHaveGotFocus(object sender, RoutedEventArgs e)
        {
            tb_javascriptCopy.SelectAll();
        }

        private void OnViewLoaded(object sender, RoutedEventArgs e)
        {
            string fileText = File.ReadAllText("getMatchData.js");
            tb_javascriptCopy.Text = fileText;

            SetView(true);
        }

        private void OnRequestOpenUri(object sender, RequestNavigateEventArgs e)
        {
            //Open the Url
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void OnResetView(object sender, RoutedEventArgs e)
        {
            SetView(true);
            t_invalidCode.Visibility = Visibility.Collapsed;
            ic_gameHistory.ItemsSource = null;
            outputBox.Text = null;
        }

        private void SetView(bool isStartView)
        {
            if (isStartView)
            {
                c_getHistory.Visibility = Visibility.Visible;
                c_matchHistory.Visibility = Visibility.Collapsed;
            }
            else
            {
                c_getHistory.Visibility = Visibility.Collapsed;
                c_matchHistory.Visibility = Visibility.Visible;
            }
        }

        private void OnOutputPasted(object sender, TextChangedEventArgs e)
        {
            TextBox box = e.Source as TextBox;
            string pasted = box.Text;

            List<Match> matches = MatchDecoder.DecodeMatch(pasted, m_client);
            if(matches == null)
            {
                t_invalidCode.Visibility = Visibility.Visible;
            }
            else
            {
                t_invalidCode.Visibility = Visibility.Collapsed;
                PopulateGameHistory(matches);
            }
        }

        private void PopulateGameHistory(List<Match> allMatches)
        {
            m_matches = allMatches;

            if (allMatches == null)
                return;

            if (allMatches == null || (allMatches != null && allMatches.Count <= 0))
                return;

            SetView(false);

            //Set total stats
            tb_totalMatches.Text = $"{allMatches.Count} matches";

            int totalWins = allMatches.Sum(x => x.MatchOutcome == Enums.Outcome.Victory ? 1 : 0);
            int totalLoss = allMatches.Sum(x => (x.MatchOutcome == Enums.Outcome.Loss) ? 1 : 0);
            int totalDraw = allMatches.Sum(x => x.MatchOutcome == Enums.Outcome.Draw ? 1 : 0);
            tb_totalWinLoss.Text = $"{totalWins}/{totalDraw}/{totalLoss}";

            
            SetRate(Enums.MatchMode.Bot_Match, allMatches, tb_bmwr);

            //Featured tab Gauntlet mode
            SetRate(Enums.GauntletType.RandomMeta, allMatches, tb_rmwr);
            
            //Casual modes
            SetRate(Enums.GauntletType.Constructed, allMatches, tb_ccwr);
            SetRate(Enums.MatchMode.Matchmaking, allMatches, tb_mmwr);
            
            //'Ranked' modes
            SetRate(Enums.GauntletType.ConstructedExpert, allMatches, tb_ecwr);
            SetRate(Enums.GauntletType.PhantomDraftExpert, allMatches, tb_pdwr);
            SetRate(Enums.GauntletType.KeeperDraftExpert, allMatches, tb_kdwr);

            ic_gameHistory.ItemsSource = allMatches;
        }

        private void SetRate(Enums.MatchMode mode, List<Match> allMatches, TextBlock tb)
        {
            MatchRate rate = GetWinRate(mode, allMatches);
            double mmPercent = GetPercent(rate.Wins, rate.Total);
            tb.Text = $"{mmPercent}% ({rate.Wins}/{rate.Draws}/{rate.Losses})";
        }

        private void SetRate(Enums.GauntletType gauntletType, List<Match> allMatches, TextBlock tb)
        {
            MatchRate rate = GetWinRate(gauntletType, allMatches);
            double mmPercent = GetPercent(rate.Wins, rate.Total);
            tb.Text = $"{mmPercent}% ({rate.Wins}/{rate.Draws}/{rate.Losses})";
        }

        private MatchRate GetWinRate(Enums.MatchMode mode, List<Match> allMatches)
        {
            int wonCount = allMatches.Sum(x => x.MatchMode == mode && x.MatchOutcome == Enums.Outcome.Victory ? 1 : 0);
            int drawCount = allMatches.Sum(x => x.MatchMode == mode && x.MatchOutcome == Enums.Outcome.Draw ? 1 : 0);
            int lossCount = allMatches.Sum(x => x.MatchMode == mode && x.MatchOutcome == Enums.Outcome.Loss ? 1 : 0);
            int total = allMatches.Sum(x => x.MatchMode == mode ? 1 : 0);

            return new MatchRate()
            {
                Wins = wonCount,
                Draws = drawCount,
                Losses = lossCount,
                Total = total,
            };
        }

        private MatchRate GetWinRate(Enums.GauntletType gauntedMode, List<Match> allMatches)
        {
            int wonCount = allMatches.Sum(x => x.GauntletType == gauntedMode && x.MatchOutcome == Enums.Outcome.Victory ? 1 : 0);
            int drawCount = allMatches.Sum(x => x.GauntletType == gauntedMode && x.MatchOutcome == Enums.Outcome.Draw ? 1 : 0);
            int lossCount = allMatches.Sum(x => x.GauntletType == gauntedMode && x.MatchOutcome == Enums.Outcome.Loss ? 1 : 0);
            int total = allMatches.Sum(x => x.GauntletType == gauntedMode ? 1 : 0);

            return new MatchRate()
            {
                Wins = wonCount,
                Draws = drawCount,
                Losses = lossCount,
                Total = total,
            };
        }

        private double GetPercent(double amount, double total, int roundAmount = 1)
        {
            double percent = (amount / total) * 100;
            if (double.IsNaN(percent))
                return 0;
            else
                return Math.Round(percent, roundAmount);
        }
    }
}
