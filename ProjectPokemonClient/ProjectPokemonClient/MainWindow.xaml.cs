 using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;
using WebSocketSharp;
using ProjectPokemonClient.Models;

namespace ProjectPokemonClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string test = "";

        static Pokemon _jsonPokemon = new Pokemon();//il pokemon

        public MainWindow()
        {
            
            InitializeComponent();
            Connesso2.Text = "Non Connesso";
            Connesso2.Background = System.Windows.Media.Brushes.Red;
        }

        private void TextBoxConnesso(object sender, TextChangedEventArgs e)
        {

        }

        private void Connettiti_clicked(object sender, RoutedEventArgs e)
        {
            Connessione();
            Connesso2.Text = "Connesso";
            Connesso2.Background = System.Windows.Media.Brushes.Green;
        }

        static void Connessione()
        {
            //create an instance of a WebSocket client that will be properly disposed
            using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Connessione"))//connessione
            {
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
            };
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            test = e.Data;
            throw new NotImplementedException();
        }

        private void btnAttack1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAttack2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAttack3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAttack4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReady_Click(object sender, RoutedEventArgs e)
        {
            //using (WebSocket ws = new WebSocket($"https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/pokedex.php?pokemon=" + TextBoxNomePokemon.Text.ToLower()))
            //{

            //}

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/pokedex.php?pokemon=" + TextBoxNomePokemon.Text.ToLower());

                var getResult = client.GetAsync(endpoint).Result;

                var getResultJson = getResult.Content.ReadAsStringAsync().Result;

                Pokemon jsonObjectInfo = JsonConvert.DeserializeObject<Pokemon>(getResultJson);

                _jsonPokemon = jsonObjectInfo;

                //Caricamento delle immagini
                BitmapImage bitmap = new BitmapImage();
                BitmapImage bitmap2 = new BitmapImage();
                BitmapImage bitmap3 = new BitmapImage();

                bitmap.BeginInit();
                bitmap.UriSource = new Uri($"https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/" + _jsonPokemon.images.photo);
                bitmap.EndInit();
                imgPokemonP1.Source = bitmap;

                bitmap2.BeginInit();
                bitmap2.UriSource = new Uri($"https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/" + _jsonPokemon.images.typeIcon);
                bitmap2.EndInit();
                ImgTypeP1.Source = bitmap2;

                bitmap3.BeginInit();
                bitmap3.UriSource = new Uri($"https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/" + _jsonPokemon.images.weaknessIcon);
                bitmap3.EndInit();
                ImgWeaknessP1.Source = bitmap3;

                if (_jsonPokemon.moves.Count() == 1)
                {
                    btnAttack1.Content = _jsonPokemon.moves[0].name;
                    btnAttack2.IsEnabled = false;
                    btnAttack3.IsEnabled = false;
                    btnAttack4.IsEnabled = false;
                }

                if (_jsonPokemon.moves.Count() == 2)
                {
                    btnAttack1.Content = _jsonPokemon.moves[0].name;
                    btnAttack2.Content = _jsonPokemon.moves[1].name;
                    btnAttack3.IsEnabled = false;
                    btnAttack4.IsEnabled = false;
                }
                if (_jsonPokemon.moves.Count() == 3)
                {
                    btnAttack1.Content = _jsonPokemon.moves[0].name;
                    btnAttack2.Content = _jsonPokemon.moves[1].name;
                    btnAttack3.Content = _jsonPokemon.moves[2].name;
                    btnAttack4.IsEnabled = false;
                }

                if (_jsonPokemon.moves.Count() == 4)
                {
                    btnAttack1.Content = _jsonPokemon.moves[0].name;
                    btnAttack2.Content = _jsonPokemon.moves[1].name;
                    btnAttack3.Content = _jsonPokemon.moves[2].name;
                    btnAttack4.Content = _jsonPokemon.moves[3].name;
                }

                HpP1.Content = _jsonPokemon.hp;

            }
        }

        private void Connesso2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
