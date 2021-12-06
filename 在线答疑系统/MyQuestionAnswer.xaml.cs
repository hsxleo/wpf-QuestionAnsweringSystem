using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

namespace QuestionAnsweringSystem
{
    /// <summary>
    /// MyQuestionAnswer.xaml 的交互逻辑
    /// </summary>
    public partial class MyQuestionAnswer : Window
    {
        private int qid = 0;
        public MyQuestionAnswer(int _qid)
        {
            InitializeComponent();
            qid = _qid;
            string sql = "select headimage,acontent,role,name from users,answering,question " +
                "where question.qid = answering.qid and users.id = answering.id and answering.qid = @qid";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@qid",qid),
            };
            DataTable dt = new DataTable();
            dt = SqlHelper.GetDataTable(sql, sqlParameters);
            LbMyQuestionAnswer.ItemsSource = dt.DefaultView;
        }
    }
}
