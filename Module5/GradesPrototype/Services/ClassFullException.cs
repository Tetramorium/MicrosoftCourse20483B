using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GradesPrototype.Services
{
    [Serializable]
    class ClassFullException : Exception
    {
        // TODO: Exercise 3: Task 1a: Add custom data: the name of the class that is full
        // Code that catches this exception can query the public ClassName property to determine which class caused the exception

        private string className;

        public string ClassName
        {
            get { return className; }
        }


        // TODO: Exercise 3: Task 1b: Delegate functionality for the common constructors directly to the Exception class

        public ClassFullException(string message) : base(message)
        {

        }

        // TODO: Exercise 3: Task 1c: Add custom constructors that populate the _className field.
        // The code that invokes this exception is expected to provide the class name

        public ClassFullException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ClassFullException(string message, string _className)
    : base(message)
        {
            className = _className;
        }

        public ClassFullException(string message, string _className, Exception inner)
                : base(message, inner)
        {
            className = _className;
        }



        #region Code provided to handle deserialization of a custom exception
        // Constructor for deserializing a ClassFullException object
        // The _className field contains custom data, so it must be handled explicitly
        // The details are outside the scope of this lab
        protected ClassFullException(SerializationInfo info, StreamingContext context)
                    : base(info, context)
        {
            // Populate the _className member from the deserialization stream 
            className = info.GetString("ClassName");
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentException("info");
            }
            base.GetObjectData(info, context);
            info.AddValue("ClassName", className, typeof(string));
        }
        #endregion
    }
}
