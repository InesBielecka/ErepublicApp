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
    public class TabTemplate
    {
        public object Template { get; private set; }

        public TabTemplate(object content)
        {
            Template = content;
        }
    }


    public partial class MainWindow : Window
    {
        private TabTemplate template;
        public MainWindow()
        {

            InitializeComponent();
            template = new TabTemplate(tab_battle_id.Content);
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


                if(tabControlBattles.Items.Count > 1)
                {
                    template = new TabTemplate(tab_battle_id.Content);
                    var tabItemNew = new TabItem { Header = battleId.ToString(), Content = template.Template };
                    var tabItemNewIndex = tabControlBattles.Items.Add(tabItemNew);
                }
                else
                {
                    var tabItemNew = new TabItem { Header = battleId.ToString(), Content = template.Template };
                    var tabItemNewIndex = tabControlBattles.Items.Add(tabItemNew);
                }

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
                            EnableTabs(true);

                            SetWall(batl, "1", lbl_wall_for_div1, lbl_wall_dom_div1);
                            SetWall(batl, "2", lbl_wall_for_div2, lbl_wall_dom_div2);
                            SetWall(batl, "4", lbl_wall_for_div3, lbl_wall_dom_div3);
                            SetWall(batl, "3", lbl_wall_for_div4, lbl_wall_dom_div4);
                        }
                        else
                        {
                            EnableTabs(false);
                            SetWall(batl, "11", lbl_wall_for_air, lbl_wall_dom_air);
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

        private void EnableTabs(bool IsTankBattle)
        {
            tab_air.IsEnabled = !IsTankBattle;
            tab_div1.IsEnabled = IsTankBattle;
            tab_div2.IsEnabled = IsTankBattle;
            tab_div3.IsEnabled = IsTankBattle;
            tab_div4.IsEnabled = IsTankBattle;
            if(IsTankBattle)
            {
                tab_div4.Focus();
            }
            else
            {
                tab_air.Focus();
            }
        }

        private void SetWall(Battle batl, string divNumber, Label lbl_wall_for, Label lbl_wall_dom)
        {
            int countryWallForId = batl.Div[divNumber].Wall.For;
            lbl_wall_for.Content = Constants.countriesDic[countryWallForId];
            lbl_wall_dom.Content = batl.Div[divNumber].Wall.Dom.ToString();
        }
    }
}
