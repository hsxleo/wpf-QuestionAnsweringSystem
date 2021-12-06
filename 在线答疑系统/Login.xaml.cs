using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace QuestionAnsweringSystem
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;  //窗口居中弹出
            InitializeComponent();
        }



        //语言切换
        private void language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            
            switch (language.SelectedIndex)
            {
                case 0:
                    resourceDictionary.Source = new Uri("dictChinese.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case 1:
                    resourceDictionary.Source = new Uri("dictEnglish.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case 2:
                    resourceDictionary.Source = new Uri("dictJapanese.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case 3:
                    resourceDictionary.Source = new Uri("dictKorean.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case 4:
                    resourceDictionary.Source = new Uri("dictFrench.xaml", UriKind.RelativeOrAbsolute);
                    break;
            }
            Application.Current.Resources.MergedDictionaries[0] = resourceDictionary;
        }

        //注册
        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            //打开注册窗口
            Register register = new Register();
            register.Show();
            this.Close();
        }

        //登录
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            if(tbAccount.Text == null)
            {
                MessageBox.Show("账号不能为空！");
                return;
            }
            if(pbPassword.Password == null)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            if(tbAccount.Text.Length < 6)
            {
                MessageBox.Show("账号不能小于6位！");
                return;
            }
            if(pbPassword.Password.Length < 6)
            {
                MessageBox.Show("密码不能小于6位！");
                return;
            }

            string role = "student";
            if(rbRoleStudent.IsChecked == true)
            {
                role = "student";
            }
            else if (rbRoleTeacher.IsChecked == true)
            {
                role = "teacher";
            }
            else if(rbRoleAdmin.IsChecked == true)
            {
                role = "admin";
            }

            int flg = 0;
            using(SqlConnection conn = new SqlConnection(SqlHelper.ConnStr))
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from users where account = @account and password = @password and role = @role;";
                    cmd.Parameters.Add(new SqlParameter("@account", tbAccount.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@password", pbPassword.Password.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@role", role));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User.Account = reader[0].ToString();
                            User.Password = reader[1].ToString();
                            User.Name = reader[2].ToString();
                            User.Id = reader[3].ToString();
                            User.Gender = reader[4].ToString();
                            User.Mail = reader[5].ToString();
                            User.Specialty = reader[6].ToString();
                            User.Role = reader[7].ToString();
                            User.HeadImage = reader[8].ToString();
                            flg = 1;
                        }
                    }
                }
            }
            if (flg == 0)
            {
                MessageBox.Show("账号或密码输入错误！");
                return;
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            
        }
    }
}
