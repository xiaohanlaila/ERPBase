using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;


public partial class File : AjaxSmartBasePage1
{
    ERPBaseContext db = new ERPBaseContext();
    public string test()
    {
        SYS_FILE f = new SYS_FILE();
        f.FL_NAME = DateTime.Now.ToString("yyyy-MM-dd");
        db.SYS_FILE.Add(f);
        db.SaveChanges();
        return new AjaxSuccessResult("s").ToString();
    }

    /// <summary>
    /// 上传一个文件
    /// </summary>
    public void Upload()
    {
        HttpFileCollection files = Request.Files;
        AjaxErrorResult r = new AjaxErrorResult("error");

        string str_FD_ID = Request["FD_ID"];
        int FD_ID = 0;
        if (!string.IsNullOrEmpty(str_FD_ID))
        {
            FD_ID = Convert.ToInt32(str_FD_ID);
        }
        string BUSINESS_TYPE = Request["BUSINESS_TYPE"];
        if (string.IsNullOrEmpty(BUSINESS_TYPE))
        {
            BUSINESS_TYPE = string.Empty;
        }

        try
        {
            if (files == null)
            {
                Response.Write(r.ToString());
                return;
            }

            if (files.Count == 0)
            {
                Response.Write(r.ToString());
                return;
            }

            HttpPostedFile f = files[0];
            string extension = f.FileName.Substring(f.FileName.LastIndexOf(".") + 1);
            string str_path_v = "/file/upload/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string str_file_name = Guid.NewGuid().ToString("N") + "." + extension;
            string str_path_s = Server.MapPath("~" + str_path_v);
            //string str_file_old = "";
            //Dim i_index As Int32 = str_file_url_v.LastIndexOf("/")
            //Dim str_file_name As String = comm_fun.convert_string(str_file_url_v.Substring(i_index + 1, str_file_url_v.Length - i_index - 1))

            if (!System.IO.Directory.Exists(str_path_s))
            {
                System.IO.Directory.CreateDirectory(str_path_s);
            }
            string str_file_url_s = str_path_s + str_file_name;
            f.SaveAs(str_file_url_s);
            string str_file_url_v = str_path_v + str_file_name;

            //新增文件表
            SYS_FILE f1 = new SYS_FILE();
            f1.FL_ACTIVE = true;
            f1.FL_SIZE = f.ContentLength;
            f1.FL_EXTENSION = extension;
            f1.FL_NAME = get_old_filename(f.FileName);
            f1.FL_URL = str_file_url_v;
            f1.FL_CREATE_DATE = DateTime.Now;
            f1.FL_FD_ID = FD_ID;
            f1.FL_BUSINESS_TYPE = BUSINESS_TYPE;

            db.SYS_FILE.Add(f1);
            db.SaveChanges();
            Response.Write(new AjaxSuccessResult(f1.FL_ID.ToString()).ToString());
            return;
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            Response.Write(r.ToString());
            return;
        }

    }

    private string get_old_filename(string str_filename)
    {
        if (str_filename.IndexOf("/") > 0)
        {
            //有文件夹
            int i_index = str_filename.LastIndexOf("/");
            string filename = str_filename.Substring(i_index + 1, str_filename.Length - i_index - 1);
            return filename;
        }
        else
        {
            //没有文件夹
            return str_filename;
        }

        //Dim i_index As Int32 = str_file_url_v.LastIndexOf("/")
        //Dim str_file_name As String = comm_fun.convert_string(str_file_url_v.Substring(i_index + 1, str_file_url_v.Length - i_index - 1))

    }

