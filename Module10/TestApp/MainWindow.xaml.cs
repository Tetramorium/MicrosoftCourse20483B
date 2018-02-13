using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using TestApp.Model;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskController tc;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            tc = new TaskController();
        }

        private void bt_Test_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                string currentTime = DateTime.Now.ToLongTimeString();
                lbl_Time.Dispatcher.Invoke(new Action(() => SetTime(currentTime)));
            });
        }

        private void SetTime(string _Time)
        {
            this.lbl_Time.Content = _Time;
        }

        private async void bt_LongTask_Click(object sender, RoutedEventArgs e)
        {
            this.lbl_LongTask.Content = "Commencing Task";

            Task<string> task1 = Task.Run<string>(() =>
           {
               Thread.Sleep(2500);
               return "Done!";
           });

            lbl_LongTask.Content = await task1;
        }

        private async Task<string> GetDate()
        {
            return await Task.Run<string>(() =>
           {
               Thread.Sleep(2500);
               return DateTime.Now.ToLongTimeString();
           });
        }

        private async void bt_GetData_Click(object sender, RoutedEventArgs e)
        {
            lbl_GetData.Content = await GetDate();
        }

        private async void bt_GetHttp_Click(object sender, RoutedEventArgs e)
        {
            this.dg_Data.ItemsSource = await GetData();
        }

        private async Task<List<Country>> GetData()
        {
            string url = "https://restcountries.eu/rest/v2/all";

            this.lbl_Result.Content = "Getting Data";

            using (HttpClient httpClient = new HttpClient())
            {
                // Get all countries remotely and deserialize them into a list
                try
                {
                    var message = await httpClient.GetAsync(url);
                    Task<string> content = message.Content.ReadAsStringAsync();

                    this.lbl_Result.Content = "Finished";

                    return JsonConvert.DeserializeObject<List<Country>>(content.Result);
                }
                catch (WebException ex)
                {
                    this.lbl_Result.Content = ex.Message;
                }
                catch (Exception ex)
                {
                    this.lbl_Result.Content = ex.Message;
                }
            }

            return new List<Country>();
        }
    }
}
