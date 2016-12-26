using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfDemo
{

    public class UserInfo : ValidationUtility, INotifyPropertyChanged
    {
        #region 数据更新通知

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private string name;

        [Required(ErrorMessage = "[登录名]内容不能为空！")]
        [StringLength(255, ErrorMessage = "[登录名]内容最大允许255个字符！")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "[登录名]格式不正确！")]
        public string Name
        {

            get { return name; }
            set
            {
                //Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Name" });
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new Exception("用户名不能为空.");
                //}
                name = value;
                notifyPropertyChange("Name");
            }
        }

        private string password;
        [Required(ErrorMessage = "[密码]内容不能为空！")]
        [StringLength(255, ErrorMessage = "[密码]内容最大允许255个字符！")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "[密码]格式不正确！")]
        public string Password
        {
            get { return password; }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new Exception("密码不能为空.");
                //}
                password = value;
                notifyPropertyChange("Pass");
            }
        }

        private int age;

        [Range(18,99,ErrorMessage="年龄必须在18--99之间")]
        public int Age
        { 
            get { return age; } 
            set 
            { 
                age = value; 
                notifyPropertyChange("Age"); 
            } 
        }

    }
}
