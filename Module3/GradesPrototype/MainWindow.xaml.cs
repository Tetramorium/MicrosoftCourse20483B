using System;
using System.Collections;
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
using GradesPrototype.Views;

namespace GradesPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.studentsPage.StudentSelected += studentsPage_StudentSelected;

            DataSource.CreateData();
            Test.test();

            GotoLogon();
        }

        #region Navigation
        // TODO: Exercise 1: Task 3a: Display the logon view and and hide the list of students and single student view
        public void GotoLogon()
        {
            this.logonPage.Visibility = Visibility.Visible;
            this.studentsPage.Visibility = Visibility.Collapsed;
            this.studentProfile.Visibility = Visibility.Collapsed;
        }

        // TODO: Exercise 1: Task 4c: Display the list of students
        private void GotoStudentsPage()
        {
            this.studentsPage.Visibility = Visibility.Visible;
            this.studentProfile.Visibility = Visibility.Collapsed;
            this.studentsPage.Refresh();
        }

        // TODO: Exercise 1: Task 4b: Display the details for a single student
        public void GotoStudentProfile()
        {
            this.studentsPage.Visibility = Visibility.Collapsed;
            this.studentProfile.Visibility = Visibility.Visible;
            studentProfile.Refresh();
        }
        #endregion

        #region Event Handlers

        // TODO: Exercise 1: Task 3b: Handle successful logon
        // Update the display and show the data for the logged on user

        // Handle logon
        private void Logon_Success(object sender, EventArgs e)
        {
            logonPage.Visibility = Visibility.Collapsed;
            gridLoggedIn.Visibility = Visibility.Visible;
            Refresh();
        }

        // Handle logoff
        private void Logoff_Click(object sender, RoutedEventArgs e)
        {
            // Hide all views apart from the logon page
            gridLoggedIn.Visibility = Visibility.Collapsed;
            studentsPage.Visibility = Visibility.Collapsed;
            studentProfile.Visibility = Visibility.Collapsed;
            logonPage.Visibility = Visibility.Visible;
        }

        // Handle logon failed

        private void Logon_Failed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed! Invalid username or password", "Login failed");
        }

        // Handle the Back button on the Student page
        private void studentPage_Back(object sender, EventArgs e)
        {
            GotoStudentsPage();
        }

        // TODO: Exercise 1: Task 5b: Handle the StudentSelected event when the user clicks a student on the Students page
        // Set the global context to the name of the student and call the GotoStudentProfile method to display the details of the student
        private void studentsPage_StudentSelected(object sender, StudentEventArgs e)
        {
            SessionContext.CurrentStudent = e.Child;
            GotoStudentProfile();
        }
        #endregion

        #region Display Logic

        // TODO: Exercise 1: Task 4a: Update the display for the logged on user (student or teacher)
        private void Refresh()
        {
            this.txtName.Text = String.Format("Welcome {0}", SessionContext.UserName);

            if (SessionContext.UserRole == Role.Teacher)
            {
                this.txtName.Text = String.Format("Welcome {0} {1}", SessionContext.CurrentTeacher.FirstName, SessionContext.CurrentTeacher.LastName);
                GotoStudentsPage();
            }
            else
            {
                this.txtName.Text = String.Format("Welcome {0} {1}", SessionContext.CurrentStudent.FirstName, SessionContext.CurrentStudent.LastName);
                GotoStudentProfile();
            }
        }
        #endregion
    }
}
