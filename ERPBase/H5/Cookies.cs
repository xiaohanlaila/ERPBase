using ERPBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;


public class Cookies
{
    public string this[string key]
    {
        get
        {
            if (HttpContext.Current.Request.Cookies[SystemName + "_" + key] == null)
            {
                return null;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.Cookies[SystemName + "_" + key].Value);
            }
        }
        set
        {
            System.Web.HttpContext.Current.Response.Cookies[SystemName + "_" + key].Value = System.Web.HttpContext.Current.Server.UrlEncode(value);
        }
    }

    public static string SystemName = "sp";

    public static Cookies cookies { get { return new Cookies(); } }

    public static string Login(string str_code)
    {
        comm_fun.WriteLog("str_code=" + str_code);
        string str_userID = string.Empty;
        try
        {
            Password_Encrypt_ASC.Password_Encrypt_ASC encry = new Password_Encrypt_ASC.Password_Encrypt_ASC();
            string str_encry = encry.get_password_ASC(HttpUtility.UrlDecode(str_code));
            str_userID = str_encry.Substring(0, str_encry.IndexOf("/"));
            Cookies.UserCode = str_userID;
            comm_fun.WriteLog("str_userID=" + str_userID);
        }
        catch (Exception)
        {
            try
            {
                Password_Encrypt_ASC.Password_Encrypt_ASC encry = new Password_Encrypt_ASC.Password_Encrypt_ASC();
                string str_encry = encry.get_password_ASC(str_code);
                str_userID = str_encry.Substring(0, str_encry.IndexOf("/"));
                Cookies.UserCode = str_userID;
                comm_fun.WriteLog("str_userID1=" + str_userID);
            }
            catch
            {
                comm_fun.WriteLog("ex");
            }
        }
        return str_userID;
    }

    #region 普通属性
    public static string UserCode
    {
        get
        {
            //return "272";
            //return "79";
            //return "11";
            return cookies["UserCode"];
        }
        set
        {
            cookies["UserCode"] = value;
        }
    }
    #endregion
}