using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace QuestionAnsweringSystem
{

    //                          _ooOoo_                               //
    //                         o8888888o                              //
    //                         88" . "88                              //
    //                         (| ^_^ |)                              //
    //                         O\  =  /O                              //
    //                      ____/`---'\____                           //
    //                    .'  \\|     |//  `.                         //
    //                   /  \\|||  :  |||//  \                        //
    //                  /  _||||| -:- |||||-  \                       //
    //                  |   | \\\  -  /// |   |                       //
    //                  | \_|  ''\---/''  |   |                       //
    //                  \  .-\__  `-`  ___/-. /                       //
    //                ___`. .'  /--.--\  `. . ___                     //
    //              ."" '<  `.___\_<|>_/___.'  >'"".                  //
    //            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
    //            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
    //      ========`-.____`-.___\_____/___.-`____.-'========         //
    //                           `=---='                              //
    //      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
    //                  佛祖保佑       永不宕机     永无BUG           //
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //oss所需的数据
        private static string endpoint = "oss-cn-beijing.aliyuncs.com";
        private static string accessKeyId = "LTAI4GFSAC36KGJb4q3H1ugL";
        private static string accessKeySecret = "8t1D7Lti34bb5Gjf6FNJlj5yCHqvJs";
        private static string bucketName = "images0506";
        public MainWindow()
        {
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
            System.Windows.Application.Current.Resources.MergedDictionaries[0] = resourceDictionary;
        }

        // 获取时间戳
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        //头像的上传
        private void headshot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow;
            //选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "图像文件(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                var suffix = file.Substring(file.LastIndexOf('.'));//截取后缀名
                string key = "headImage/" + GetTimeStamp() + suffix;
                OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                client.PutObject(bucketName, key, file);
                string url = "http://" + bucketName + "." + endpoint + "/" + key;
                User.HeadImage = url;
            }
            //保存到数据库
            string sql = "update users set headimage = @headimage where account = @account and role = @role";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@headimage",User.HeadImage),
                new SqlParameter("@account",User.Account),
                new SqlParameter("@role",User.Role),
            };
            SqlHelper.ExecuteNonQurey(sql, sqlParameters);
        }

        private void btUpload_Click(object sender, RoutedEventArgs e)
        {
            string sql = "insert into question(id,qcontent) values(@id,@qcontent)";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@id",User.Id),
                new SqlParameter("@qcontent",tbQuestion.Text),
            };
            if (SqlHelper.ExecuteNonQurey(sql, sqlParameters) == 1)
            {
                tbQuestion.Text = "";
                System.Windows.MessageBox.Show("问题提交成功！");
            }
        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            //判断几个查询的textbox里面是否输入内容  构建sql语句
            string sql = "select headimage,qcontent,role,name,qid from question,users where question.id = users.id";
            bool[] isSelect = {false,false,false,false};
            int count = 0;
            if (!string.IsNullOrEmpty(tbSelectId.Text))
            {
                sql += " and question.id  = @id ";
                isSelect[0] = true;
                count++;
            }
            if (!string.IsNullOrEmpty(tbSelectName.Text))
            {
                sql += " and name = @name ";
                isSelect[1] = true;
                count++;
            }
            if (!string.IsNullOrEmpty(tbSelectSpecialty.Text))
            {
                sql += " and specialty = @specialty";
                isSelect[2] = true;
                count++;
            }
            if (!string.IsNullOrEmpty(tbSelectKeyword.Text))
            {
                sql += " and qcontent like @keyword";
                isSelect[3] = true;
                count++;
            }      
            SqlParameter[] sqlparameters = new SqlParameter[count];
            count = 0;
            if (isSelect[0])
            {
                sqlparameters[count++] = new SqlParameter("@id", tbSelectId.Text);
            }
            if (isSelect[1])
            {
                sqlparameters[count++] = new SqlParameter("@Name", tbSelectName.Text);
            }
            if (isSelect[2])
            {
                sqlparameters[count++] = new SqlParameter("@Specialty", tbSelectSpecialty.Text);
            }
            if (isSelect[3])
            {
                sqlparameters[count++] = new SqlParameter("@Keyword","%" + tbSelectKeyword.Text + "%");
            }

            DataTable dt = new DataTable();
            dt = SqlHelper.GetDataTable(sql, sqlparameters);
            LbAnswering.ItemsSource = dt.DefaultView;         
        }

        private void LbAnswering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbAnswering.SelectedItem == null)
                return;
            DataRowView dataRowView = LbAnswering.SelectedItem as DataRowView;
            Answering answering = new Answering(Convert.ToInt32(dataRowView["qid"].ToString()));
            answering.Show();
        }
            

        private void LbMyQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbMyQuestion.SelectedItem == null)
                return;
            DataRowView dataRowView = LbMyQuestion.SelectedItem as DataRowView;
            MyQuestionAnswer myQuestionAnswer = new MyQuestionAnswer(Convert.ToInt32(dataRowView["qid"].ToString()));
            myQuestionAnswer.Show();
            
        }
        private void TabItem_Initialized(object sender, EventArgs e)
        {
            string sql = "select headimage,qcontent,role,name,question.qid from question,users " +
                " where question.id = users.id and question.id = @id";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@id",User.Id),
            };
            DataTable dt = new DataTable();
            dt = SqlHelper.GetDataTable(sql, sqlParameters);
            LbMyQuestion.ItemsSource = dt.DefaultView;
        }
        private void tbChange_Click(object sender, RoutedEventArgs e)
        {
            User.Name = this.tbName.Text;
            User.Id = this.tbId.Text;
            User.Gender = this.tbGender.Text;
            User.Mail = this.tbMail.Text;
            User.Specialty = this.tbSpecialty.Text;
            string sql = "update users set name = @name,id = @id,gender = @gender,mail = @mail,specialty = @specialty where account = @account and role = @role";
            SqlParameter[] parameters =
            {
                    new SqlParameter("@name",User.Name),
                    new SqlParameter("@id",User.Id),
                    new SqlParameter("@gender",User.Gender),
                    new SqlParameter("@mail",User.Mail),
                    new SqlParameter("@specialty",User.Specialty),
                    new SqlParameter("@account",User.Account),
                    new SqlParameter("@role",User.Role),
            };
            if (SqlHelper.ExecuteNonQurey(sql, parameters) == 1)
            {
                System.Windows.MessageBox.Show("修改成功！");
            }
            else
            {
                System.Windows.MessageBox.Show("修改失败！");
            }
        }

        private void TabItem_Initialized_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@id",User.Id),
            };
            dt = SqlHelper.GetDataTable("select * from users where id = @id", sqlParameters);
            DataRow dataRow = dt.Rows[0];
            if(dataRow["gender"].ToString() == "Man")
            {
                man.IsSelected = true;
            }
            else
            {
                woman.IsSelected = true;
            }
            PersonalInformation.DataContext = dt;

        }

        private void tbModSelect_Click(object sender, RoutedEventArgs e)
        {
            if (User.Role != "admin")
            {
                System.Windows.MessageBox.Show("你不具备管理员的权限！");
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@account",User.Account),
                };
                dt = SqlHelper.GetDataTable("select * from users where account = @account", sqlParameters);
                Administration.DataContext = dt;
            }
            
        }

        private void btModification_Click(object sender, RoutedEventArgs e)
        {
            string sql = "update users set password = @password, name = @name,id = @id,gender = @gender,mail = @mail,specialty = @specialty where account = @account and role = @role";
            SqlParameter[] parameters =
            {
                    new SqlParameter("@password",tbModPassword.Text),
                    new SqlParameter("@name",tbModName.Text),
                    new SqlParameter("@id",tbModId.Text),
                    new SqlParameter("@gender",tbModGender.Text),
                    new SqlParameter("@mail",tbModMail.Text),
                    new SqlParameter("@specialty",tbModSpecialty.Text),
                    new SqlParameter("@account",tbModAccount.Text),
                    new SqlParameter("@role",tbModRole.Text),
            };
            if (SqlHelper.ExecuteNonQurey(sql, parameters) == 1)
            {
                System.Windows.MessageBox.Show("修改成功！");
            }
            else
            {
                System.Windows.MessageBox.Show("修改失败！");
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public static uint SND_ASYNC = 0x0001;
        public static uint SND_FILENAME = 0x00020000;
        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand, string lpstrReturnString, uint uReturnLength, uint hWndCallback);
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {

            mciSendString(@"open ""E:\WPF\在线答疑系统\在线答疑系统\music.mp3"" alias temp_alias", null, 0, 0); //音乐文件
            mciSendString("play temp_alias", null, 0, 0);
            
        }

        private void TabItem_LostFocus(object sender, RoutedEventArgs e)
        {
            mciSendString(@"close temp_alias", null, 0, 0);
        }
    }
}

