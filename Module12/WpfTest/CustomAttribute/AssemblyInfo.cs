using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AssemblyInfo : Attribute
    {
        private string Name;

        public AssemblyInfo(string _Name)
        {
            this.Name = _Name;
        }

        public string GetName
        {
            get { return this.Name; }
        }
    }
}
