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

namespace information_security_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Clear(EncryptText);
            if (Check_number(Step) && Check_empty(UnencryptText))
            {
                EncryptText.Text = Encrypt_func(UnencryptText, Step);
            }
        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (Check_empty(EncryptText))
            {

            }
        }

        private string Encrypt_func(TextBox Text, TextBox Step)
        {
            char[] c = Text.Text.ToCharArray();
            int s = Convert.ToInt32(Step.Text);

            if (s >= 32) { s -= 32; }

            c = shift(c, s);

            return new string(c);
        }

        private string Unencrypt_func(TextBox Text, TextBox Step)
        {
            char[] c = Text.Text.ToCharArray();
            int s = Convert.ToInt32(Step.Text);

            if (s >= 32) { s -= 32; }

            c = shift(c, s);

            return new string(c);
        }

        private char[] shift(char[] c, int s)
        {
            for (int i = 0; i < c.Length; i++)
                if (!Check_Char(c[i]))
                    c[i] = Convert.ToChar(c[i] + s);

            return c;
        }
        private bool Check_Char(char c)
        {
            bool err = false;
            string s = "!@#$%^&*();:?_+-=123456789/<>|\\.,`~[]{}";

            for (int i = 0; i < s.Length; i++)
                if (c == s[i]) { err = true; break; }

            return err;

        }
        private void Clear(TextBox sender)
        {
            sender.Text = string.Empty;
        }
        private bool Check_empty(TextBox sender)
        {
            bool err = true;
            if (sender.Text == string.Empty) { err = false; }
            return err;

        }
        private bool Check_number(TextBox sender)
        {
            bool err = false;
            int step;
            if (int.TryParse(sender.Text, out step))
            {
                if (step > 0 && step <= 50) { err = true; }
            }
            return err;

        }

       
    }
}
