using carfactory.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carfactory.Entities
{
    class CarFactory: IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
