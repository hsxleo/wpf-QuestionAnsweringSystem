using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnsweringSystem
{
    public class User
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static string name = "name";
        public static string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                //调用通知
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        private static string id = "id";
        public static string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                //调用通知
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private static string role = "student";
        public static string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                //调用通知
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(Role)));
            }
        }

        private static string headImage = "icon.jpg";
        public static string HeadImage
        {
            get
            {
                return headImage;
            }
            set
            {
                headImage = value;
                //调用通知
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(HeadImage)));
            }
        }
        public static string Account;
        public static string Password;
        public static string Gender;
        public static string Mail;
        public static string Specialty;
        
    }
}
