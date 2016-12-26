using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo
{
    /// <summary>
    /// WindowTBlock.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTBlock : Window
    {
        public WindowTBlock()
        {
            InitializeComponent();

            operateInlines();
        }

        public string RunTitle
        {
            get { return (string)this.GetValue(RunTitleProperty); }
            set { this.SetValue(RunTitleProperty, value); }
        }
        public static readonly DependencyProperty RunTitleProperty = DependencyProperty.Register("RunTitle", typeof(String), typeof(WindowTBlock), new PropertyMetadata("Inline 元素介绍"));

        public Boolean State
        {
            get { return (Boolean)this.GetValue(StateProperty); }
            set { this.SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
          "State", typeof(Boolean), typeof(WindowTBlock), new PropertyMetadata(false));

        void operateInlines()
        {
            Run title = new Run() { };
            title.SetBinding(Run.TextProperty, new Binding("RunTitle") { Source = this});//对Text属性进行Binding
            this.tblkDemo1.Inlines.Add(new Bold(title) { FontSize = 20 });
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Run("Key words: Run,LineBreak,Span 继承自Inline"));
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Run("Key words: Bold,Italic,Underline,Hyperlink 继承自Span"));
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Bold(new Run() { Text = "Bold" }));
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Italic(new Run("Italic")));
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Underline(new Run("Underline")));
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Hyperlink(new Run("Bing")) { NavigateUri = new Uri("http://bing.com") });
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new Span(new Run("set font&color in Span ")) { FontSize = 20, Foreground = Brushes.Green });
            this.tblkDemo1.Inlines.Add(new LineBreak());
            this.tblkDemo1.Inlines.Add(new InlineUIContainer(new Button() { Content = "Inline UIElement" }));
            this.tblkDemo1.Inlines.Add(new LineBreak());

            this.tblkDemo1.AddInline("add by ExtensionMethon AddInline", Brushes.Red);
            this.tblkDemo1.AddInline("add by ExtensionMethon AddInline", new LinearGradientBrush(Colors.Blue,Colors.Violet,30),20,true,true,true,true);
        }
    }
}
