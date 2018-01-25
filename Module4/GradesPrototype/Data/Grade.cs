using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    // WPF Databinding requires properties

    // TODO: Exercise 1: Task 1a: Convert Grade into a class and define constructors
    public class Grade
    {
        public int StudentID { get; set; }

        private string assessmentDate;
        public string AssessmentDate
        {
            get
            {
                return assessmentDate;
            }

            set
            {
                DateTime assDate;

                if (DateTime.TryParse(value, out assDate))
                {
                    if (assDate > DateTime.Now)
                    {
                        throw new ArgumentOutOfRangeException("AssessmentDate", "Assessment date must be on or before the current date");
                    }

                    assessmentDate = assDate.ToString("d");
                }
                else
                {
                    throw new ArgumentException("AssessmentDate", "AssessmentDate is not valid");
                }
            }
        }
        private string subjectName;

        public string SubjectName
        {
            get
            {
                return subjectName;
            }
            set
            {
                if (DataSource.Subjects.Contains(value))
                    subjectName = value;
                else
                    throw new ArgumentException("Subject", "Subject not found in predefined list of subjects");
            }
        }

        private string assessment;
        public string Assessment
        {
            get
            {
                return assessment;
            }

            set
            {
                Match matchGrade = Regex.Match(value, @"^[A-E][+-]?$");
                if (matchGrade.Success)
                {
                    assessment = value;
                }
                else
                {
                    // If the grade is not valid then throw an ArgumentOutOfRangeException
                    throw new ArgumentOutOfRangeException("Assessment", "Assessment grade must be in the range A+ to E-"); ;
                }
            }
        }
        public string Comments { get; set; }

        public Grade(int _StudentID, string _AssessmentDate, string _SubjectName, string _Assessment, string _Comments)
        {
            this.StudentID = _StudentID;
            this.AssessmentDate = _AssessmentDate;
            this.SubjectName = _SubjectName;
            this.Assessment = _Assessment;
            this.Comments = _Comments;
        }

        public Grade()
        {
            this.StudentID = 0;
            this.AssessmentDate = DateTime.Now.ToString("d");
            this.SubjectName = "Math";
            this.Assessment = "A";
            this.Comments = "An empty string";
        }
    }

    // TODO: Exercise 1: Task 2a: Convert Student into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Student : IComparable<Student>
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }

        private string _Password = Guid.NewGuid().ToString(); // Generate a random password by default
        public string Password
        {
            set
            {
                _Password = value;
            }
        }

        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int _StudentID, string _UserName, string _Password, string _Firstname, string _Lastname, int _TeacherID)
        {
            this.StudentID = _StudentID;
            this.UserName = _UserName;
            this.Password = _Password;
            this.TeacherID = _TeacherID;
            this.FirstName = _Firstname;
            this.LastName = _Lastname;
        }

        public Student()
        {
            this.StudentID = 0;
            this.UserName = "an empty string";
            this.Password = "an empty string";
            this.TeacherID = 0;
            this.FirstName = "an empty string";
            this.LastName = "an empty string";
        }

        public bool VerifyPassword(string _password)
        {
            return (this._Password == _password);
        }

        int IComparable<Student>.CompareTo(Student other)
        {
            return string.Compare((this.FirstName + this.LastName), (other.FirstName + other.LastName));
        }

        public void AddGrade(Grade _grade)
        {
            if (_grade.StudentID == 0)
            {
                _grade.StudentID = this.StudentID;
            }
            else
            {
                throw new ArgumentException("Grade", "Grade belongs to a different student");
            }
        }
    }

    // TODO: Exercise 1: Task 2b: Convert Teacher into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }

        private string _Password = Guid.NewGuid().ToString(); // Generate a random password by default
        public string Password
        {
            set
            {
                _Password = value;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public Teacher(int _TeacherID, string _Username, string _Password, string _Firstname, string _Lastname, string _Class)
        {
            this.TeacherID = _TeacherID;
            this.UserName = _Username;
            this.Password = _Password;
            this.FirstName = _Firstname;
            this.LastName = _Lastname;
            this.Class = _Class;
        }

        public Teacher()
        {
            this.TeacherID = 0;
            this.UserName = "an empty string";
            this.Password = "an empty string";
            this.FirstName = "an empty string";
            this.LastName = "an empty string";
            this.Class = "an empty string";
        }

        public bool VerifyPassword(string _password)
        {
            return (this._Password == _password);
        }

        public void EnrollInClass(Student _student)
        {
            if (_student.TeacherID == 0)
            {
                _student.TeacherID = this.TeacherID;
            }
            else
            {
                throw new ArgumentException("Student", "Student is already assigned to a class");
            }
        }

        public void RemoveFromClass(Student _student)
        {
            if (_student.TeacherID == this.TeacherID)
            {
                _student.TeacherID = 0;
            }
            else
            {
                throw new ArgumentException("Student", "Student is not assigned to this class");
            }
        }
    }
}
