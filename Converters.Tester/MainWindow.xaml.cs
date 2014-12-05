using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Converters.Tester.Annotations;

namespace Converters.Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int m_value;

        private int m_x;
        private int m_y;
        private int m_result;

        public MainWindow()
        {
            Value = 1;

            X = 2;
            Y = 3;

            DataContext = this;
            InitializeComponent();
        }

        public int X
        {
            get { return m_x; }
            set
            {
                if (m_x != value)
                {
                    m_x = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Y
        {
            get { return m_y; }
            set
            {
                if (m_y != value)
                {
                    m_y = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Result
        {
            get { return m_result; }
            set
            {
                if (m_result != value)
                {
                    m_result = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Value
        {
            get { return m_value; }
            set
            {
                if (m_value != value)
                {
                    m_value = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
