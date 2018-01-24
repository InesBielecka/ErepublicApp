using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
    public class BattleSite
    {
        public int Id { get; set; }
        public int[] Allies { get; set; }
        public AllyList[] Ally_list { get; set; }
        public int Points { get; set; }
    }
}
