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
        int _battleId;
        HttpResponseMessage _response;
        HttpClient _client;
        private Campaigns _campaings;
        private System.Timers.Timer _timer;
        List<int> _currentBattles;

        public MainWindow()
        {
            InitializeComponent();
            _currentBattles = new List<int>(_battleId);
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
            _response = _client.GetAsync("").Result;

            if (_response.IsSuccessStatusCode)
            {
                var campaignsString = _response.Content.ReadAsStringAsync().Result; // ReadAsA sync<IEnumerable<Employee>>().Result;

                var objects = JObject.Parse(campaignsString); // parse as array  
                _campaings = objects.ToObject<Campaigns>();

                _battleId = Int32.Parse(txtbx_battle_id.Text);
                _currentBattles.Add(_battleId);
                var chosenBattle = _campaings.Battles.Values.Where(x => x.Id == _battleId);
                var battlesList = _campaings.Battles.Values.Where(x => _currentBattles.Contains(x.Id)).ToList();

                tabControlBattles.ItemsSource = _currentBattles;

                var tabItemNew = new TabItem { Header = _battleId.ToString()};
                var tabItemNewIndex = tabControlBattles.Items.Add(tabItemNew);
                tabItemNew.Focus();
            }
            else
            {
                MessageBox.Show("Error Code" + _response.StatusCode + " : Message - " + _response.ReasonPhrase);
            }
        }

        private void FillingData(IEnumerable<Battle> chosenBattle)
        {
            foreach (var batl in chosenBattle)
            {
                lbl_type_show.Content = batl.Type.ToString();
                lbl_region_show.Content = batl.Region.Name.ToString();
                int countryinvid = batl.Invader.Id;
                lbl_invader_country_name.Content = Constants.countriesDic[countryinvid];
                int countrydefid = batl.Defender.Id;
                lbl_defender_country_name.Content = Constants.countriesDic[countrydefid];

                if (batl.Type.ToString() == "tanks")
                {
                    EnableTabs(true);

                    SetWall(batl, "1", lbl_wall_for_div1, lbl_wall_dom_div1, progressbardiv1);
                    SetWall(batl, "2", lbl_wall_for_div2, lbl_wall_dom_div2, progressbardiv2);
                    SetWall(batl, "4", lbl_wall_for_div3, lbl_wall_dom_div3, progressbardiv3);
                    SetWall(batl, "3", lbl_wall_for_div4, lbl_wall_dom_div4, progressbardiv4);
                }
                else
                {
                    EnableTabs(false);
                    SetWall(batl, "11", lbl_wall_for_air, lbl_wall_dom_air, progressbarair);
                }
            }
        }

        private void EnableTabs(bool IsTankBattle)
        {
            tab_air.IsEnabled = !IsTankBattle;
            tab_div1.IsEnabled = IsTankBattle;
            tab_div2.IsEnabled = IsTankBattle;
            tab_div3.IsEnabled = IsTankBattle;
            tab_div4.IsEnabled = IsTankBattle;
            if (IsTankBattle)
            {
                tab_div4.Focus();
            }
            else
            {
                tab_air.Focus();
            }
        }

        private void SetWall(Battle batl, string divNumber, Label lbl_wall_for, Label lbl_wall_dom, ProgressBar progressbardiv)
        {
            int countryWallForId = batl.Div[divNumber].Wall.For;
            lbl_wall_for.Content = Constants.countriesDic[countryWallForId];
            lbl_wall_dom.Content = batl.Div[divNumber].Wall.Dom.ToString();
            progressbardiv.Value = Double.Parse(batl.Div[divNumber].Wall.Dom.ToString());
        }

        private void tabControlBattles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int k = tabControlBattles.SelectedIndex;

            TabItem selectedTab = e.AddedItems[0] as TabItem;

            if (int.TryParse(selectedTab.Header.ToString(), out _battleId))
            {
                var chosenBattle = _campaings.Battles.Values.Where(x => x.Id == _battleId);
                //FillingData(chosenBattle);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://www.erepublik.com/en/military/campaigns-new");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _timer = new System.Timers.Timer();
            _timer.Interval = 5000;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _response = _client.GetAsync("").Result;
        }
    }
}
