using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnect
    {
        private static SqlConnection conn;
        private static string strconn;
        public static string DirectoryConnect { get { return strconn; } set { strconn = value; } }
        public static SqlConnection SqlConnect { get { return conn; } }

        //tao ket noi vs sql
        public static SqlConnection Connect()
        {
            try
            {
                conn = null;
                string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + strconn + ";Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                conn = con;
                return con;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        //Dua du lieu vao bang
        public static DataTable GetData(string proc)
        {
            try
            {
                if (conn == null || conn.State == ConnectionState.Closed) Connect();
                if (conn == null) return null;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(proc, conn);
                da.Fill(dt);
                return dt;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        //thuc hien thu tuc
        public static int ExecuteNonQuery(string proc, SqlParameter[] para)
        {
            //try
            //{
            if (conn == null || conn.State == ConnectionState.Closed) Connect();
            if (conn == null) return 0;
            Connect();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = proc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (para != null)
                cmd.Parameters.AddRange(para);

            int val = cmd.ExecuteNonQuery();
            //conn.Close();
            return val;
            //}
            //catch (SqlException)
            //{
            //    return 0;
            //}
        }
    }
}
