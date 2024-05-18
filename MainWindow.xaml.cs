using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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
using System.Threading;
using System.Numerics;


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
        User_A User_A_Start = new User_A();
        User_B User_B_Start = new User_B();

        public bool window = true;
       

        public Dictionary<char, double> letterProbability = new();
        public Dictionary<char, double> Probability = new();
        public Dictionary<char, double> RusProbability = new();
        public Dictionary<char, double> EngProbability = new();


        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);
            TextBox Data = (TextBox)FindName("Data" + s[s.Length - 1]);
           


            
            
                bool b1, b2, b3;
                b1 = Check_empty(UnencryptText);
                b2 = Check_empty(Key);

                switch (s[s.Length - 1])
                {
                    case '1': // cesar lab 1

                        if (Check_number(Key,1,50) && b1 && b2)
                        {
                            EncryptText.Text = Encrypt_func_Caesar_1(UnencryptText, Key);
                        }

                    break;

                    case '2': // Vigenere lab 2

                        if (Check_Str(Get_String(Key), AllLeters) && Check_Str(Get_String(UnencryptText), AllLeters) && b1 && b2)
                        {

                            EncryptText.Text = Encrypt_func_Vigener_2(UnencryptText, Key , Data);
                        }
                       
                        break;

                    case '3': // bits lab 3
                        if (b1 && b2)
                            EncryptText.Text = Encrypt_func_Bits_3(UnencryptText, Key, Data);
                    break;

                    case '4': // Euclidean algorithm lab4
                        if (b1 && b2)
                        {
                            b1 = Check_number(Key, 1, 3);
                            b2 = Check_number(UnencryptText, 1, int.MaxValue);
                            b3 = Check_number(EncryptText, 1, int.MaxValue);

                            //if (Check_number(Key, 1, 3) && Check_number(UnencryptText, 1, int.MaxValue) && Check_number(EncryptText, 1, int.MaxValue))
                            if (b1 && b2 && b3)

                            {
                                Encrypt_func_Euclidean_Algorithm_4(UnencryptText, EncryptText, Key, Data);
                            }
                        }
                        break;
                    case '5': // PSP


                        if (Check_number(Key, 1, 100000) && b2)

                        {
                            Encrypt_func_PSP_5(UnencryptText, EncryptText, Key, Data);
                        }
                        break;
                    case '6': // Prime numbers
                        if (b1 && b2)
                        {
                            Encrypt_func_Prime_6(UnencryptText, EncryptText, Key, Data);
                        }   
                        break;
                    case '7': // DiffieHellman
                   
                            Encrypt_func_DiffieHellman_7(UnencryptText, EncryptText, Key, Data);
                    
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
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);
            TextBox Data = (TextBox)FindName("Data" + s[s.Length - 1]);



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
                    Clear(EncryptText);

                    if (Check_empty(UnencryptText) && Check_empty(Key) && Check_Str(Get_String(Key), AllLeters) && Check_Str(Get_String(UnencryptText), AllLeters))
                    {

                        EncryptText.Text = Unencrypt_func_Vigener_2(UnencryptText, Key, Data);
                    }
                break;

                case '3': // Bits 3
                   
                    Clear(Data);

                    if (Check_empty(UnencryptText) && Check_empty(Key))
                    {

                        EncryptText.Text = Unencrypt_func_Bits_3(UnencryptText, Key, Data);
                    }
                    break;


                default:
                    
                break;

            }
            
        }
          
        private string Encrypt_func_Caesar_1(TextBox Text, TextBox Step)
        {

            char[] t = Text.Text.ToCharArray();
            int s = Convert.ToInt32(Step.Text);

            t = shift(t, s);

            return new string(t);
        }
        private string Encrypt_func_Vigener_2(TextBox Text, TextBox Key, TextBox Data)
        {
            char[] t = Text.Text.ToCharArray();
            char[] k = normalize_key(Text, Key).ToCharArray();
            char[] result = new char[t.Length]; 


            for (int i = 0; i < t.Length; i++)
            {
                int first = Get_Char_Number(t[i], AllLeters);
                int second = Get_Char_Number(k[i], AllLeters);
                result[i] = AllLeters[(first + second)%AllLeters.Length];
            }


            Data.Text += entropia(Text.Text);
            Data.Text += "\n";
            Data.Text += entropia(new string(result));


            return new string(result);
        }
        private string Encrypt_func_Bits_3(TextBox Text, TextBox Key, TextBox Data)
        {
            Clear(Data);

         
            string res = string.Empty;


            if (Key.Text.Length == 1)
            {
                char[] t = Text.Text.ToCharArray();
                string  k = Convert_num_str((int)Key.Text[0]).PadLeft(16, '0');

                
                for (int i = 0; i < t.Length; i++)
                {
                    string first = Convert_num_str((int)t[i]).PadLeft(16, '0');
                    string second = k;

                    int one = Convert_Str_num(first);
                    int two = Convert_Str_num(second);

                    int r = one ^ two;

                    string res2 = (Convert_num_str(r)).PadLeft(16,'0');
                  


                    string result = (sum_bit(first, second)).PadLeft(16, '0');

                    Data.Text += Convert.ToString(first) + " --- " + Convert.ToChar(Convert_Str_num(first)).ToString();
                    Data.Text += Environment.NewLine;
                    // Data.Text += Environment.NewLine;


                    Data.Text += Convert.ToString(second) + " --- " + Convert.ToChar(Convert_Str_num(second)).ToString();
                    Data.Text += Environment.NewLine;
                    // Data.Text += Environment.NewLine;


                    Data.Text += Convert.ToString(result) + " --- " + Convert.ToChar(Convert_Str_num(result)).ToString();
                    Data.Text += Environment.NewLine;
                    //  Data.Text += Environment.NewLine;
                    Data.Text += Environment.NewLine;


                    //k = 123;
                    k = cycle_shift_r(k, 3);

                    res += Convert.ToChar(Convert_Str_num(result)).ToString();
                }
            }

            return res;
        }
        private void   Encrypt_func_Euclidean_Algorithm_4(TextBox one, TextBox two, TextBox Key, TextBox Data)
        {
            int u1, u2, u3;
            int a = Convert.ToInt32(one.Text);
            int b = Convert.ToInt32(two.Text);
            int t = Convert.ToInt32(Key.Text);
            Stopwatch stopwatch = new Stopwatch();

            //System.Threading.Thread.Sleep(1000);

            switch (t)
            {
                case 1: // 
                    stopwatch.Start();
                    Data.Text = EuclideanAlgorithm(a, b).ToString();
                    stopwatch.Stop();


                    break;

                case 2: // 
                    stopwatch.Start();
                    ExtendedEuclideanAlgorithm(a, b, out u1, out u2, out u3);
                    stopwatch.Stop();

                    Data.Text = ($"a * u1 + b * u2 = НОД(a, b): {a} * {u1} + {b} * {u2} = {u3}");


                break;

                case 3: // 
                    stopwatch.Start();

                    if (EuclideanAlgorithm(a, b) == 1)
                        Data.Text = ExtendedEuclideanAlgorithm_3(a, b).ToString();
                        
                    else
                        Data.Text = "НОД(a,n) (наибольший общий делитель) должен быть равен 1";
                    stopwatch.Stop();
                    break;


                default:

                    break;
            }
            System.Threading.Thread.Sleep(1);
            Data.Text += Environment.NewLine;
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:f8}",
            ts.TotalMilliseconds);
            Data.Text += elapsedTime;



        }
        private void   Encrypt_func_PSP_5(TextBox Input, TextBox OutPut, TextBox Key, TextBox Data)
        {

            if (Input.Text == string.Empty || (Check_Str(Input.Text, "01") && Input.Text.Length == 32 ))
            {
                Clear(Data);

                OutPut.Text = string.Empty;
                string s = String.Empty;
                string[] arr = { "31", "29", "19", "2", "0" };
                string bits = Input.Text;


                if (Input.Text == string.Empty)
                {
                    bits = Generate_zero_one(32);
                    Data.Text += bits + " Start BITS";
                }
                else
                    Data.Text += bits + " Start BITS";

                Data.Text += Environment.NewLine;
                 

                for (int j = 0; j < Convert.ToInt32(Key.Text); j++)
                {
                    s = sum_bit(bits[Convert.ToInt32(arr[0])].ToString(), bits[Convert.ToInt32(arr[1])].ToString());
                    //Data.Text += "b" + Convert.ToInt32(arr[0]) + " = " + bits[Convert.ToInt32(arr[0])].ToString();
                    //Data.Text += Environment.NewLine;
                    //Data.Text += "b" + Convert.ToInt32(arr[1]) + " = " + bits[Convert.ToInt32(arr[1])].ToString();
                    //Data.Text += Environment.NewLine;

                    for (int i = 2; i < arr.Length; i++)
                    {
                        s = sum_bit(s, bits[Convert.ToInt32(arr[i])].ToString());
                    //    Data.Text += "b" + Convert.ToInt32(arr[i]) + " = " + bits[Convert.ToInt32(arr[i])].ToString();
                    //    Data.Text += Environment.NewLine;
                    }

                    //Data.Text += s + " THIS SUM BITS";
                    Data.Text += Environment.NewLine;

                    bits = bits.Remove(0,1);
                    //bits = bits.Remove(bits.Length - 1);
                    bits += s;
                    if (Input.Text == bits.ToString())
                    {
                        OutPut.Text += "POVTOR " + j;
                        OutPut.Text += Environment.NewLine;
                    }
                    Data.Text += bits + " THIS NEW BITS" + " ITERATION " + j;
                    Data.Text += Environment.NewLine;
                  
                }

            }
            else
            {
                Data.Text = "NOT OK";
            }
        }
        private void   Encrypt_func_Prime_6(TextBox Input, TextBox OutPut, TextBox Key, TextBox Data)
        {
            int Count_true = 0;
            int Count_false = 0;
            Random r = new Random();
            int[] mas = { 2, 3, 5, 7, 11, 13 }; 
            if (Check_number(Input,1,int.MaxValue) && Check_number(Key, 1, 10000))
            {

                int p = Convert.ToInt32(Input.Text);
                int k = Convert.ToInt32(Key.Text);
                if (p == 1 || p == 2 || p == 3)
                {
                    Data.Text += "Простое";
                    Data.Text += Environment.NewLine;
                }
                else
                {
                    if (Check_Prime_Simple(p, mas))
                    {
                        for (int i = 0; i < k; i++)
                        {
                            int b = r.Next(2, p - 1);
                            int z = ModPow(b, (p - 1) / 2, p);
                            if (z == 1 || z == p - 1)
                                Count_true++;
                            else
                                Count_false++;
                        }

                        if (Count_true == k)
                            Data.Text += "Простое, Ответ не знаю был " + Count_true + " раз, Ответ составное был " + Count_false + " раз ";
                        else
                            Data.Text += "Cоставное, Ответ не знаю был " + Count_true + " раз, Ответ составное был " + Count_false + " раз ";

                        Data.Text += Environment.NewLine;

                    }
                    else
                    {
                        Data.Text += "Составное";
                        Data.Text += Environment.NewLine;
                    }
                }
            }
        }
        private void Encrypt_func_DiffieHellman_7(TextBox Input, TextBox OutPut, TextBox Key, TextBox Data)
        {

            Random r = new Random();
            
            ulong t = (ulong)r.NextInt64(0, int.MaxValue);

            while (!(IsPrime(t) && IsPrime((t - 1) / 2))) 
            {
                t = (ulong)r.NextInt64(0, int.MaxValue);
            }
            Input.Text = t.ToString();
            
            t = (ulong)r.NextInt64(0, Convert.ToInt32(Input.Text)-1);

            while (!IsPrime(t))
            {
                t = (ulong)r.NextInt64(0, Convert.ToInt32(Input.Text) - 1);
            }

            OutPut.Text = t.ToString();

            
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
        private string Unencrypt_func_Vigener_2(TextBox Text, TextBox Key, TextBox Data)
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

            Data.Text += entropia(Text.Text);
            Data.Text += "\n";
            Data.Text += entropia(new string(result));

            Data.Text += Environment.NewLine;


            return new string(result);
        }
        private string Unencrypt_func_Bits_3(TextBox Text, TextBox Key, TextBox Data)
        {

            Clear(Data);


            string res = string.Empty;


            if (Key.Text.Length == 1)
            {
                char[] t = Text.Text.ToCharArray();
                string k = Convert_num_str((int)Key.Text[0]).PadLeft(16, '0');


                for (int i = t.Length - 1; i >= 0; i--)
                {
                    string first = Convert_num_str((int)t[i]).PadLeft(16, '0');
                    string second = k;
                    k = cycle_shift_l(k, 3);

                    int one = Convert_Str_num(first);
                    int two = Convert_Str_num(second);
           
                    int r = one ^ two;

                    string res2 = (Convert_num_str(r)).PadLeft(16, '0');



                    string result = (sum_bit(first, second)).PadLeft(16, '0');

                    Data.Text += Convert.ToString(first) + " --- " + Convert.ToChar(Convert_Str_num(first)).ToString();
                    Data.Text += Environment.NewLine;
                    // Data.Text += Environment.NewLine;


                    Data.Text += Convert.ToString(second) + " --- " + Convert.ToChar(Convert_Str_num(second)).ToString();
                    Data.Text += Environment.NewLine;
                    // Data.Text += Environment.NewLine;


                    Data.Text += Convert.ToString(result) + " --- " + Convert.ToChar(Convert_Str_num(result)).ToString();
                    Data.Text += Environment.NewLine;
                    //  Data.Text += Environment.NewLine;


                    //k = 123;
                    

                    res += Convert.ToChar(Convert_Str_num(result)).ToString();
                }
            }

            return ReverseString(res);
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
        private double entropia(string str)
        {

            Symbol_probabilities(str);
            double x = 0;
            for (int i = 0; i < AllLeters.Length; i++)
            {
                Char t = AllLeters[i];
                if (str.IndexOf(t) != -1)
                    x += (letterProbability[t] * Math.Log2(letterProbability[t]));
            }

            return -1 * x;
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        private char[] shift(char[] c, int s)
        { 
            for (int i = 0; i < c.Length; i++)
                if (!Check_Char(c[i], AllLeters))
                    c[i] = Convert.ToChar(AllLeters[0] + normalize_step(s, c[i])) ;

            return c;
        }
        private int    normalize_step(int s, char c)
        {
            s = ((s + AllLeters.Length * 100) % AllLeters.Length + c - AllLeters[0]) % AllLeters.Length;
            return s;
        }
        private string sum_bit(string s1, string s2)
        {
            char[] r = s1.ToCharArray();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == s2[i])
                    r[i] = '0';
                else
                    r[i] = '1';

            }
            return new string(r);
        }
        private int normalize_step_2(int s)
        {
            s = (s + AllLeters.Length * 100) % AllLeters.Length;
            return s;
        }
        private string normalize_key(TextBox Text, TextBox Key)
        {
            string tmp = string.Empty;
            while (tmp.Length < Get_String(Text).Length)
            {
                tmp += Get_String(Key);
            }
            return new string(tmp);
        }
        private string cycle_shift_r(string s, int c)
        {
            int number = Convert_Str_num(s);
            int numBits = s.Length;

            number &= (1 << numBits) - 1;
            number = (number >> c) | (number << (numBits - c));

            // Преобразование результата обратно в двоичную строку
            return (Convert_num_str(number & ((1 << numBits) - 1))).PadLeft(numBits, '0');
        }
        private string cycle_shift_l(string s, int c)
        {
            int number = Convert_Str_num(s);
            int numBits = s.Length;

            number &= (1 << numBits) - 1;
            number = (number << c) | (number >> (numBits - c));

            // Преобразование результата обратно в двоичную строку
            return (Convert_num_str(number & ((1 << numBits) - 1))).PadLeft(numBits, '0');
        }
        private string Get_String(TextBox tb)
        {
            return tb.Text;
        }
        private bool Check_Str(string str, string Leters)
        {
            bool err = true;
           
            for (int i = 0; i < str.Length; i++)
            {
                if (Check_Char(str[i], Leters)) { err = false; }
            }
            

            return err;

        }
        private int Convert_Str_num(string s)
        {
            double num = 0;
            int t;
            for (int i = s.Length-1; i >= 0; i-- )
            {
                if (s[i] == '0')
                    t = 0;
                else
                    t = 1;
                num += Math.Pow(2, s.Length -1 - i) * t;
            }
            return (int)num;
        }
        private string Convert_num_str(int n)
        {
            if (n == 0) return "0";
            if (n == 1) return "1";
            else        return Convert_num_str(n / 2) + (n % 2);
        }
        private int Get_Char_Number(char c, string str)
        {
            return str.IndexOf(c);
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
        private bool Check_number(TextBox sender, int lover, int up)
        {
            bool err = false;
            int step;
            if (int.TryParse(sender.Text, out step))
            {
                if (step >= lover && step <= up) { err = true; }
            }
            return err;

        }
        static int EuclideanAlgorithm(int m, int n)
        {
            int temp;
            while (n != 0)
            {
                temp = n;
                n = m % n;
                m = temp;
            }
            return m;
        }
        static void ExtendedEuclideanAlgorithm(int a, int b, out int u1, out int u2, out int u3)
        {
            int v1 = 1, v2 = 0, v3 = a;
            u1 = 0; u2 = 1; u3 = b;
            while (v3 != 0)
            {
                int q = u3 / v3;
                int t1 = u1 - v1 * q;
                int t2 = u2 - v2 * q;
                int t3 = u3 - v3 * q;
                u1 = v1; u2 = v2; u3 = v3;
                v1 = t1; v2 = t2; v3 = t3;
            }
        }
        static int ExtendedEuclideanAlgorithm_3(int a, int n)
        {
            int u1 = 0, u2 = 1, u3 = n;
            int v1 = 1, v2 = 0, v3 = a;
            while (u3 != 1)
            {
                int q = u3 / v3;
                int t1 = u1 - v1 * q;
                int t2 = u2 - v2 * q;
                int t3 = u3 - v3 * q;
                u1 = v1; u2 = v2; u3 = v3;
                v1 = t1; v2 = t2; v3 = t3;
            }
            return (u1 % n + n) % n;
        }
        private string Generate_zero_one(int count)
        {
            Random r = new Random();
            string s = String.Empty;
            int k = 0;
            for (int i = 0; i < count; i++)
            {
                k = r.Next(1,10000);
                if (k > 5000)
                    s += "1";
                else
                    s += "0";

            }

            return s;
        }

        private bool Check_Prime_Simple(int number, int[] mas)
        {
            bool err = true;

            for (int i = 0; i < mas.Length; i++)
            {
                if (number%mas[i] == 0 && number != mas[i])
                {
                    err = false;
                    break;
                }
            }

            return err;
        }

        public static int ModPow(int a, int b, int m)
        {
            if (b == 0)
                return 1;

            long result = 1;
            long baseValue = a % m;
            while (b > 0)
            {
                if (b % 2 == 1)
                    result = (result * baseValue) % m;
                baseValue = (baseValue * baseValue) % m;
                b /= 2;
            }
            return (int)result;
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
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);


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
                f.WriteLine(UnencryptText.Text + "|" + EncryptText.Text + "|" + Key.Text);
                f.Close();
            }
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);

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
                string pattern = @".*[|].*[|].*";
                if (Regex.IsMatch(first_line, pattern, RegexOptions.IgnoreCase))
                {
                    string[] str = first_line.Split('|');
                    UnencryptText.Text = str[0];
                    EncryptText.Text = str[1];

                    Key.Text = str[2];
                }
            }
        }
        private void ClearTextBox_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);
            TextBox Data = (TextBox)FindName("Data" + s[s.Length - 1]);



            Clear(EncryptText);
            Clear(UnencryptText);
            Clear(Key);
            Clear(Data);
            window = true;
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

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox Data = (TextBox)FindName("Data" + s[s.Length - 1]);

            Stopwatch stopwatch = new Stopwatch();
            int count = 0;
            int a = 0; int b = 0;
            Data.Text = String.Empty;
            while (count != 5)
            {
                Random r = new Random();
               
                a = r.Next((int)Math.Pow(10, count), (int)Math.Pow(10, count + 1) - 1);
                b = r.Next((int)Math.Pow(10, count), (int)Math.Pow(10, count + 1) - 1);

                if (EuclideanAlgorithm(a, b) == 1)
                {
                    count++;
                    stopwatch.Start();
                    Data.Text += ExtendedEuclideanAlgorithm_3(a, b).ToString() + " otvet";
                    stopwatch.Stop();
                    Data.Text += Environment.NewLine;
                    Data.Text += "a = "+ a + "  ---  "+ " b = " + b ;
                    System.Threading.Thread.Sleep(1);
                    Data.Text += Environment.NewLine;
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime = String.Format("{0:f8}",
                    ts.TotalMilliseconds);
                    Data.Text += elapsedTime;
                    Data.Text += Environment.NewLine;

                }


            }


        }
        private void Time_Click_2(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Name.ToString();
            TextBox EncryptText = (TextBox)FindName("EncryptText" + s[s.Length - 1]);
            TextBox UnencryptText = (TextBox)FindName("UnencryptText" + s[s.Length - 1]);
            TextBox Key = (TextBox)FindName("Key" + s[s.Length - 1]);
            TextBox Data = (TextBox)FindName("Data" + s[s.Length - 1]);

            Stopwatch stopwatch = new Stopwatch();
            int count = 0;
            int a = 0; int b = 0;
            Data.Text = String.Empty;
            if (Check_empty(Key))
            {


                while (count != 6)
                {
                    Random r = new Random();

                    a = r.Next((int)Math.Pow(10, count), (int)Math.Pow(10, count + 1) - 1);
                    UnencryptText.Text = a.ToString();
                    Data.Text += "a = " + a;
                    Data.Text += Environment.NewLine;
                    stopwatch.Start();
                    Encrypt_func_Prime_6(UnencryptText, EncryptText, Key, Data);
                    stopwatch.Stop();

                    System.Threading.Thread.Sleep(1);
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime = String.Format("{0:f8}",
                    ts.TotalMilliseconds);
                    Data.Text += elapsedTime;
                    Data.Text += Environment.NewLine;
                    count++;



                }
            }
        }

        public void Create_Users_Click(object sender, RoutedEventArgs e)
        {
            bool b1 = Check_number(UnencryptText7, 1, int.MaxValue);
            bool b2 = Check_number(EncryptText7, 1, int.MaxValue);
            bool b3 = false;
            if (b1 && b2)
                b3 = Convert.ToInt64(UnencryptText7.Text) > Convert.ToInt64(EncryptText7.Text);

            if (window && b1 && b2 && b3)
            {
                User_A User_1 = new User_A();
                User_B User_2 = new User_B();
                User_1.Show();
                User_2.Show();
                User_1.p = Convert.ToInt32(UnencryptText7.Text);
                User_1.g = Convert.ToInt32(EncryptText7.Text);
                User_2.p = Convert.ToInt32(UnencryptText7.Text);
                User_2.g = Convert.ToInt32(EncryptText7.Text);

                User_1.User_B = User_2;
                User_2.User_A = User_1;
                window = false;
            }
           

           




        }
        static bool IsPrime(ulong number)
        {
            if (number <= 1)
                return false;
            if (number <= 3)
                return true;
            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (ulong i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

      
    }
}
