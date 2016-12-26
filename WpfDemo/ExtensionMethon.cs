using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;

namespace WpfDemo
{
    public static class ExtensionMethon
    {
        public static void AddInline(this TextBlock tblock, string text, Brush brush, double fontSize = 16,bool breakLine = true, bool bold=false,bool italic = false,bool underLine = false)
        {
            Inline line = new Run(text) { Foreground = brush ,FontSize = fontSize};
            if(bold)
            {
                line = new Bold(line);
            }
            if(italic)
            {
                line = new Italic(line);
            }
            if(underLine)
            {
                line = new Underline(line);
            }
            tblock.Inlines.Add(line);

            if(breakLine)
            {
                tblock.Inlines.Add(new LineBreak());
            }
        }
    }
}
