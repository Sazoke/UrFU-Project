using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

namespace Elements
{
    public class Buttons : IEnumerable
    {
        private List<Button> buttons;
        public Buttons()
        {
            buttons = new List<Button>();
            var left = 200;
            var right = 200;
            for (int i = 0; i < 3; i++)
            {
                var newButton = new Button();
                newButton.Margin = new System.Windows.Thickness(left, 50 + 100 * i, right, 300 - 100 * i);
                newButton.Content = "Text " + (i + 1);
                newButton.FontSize = 20;
                buttons.Add(newButton);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var button in buttons)
                yield return button;
        }
    }

    public class MyTextBox
    {
        private TextBox textBox;
        public MyTextBox()
        {
            textBox = new TextBox();
            textBox.Margin = new System.Windows.Thickness(50, 250, 50, 20);
            textBox.TextWrapping = System.Windows.TextWrapping.Wrap;
        }

        public void Focus() => textBox.Focus();
        public void SetChager(TextChangedEventHandler action) => textBox.TextChanged += action;
        public void Show(Grid grid) => grid.Children.Add(textBox);
        public string Text => textBox.Text;
    }

    public class MyTextBlock
    {
        public int wrongs;
        private string beginText;
        private TextBlock textBlock;
        public MyTextBlock()
        {
            textBlock = new TextBlock();
            textBlock.Margin = new System.Windows.Thickness(50, 50, 50, 250);
        }

        public void SetText(char numberOfText)
        {
            var str = new StreamReader(@"D:\Projects\UrFu Practic\UrFU-Project\UrFU_Prokect\Текст" + numberOfText + ".txt");
            textBlock.Text = str.ReadToEnd();
            beginText = textBlock.Text.ToString();
            textBlock.TextWrapping = System.Windows.TextWrapping.Wrap;
        }

        public void Show(Grid grid) => grid.Children.Add(textBlock);
        public string Text => textBlock.Text;

        public void Refresh(string textOfBox, ref bool IsEnd)
        {
            
            var text = new StringBuilder(textBlock.Text);
            if(textOfBox.Length < beginText.Length)
                if ((text[textOfBox.Length].CompareTo('!') == 0 ||
                    text[textOfBox.Length].CompareTo('*') == 0) &&
                    beginText[textOfBox.Length].CompareTo('*') != 0 &&
                    beginText[textOfBox.Length].CompareTo('!') != 0)
                {
                    text[textOfBox.Length] = beginText[textOfBox.Length];
                    textBlock.Text = text.ToString();
                    return;
                }   
            if (text[textOfBox.Length - 1].CompareTo(textOfBox.Last()) == 0)
                text[textOfBox.Length - 1] = '*';
            else
            {
                text[textOfBox.Length - 1] = '!';
                wrongs++;
            }
            textBlock.Text = text.ToString();
            if (textOfBox.Length == beginText.Length)
                IsEnd = true;
        }
    }
}
