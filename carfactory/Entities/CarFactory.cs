using carfactory.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carfactory.Entities
{
    public class CarFactory : IToyFactory
    {
        public Abstraction.Toy CreateNew()
        {
            return new Car();
        }
    }
}
