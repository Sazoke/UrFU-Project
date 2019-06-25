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

namespace ProjectURFU
{
    public partial class MainWindow : Window
    {
        Buttons buttons;
        private List<string> Levels;
        private List<TextBlock> textBlocks;
        DateTime StartTime;
        int wrongs;
        int number;
        int nowBlock;
        int page;
        Level game;
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Levels = new List<string>();
            var EasyLevel = @"asdfghjkl;'";
            Levels.Add(EasyLevel);
            var NormalLevel = EasyLevel + @"rtyuvbnm";
            Levels.Add(NormalLevel);
            var HardLevel = NormalLevel + @"qweiop[]zxc,./";
            Levels.Add(HardLevel);
            buttons = new Buttons();
            foreach (Button button in buttons)
            {
                button.Click += new RoutedEventHandler(Button_Click);
                this.importantGrid.Children.Add(button);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartTime =  DateTime.Now;
            game = new Level(Levels[int.Parse("" + e.Source.ToString().Last()) - 1]);
            this.importantGrid.Children.Clear();
            CreateText(game);
        }

        private void CreateText(Level game)
        {
            textBlocks = new List<TextBlock>();
            AddTextBlock(textBlocks);
            for (int i = 0; i < 5; i++)
                textBlocks[i].Text = game.Text(rnd.Next(1, 5));
        }
        private void ShowResult()
        {
            var timeBlock = new TextBlock();
            timeBlock.FontSize = 20;
            timeBlock.Margin = new Thickness(350, 100, 300, 200);
            timeBlock.Text = Time();
            var wrongsBlock = new TextBlock();
            wrongsBlock.FontSize = 20;
            wrongsBlock.Margin = new Thickness(400, 200, 300, 100);
            wrongsBlock.Text = wrongs.ToString();
            this.importantGrid.Children.Clear();
            this.importantGrid.Children.Add(timeBlock);
            this.importantGrid.Children.Add(wrongsBlock);
        }

        private string Time()
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string text = "";
            int tenths = elapsed.Milliseconds / 100;
            text +=
                elapsed.Hours.ToString("00") + ":" +
                elapsed.Minutes.ToString("00") + ":" +
                elapsed.Seconds.ToString("00") + "." +
                tenths.ToString("0");
            return text;
        }

        private void AddTextBlock(List<TextBlock> textBlocks)
        {
            for (int i = 0; i < 5; i++)
            {
                var newTextBlock = new TextBlock();
                newTextBlock.FontSize = 20;
                newTextBlock.Margin = new Thickness(100 + 100 * i, 150, 600 - 100 * i, 200);
                newTextBlock.Name = "textBlock" + i.ToString();
                if(i>0)
                    newTextBlock.Background = Brushes.White;
                else
                    newTextBlock.Background = Brushes.AliceBlue;
                this.importantGrid.Children.Add(newTextBlock);
                textBlocks.Add(newTextBlock);
            }
        }

        private void ImportantGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (textBlocks[nowBlock].Text.Length > number && textBlocks[nowBlock].Text[number] != ' ')
                {
                    number++;
                    wrongs++;
                    return;
                }
                textBlocks[nowBlock].Background = Brushes.White;
                nowBlock++;
                if(nowBlock < 5)
                    textBlocks[nowBlock].Background = Brushes.AliceBlue;
                number = 0;
            }
            else
            {
                if (number >= textBlocks[nowBlock].Text.Length)
                {
                    number = 0;
                    textBlocks[nowBlock].Background = Brushes.White;
                    nowBlock++;
                    if (nowBlock < 5)
                        textBlocks[nowBlock].Background = Brushes.AliceBlue;
                    wrongs++;
                    return;
                }
                if (e.Key.ToString().ToLower()[0] != (textBlocks[nowBlock].Text[number]))
                    wrongs++;
                number++;
            }
            if (nowBlock == 5 && number == 0 && nowBlock != 0)
            {
                CreateText(game);
                page++;
                nowBlock = 0;
            }
            if (page == 5)
                ShowResult();
        }
    }
}
