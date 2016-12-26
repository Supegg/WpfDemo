using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace BindingTest
{
    /// <summary>
    /// 如果绑定的资源不存在，不会触发转换事件
    /// </summary>
    class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class Class1
    {
        public int Id { get; set; }
        public int Id1 { get; set; }
    }
}
