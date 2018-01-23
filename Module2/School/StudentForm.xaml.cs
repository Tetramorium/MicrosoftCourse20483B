using System;
using System.Windows;

namespace School
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        #region Predefined code

        public StudentForm()
        {
            InitializeComponent();
        }

        #endregion

        // If the user clicks OK to save the Student details, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (this.firstName.Text.Trim() == "")
            {
                MessageBox.Show("The student must have a first name", "Error");
                return;
            }

            if (this.lastName.Text.Trim() == "")
            {
                MessageBox.Show("The student must have a last name", "Error");
                return;
            }

            if (this.dateOfBirth.Text.Trim() == "")
            {
                MessageBox.Show("Enter a date of birth", "Error");
                return;
            }

            DateTime studentDateOfBirth = DateTime.Parse(this.dateOfBirth.Text);
            TimeSpan difference = DateTime.Now.Subtract(studentDateOfBirth);
            int ageInYears = (int)(difference.Days / 365.25);

            if (ageInYears < 5)
            {
                MessageBox.Show("The student must be atleast 5 years old", "Error");
                return;
            }
                      
            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
