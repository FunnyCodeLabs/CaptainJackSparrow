using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TestSparrow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ICrypto __EncoderDecoder = new EncDec();

        public MainWindow()
        {
            InitializeComponent();
            App.Current.Activated += Current_Activated;
        }

        private void Current_Activated(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LeftText))
                LeftText = Clipboard.GetText();
            if (String.IsNullOrWhiteSpace(RightText))
                RightText = Clipboard.GetText();
        }

        private string __LeftText;

        public string LeftText
        {
            get { return __LeftText; }
            set { __LeftText = value; OnPropertyChanged("LeftText"); }
        }

        private string __RightText;

        public string RightText
        {
            get { return __RightText; }
            set { __RightText = value; OnPropertyChanged("RightText"); }
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LeftText = __EncoderDecoder.Encode(LeftText);
            }
            catch { }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RightText = __EncoderDecoder.Decode(RightText);
            }
            catch { }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void TextBox_PreviewMouseUp(object sender, EventArgs e)
        {
            TextBox b = sender as TextBox;
            if (b != null)
            {
                b.SelectAll();
            }
        }
    }
}
