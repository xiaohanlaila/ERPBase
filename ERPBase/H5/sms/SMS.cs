using Qcloud.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    /// <summary>
    /// 腾讯云短信发送
    /// </summary>
    public class SMS
    {
        //1=腾讯云创建账号
        //2=开通短信发送并且得到sdkappid与appkey
        //3=申请签名与模板
        //4=通过模板发送消息

        public static int sdkappid = 1400140914;
        public static string appkey = "355f08f7f48528ab8c7bd21ebbcaf8d5";

        public static bool Send(string phone, int templateId, List<string> parameter)
        {
            try
            {
                SmsSingleSenderResult singleResult;
                SmsSingleSender singleSender = new SmsSingleSender(sdkappid, appkey);
                singleResult = singleSender.SendWithParam("86", phone, templateId, parameter, "", "", "");
                if (singleResult.errmsg == "OK")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}