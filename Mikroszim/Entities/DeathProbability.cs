using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikroszim.Entities
{
    public class DeathProbability
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public double DProbability { get; set; }
    }
}
