using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
    class Battle
    {
        public int Id { get; set; }
        public int War_id { get; set; }
        public int Zone_id { get; set; }
        public bool Is_rw { get; set; }
        public bool Is_as { get; set; }
        public string Type { get; set; }
        public int Start { get; set; }
        public int Det { get; set; }
        public Region Region { get; set; }
        public bool Is_dict { get; set; }
        public bool Is_lib { get; set; }
        [JsonProperty("inv")]
        public BattleSite Invader { get; set; }
        [JsonProperty("def")]
        public BattleSite Defender { get; set; }
        public Dictionary<string, Division> Div { get; set; }
        public DominationPoints Dom_pts { get; set; }
        public Dictionary<int,string> BattleDictionary { get; set; }

        public Battle()
        {
            BattleDictionary = Constants.countriesDic;
        }

    }
}
