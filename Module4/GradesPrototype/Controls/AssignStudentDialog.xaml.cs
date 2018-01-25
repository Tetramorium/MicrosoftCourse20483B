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
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for AssignStudentDialog.xaml
    /// </summary>
    public partial class AssignStudentDialog : Window
    {
        public AssignStudentDialog()
        {
            InitializeComponent();
        }

        // TODO: Exercise 4: Task 3b: Refresh the display of unassigned students
        private void Refresh()
        {
            List<Student> unassignedStudents = DataSource.Students.Where(e => e.TeacherID == 0).ToList();

            if (unassignedStudents == null || unassignedStudents.Count == 0)
            {
                txtMessage.Visibility = Visibility.Visible;
                list.Visibility = Visibility.Collapsed;
            }
            else
            {
                list.ItemsSource = unassignedStudents;
                txtMessage.Visibility = Visibility.Collapsed;
                list.Visibility = Visibility.Visible;
            }
        }

        private void AssignStudentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        // TODO: Exercise 4: Task 3a: Enroll a student in the teacher's class
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = sender as Button;
                int studentID = (int)b.Tag;

                Student student = DataSource.Students.Find(s => s.StudentID == studentID);

                if (student != null)
                {
                    string message = String.Format("Add {0} {1} to your class?", student.FirstName, student.LastName);
                    MessageBoxResult result = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        SessionContext.CurrentTeacher.EnrollInClass(student);
                    }

                    Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error enrolling student", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog box
            this.Close();
        }
    }
}
