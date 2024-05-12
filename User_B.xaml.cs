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
using System.Windows.Shapes;
using static information_security_2.MainWindow;
using static information_security_2.User_A;


namespace information_security_2
{
    /// <summary>
    /// Логика взаимодействия для User_B.xaml
    /// </summary>
    public partial class User_B : Window
    {
        //MainWindow Mw = new MainWindow();

        public int p { get; set; }
        public int g { get; set; }

        public User_A User_A { get; set; }


        public int key { get; set; }
        public int key_2 { get; set; }

        public int y { get; set; }

        public int Check { get; set; }
        public User_B()
        {
            InitializeComponent();
        }
        public void ShowViewModel()
        {
            MessageBox.Show("123");
        }
        private void Gen_Key_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            y = r.Next(1, int.MaxValue);
            key = ModPow(g, y, p);

            B3.Text = key.ToString();
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            User_A.key_2 = key;
            User_A.A4.Text = key.ToString();
        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            Check = ModPow(key_2, y, p);
            B6.Text = Check.ToString();
        }
    }
}
