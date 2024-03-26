using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            EngProbilities();

        }

        public Dictionary<char, double> letterProbability = new();
        public Dictionary<char, double> Probability = new();
        public Dictionary<char, double> RusProbability = new();
        public Dictionary<char, double> EngProbability = new();


        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);



            switch (s[s.Length - 1])
            {
                case '1': // cesar lab 1
                    Clear(EncryptText);
                    if (Check_number(Step) && Check_empty(UnencryptText))
                    {
                        EncryptText.Text = Encrypt_func_Caesar_1(UnencryptText, Step);
                    }
                    break;

                case '2': // Vigenère lab 2
                    Clear(Data);

                    if (Check_empty(EncryptText))
                    {
                        for (int i = 0; i <= AllLeters.Length; i++)
                        {

                            Data.Text += Unencrypt_func_Caesar_1(EncryptText, i);
                            Data.Text += Environment.NewLine;
                        }
                    }
                    break;


                default:

                break;

            }

           
        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);

            switch (s[s.Length-1])
            {
                case '1': // cesar lab 1
                    Clear(Data);

                    if (Check_empty(EncryptText))
                    {
                        for (int i = 0; i <= AllLeters.Length; i++)
                        {

                            Data.Text += Unencrypt_func_Caesar_1(EncryptText, i);
                            Data.Text += Environment.NewLine;
                        }
                    }
                break;

                case '2': // Vigenère lab 2
                    Clear(Data);

                    if (Check_empty(EncryptText))
                    {
                        for (int i = 0; i <= AllLeters.Length; i++)
                        {

                            Data.Text += Unencrypt_func_Caesar_1(EncryptText, i);
                            Data.Text += Environment.NewLine;
                        }
                    }
                    break;


                default:
                    
                break;

            }
            
        }

        private string Encrypt_func_Caesar_1(TextBox Text, TextBox Step)
        {
            char[] c = Text.Text.ToCharArray();
            int s = Convert.ToInt32(Step.Text);

            c = shift(c, s);

            return new string(c);
        }

        private string Unencrypt_func_Caesar_1(TextBox Text, int s)
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
            for (int i = 0; i < AllLeters.Length; i++)
            {
                Char t = AllLeters[i];
                if (str.IndexOf(t) != -1)
                    x += (letterProbability[t] - Probability[t]) * (letterProbability[t] - Probability[t]) / Probability[t];
            }

            return x;
        }

        private char[] shift(char[] c, int s)
        { 
            for (int i = 0; i < c.Length; i++)
                if (!Check_Char(c[i], AllLeters))
                    c[i] = Convert.ToChar(AllLeters[0] + normalize_step(s, c[i])) ;

            return c;
        }
        
        private int normalize_step(int s, char c)
        {
            s = ((s + AllLeters.Length * 100) % AllLeters.Length + c - AllLeters[0]) % AllLeters.Length;
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

        public string AllLeters =    "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        public string RUSAllLeters = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        public string ENGAllLeters = "abcdefghijklmnopqrstuvwxyz";


        public void RusProbilities()
        {
            Probability = RusProbability;

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

        public void EngProbilities()
        {
            EngProbability.Add('a', 8.55);
            EngProbability.Add('b', 1.60);
            EngProbability.Add('c', 3.16);
            EngProbability.Add('d', 3.87);
            EngProbability.Add('e', 12.10);
            EngProbability.Add('f', 2.18);
            EngProbability.Add('g', 2.09);
            EngProbability.Add('h', 4.96);
            EngProbability.Add('i', 7.33);
            EngProbability.Add('j', 0.22);
            EngProbability.Add('k', 0.81);
            EngProbability.Add('l', 4.21);
            EngProbability.Add('m', 2.53);
            EngProbability.Add('n', 7.17);
            EngProbability.Add('o', 7.47);
            EngProbability.Add('p', 2.07);
            EngProbability.Add('q', 0.10);
            EngProbability.Add('r', 6.33);
            EngProbability.Add('s', 6.73);
            EngProbability.Add('t', 8.94);
            EngProbability.Add('u', 2.68);
            EngProbability.Add('v', 1.06);
            EngProbability.Add('w', 1.83);
            EngProbability.Add('x', 0.19);
            EngProbability.Add('y', 1.72);
            EngProbability.Add('z', 0.11);
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);

            string filename = "";
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;
            }
            if (filename != "")
            {
                StreamWriter f = new StreamWriter(filename, false);
                f.WriteLine(UnencryptText.Text + "|" + Step.Text);
                f.Close();
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);

            string filename = "";
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Data"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
            }
            if (filename != "")
            {
                StreamReader f = new StreamReader(filename);
                string first_line = f.ReadLine();
                f.Close();
                string pattern = @"[^|][|]\d+";
                if (Regex.IsMatch(first_line, pattern, RegexOptions.IgnoreCase))
                {
                    string[] str = first_line.Split('|');
                    UnencryptText.Text = str[0];
                    Step.Text = str[1];
                }
            }
        }

        private void ClearTextBox_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);

            Clear(EncryptText);
            Clear(UnencryptText);
            Clear(Step);
            Clear(Data);
        }

    
        int count = 1;
        private void Language_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString().Substring(0,4);


            if (count%2 == 0) 
            {
                for (int i = 1; i <3; i++ ) { ((Label)FindName(s + i)).Content = "Язык: RUS"; }

                AllLeters = RUSAllLeters;
                Probability = RusProbability;
            }
            else
            {
                for (int i = 1; i < 3; i++) { ((Label)FindName(s + i)).Content = "Язык: ENG"; }

                AllLeters = ENGAllLeters;
                Probability = EngProbability;
            }

            count++;
        }
    }
}
