using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Controller
{
    public class TaskController
    {
        private Park park;

        public TaskController()
        {
            // No parameter or return value
            Task task1_A = new Task(new Action(GetTheTime));
            // Can have parameters and return values
            Task<int> task1_B = new Task<int>(() => GetTheTime("task1_B"));

            Task task2_A = new Task(delegate { Console.WriteLine("Hello {0}, The time is now {1}", "task2_A", DateTime.Now); });
            Task task2_B = new Task(() => Console.WriteLine("Hello {0}, The time is now {1}", "task2_B", DateTime.Now));

            task1_A.Start();
            task1_B.Start();

            Console.WriteLine(task1_B.Result);

            park = new Park();
            // Using a Parallel.ForEach loop 10-9
            Parallel.ForEach(park.Animals, animal => animal.MakeNoise());

            //Using PLINQ 10-9
            List<Animal> OlderThan5Animals = park.Animals.AsParallel().Where(e => e.Age > 5).ToList();
            Parallel.ForEach(OlderThan5Animals, animal => animal.MakeNoise());
        }

        private void GetTheTime()
        {
            Console.WriteLine("Hello {0}, The time is now {1}", "task1_A", DateTime.Now);
        }

        private int GetTheTime(string _Name)
        {
            Console.WriteLine("Hello {0}, The time is now {1}", _Name, DateTime.Now);

            return 1;
        }
    }
}
