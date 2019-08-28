using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace ERPBase
{
    public class comm_fun
    {

        public static void CloseConnection(SqlConnection cn)
        {
            try
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    cn.Dispose();
                }
            }
            catch
            {
            }
        }



        //public static bool convert_to_boolean(object Mobj_str)
        //{
        //    if (Mobj_str == DBNull.Value)
        //    {
        //        return false;
        //    }

        //    if (Mobj_str == null || Mobj_str.ToString().Trim().Equals(""))
        //    {
        //        return false;
        //    }


        //    if (Mobj_str.ToString() == "1" || Mobj_str.ToString().ToUpper() == "true".ToUpper() || Mobj_str.ToString() == bool.TrueString)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }



        //}

        //public static int convert_to_int(object Mobj_str)
        //{
        //    if (Mobj_str == DBNull.Value)
        //    {
        //        return 0;
        //    }

        //    if (string.IsNullOrEmpty(Mobj_str.ToString()))
        //    {
        //        return 0;
        //    }

        //    return Convert.ToInt32(Mobj_str.ToString());
        //}

        public static SqlConnection get_cn()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["server"]);
        }

        public static bool ExecuteNonQuery(string Mstrsql, SqlConnection cn, IEnumerable<SqlParameter> list = null)
        {
            SqlCommand cmd=new SqlCommand(Mstrsql, cn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 60;
            if (list != null)
            {

                foreach (SqlParameter item in list)
                {
                    if (item != null)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
            }
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
                if (cmd.ExecuteNonQuery() > 0)
                {
                    if (true)
                    {
                        cmd.Dispose();
                        cn.Close();
                    }
                    cmd.Parameters.Clear();
                    return true;
                }
                else
                {
                    if (true)
                    {
                        cmd.Dispose();
                        cn.Close();
                    }
                    cmd.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {

                if (true)
                {
                    cmd.Dispose();
                    cn.Close();
                }
                throw ex;
            }
        }

        public static string ExecuteScalar(string Mstrsql, SqlConnection cn, IEnumerable<SqlParameter> list = null)
        {
            SqlCommand cmd = new SqlCommand(Mstrsql, cn);
            if (list != null)
            {
                foreach (SqlParameter item in list)
                {
                    if (item != null)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
            }
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            string result;
            try
            {
                string text = cmd.ExecuteScalar().ToString();
                cmd.Parameters.Clear();
                cmd.Dispose();
                result = text;
            }
            catch (Exception ex)
            {
                if (true)
                {
                    cmd.Dispose();
                    cn.Close();
                }
                throw ex;
            }
            return result;
        }

        public static DataTable GetDatatable(string Mstrsql, SqlConnection cn, IEnumerable<SqlParameter> list = null)
        {
            DataSet dataSet = new DataSet();
            dataSet = comm_fun.getds(Mstrsql, cn, list, "");
            return dataSet.Tables[0];
        }

        private static DataSet getds(string Mstrsql, SqlConnection cn, IEnumerable<SqlParameter> list = null, string mStrTableName = "", SqlTransaction myTrans = null)
        {
            SqlCommand cmd;
            if (myTrans == null)
            {
                cmd = new SqlCommand(Mstrsql, cn);
            }
            else
            {
                cmd = new SqlCommand(Mstrsql, cn, myTrans);
            }
            if (list != null)
            {
                foreach (SqlParameter item in list)
                {
                    if (item != null)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(mStrTableName))
            {
                sda.Fill(ds);
            }
            else
            {
                sda.Fill(ds, mStrTableName);
            }
            cmd.Parameters.Clear();
            return ds;
        }

        public static void WriteLog(string str_content)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(@"~\LogFile");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                System.IO.File.AppendAllText(path + @"\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", DateTime.Now.ToString("yyyyMMdd_HHmmss") + "   " + str_content + Environment.NewLine);
            }
            catch
            {

            }

        }

    }

}

