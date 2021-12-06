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
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        public Register()
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
        //注册账号
        private void tbRegister_Click(object sender, RoutedEventArgs e)
        {
            if(tbAccount.Text.Length < 6)
            {
                MessageBox.Show("账号不能小于6位");
                return;
            }
            if(pbPassword.Password.Length < 6)
            {
                MessageBox.Show("密码不能小于6位");
                return;
            }
            if(tbName.Text.Length <= 0)
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            if(tbId.Text.Length <= 0)
            {
                MessageBox.Show("学号/教工号不能为空");
                return;
            }
            if(tbGender.SelectedIndex == -1)
            {
                MessageBox.Show("性别不能为空");
                return;
            }
            if(tbSpecialty.Text.Length <= 0)
            {
                MessageBox.Show("专业不能为空");
                return;
            }
            if (rbRoleStudent.IsChecked == false && rbRoleTeacher.IsChecked == false && rbRoleAdmin.IsChecked == false)
            {
                MessageBox.Show("请选择身份");
                return;
            }
            if(pbPassword.Password != pbConfirm.Password)
            {
                MessageBox.Show("两次密码输入不同！");
                return;
            }


            //赋值
            User.Role = "student";
            if (rbRoleStudent.IsChecked == true)
            {
                User.Role = "student";
            }
            else if (rbRoleTeacher.IsChecked == true)
            {
                User.Role = "teacher";
            }
            else if (rbRoleAdmin.IsChecked == true)
            {
                User.Role = "admin";
            }
            string sql = "select * from users where account = @account and role = @role";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@account",tbAccount.Text),
                new SqlParameter("@role",User.Role),
            };
            if(SqlHelper.ExecuteNonQurey(sql,sqlParameters) >= 1)
            {
                MessageBox.Show("该用户已存在");
            }
            else
            {
                User.Account = tbAccount.Text;
                User.Password = pbPassword.Password;
                User.Gender = tbGender.Text;
                User.Id = tbId.Text;
                User.Mail = tbMail.Text;
                User.Name = tbName.Text;
                User.Specialty = tbSpecialty.Text;
                sql = "insert into users(account,password,name,id,gender,mail,specialty,role,headimage) " +
                    "values(@account,@password,@name,@id,@gender,@mail,@specialty,@role,@headimage)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@account",User.Account),
                    new SqlParameter("@password",User.Password),
                    new SqlParameter("@name",User.Name),
                    new SqlParameter("@id",User.Id),
                    new SqlParameter("@gender",User.Gender),
                    new SqlParameter("@mail",User.Mail),
                    new SqlParameter("@specialty",User.Specialty),
                    new SqlParameter("@role",User.Role),
                    new SqlParameter("@headimage",User.HeadImage),
                };
                if(SqlHelper.ExecuteNonQurey(sql,parameters) == 1)
                {
                    MessageBox.Show("注册成功！");
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("存入数据库失败！");
                }
                
            }
        }
    }
}
