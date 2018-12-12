using ArtifactAPI.MatchHistory.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ArtifactClient m_client;

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
            if (allMatches == null)
                return;

            if (allMatches == null || (allMatches != null && allMatches.Count <= 0))
                return;

            SetView(false);

            //Set total stats
            tb_totalMatches.Text = $"{allMatches.Count} matches";
            ic_gameHistory.ItemsSource = allMatches;
        }
    }
}
