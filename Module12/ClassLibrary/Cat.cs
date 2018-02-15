using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Cat
    {
        public string Name { get; set; }

        public Cat()
        {
            this.Name = "Minoes";
        }

        public Cat(string _Name)
        {
            this.Name = _Name;
        }

        public string MakeNoise()
        {
            return "Meow!";
        }
    }
}
