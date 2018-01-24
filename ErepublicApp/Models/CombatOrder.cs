using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
   public class CombatOrder
    {
        public int Reward { get; set; }
        public double Budget { get; set; }
        public int Threshold { get; set; }
        public int Sub_country { get; set; }
        public int Sub_mu { get; set; }
        public int Co_id { get; set; }
        public int Group_id { get; set; }
    }
}
