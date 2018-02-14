using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErepublicApp.Models
{
  public class Wall
    {
        public int For { get; set; }
        public double Dom { get; set; }
        public Dictionary<string, string> DivisionDictionary { get; set; }
        public string DictDiv { get { return DivisionDictionary[For.ToString()]; } }

        public Wall()
        {
            DivisionDictionary = Constants.countriesDic;
        }
    }
}
