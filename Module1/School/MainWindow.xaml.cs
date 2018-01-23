using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using School.Data;


namespace School
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Connection to the School database
        private SchoolDBEntities schoolContext = null;

        // Field for tracking the currently selected teacher
        private Teacher teacher = null;

        // List for tracking the students assigned to the teacher's class
        private IList studentsInfo = null;

        #region Predefined code

        public MainWindow()
        {
            InitializeComponent();
        }

        // Connect to the database and display the list of teachers when the window appears
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.schoolContext = new SchoolDBEntities();
            teachersList.DataContext = this.schoolContext.Teachers;
        }

        // When the user selects a different teacher, fetch and display the students for that teacher
        private void teachersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Find the teacher that has been selected
            this.teacher = teachersList.SelectedItem as Teacher;
            this.schoolContext.LoadProperty<Teacher>(this.teacher, s => s.Students);

            // Find the students for this teacher
            this.studentsInfo = ((IListSource)teacher.Students).GetList();

            // Use databinding to display these students
            studentsList.DataContext = this.studentsInfo;
        }

        #endregion

        // When the user presses a key, determine whether to add a new student to a class, remove a student from a class, or modify the details of a student
        private void studentsList_KeyDown(object sender, KeyEventArgs e)
        {
            Student selectedStudent;

            switch (e.Key)
            {
                case Key.Enter:
                    selectedStudent = this.studentsList.SelectedItem as Student;

                    StudentForm sf = new StudentForm();
                    sf.Title = "Edit Student Details";

                    sf.firstName.Text = selectedStudent.FirstName;
                    sf.lastName.Text = selectedStudent.LastName;
                    sf.dateOfBirth.Text = selectedStudent.DateOfBirth.ToString("d");

                    if (sf.ShowDialog() == true)
                    {
                        selectedStudent.FirstName = sf.firstName.Text;
                        selectedStudent.LastName = sf.lastName.Text;
                        selectedStudent.DateOfBirth = DateTime.Parse(sf.dateOfBirth.Text);

                        this.saveChanges.IsEnabled = true;
                    }

                    break;
                case Key.Insert:
                    StudentForm sf2 = new StudentForm();
                    sf2.Title = string.Format("New Student for Class {0}", teacher.Class);

                    if (sf2.ShowDialog() == true)
                    {
                        Student student = new Student
                        {
                            FirstName = sf2.firstName.Text,
                            LastName = sf2.lastName.Text,
                            DateOfBirth = DateTime.Parse(sf2.dateOfBirth.Text)
                        };

                        this.teacher.Students.Add(student);
                        this.saveChanges.IsEnabled = true;
                    }
                    break;
                case Key.Delete:
                    selectedStudent = this.studentsList.SelectedItem as Student;

                    MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Remove {0} {1}?", selectedStudent.FirstName, selectedStudent.LastName), "Confirm", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        this.teacher.Students.Remove(selectedStudent);
                    }
                    break;
            }

        }

        #region Predefined code

        private void studentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        // Save changes back to the database and make them permanent
        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Decimal))]
    class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return ((int)(DateTime.Now.Subtract((DateTime)value).Days / 362.25)).ToString();
            else
                return "";
        }

        #region Predefined code

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