    /// <summary>
    /// 下载一个文件
    /// </summary>
    public void DownLoad()
    {
        int FL_ID = Convert.ToInt32(Request["FL_ID"]);
        SYS_FILE f = db.SYS_FILE.Where(o => o.FL_ID == FL_ID).FirstOrDefault();
        string str_url_a = Server.MapPath("~" + f.FL_URL);

        System.IO.FileInfo file = new System.IO.FileInfo(str_url_a);
        Response.Clear();
        Response.Charset = "utf-8";//设置输出的编码
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + f.FL_NAME);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(file.FullName);
        Response.End();

    }

    /// <summary>
    /// 获取一个文件信息
    /// </summary>
    public void GetOneFile()
    {
        int FL_ID = Convert.ToInt32(Request["FL_ID"]);
        SYS_FILE f = db.SYS_FILE.Where(o => o.FL_ID == FL_ID).FirstOrDefault();
        string str_result = new AjaxObjectResult(f).ToString();
        Response.Write(str_result);
    }

    /// <summary>
    /// 获取一个文件夹信息
    /// </summary>
    public void GetOneFolder()
    {
        int FD_ID = Convert.ToInt32(Request["FD_ID"]);
        SYS_FOLDER f = db.SYS_FOLDER.Where(o => o.FD_ID == FD_ID).FirstOrDefault();
        string str_result = new AjaxObjectResult(f).ToString();
        Response.Write(str_result);
    }

    /// <summary>
    /// 按照文件夹获取文件
    /// </summary>
    public void GetFileByFolder()
    {
        int FD_ID = Convert.ToInt32(Request["FD_ID"]);
        List<SYS_FILE> list_file = db.SYS_FILE.Where(o => o.FL_FD_ID == FD_ID && o.FL_ACTIVE == true).ToList();
        string str_result = new AjaxObjectResult(list_file).ToString();
        Response.Write(str_result);
    }

    /// <summary>
    /// 新增文件夹
    /// </summary>
    public void CreateFolder()
    {
        //业务类型
        string BUSINESS_TYPE = Request["BUSINESS_TYPE"];
        if (string.IsNullOrEmpty(BUSINESS_TYPE))
        {
            BUSINESS_TYPE = string.Empty;
        }

        SYS_FOLDER fd = new SYS_FOLDER();
        fd.FD_BUSINESS_TYPE = BUSINESS_TYPE;
        fd.FD_CREATE_USER = 0;
        fd.FD_CREATE_DATE = DateTime.Now;
        db.SYS_FOLDER.Add(fd);
        db.SaveChanges();
        string str_result = new AjaxSuccessResult(fd.FD_ID.ToString()).ToString();
        Response.Write(str_result);
    }

    public string Uploads()
    {
        HttpFileCollection files = Request.Files;
        AjaxErrorResult r = new AjaxErrorResult("error");
        try
        {
            if (files == null)
            {
                return r.ToString();
            }

            if (files.Count == 0)
            {
                return r.ToString();
            }

            for (int i = 0; i < files.Count; i++)
            {

                HttpPostedFile f = files[i];
                string ex = f.FileName.Substring(f.FileName.LastIndexOf(".") + 1);
                string str_path = "/file/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                string str_file_name = Guid.NewGuid().ToString("N") + "." + ex;
                string str_path_absult = Server.MapPath("~" + str_path);
                if (!System.IO.Directory.Exists(str_path_absult))
                {
                    System.IO.Directory.CreateDirectory(str_path_absult);
                }

                string str_file_url = str_path_absult + str_file_name;

                f.SaveAs(str_file_url);

                return new AjaxSuccessResult(str_file_url).ToString();
            }

            return r.ToString();
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            return r.ToString();
        }

    }

    public void UpdateFolder()
    {
        int FD_ID = Convert.ToInt32(Request["FD_ID"]);
        update_folder(FD_ID);
        Response.Write(new AjaxSuccessResult("").ToString());
    }

    private void update_folder(int FD_ID)
    {
        List<SYS_FILE> list_file = db.SYS_FILE.Where(o => o.FL_FD_ID == FD_ID && o.FL_ACTIVE == true).ToList();
        SYS_FOLDER fd = db.SYS_FOLDER.Where(o => o.FD_ID == FD_ID).FirstOrDefault();
        fd.FD_FILE_COUNT = list_file.Count;
        fd.FD_FILE_SIZE = list_file.Sum(o => o.FL_SIZE);
        db.SaveChanges();
    }

    public void DeleteFile()
    {
        int FL_ID = Convert.ToInt32(Request["FL_ID"]);
        SYS_FILE f = db.SYS_FILE.Where(o => o.FL_ID == FL_ID).FirstOrDefault();
        if (f != null)
        {
            var FD_ID = f.FL_FD_ID;
            f.FL_ACTIVE = false;
            db.SaveChanges();
            update_folder(FD_ID);
            Response.Write(new AjaxSuccessResult("删除成功").ToString());
        }
        else
        {
            Response.Write(new AjaxSuccessResult("删除成功").ToString());
        }

    }


    public void PreViewFile()
    {
        List<string> list_image_ex = new List<string>();
        list_image_ex.Add("jpg");
        list_image_ex.Add("png");
        list_image_ex.Add("bmp");
        list_image_ex.Add("jpeg");

        int FL_ID = Convert.ToInt32(Request["FL_ID"]);
        SYS_FILE f = db.SYS_FILE.Where(o => o.FL_ID == FL_ID).FirstOrDefault();
        if (string.IsNullOrEmpty(f.FL_ICON_S))
        {
            if (list_image_ex.Contains(f.FL_EXTENSION))
            {
                //1查询图片的绝对路径
                string str_url = Server.MapPath("~" + f.FL_URL);
                //2生成图片的预览路径
                string str_url_new = SmallImageWidth(str_url, 200);
                //3赋值给FL_EXTENSION
                f.FL_ICON_S = str_url_new;
                //返回
                string str_result = new AjaxSuccessResult(str_url_new).ToString();
                Response.Write(str_result);
            }
            else
            {
                string str_result = new AjaxErrorResult("暂时不支持缩略图").ToString();
                Response.Write(str_result);
            }
        }
        else
        {
            string str_result = new AjaxSuccessResult(f.FL_ICON_S).ToString();
            Response.Write(str_result);
        }
    }

    private string SmallImageWidth(string str_ori_url, double target_width)
    {
        if (System.IO.File.Exists(str_ori_url))
        {
            System.IO.FileInfo f = new System.IO.FileInfo(str_ori_url);
            string str_path_new_v = "/file/upload/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string str_file_name_new = Guid.NewGuid().ToString("N") + "." + f.Name.Substring(f.Name.LastIndexOf(".") + 1);
            string str_url_new_v = str_path_new_v + str_file_name_new;
            string str_url_new_a = Server.MapPath("~" + str_url_new_v);
            if (System.IO.File.Exists(str_url_new_a))//已经存在文件
            {
                return str_url_new_v;
            }

            System.Drawing.Image img_ori = System.Drawing.Image.FromFile(str_ori_url);
            //定义缩略图片宽度和高度  
            int i_width_new = Convert.ToInt32(target_width);
            int i_height_new = Convert.ToInt32(img_ori.Height / (img_ori.Width / target_width));

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(i_width_new, i_height_new);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            //设置缩略图片质量
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graphics.DrawImage(img_ori, 0, 0, i_width_new, i_height_new);

            // 保存缩略图片
            bitmap.Save(str_url_new_a);
            img_ori.Dispose();
            return str_url_new_v;
        }
        return str_ori_url;

    }

}
