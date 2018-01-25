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

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : UserControl
    {
        public StudentsPage()
        {
            InitializeComponent();
        }

        #region Display Logic

        // Display students for the current teacher
        // Student details are hard coded in the XAML definition of the view
        public void Refresh()
        {
            // Display the class name - hard-coded
            List<Student> students = DataSource.Students.Where(e => e.TeacherID == SessionContext.CurrentTeacher.TeacherID).ToList();
            this.list.ItemsSource = students;
            txtClass.Text = String.Format("Class {0}", SessionContext.CurrentTeacher.Class);
        }
        #endregion

        #region Event Members
        public delegate void StudentSelectionHandler(object sender, StudentEventArgs e);
        public event StudentSelectionHandler StudentSelected;
        #endregion

        #region Event Handlers
        // TODO: Exercise 1: Task 5a: Handle the click event for a student
        // Raise the StudentSelected event and indicate which student was selected
        // The MainWindow window subscribes to this event and displays the view for a single student
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = sender as Button;

            if (sender != null)
            {
                Student student = (Student)btnClicked.Tag;
                this.StudentSelected?.Invoke(sender, new StudentEventArgs(student));
            }
        }
        #endregion
    }

    // EventArgs class for passing Student information to an event
    public class StudentEventArgs : EventArgs
    {
        public Student Child { get; set; }

        public StudentEventArgs(Student s)
        {
            Child = s;
        }
    }
}
