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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static information_security_2.MainWindow;
using static information_security_2.User_B;


namespace information_security_2
{
    /// <summary>
    /// Логика взаимодействия для User_A.xaml
    /// </summary>
    public partial class User_A : Window
    {
        //MainWindow Mw = new MainWindow();

        public int p { get; set; }
        public int g { get; set; }

        public User_B User_B { get; set; }
        public int x { get; set; }
        public int key { get; set; }
        public int key_2 { get; set; }

        public int Check { get; set; }
        public User_A()
        {
            InitializeComponent();
        }
        public void ShowViewModel()
        {
            MessageBox.Show("333");
        }
        private void Gen_Key_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            x = r.Next(1, int.MaxValue);
            key = ModPow(g, x , p);
            A3.Text = key.ToString();

        }

        private void A2_Click(object sender, RoutedEventArgs e)
        {
            User_B.key_2 = key;
            User_B.B4.Text = key.ToString();

        }

        private void A5_Click(object sender, RoutedEventArgs e)
        {
            Check = ModPow(key_2, x, p);
            A6.Text = Check.ToString();
        }
    }
}
