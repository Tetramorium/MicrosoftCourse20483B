using System;
using System.Collections.Generic;
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
using TestApp.Controller;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MP3PlayerController musicPlayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string file = @"Media/Music/Bretagne.mp3";

            musicPlayer = new MP3PlayerController(file);
        }

        private void bt_PlaySong_Click(object sender, RoutedEventArgs e)
        {
            if (!musicPlayer.IsPlaying)
            {
                musicPlayer.Play();
                bt_PlaySong.Content = "Pause Song";
            }
            else
            {
                musicPlayer.Stop();
                bt_PlaySong.Content = "Play Song";
            }
        }
    }
}
