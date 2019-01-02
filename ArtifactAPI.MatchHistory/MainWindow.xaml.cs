using ArtifactAPI.MatchHistory.Dtos;
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

            double totalMMWR = GetWinRate(Enums.MatchMode.Matchmaking, allMatches);
            tb_mmwr.Text = $"{Math.Round(totalMMWR, 1)}%";

            double totalBMWM = GetWinRate(Enums.MatchMode.Bot_Match, allMatches);
            tb_bmwr.Text = $"{Math.Round(totalBMWM, 1)}%";

            double totalCCWM = GetWinRate(Enums.GauntletType.ConstructedExpert, allMatches);
            tb_ccwr.Text = $"{Math.Round(totalCCWM, 1)}%";

            double totalCPDWR = GetWinRate(Enums.GauntletType.CasualPhantomDraft, allMatches);
            tb_cpdwr.Text = $"{Math.Round(totalCPDWR, 1)}%";

            double totalecWM = GetWinRate(Enums.GauntletType.ConstructedExpert, allMatches);
            tb_ecwr.Text = $"{Math.Round(totalecWM, 1)}%";

            double totalpdWM = GetWinRate(Enums.GauntletType.PhantomDraftExpert, allMatches);
            tb_pdwr.Text = $"{Math.Round(totalpdWM, 1)}%";

            double totalkdWM = GetWinRate(Enums.GauntletType.KeeperDraftExpert, allMatches);
            tb_kdwr.Text = $"{Math.Round(totalkdWM, 1)}%";

            double totalRMWM = GetWinRate(Enums.GauntletType.RandomMeta, allMatches);
            tb_rmwr.Text = $"{Math.Round(totalRMWM, 1)}%";

            ic_gameHistory.ItemsSource = allMatches;
        }

        private double GetWinRate(Enums.MatchMode mode, List<Match> allMatches)
        {
            int wonMatchesCount = allMatches.Sum(x => x.MatchMode == mode ? 1 : 0);
            int total = allMatches.Count;

            return ((double)wonMatchesCount / (double)total) * 100;
        }

        private double GetWinRate(Enums.GauntletType gauntedMode, List<Match> allMatches)
        {
            int wonMatchesCount = allMatches.Sum(x => x.GauntletType == gauntedMode ? 1 : 0);
            int total = allMatches.Count;

            return ((double)wonMatchesCount / (double)total) * 100;
        }
    }
}
