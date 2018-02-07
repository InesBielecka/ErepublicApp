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
using System.Web.Script.Serialization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ErepublicApp.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ErepublicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }


        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtbx_battle_id_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void txtbx_battle_id_GotFocus(object sender, RoutedEventArgs e)
        {
            txtbx_battle_id.Clear();
        }

        private void NumberValidationTxtbx_battle_id(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        

        private void btn_show_battle_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.erepublik.com/en/military/campaigns-new");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;

            if (response.IsSuccessStatusCode)
            {
                var campaignsString = response.Content.ReadAsStringAsync().Result; // ReadAsA sync<IEnumerable<Employee>>().Result;

                var objects = JObject.Parse(campaignsString); // parse as array  
                var campaings = objects.ToObject<Campaigns>();

                int battleId = Int32.Parse(txtbx_battle_id.Text);
                var chosenBattle = campaings.Battles.Values.Where(x => x.Id == battleId);
                

                foreach (var batl in chosenBattle)
                {
                    lbl_type_show.Content = batl.Type.ToString();
                    lbl_region_show.Content = batl.Region.Name.ToString();
                    int countryInvId = batl.Invader.Id;
                    lbl_invader_country_name.Content = Constants.countriesDic[countryInvId];
                    int countryDefId = batl.Defender.Id;
                    lbl_defender_country_name.Content = Constants.countriesDic[countryDefId];

                    if (batl.Type.ToString() == "tanks")
                    {
                        int countryWallForIdDiv1 = batl.Div["1"].Wall.For;
                        lbl_wall_for_div1.Content = Constants.countriesDic[countryWallForIdDiv1];
                        lbl_wall_dom_div1.Content = batl.Div["1"].Wall.Dom.ToString();

                        int countryWallForIdDiv2 = batl.Div["2"].Wall.For;
                        lbl_wall_for_div2.Content = Constants.countriesDic[countryWallForIdDiv2];
                        lbl_wall_dom_div2.Content = batl.Div["2"].Wall.Dom.ToString();

                        int countryWallForIdDiv3 = batl.Div["3"].Wall.For;
                        lbl_wall_for_div3.Content = Constants.countriesDic[countryWallForIdDiv3];
                        lbl_wall_dom_div3.Content = batl.Div["3"].Wall.Dom.ToString();

                        int countryWallForIdDiv4 = batl.Div["4"].Wall.For;
                        lbl_wall_for_div4.Content = Constants.countriesDic[countryWallForIdDiv4];
                        lbl_wall_dom_div4.Content = batl.Div["4"].Wall.Dom.ToString();
                    }
                    else
                    {
                        int countrWallForIdAir = batl.Div["11"].Wall.For;
                        lbl_wall_for_air.Content = Constants.countriesDic[countrWallForIdAir];
                        lbl_wall_dom_air.Content = batl.Div["11"].Wall.Dom.ToString();
                    }
                }



                //var polskieWalki = campaings.Battles.Values.Where(x => x.Invader.Id == 35 || x.Defender.Id == 35);
                //var liczbaPolskichWalk = polskieWalki.Count();
                //var typWalki = polskieWalki.Where(x => x.Type == Constants.Tanks).Count();

                //foreach(var walka in polskieWalki)
                //{
                //   foreach(var dywizja in walka.Div)
                //    {
                //        textBlock.Text += dywizja.Value.Wall.For + ": " + dywizja.Value.Wall.Dom.ToString() + Environment.NewLine;
                //    }
                //}

                //foreach (var bitwa in campaings.Battles.Reverse())
                //{
                //    tb1_battleId.Text = bitwa.Value.Id.ToString();
                //    textBox.Text = typWalki.ToString();
                //}
                //var battlesModel = battles.ToObject<List<Battle>>();

                //textBox.Text = battlesModel.First().War_id.ToString();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }
    }
}
