using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> OldNames { get; set; }

        public Animal(string _Name, int _Age)
        {
            this.Name = _Name;
            this.Age = _Age;

            this.OldNames = new List<string>();
        }

        public abstract void MakeNoise();

        public void ChangeName(string _NewName)
        {
            lock (Name)
            {
                if (Name != _NewName && !OldNames.Contains(_NewName))
                {
                    this.OldNames.Add(Name);
                    this.Name = _NewName;
                }
            }
        }
    }

    public class Cat : Animal
    {
        public Cat(string _Name, int _Age) : base(_Name, _Age)
        {
        }

        public override void MakeNoise()
        {
            Console.WriteLine("{0} : Meow!", Name);
        }
    }
    public class Dog : Animal
    {
        public Dog(string _Name, int _Age) : base(_Name, _Age)
        {
        }

        public override void MakeNoise()
        {
            Console.WriteLine("{0} : Woef!", Name);
        }
    }
}
