using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    public abstract class User
    {
        public string UserName { get; set; }

        protected string password = Guid.NewGuid().ToString(); // Generate a random password by default
        public string Password
        {
            set
            {
                if (!SetPassword(value))
                    throw new ArgumentException("SetPassword", "Password is not valid");
            }
        }

        public bool VerifyPassword(string pass)
        {
            return (String.Compare(pass, password) == 0);
        }

        public abstract bool SetPassword(string _password);
    }
}
