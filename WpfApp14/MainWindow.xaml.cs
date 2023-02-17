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
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WpfApp14
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> mobnumbers = new List<string>();
            mobnumbers.Add("+7");
            mobnumbers.Add("8");
            CBMobnum.ItemsSource = mobnumbers;
            CBMobnum.SelectedIndex = 0;
            List<string> Domen = new List<string>();
            Domen.Add("@firma.ru");
            domen.ItemsSource = Domen;
            domen.SelectedIndex = 0;
        }
        Regex regexID = new Regex(@"[0-9]");
        private void TextBox_1(object sender, TextCompositionEventArgs e)
        {
            if (!regexID.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Возможно вы вводите буквы или специальные символы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        Regex regexFIO = new Regex(@"[а-я]");
        private void TextBox_2(object sender, TextCompositionEventArgs e)
        {
            if (!regexFIO.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Возможно вы вводите буквы латинского алфавита или специальные символы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        Regex regexMobNumber = new Regex(@"[0-9]");
        private void TextBox_3(object sender, TextCompositionEventArgs e)
        {
            if (!regexMobNumber.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Возможно вы вводите буквы или специальные символы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (((TextBox)sender).Text.Length > 10)
            {
                e.Handled = true;
            }
        }
        Regex regexPasportnom = new Regex(@"[0-9]");
        private void TextBox_4(object sender, TextCompositionEventArgs e)
        {
            if (!regexPasportnom.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Возможно вы вводите буквы или специальные символы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (((TextBox)sender).Text.Length > 6)
            {
                e.Handled = true;
            }
        }
        Regex regexPasportser = new Regex(@"[0-9]");
        private void TextBox_5(object sender, TextCompositionEventArgs e)
        {
            if (!regexPasportser.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Возможно вы вводите буквы или специальные символы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (((TextBox)sender).Text.Length > 4)
            {
                e.Handled = true;
            }
        }
        Regex regexlogin = new Regex(@"[0-9A-Za-z_]");
        private void TextBox_6(object sender, TextCompositionEventArgs e)
        {
            if (!regexlogin.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Можно использовать только буквы латинского алфавита, цифры и знак нижнего подчеркивания", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Surname.Clear();
            Name.Clear();
            ID.Clear();
            Otch.Clear();
            Ser.Clear();
            Nomer.Clear();
            Mobnum.Clear();
            Email.Clear();
            CBMobnum.SelectedIndex = -1;
            domen.SelectedIndex = -1;
            MessageBox.Show("Данные стерты", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /*----------------------------------------------------------------------------------------------------------------------------------*/

        string path = "employee.txt";
        private void button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            bool v = true;
            bool k = true;
            if (ID.Text.Length == 0 || Email.Text.Length == 0 || Surname.Text.Length == 0 || Name.Text.Length == 0 ||
            Otch.Text.Length == 0 || Ser.Text.Length == 0 || Nomer.Text.Length == 0 || Mobnum.Text.Length == 0 || domen.SelectedIndex == -1 || CBMobnum.SelectedIndex == -1) { v = false; }
            if (v == false) { MessageBoxResult res3 = MessageBox.Show("Вы ввели не все данные о себе", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error); if (res3 == MessageBoxResult.OK) return; }
            if (Mobnum.Text.Length < 10) { k = false; }
            if (k == false)
            {
            MessageBoxResult res = MessageBox.Show("Неверная длина телефона");
                if (res == MessageBoxResult.OK) return;
            }
            string[] valueN = { Name.Text };
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (var value in valueN)
                Name.Text = ti.ToTitleCase(value);
            string[] valueS = { Surname.Text };
            TextInfo t = CultureInfo.CurrentCulture.TextInfo;
            foreach (var value in valueS)
                Surname.Text = ti.ToTitleCase(value);
            string[] valueP = { Otch.Text };
            TextInfo i = CultureInfo.CurrentCulture.TextInfo;
            foreach (var value in valueP)
                Otch.Text = ti.ToTitleCase(value);
            char h = Email.Text[0];
            bool r = true;
            if (Email.Text != " " && Char.IsLetter(Email.Text, 0) == true)
            {
                error.Append("Ваш Email корректный");
                r = true;
            }
            else
            {
                error.Append("Некорректный email");
                r = false;
            }
            string IDTEXT = $"ID: {ID.Text} ";
            string pID = "";
            bool pr = false;
            StringBuilder sb = new StringBuilder();
            StreamReader streamReader = new StreamReader(path);
            {
                List<string> list = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine());
                }
                if (list.Exists(x => x.Contains(IDTEXT) == true))
                {
                    sb.Append("Такой идентификатор уже занят");
                }
                else
                {
                    MessageBox.Show("Идентификатор свободен");
                    pID = IDTEXT;
                }
                streamReader.Close();
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
            }
            else
            {
                if (r == false)
                {
                    MessageBoxResult res2 = MessageBox.Show(error.ToString(), "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (res2 == MessageBoxResult.OK) return;
                }
                if (r == true)
                {
                    MessageBox.Show(error.ToString(), "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    if (v == true && k == true)
                    {
                        sw.Write("ID: " + ID.Text + " ");
                        sw.Write("Фамилия: " + Surname.Text + "\t");
                        sw.Write("Имя: " + Name.Text + "\t");
                        sw.Write("Отчество: " + Otch.Text + "\t ");
                        sw.Write("Паспорт.Серия паспорта: " + Ser.Text + " номер: " + Nomer.Text + " \t");
                        sw.Write("Мобильный номер: " + CBMobnum.SelectedItem + Mobnum.Text + " \t");
                        sw.Write("Mail: " + Email.Text + domen.SelectedItem + "\t " + Environment.NewLine);
                        pr = true;
                    }
                }
            }
            if (pr == true)
            {
                MessageBoxResult result = MessageBox.Show
                ("Сотрудник сохранен в базу данных", "Сообщение", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
                if (result == MessageBoxResult.Cancel)
                {
                    List<string> lines = File.ReadAllLines(path).ToList();
                    File.WriteAllLines(path, lines.GetRange(0, lines.Count - 1).ToArray());
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Сотрудник не сохранен в базу данных, перепроверьте ваш ID", "Сообщение", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
            }
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(path, string.Empty);
            MessageBox.Show("Файл очищен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}