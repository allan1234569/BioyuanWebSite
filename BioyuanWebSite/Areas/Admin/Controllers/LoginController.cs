using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BLL;
using Models;

namespace MvcApplication1.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserInfo admin)
        {
            string name = admin.LoginName;
            string password = admin.LoginPwd;

            Models.UserInfo tAdmin = new UserManager().Login(admin);

            //tAdmin不为空
            if (tAdmin != null)
            {
                if (tAdmin.LoginPwd != password)
                {
                    ViewBag.LoginFailed = "登录失败：登录密码错误";
                    return View("Index");
                }

                //保存记录
                ViewBag.adminName = tAdmin;
                Session["AdminName"] = tAdmin.NickName;
                FormsAuthentication.SetAuthCookie(admin.NickName, false);
                return RedirectToRoute("Admin_default", new { controller = "Home", action = "Index" });
            }

            //tAdmin为空，用户不存在
            ViewBag.LoginFailed = "登录失败：用户不存在";

            return View("Index");
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();//注销
            Session["AdminName"] = null;//清除Session
            return View("Index");
        }
    }
}
