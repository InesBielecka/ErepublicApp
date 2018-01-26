using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
    public class Countries
    {

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("CountryName.json"))
            {
                string json = r.ReadToEnd();
                List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(json);
            }

        }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
