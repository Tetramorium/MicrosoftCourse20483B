using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    public class Grade
    {
        public int StudentID { get; set; }
        public string AssessmentDate { get; set; }
        public string SubjectName { get; set; }
        public string Assessment { get; set; }
        public string Comments { get; set; }
    }

    public partial class Test
    {
        public int MyProperty { get; set; }
    }

    public partial class Test
    {
        public static void test()
        {
            //this.MyProperty++;

            var t = DataSource.Students
                .Select(e => new { e.StudentID })
                .Where(e => e.StudentID == 1);

            var b = DataSource.Students
                .Where(e => e.StudentID == 1)
                .Select(e => new { e.StudentID });

            var c = from Student in DataSource.Students
                    where Student.StudentID == 1
                    select new { Student.StudentID };

            var lambda = DataSource.Students.Join(DataSource.Grades,              // outer sequence
                            Grade => Grade.StudentID,  // inner sequence key
                            Student => Student.StudentID,  // outer sequence key
                            (Grade, Student) =>
                                new { Name = Grade.FirstName, Mark = Student.Assessment });

            // using query expression
            var query = from student in DataSource.Students
                        join grade in DataSource.Grades on student.StudentID equals grade.StudentID
                        select new { Name = student.FirstName, Grade = grade.Assessment };
        }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
        }

        public Student()
        {
            this.myVar = 1;
        }

    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public Teacher()
        {
            Student student = new Student();
            student.MyProperty = 1;
        }
    }
}
