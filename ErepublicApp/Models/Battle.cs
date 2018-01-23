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
        public Division Div { get; set; }

    }
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BattleSite
    {
        public int Id { get; set; }
        public int[] Allies { get; set; }
        public AllyList[] Ally_list { get; set; }
        public int Points { get; set; }
    }

    public class AllyList
    {
        public int Id { get; set; }
        public bool Deployed { get; set; }
    }

    public class Division
    {
        [JsonProperty("1")]
        public DivisionNumber First { get; set; }
        [JsonProperty("2")]
        public DivisionNumber Second { get; set; }
        [JsonProperty("3")]
        public DivisionNumber Third { get; set; }
        [JsonProperty("4")]
        public DivisionNumber Fourth { get; set; }
    }

    public class DivisionNumber
    {
        public int Div { get; set; }
        public bool? End { get; set; }
        public int Epic { get; set; }
        public int Epic_type { get; set; }

    }
}
