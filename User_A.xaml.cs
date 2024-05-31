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
        private void A7_Click(object sender, RoutedEventArgs e)
        {
            A8.Text = ConvertNumToABC(A6).ToLower();
        }

        private static string ConvertNumToABC(TextBox tb)
        {
            char[] c = tb.Text.ToCharArray();

            int s = Convert.ToInt32(tb.Text.Substring(0, 2));

            for (int i = 0; i < c.Length; i++)
                c[i] = Convert.ToChar(AllLeters[0] + normalize_step(s, c[i]));
            return new string(c);
        }

        private void A9_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Str(Get_String(A8), AllLeters) && Check_Str(Get_String(A10), AllLeters) && Check_empty(A10) && Check_empty(A8))
                User_B.B12.Text = Encrypt_func_Vigener_2(A10, A8);
        }
        private string Encrypt_func_Vigener_2(TextBox Text, TextBox Key)
        {
            char[] t = Text.Text.ToCharArray();
            char[] k = normalize_key(Text, Key).ToCharArray();
            char[] result = new char[t.Length];


            for (int i = 0; i < t.Length; i++)
            {
                int first = Get_Char_Number(t[i], AllLeters);
                int second = Get_Char_Number(k[i], AllLeters);
                result[i] = AllLeters[(first + second) % AllLeters.Length];
            }
            return new string(result);
        }

        private void A11_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Str(Get_String(A12), AllLeters) && Check_Str(Get_String(A8), AllLeters) && Check_empty(A8) && Check_empty(A12))
                A13.Text = Unencrypt_func_Vigener_2(A12, A8);
        }
        private string Unencrypt_func_Vigener_2(TextBox Text, TextBox Key)
        {
            char[] t = Text.Text.ToCharArray();
            char[] k = normalize_key(Text, Key).ToCharArray();
            char[] result = new char[t.Length];


            for (int i = 0; i < t.Length; i++)
            {
                int first = Get_Char_Number(t[i], AllLeters);
                int second = Get_Char_Number(k[i], AllLeters);
                result[i] = AllLeters[(Math.Abs(normalize_step_2(first - second))) % AllLeters.Length];
            }

            return new string(result);
        }
    }
}
