using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectURFU
{
    public class Buttons:IEnumerable
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
                newButton.Content = "Level " + (i + 1);
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
}
