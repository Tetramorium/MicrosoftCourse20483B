﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace App.WPF.Model
{
    public class TextBlockStyle : INotifyPropertyChanged
    {
        private string content;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                NotifyPropertyChanged();
            }
        }

        private SolidColorBrush fontColor;

        public SolidColorBrush FontColor
        {
            get { return fontColor; }
            set
            {
                fontColor = value;
                NotifyPropertyChanged();
            }
        }

        //https://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
