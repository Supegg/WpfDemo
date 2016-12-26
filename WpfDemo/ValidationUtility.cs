using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfDemo
{

    public class ValidationUtility : IDataErrorInfo
    {
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                Type tp = this.GetType();
                PropertyInfo pi = tp.GetProperty(columnName);
                var value = pi.GetValue(this, null);
                object[] Attributes = pi.GetCustomAttributes(false);
                if (Attributes != null && Attributes.Length > 0)
                {
                    foreach (object attribute in Attributes)
                    {
                        if (attribute is ValidationAttribute)
                        {
                            ValidationAttribute vAttribute = attribute as ValidationAttribute;
                            if (!vAttribute.IsValid(value))
                            {
                                return vAttribute.ErrorMessage;
                            }
                        }
                    }
                }
                return null;
            }
        }
    }
}
