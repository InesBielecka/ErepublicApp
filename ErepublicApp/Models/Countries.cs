using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public List<Country> Countrie { get; set; }
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("c:\\users\\ines\\documents\\visual studio 2015\\Projects\\ErepublicApp\\ErepublicApp\\CountryName.json"))
            {
                string json = r.ReadToEnd();
                Countrie = JsonConvert.DeserializeObject<List<Country>>(json);
            }

        }
        public List<Country> GetAll()
        {
            return Countrie;
        }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
