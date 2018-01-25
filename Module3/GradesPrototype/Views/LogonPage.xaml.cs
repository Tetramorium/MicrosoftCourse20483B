using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for LogonPage.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {
        public LogonPage()
        {
            InitializeComponent();
        }

        #region Event Members
        // TODO: Exercise 1: Task 2a: Define the LogonSuccess event handler

        public event EventHandler LogonSuccess;
        public event EventHandler LogonFailed;

        #endregion

        #region Logon Validation

        // TODO: Exercise 1: Task 2b: Implement the Logon_Click event handler for the Logon button
        // Simulate logging on (no validation or authentication performed yet)

        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Password))
            {
                Teacher teacher = DataSource.Teachers.FirstOrDefault(t => t.UserName == username.Text && t.Password == this.password.Password);

                if (teacher != null)
                {
                    SessionContext.UserRole = Role.Teacher;
                    SessionContext.UserID = teacher.TeacherID;
                    SessionContext.UserName = teacher.UserName;
                    SessionContext.CurrentTeacher = teacher;

                    LogonSuccess?.Invoke(this, null);
                }
                else
                {
                    Student student = DataSource.Students.FirstOrDefault(s => s.UserName == username.Text && s.Password == this.password.Password);

                    if (student != null)
                    {
                        SessionContext.UserRole = Role.Student;
                        SessionContext.UserID = student.StudentID;
                        SessionContext.UserName = student.UserName;
                        SessionContext.CurrentStudent = student;

                        LogonSuccess?.Invoke(this, null);
                    }
                    else
                    {
                        LogonFailed?.Invoke(this, null);
                    }
                }
            }
            else
            {
                LogonFailed?.Invoke(this, null);
            }
        }


        #endregion
    }
}
