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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;


namespace BindingTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Class1 c = new Class1() { Id = 1223 };
            L1.DataContext = c;

            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calculator();
            odp.MethodName = "Add";
            odp.MethodParameters.Add(10);
            odp.MethodParameters.Add(10);
            object data = odp.Data;

            Binding bindingToArg1 = new Binding("MethodParameters[0]")
            {
                Source=odp,
                BindsDirectlyToSource = true,
                UpdateSourceTrigger= UpdateSourceTrigger.PropertyChanged
            };

            Binding bindingToArg2 = new Binding()
            {
                Source = odp,
                Path = new PropertyPath("MethodParameters[1]"),
                BindsDirectlyToSource = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            Binding bindingToResult = new Binding(".") { Source = odp };
            
            BindingOperations.SetBinding(this.txtBoxArg1, TextBox.TextProperty, bindingToArg1);
            this.txtBoxArg2.SetBinding(TextBox.TextProperty, bindingToArg2);
            this.txtBoxResult.SetBinding(TextBox.TextProperty, bindingToResult);
        }
    }

    class Calculator
    {
        public string Add(string arg1, string arg2)
        {
            double x = 0;
            double y = 0;
            double z = 0;

            if(double.TryParse(arg1,out x)&&double.TryParse(arg2,out y))
            {
                z = x + y;
                return z.ToString();
            }

            return "Input error";
        }
    }
}
