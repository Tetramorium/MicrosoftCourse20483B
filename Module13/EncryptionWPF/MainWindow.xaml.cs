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

            string sensitiveData = "My password is abc123";

            byte[] bytes_1 = Encoding.ASCII.GetBytes(sensitiveData);

            // Initial call to setup to set up keys for this run

            Encryption.Setup(password, salt);

            // Encrypt the sensitive data

            byte[] test1 = Encryption.SymmetricEncryption(bytes_1);

            // Decrypt the sensitive data back to string

            string test2 = Encryption.SymmetricDecryption(test1);

            // Generate a hash from the sensitive data

            byte[] test3 = Encryption.GenerateHash(bytes_1);

            // Generate a hash from tampered sensitive data
            // Person who tampered doesn't know the secretKey so the hash above send along with the data can't be tampered with

            sensitiveData += ", please send your password to 'Insert Fake Email-adress' for confirmation";

            // Server that received the data generates a hash from the data to compare with the received hash
            // This will turn out to be false so the send data should be rejected

            byte[] bytes_2 = Encoding.ASCII.GetBytes(sensitiveData);

            byte[] test4 = Encryption.GenerateHash(bytes_2);

            bool isEqual = test3.SequenceEqual(test4);

            // Assymetric encryption - Can only encrypt small ammounts of data since process is very intensive

            byte[] bytes_3 = Encoding.ASCII.GetBytes("Hello World!");

            byte[] test5 = Encryption.AsymmetricEncryption(bytes_3);

            // Assymetric decryption

            byte[] test6 = Encryption.AsymmetricDecryption(test5);

            string test7 = Encoding.ASCII.GetString(test6);
        }
    }
}
