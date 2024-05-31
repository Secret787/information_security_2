using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            B8.Text = ConvertNumToABC(B6).ToLower();
        }

        private static string ConvertNumToABC(TextBox tb)
        {
            char[] c = tb.Text.ToCharArray();

            int s = Convert.ToInt32(tb.Text.Substring(0,2));

            for (int i = 0; i < c.Length; i++)
                    c[i] = Convert.ToChar(AllLeters[0] + normalize_step(s, c[i]));
            return new string(c);
        }

        private void B9_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Str(Get_String(B8), AllLeters) && Check_Str(Get_String(B10), AllLeters) && Check_empty(B10) && Check_empty(B8))
                User_A.A12.Text = Encrypt_func_Vigener_2(B10, B8);
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
        // 8  - ключ
        // 10 - текст
        // 12 - вход текст
        private void B11_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Str(Get_String(B12), AllLeters) && Check_Str(Get_String(B8), AllLeters) && Check_empty(B8) && Check_empty(B12))
                B13.Text = Unencrypt_func_Vigener_2(B12, B8);
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
