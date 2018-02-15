using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfTest.CustomAttribute;

namespace WpfTest.Controller
{
    [AssemblyInfo("AssemblyController")]
    public class AssemblyController
    {
        private string url;

        public Assembly Library { get; set; }

        public AssemblyController(string _url)
        {
            this.url = _url;

            Assembly assembly = Assembly.LoadFrom(url);

            Console.WriteLine(assembly.FullName);

            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine("FullName:");
                Console.WriteLine(type.FullName);

                foreach (var constructor in type.GetConstructors())
                {
                    Console.WriteLine("Constructor:");
                    Console.WriteLine(constructor);
                }
            }

            var Type = assembly.GetType("ClassLibrary.Cat");
            var Constructor = Type.GetConstructor(new Type[0]);
            var initializedObject = Constructor.Invoke(new object[0]);
            var MethodToExecute = Type.GetMethod("MakeNoise");
            var response = MethodToExecute.Invoke(initializedObject, new object[0]);

            Console.WriteLine(response);
        }
    }
}
