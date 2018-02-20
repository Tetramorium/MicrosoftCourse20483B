using EncryptionWPF.Tools;
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

namespace EncryptionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Generate salt
            byte[] salt = Encryption.GetSalt(12);
            string password = "abc123";

            string sensitiveData = "Mijn wachtwoord is abc123";

            byte[] bytes = Encoding.ASCII.GetBytes(sensitiveData);

            // Initial call to setup to set up keys for this run

            Encryption.Setup(password, salt);

            var test = Encryption.SymmetricEncryption(bytes);

            var test2 = Encryption.SymmetricDecryption(test);
        }
    }
}
