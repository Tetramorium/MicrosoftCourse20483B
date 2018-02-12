using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace TestApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PropertyInfo[] propInfoList;

        public MainWindow()
        {
            InitializeComponent();

            ArrayList ColorList = new ArrayList();
            Type colorType = typeof(Brushes);
            
            propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);

            this.cb_ColorPicker.ItemsSource = propInfoList;
            this.cb_ColorPicker.DisplayMemberPath = "Name";
        }
    }
}
