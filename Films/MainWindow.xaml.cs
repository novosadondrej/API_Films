using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace Films
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            Helper.Initialize();
        }

        private async void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            var loadInfo = await Load();

            Vysledek.Text += loadInfo.Title + " ";
            Vysledek.Text += loadInfo.Runtime + " ";
            Vysledek.Text += loadInfo.Genre + " ";
        
        }

        private async void Button_Click_MoreInfo(object sender, RoutedEventArgs e)
        {
            

            var loadInfo = await Load();

            MoreInfo moreInfo = new MoreInfo();
            moreInfo.Show();

            moreInfo.Title.Text = loadInfo.Title;
            moreInfo.Year.Text = loadInfo.Year;
            moreInfo.Actors.Text = loadInfo.Actors;
            moreInfo.Director.Text = loadInfo.Director;
            moreInfo.Writer.Text = loadInfo.Writer;
            moreInfo.Genre.Text = loadInfo.Genre;
            moreInfo.Plot.Text = loadInfo.Plot;
            moreInfo.Rating.Text = loadInfo.ImdbRating;
            moreInfo.Runtime.Text = loadInfo.Runtime;

            Vyhledat.Clear();
            Vysledek.Clear();
        }

        private void Button_Click_Internet(object sender, RoutedEventArgs e)
        {
            if (IsNetworkAvailable() == true)
            {
                MessageBox.Show("Internet funguje :)");
            }
            else
                MessageBox.Show("Internet nefunguje :(");
        }

        public async Task<Model> Load()
        {
            string url = "";
            string search = Vyhledat.Text;
            string apikey = "15f15101";

            url = $"http://www.omdbapi.com/?t=" + search + "&apikey=" + apikey;

            using (HttpResponseMessage response = await Helper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Model data = await response.Content.ReadAsAsync<Model>();

                    return data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static bool IsNetworkAvailable()
        {
            try
            {

                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/"))
                    return true;
                

            }
            catch
            {
                return false;
            }
            return false;
        }

      
    }
}
