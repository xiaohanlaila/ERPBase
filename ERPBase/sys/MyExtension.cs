using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;


public static class MyExtension
{
    public static string ToJsonString(this DataTable dt)
    {
        //为了不想用太多的dll，暂时用微软自带序列化方法
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in dt.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    //public static DataTable Where(this DataTable dt, string Fiter)
    //{
    //    DataView dv = dt.DefaultView;
    //    dv.RowFilter = Fiter;
    //    DataTable dt_result = dv.ToTable();
    //    return dt_result;
    //}

    //public static string ToTimeStamp(this DateTime dt)
    //{
    //    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
    //    DateTime nowTime = DateTime.Now;
    //    long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
    //    return (unixTime / 1000).ToString();
    //}

    //public static void Clear(this System.Text.StringBuilder str)
    //{
    //    str.Length = 0;
    //}

    public static string ToColumnName(this string str_column_name)
    {
        if (str_column_name.LastIndexOf("as") < 0)
        {
            return str_column_name;
        }
        int i_start = str_column_name.LastIndexOf("as") + 2;
        str_column_name = str_column_name.Substring(i_start, str_column_name.Length - i_start).Trim();
        return str_column_name;
    }

}


