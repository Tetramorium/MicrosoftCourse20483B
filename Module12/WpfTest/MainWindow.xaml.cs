using System.Windows;
using WpfTest.Controller;
using WpfTest.Model;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AssemblyController ac = new AssemblyController("ClassLibrary.dll");
        }

        private void bt_Generate_Click(object sender, RoutedEventArgs e)
        {
            GeneratedClass gc = new GeneratedClass();

            gc.ClassName = "Bob";
            gc.NameSpace = "Bakker";
            gc.AddField("age", typeof(int));

            gc.Generate("test.cs");
        }
    }
}
