using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void OnRequestOpenUri(object sender, RequestNavigateEventArgs e)
        {
            //Open the Url
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void OnOutputPasted(object sender, TextChangedEventArgs e)
        {
            string pastedString = e.Source as String;

            object result = StringDecoder.DecodeString(pastedString);
            if (result == null)
                return;

        }
    }
}
