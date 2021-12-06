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
    /// Answering.xaml 的交互逻辑
    /// </summary>
    public partial class Answering : Window
    {
        private int qid = 0;
        public Answering(int _qid)
        {
            InitializeComponent();
            qid = _qid;
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@qid",qid),
            };
            tbQuestion.Text = SqlHelper.ExecuteScalar("select qcontent from question where qid = @qid", sqlParameters).ToString();
        }

        private void btUpLoad_Click(object sender, RoutedEventArgs e)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@qid",qid),
                new SqlParameter("@id",User.Id),
                new SqlParameter("@acontent",tbAnswer.Text),
            };
            SqlHelper.ExecuteNonQurey("insert into answering(qid,id,acontent) values(@qid,@id,@acontent)", sqlParameters);
            MessageBox.Show("回复成功！");
            this.Close();
        }
    }
}
