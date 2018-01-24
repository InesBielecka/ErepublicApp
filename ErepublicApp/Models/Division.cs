using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
  public class Division
    {
        public int Div { get; set; }
        public bool? End { get; set; }
        public int Epic { get; set; }
        public int Epic_type { get; set; }
        public KeyValuePair<string, CombatOrder[]> Co { get; set; }
    }
}
