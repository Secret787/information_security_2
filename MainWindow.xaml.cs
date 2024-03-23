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
            RusProbilities();
        }
        public Dictionary<char, double> letterProbability = new();
        public Dictionary<char, double> RusProbability = new();

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
            Clear(Data);

            if (Check_empty(EncryptText))
            {
                for (int i = 0; i <= 32; i++)
                {
                   
                    Data.Text += Unencrypt_func(EncryptText, i);
                    Data.Text += Environment.NewLine;
                }
            }
        }

        private string Encrypt_func(TextBox Text, TextBox Step)
        {
            char[] c = Text.Text.ToCharArray();
            int s = Convert.ToInt32(Step.Text);

            c = shift(c, s);

            return new string(c);
        }

        private string Unencrypt_func(TextBox Text, int s)
        {
            char[] c = Text.Text.ToCharArray();
            c = shift(c, -1 * s);
            string str = new string(c);
            Symbol_probabilities(str);
            double d = Math.Round(xi_sqeare(str),2);

            if (str.Length > 10)
                str = str.Substring(0, 10);


            return str + "\t" + s + "\t" + d;
        }

        private double xi_sqeare(string str)
        {
            double x = 0;
            for (int i = 0; i < 31; i++)
            {
                Char t = AllLeters[i];
                if (str.IndexOf(t) != -1)
                    x += (letterProbability[t] - RusProbability[t]) * (letterProbability[t] - RusProbability[t]) / RusProbability[t];
            }

            return x;
        }

        private char[] shift(char[] c, int s)
        { 
            for (int i = 0; i < c.Length; i++)
                if (!Check_Char(c[i], AllLeters))
                    c[i] = Convert.ToChar(1072 + normalize_step(s, c[i])) ;

            return c;
        }
        
        private int normalize_step(int s, char c)
        {
            s = ((s + 32 * 100) % 32 + c - 1072) % 32;
            return s;
        }

         
        
        private bool Check_Char(char c, string Leters)
        {
            bool err = true;
            string s = Leters;

            if (s.IndexOf(c) != -1) { err = false; }

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

        public string AllLeters = "абвгдежзийклмнопрстуфхцшщъыьэюя";
        public void RusProbilities()
        {

            RusProbability.Add('а', 8.04);
            RusProbability.Add('б', 1.55);
            RusProbability.Add('в', 4.75);
            RusProbability.Add('г', 1.88);
            RusProbability.Add('д', 2.95);
            RusProbability.Add('е', 8.21);
            RusProbability.Add('ж', 0.88);
            RusProbability.Add('з', 1.61);
            RusProbability.Add('и', 7.98);
            RusProbability.Add('й', 1.36);
            RusProbability.Add('к', 3.49);
            RusProbability.Add('л', 4.32);
            RusProbability.Add('м', 3.11);
            RusProbability.Add('н', 6.72);
            RusProbability.Add('о', 10.61);
            RusProbability.Add('п', 2.82);
            RusProbability.Add('р', 5.38);
            RusProbability.Add('с', 5.71);
            RusProbability.Add('т', 5.83);
            RusProbability.Add('у', 2.28);
            RusProbability.Add('ф', 0.41);
            RusProbability.Add('х', 1.02);
            RusProbability.Add('ц', 0.58);
            RusProbability.Add('ч', 1.23);
            RusProbability.Add('ш', 0.55);
            RusProbability.Add('щ', 0.34);
            RusProbability.Add('ъ', 0.03);
            RusProbability.Add('ы', 1.91);
            RusProbability.Add('ь', 1.39);
            RusProbability.Add('э', 0.31);
            RusProbability.Add('ю', 0.63);
            RusProbability.Add('я', 2.00);


        }
        private void Symbol_probabilities(string inputText)
        {
            string s = inputText;
            for (int i = 0; i < s.Length; i++)
                if (AllLeters.IndexOf(s[i]) == -1)
                {
                    s = s.Remove(i, 1);
                    i--;
                }
            letterProbability = new();
            int totalCharacters = s.Length;

            foreach (char character in s)
            {
                if (letterProbability.ContainsKey(character)) { letterProbability[character] += 1.0 / totalCharacters; }
                else { letterProbability[character] = 1.0 / totalCharacters; }
            }

            letterProbability = letterProbability.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
      
    }
}
