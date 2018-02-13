using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public class Park
    {
        public List<Animal> Animals { get; set; }
        public Park()
        {
            this.Animals = new List<Animal>();

            PopulatePark();
        }

        private void PopulatePark()
        {
            this.Animals.Add(new Dog("Bob", 12));
            this.Animals.Add(new Dog("Dave", 1));
            this.Animals.Add(new Cat("Felix", 2));
            this.Animals.Add(new Cat("Bram", 6));
        }
    }
}
