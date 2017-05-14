using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace CGit.Src.Util
{
    public static class EmailUtil
    {
        /// <summary>
        /// 发送邮件功能
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="body">正文</param>
        /// <param name="title">标题</param>
        /// <param name="Fname">发件人姓名</param>
        /// <returns></returns>
        public static bool SentMail(string to, string body, string title, string Fname)
        {
            bool retrunBool = false;
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string strFromEmail = "1137889088@qq.com";//你的邮箱
            string strEmailPassword = "ylmqrxctxycybagj";//QQPOP3/SMTP服务码
            try
            {
                mail.From = new MailAddress("" + Fname + "<" + strFromEmail + ">");
                mail.To.Add(new MailAddress(to));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                mail.Body = body;//正文
                mail.Subject = title;//标题
                smtp.EnableSsl = true;//不设置    QQ发送会出错
                smtp.Host = "smtp.qq.com";//可以换成163
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(strFromEmail, strEmailPassword);
                //发送邮件
                try
                {
                    smtp.Send(mail);   //同步发送
                }
                catch (Exception ex)
                {
                   
                }
                retrunBool = true;
            }
            catch (Exception ex)
            {
                retrunBool = false;
            }
            //smtp.SendAsync(mail, mail.To); //异步发送 （异步发送时页面上要加上Async="true" ）
            return retrunBool;
        }
    }
}