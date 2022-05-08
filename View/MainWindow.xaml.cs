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
using System.Globalization;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class DoubleStrConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val = (double)value;
                return $"{val:0.0}";
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleStrConv: ERROR";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string val = value as string;
                return double.Parse(val);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleStrConv: ERROR";
            }
        }
    }

    public static class Cmd
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(Cmd),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
        (
            "Splines",
            "Splines",
            typeof(Cmd),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
            }
        );
    }
    public partial class MainWindow : Window
    {
        public ViewData Vd { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Vd.Clear();
        }
        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Vd.Data.SetErr();
        }

        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Vd.Measured_on_Chart();
        }

        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Vd.Data.SetErr();
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Vd.Splain_on_Chart();
        }

        private void TextBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != ",")
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }


    }
}
