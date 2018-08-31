using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Models;
using BLL;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="id">图片名称</param>
        /// <returns></returns>
        public FileResult ShowImage(string id)
        {

            string filename = id;

            if (filename == null || filename == "")
            {
                return File(" ", "image/jpg");
            }

            try
            {
                string _path = Server.MapPath("/Data/Images/") + "" + filename;
                FileStream fs = new FileStream(_path, FileMode.Open);
                byte[] byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();

                return File(byData, "image/jpg");
            }
            catch (IOException e)
            {
                return File(" ", "image/jpg");
            }

        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }




        #region 关于我们


        /// <summary>
        /// 关于页动作
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUs()
        {
            string id = "";
            if (RouteData.Values.ContainsKey("id"))
            {
                id = RouteData.Values["id"].ToString();
            }

            string[] sub = id.Split('=');

            ViewBag.partialViewAction = sub[1];
            ViewBag.TitleCategory = sub[1];

            return View("AboutUs");
        }


        public ActionResult Introduction()
        {
            Introduction introduction = new IntroductionManage().GetIntroductionDetail();

            ViewBag.Title = "公司简介";

            return View("Introduction", introduction);
        }

        public ActionResult News()
        {
            ViewBag.Title = "新闻资讯";

            string id = null;

            string page = null;

            if (Request.Params["page"] != null)
            {
                page = Request.Params["page"].ToString();
            }

            if (Request.Params["id"] != null)
            {
                id = Request.Params["id"].ToString();
            }

            if (id != null)
            {
                //翻页查询
                List<News> list = new NewsManager().GetNews(id);
            }
            else
            {

            }


            return View();
        }

        public ActionResult NewsShow()
        {
            string id = Request.Params["id"].ToString();
            List<News> list = new NewsManager().GetNews("");

            ViewBag.Title = "新闻资讯";

            return View();
        }


        #endregion



        public ActionResult Contact()
        {
            return View("Contact");
        }

        /// <summary>
        /// 
        /// 质控品类动作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Products()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            if (!RouteData.Values.ContainsKey("id"))
            {
                ViewBag.partial_view = "Products";
                ViewBag.partialViewAction = "GetLaboratoryQuailtyControlProducts";
            }
            else
            {
                string id = RouteData.Values["id"].ToString();//获取id值

                d = ParseUrlId(id);//解析id内容

                string category = "";
                //ViewBag.product_category = productCatgory;

                if (d.ContainsKey("pro"))
                {
                    if (d["pro"].CompareTo("lqc") == 0)//室内质控品
                    {
                        category = "LaboratoryQuailtyControl";
                        ViewBag.panel_title = "室内质控品";
                    }
                    else if (d["pro"].CompareTo("iqc") == 0)//室间质评品
                    {
                        category = "InterroomQualityControl";
                        ViewBag.panel_title = "室间质评品";
                    }
                    else if (d["pro"].CompareTo("rm") == 0)//标准物质
                    {
                        category = "Material";
                        ViewBag.panel_title = "标准物质";
                    }
                    else//默认显示室内质控品
                    {
                        category = "LaboratoryQuailtyControl";
                        ViewBag.panel_title = "室内质控品";
                    }
                }
                else
                {
                    //默认显示室内质控品

                    category = "LaboratoryQuailtyControl";
                    ViewBag.panel_title = "室内质控品";

                }

                if (d.ContainsKey("cat") && d["cat"].ToString() != string.Empty)
                {
                    //产品分部视图切换
                    if (d["cat"].CompareTo("p") == 0)//质控品
                    {
                        ViewBag.partialViewAction = "Get" + category + "Products";
                    }
                    else if (d["cat"].CompareTo("pm") == 0)//更多
                    {
                        ViewBag.partialViewAction = "Get" + category + "ProductMore";
                    }
                    else if (d["cat"].CompareTo("ps") == 0)//专业
                    {
                        ViewBag.partialViewAction = "Get" + category + "ProductSeries";
                    }
                    else if (d["cat"].CompareTo("pd") == 0)//明细
                    {
                        ViewBag.partialViewAction = "Get" + category + "ProductDetail";
                    }
                }
                else
                {
                    ViewBag.partialViewAction = "Get" + category + "Products";
                }

                if (d.ContainsKey("cat_nama") && d["cat_name"].ToString() != string.Empty)
                {
                    ViewBag.catName = d["cat_name"].ToString();
                }

            }


            return View("ProductsContent");
        }


        #region 室内质控品动作
        /// <summary>
        /// 室内质控品动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLaboratoryQuailtyControlProducts()
        {
            List<LaboratoryQuailtyControl> lqcs = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControls(true);
            ViewBag.lqcs = lqcs;
            ViewBag.pro = "lqc";
            ViewBag.panel_title = "室内质控品";

            return PartialView("Products");
        }

        /// <summary>
        /// 室内质控品更多动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLaboratoryQuailtyControlProductMore()
        {
            ViewBag.productSeries = new LaboratoryQuailtyControlManager().GetCategorys();
            ViewBag.pro = "lqc";
            ViewBag.panel_title = "室内质控品";
            ViewBag.list_title = "产品系列";



            return PartialView("ProductMore");
        }

        /// <summary>
        /// 室内质控品专业动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLaboratoryQuailtyControlProductSeries()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string categoryName = "";
            d = ParseUrlId(id);

            List<LaboratoryQuailtyControl> lqcs = null;

            if (GetUrlFiled(d, "cat_name") != "")
            {
                categoryName = GetUrlFiled(d, "cat_name");

                lqcs = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControlsByCategory(categoryName);
            }

            ViewBag.pro = "lqc";
            ViewBag.categoryName = categoryName;
            ViewBag.lqcs = lqcs;
            ViewBag.whatme = "GetMaterialSeriesPartial";
            ViewBag.panel_title = "室内质控品";

            return PartialView("ProductSeries");
        }

        /// <summary>
        /// 室内质控品明细动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLaboratoryQuailtyControlProductDetail()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string proId = "";
            d = ParseUrlId(id);

            LaboratoryQuailtyControl lqc = null;

            if (GetUrlFiled(d, "id") != "")
            {
                proId = GetUrlFiled(d, "id");

                lqc = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControlDetail(proId);
            }

            @ViewBag.lqc = lqc;

            return PartialView("ProductDetail", lqc);
        }
        #endregion


        #region 室间质控品动作
        /// <summary>
        /// 室间质控品动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInterroomQualityControlProducts()
        {
            List<InterroomQualityControl> iqcs = new InterroomQualityControlManager().GetLaboratoryQuailtyControls(true);
            ViewBag.iqcs = iqcs;
            ViewBag.pro = "iqc";
            ViewBag.panel_title = "室间质评品";

            return PartialView("Products");
        }

        /// <summary>
        /// 室间质控品更多动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInterroomQualityControlProductMore()
        {
            ViewBag.productSeries = new InterroomQualityControlManager().GetCategorys();
            ViewBag.pro = "iqc";
            ViewBag.panel_title = "室间质控品";
            ViewBag.list_title = "产品系列";

            return PartialView("ProductMore");
        }

        /// <summary>
        /// 室间质控品专业动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInterroomQualityControlProductSeries()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string categoryName = "";
            d = ParseUrlId(id);

            List<InterroomQualityControl> iqcs = null;

            if (GetUrlFiled(d, "cat_name") != "")
            {
                categoryName = GetUrlFiled(d, "cat_name");

                iqcs = new InterroomQualityControlManager().GetInterroomQualityControlsByCategory(categoryName);
            }

            ViewBag.pro = "iqc";
            ViewBag.categoryName = categoryName;
            ViewBag.iqcs = iqcs;
            ViewBag.whatme = "GetMaterialSeriesPartial";
            ViewBag.panel_title = "室间质评品";

            return PartialView("ProductSeries");
        }

        /// <summary>
        /// 室间质控品明细动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInterroomQualityControlProductDetail()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string proId = "";
            d = ParseUrlId(id);

            InterroomQualityControl iqc = null;

            if (GetUrlFiled(d, "id") != "")
            {
                proId = GetUrlFiled(d, "id");

                iqc = new InterroomQualityControlManager().GetInterroomQualityControlDetail(proId);
            }

            @ViewBag.iqc = iqc;

            return PartialView("ProductDetail");
        }
        #endregion

        #region 标准物质
        /// <summary>
        /// 标准物动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMaterialProducts()
        {
            List<Material> ms = new MaterialManager().GetMaterials(true);
            ViewBag.ms = ms;
            ViewBag.pro = "rm";
            ViewBag.panel_title = "标准物质";

            return PartialView("Products");
        }

        /// <summary>
        /// 标准物更多动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMaterialProductMore()
        {
            ViewBag.productSeries = new MaterialManager().GetCategorys();
            ViewBag.pro = "rm";
            ViewBag.panel_title = "标准物质";
            ViewBag.list_title = "产品系列";

            return PartialView("ProductMore");
        }

        /// <summary>
        /// 标准物专业动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMaterialProductSeries()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string categoryName = "";
            d = ParseUrlId(id);

            List<Material> rm = null;

            if (GetUrlFiled(d, "cat_name") != "")
            {
                categoryName = GetUrlFiled(d, "cat_name");

                rm = new MaterialManager().GetMaterialsByCategory(categoryName);
            }

            ViewBag.pro = "rm";
            ViewBag.categoryName = categoryName;
            ViewBag.rm = rm;
            ViewBag.whatme = "GetMaterialSeriesPartial";
            ViewBag.panel_title = "标准物质";

            return PartialView("ProductSeries");
        }

        /// <summary>
        /// 标准物明细动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMaterialProductDetail()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string id = RouteData.Values["id"].ToString();
            string proId = "";
            d = ParseUrlId(id);

            Material mat = null;

            if (GetUrlFiled(d, "id") != "")
            {
                proId = GetUrlFiled(d, "id");

                mat = new MaterialManager().GetMaterialDetail(proId);
            }

            @ViewBag.mat = mat;
            return PartialView("ProductDetail");
        }
        #endregion


        public ActionResult GetConsoltionPartial()
        {
            return PartialView("Consultion");
        }

        public ActionResult GetBreadCrumb()
        {
            return PartialView("BreadCrumb");
        }

        /// <summary>
        /// 质量管理工具
        /// </summary>
        /// <returns></returns>
        public ActionResult QualityManageTools()
        {
            return View("QualityManageTools");
        }


        /// <summary>
        /// 质量管理平台
        /// </summary>
        /// <returns></returns>
        public ActionResult QualityManagePlatforms()
        {
            return View("QualityManagePlatforms");
        }


        /// <summary>
        /// 质量学坊
        /// </summary>
        /// <returns></returns>
        public ActionResult QualityXueFang()
        {
            return View("QualityXueFang");
        }

        public ActionResult ShowProducts()
        {
            return View("Products");
        }


        public string GetUrlFiled(Dictionary<string, string> d, string filedName)
        {
            if (d.ContainsKey(filedName) && string.Empty != d[filedName].ToString())
            {
                return d[filedName];
            }
            else
            {
                return "";
            }
        }

        public Dictionary<string, string> ParseUrlId(string id)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string[] typeList = null;

            typeList = id.Split('&');

            /*从id中解析产品(pro)，分类(cat)，id号(sn)等信息添加到字典中*/
            foreach (string val in typeList)
            {
                string[] fileds = val.Split('=');

                if (fileds.Length == 2)
                {
                    d.Add(fileds[0], fileds[1]);
                }
            }

            return d;
        }

        #region 图形验证码
        public ActionResult SecurityCode()
        {
            string oldcode = TempData["SecurityCode"] as string;
            string code = CreateRandomCode(4); //验证码的字符为4个
            TempData["SecurityCode"] = code; //验证码存放在TempData中

            Response.Cookies.Add(new HttpCookie("yzmcode", code));

            return File(CreateValidateGraphic(code), "image/Jpeg");
        }

        public string GetSecurityCode()
        {
            string code = TempData["SecurityCode"] as string;

            return code;
        }


        /// <summary>
        /// 生成随机的字符串
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        public string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,a,b,c,d,e,f,g,h,i,g,k,l,m,n,o,p,q,r,F,G,H,I,G,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }



        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                }
                Font font = new Font("Arial", 13, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);

                //画图片的前景干扰线
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

            
        }

        #endregion


    }




}
