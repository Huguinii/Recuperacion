using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WpfAulaAtecA.ViewModel;

namespace WpfAulaAtecA.Converters
{
    public class LoginViewToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Oculta si el ViewModel actual es LoginViewModel
            if (value is LoginViewModel)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
