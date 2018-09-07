using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;
namespace MvcApplication1.Controllers
{
    public class UserController : Controller
    {
        private string urlReferrer = "";
        #region 登录

        public ActionResult Login()
        {
            var urlReferrer = Request.UrlReferrer;

            if (urlReferrer.LocalPath == "/User/Login" | urlReferrer.LocalPath == "/User/Register" | urlReferrer.LocalPath == "/User/ResetPassword")
            {
             
            }
            else
            {
                TempData["urlReferrer"] = urlReferrer == null ? "" : urlReferrer.ToString();
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public ActionResult LoginIn(UserInfo user)
        {
            UserInfo myUser = null;

           
            if (user.LoginName != string.Empty && user.LoginPwd != string.Empty)
            {
                myUser = new UserManager().Login(user);
            }

            if (myUser == null)
            {
                TempData["LoginFailed"] = "登录失败，请检查您的账号和密码是否正确！";
                return View("Login");
            }
            else
            {
                var urlReferrer = TempData["urlReferrer"];

                return Redirect(urlReferrer == null ? "/Home/Index" : urlReferrer.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// 
        /// <returns></returns>
        [HttpPost]
        public string RegisterUser(UserInfo user)
        {
            user.UserRank = 1;//普通用户
            user.State = 1;

            //RegisterType val = new UserManager().CheckLoginName(user.LoginName);
            //if (val != RegisterType.检查通过)
            //{
            //    return val.ToString();
            //}

            //val = new UserManager().CheckMail(user.UserEmail);
            //if (val != RegisterType.检查通过)
            //{
            //    return val.ToString();
            //}


            RegisterType RegisVal = new UserManager().AddUser(user);

            string content =
@"<body>
	<p>bioyuan账户</p>
	<h1>用户注册代码</h1>
	<div>
		<p>尊敬的用户你好，恭喜您注册成为上海标源的一员，请使用以下账号登录上海标源官方网站</p>
		<div>
			<p >您的账号为：<a class='account' href='#'>" + user.LoginName + @"</a></p>
			<p>您的密码为：<a class='account' href='#'>" + user.LoginPwd + @"</a></p>
			<p>如果你忘记标源账户密码，请登录标源官网使用你注册使用的邮箱<a class='mail' href='#'>443200254@qq.com</a> 重置您的密码</p>
		</div>
		<p>请妥善保管好您的账号信息，切误泄漏</p>
		<p>谢谢！！</p>
		<p>bioyuan账户团队</p>
	</div>
</body>
<style>
	h1 {color:#0094ff;}
	.logo {text-align:center;}
	p {padding-bottom:10px;}
	p a {color:#0094ff;}
	.account {text-decoration:none;}
</style>";

            if (RegisVal == RegisterType.注册成功)
            {
                SendEmail(user.UserEmail, "用户注册成功！！！", content);
            }

            return RegisVal.ToString();
        }


        public string ForgetPassword(UserInfo user)
        {
            string LoginName = user.LoginName;
            string EmailAdress = user.UserEmail;

            string url = "http://localhost:1440/User/ResetPassword?id=" + new UserManager().GetId(EmailAdress).ToString();

            string content =
@"<body>
	<p>bioyuan账户</p>
	<h1>用户密码重置代码</h1>
	<div>
		<p>尊敬的用户你好，由于您忘记标源账户密码</p>
		<div>
            <p>请点击此链接重置您的密码<a href='" + url + @"'>点击此处</a></p>
			<p style='color:red;'>此链接有效时间5分钟，请您及时修改您的密码</p>
		</div>
		<p>请妥善保管好您的账号信息，切误泄漏</p>
		<p>谢谢！！</p>
		<p>bioyuan账户团队</p>
	</div>
</body>
<style>
	h1 {color:#0094ff;}
	.logo {text-align:center;}
	p {padding-bottom:10px;}
	p a {color:#0094ff;}
	.account {text-decoration:none;}
</style>";

            string ret = SendEmail(EmailAdress, "密码重置！！！", content);

            return ret;
        }

        [HttpPost]
        public ActionResult EmailExists(string UserEmail)
        {
            bool ret = new UserManager().EmailExists(UserEmail);

            message msg = new message();
            msg.valid = !ret;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserExists(string LoginName)
        {
            bool ret = new UserManager().LoginNameExists(LoginName);

            message msg = new message();
            msg.valid = !ret;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public string SendEmail(string reveiver, string subject, string body)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //发送邮箱服务器
            client.Host = "smtp.exmail.qq.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("biaoyuan@bioyuan.cn", "#Biaoyuan2018");//发送方的邮箱账号，密码
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress("biaoyuan@bioyuan.cn");
            message.To.Add(reveiver);//接收邮箱地址
            message.Subject = subject;  //主题
            message.Body = body;   //内容
            message.BodyEncoding = System.Text.Encoding.UTF8;  //编码方式
            message.IsBodyHtml = true;//设置HTML格式

            message.IsBodyHtml = true;

            try
            {
                client.Send(message);

                return "ok";
            }
            catch
            {
                return "err1230";
            }
        }

        #endregion


        //
        // GET: /User/
        public ActionResult ResetPassword()
        {

            return View();

            //string id = null;
            //if (Request.Params["id"] != null)
            //{
            //    id = Request.Params["id"].ToString();

            //    bool valid = new UserManager().ResetpwdUrlValid(id);
            //    if (valid)
            //    {
            //        return View("ResetPassword");
            //    }
            //    else
            //    {
            //        return View("ResetPasswordFaild");
            //    }
                
            //}
            //else
            //{
            //    return View("ResetPassword1");
            //}

        }

        public ActionResult RetrievePassword()
        {
            return View();
        }

    }
}
