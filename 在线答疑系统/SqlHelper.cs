using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QuestionAnsweringSystem
{
    class SqlHelper
    {
        //把App.config里面的字符串引用过来,需要引用程序集 System.Configuration
        static public string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        /// <summary>
        /// 获取数据库受影响的行数
        /// </summary>
        /// <param name="sql">可执行的sql语句</param>
        /// <param name="sqlParameters">sql语句里面的参数</param>
        /// <returns>返回数据库受影响的行数</returns>
        public static int ExecuteNonQurey(string sql,params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlParameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 查询一个数据时使用
        /// </summary>
        /// <param name="sql">可执行的sql语句</param>
        /// <param name="sqlParameters">sql语句里面的参数</param>
        /// <returns>只返回查询结果的第一行第一列的那个数据</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlParameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 获取一个数据集
        /// </summary>
        /// <param name="sql">可执行的sql语句</param>
        /// <param name="sqlParameters">sql语句里面的参数</param>
        /// <returns>返回一个数据集</returns>
        public static DataSet GetDataSet(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlParameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }

        /// <summary>
        /// 获取一个数据表
        /// </summary>
        /// <param name="sql">可执行的sql语句</param>
        /// <param name="sqlParameters">sql语句里面的参数</param>
        /// <returns>返回一个数据表</returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlParameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}
