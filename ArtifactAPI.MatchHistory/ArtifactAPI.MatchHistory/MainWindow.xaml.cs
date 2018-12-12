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
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnViewLoaded;
            outputBox.TextChanged += OnOutputPasted;
        }

        private void OnViewLoaded(object sender, RoutedEventArgs e)
        {
            string fileText = File.ReadAllText("getMatchData.js");
            javascriptCopy.Text = fileText;

            c_getHistory.Visibility = Visibility.Visible;
            c_matchHistory.Visibility = Visibility.Collapsed;
        }

        private void OnRequestOpenUri(object sender, RequestNavigateEventArgs e)
        {
            //Open the Url
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void OnOutputPasted(object sender, TextChangedEventArgs e)
        {
            TextBox box = e.Source as TextBox;
            string pasted = box.Text;

            List<Match> matches = MatchDecoder.DecodeMatch(pasted);
            PopulateGameHistory(matches);
        }

        void PopulateGameHistory(List<Match> allMatches)
        {
            if (allMatches == null)
            {
                throw new ArgumentNullException(nameof(allMatches));
            }

            if (allMatches == null || (allMatches != null && allMatches.Count <= 0))
                return;

            //Set UI visibility
            c_getHistory.Visibility = Visibility.Collapsed;
            c_matchHistory.Visibility = Visibility.Visible;

            //Set total stats
            tb_totalMatches.Text = $"{allMatches.Count} matches";
            ic_gameHistory.ItemsSource = allMatches;
        }
    }
}
