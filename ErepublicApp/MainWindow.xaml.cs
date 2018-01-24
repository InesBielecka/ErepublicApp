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


        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.erepublik.com/en/military/campaigns-new");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var campaignsString = response.Content.ReadAsStringAsync().Result; // ReadAsA sync<IEnumerable<Employee>>().Result;

                var objects = JObject.Parse(campaignsString); // parse as array  
                var campaings = objects.ToObject<Campaigns>();
                //var battles = objects["battles"];

                
                foreach(var bitwa in campaings.Battles)
                {

                    tb1_battleId.Text = bitwa.Value.Id.ToString();
                }
                //var battlesModel = battles.ToObject<List<Battle>>();

                //textBox.Text = battlesModel.First().War_id.ToString();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tb1_battleId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
