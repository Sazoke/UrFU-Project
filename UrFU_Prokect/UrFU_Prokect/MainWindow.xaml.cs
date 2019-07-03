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
using Elements;

namespace UrFU_Prokect
{
    public partial class MainWindow : Window
    {
        DateTime StartTime;
        MyTextBox TextBox;
        MyTextBlock TextBlock;
        bool IsEnd = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var buttons = new Buttons();
            foreach (Button button in buttons)
            {
                button.Click += new RoutedEventHandler(ButtonClick);
                this.Grid.Children.Add(button);
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Grid.Children.Clear();
            StartTime = DateTime.Now;
            TextBlock = new MyTextBlock();
            TextBox = new MyTextBox();
            TextBox.SetChager(TextBox_TextChanged);
            TextBlock.SetText(e.Source.ToString().Last());
            TextBox.Show(this.Grid);
            TextBlock.Show(this.Grid);
            TextBox.Focus();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlock.Refresh(TextBox.Text, ref IsEnd);
            if(IsEnd)
            {
                this.Grid.Children.Clear();
                ShowResult();
            }
        }

        private void ShowResult()
        {
            var timeBlock = new TextBlock();
            timeBlock.FontSize = 20;
            timeBlock.Margin = new Thickness(300, 100, 300, 200);
            timeBlock.Text = Time();
            var wrongsBlock = new TextBlock();
            wrongsBlock.FontSize = 20;
            wrongsBlock.Margin = new Thickness(300, 200, 300, 100);
            wrongsBlock.Text = "Wrongs : " + GetCountWrongs().ToString();
            this.Grid.Children.Clear();
            this.Grid.Children.Add(timeBlock);
            this.Grid.Children.Add(wrongsBlock);
        }

        private int GetCountWrongs()
        {
            var wrongs = 0;
            for (int i = 0; i < TextBlock.Text.Length; i++)
                if (TextBlock.Text[i].CompareTo('!') == 0)
                    wrongs++;
            return wrongs;
        }
        private string Time()
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string text = "";
            int tenths = elapsed.Milliseconds / 100;
            text +=
                "Time : " +
                elapsed.Hours.ToString("00") + ":" +
                elapsed.Minutes.ToString("00") + ":" +
                elapsed.Seconds.ToString("00") + "." +
                tenths.ToString("0");
            return text;
        }
    }
}
