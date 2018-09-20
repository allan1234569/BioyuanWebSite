using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Web.Security;
using BLL;
using Models;
using Newtonsoft.Json;

namespace MvcApplication1.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        #region 首页
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                string adminName = this.User.Identity.Name;//获取写入的AdminName

                ViewBag.adminName = adminName;

                return View();
            }
            else
            {
                return RedirectToRoute("Admin_default", new { controller = "Login", action = "Index" });
            }
        }

        #endregion


        #region 共用模块

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


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public string UploadFile(HttpPostedFileBase File, fileType type = fileType.ImageType)
        {
            string FileName = null;

            if (File != null)
            {
                FileName = File.FileName; //获取文件名
            }

            string path = "";


            if (type == fileType.ImageType)
            {
                path = "/Data/Images/";
            }
            else if (type == fileType.CertificateType)
            {
                path = "/Data/Certificates/";
            }

            string sPath = Server.MapPath(path);//构造绝对路径

            Directory.CreateDirectory(sPath);//创建目录

            string guid = "";
            if (FileName != null && FileName != "")
            {
                string FileType = FileName.Substring(FileName.LastIndexOf(".") + 1); //得到文件的后缀名  
                guid = System.Guid.NewGuid().ToString() + "." + FileType; //得到重命名的文件名 
                File.SaveAs(Server.MapPath(path) + guid); //保存操作
                return guid;
            }
            else
            {
                return "";
            }
        }

        [HttpPost]
        public ActionResult ckeditorUpload(HttpPostedFileBase upload)
        {
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹

            upload.SaveAs(filePhysicalPath);

            var url = "/upload/" + fileName;
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }

        private static string getJsonByObject(Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //实例化一个内存流，用于存放序列化后的数据
            MemoryStream stream = new MemoryStream();
            //使用WriteObject序列化对象
            serializer.WriteObject(stream, obj);
            //写入内存流中
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            //通过UTF8格式转换为字符串
            return Encoding.UTF8.GetString(dataBytes);
        }

        private static Object getObjectByJson(string jsonString, Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //把Json传入内存流中保存
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象
            return serializer.ReadObject(stream);
        }

        #endregion


        #region 产品分类管理

        public ActionResult ProductCategory()
        {
            List<ProductCategory> categoryList = new List<ProductCategory>();

            categoryList = new ProductCategoryManager().GetAllProductCategorys();

            ViewBag.categoryList = categoryList;

            return View("ProductCategory");
        }

        public ActionResult addProductCategory(ProductCategory category)
        {

            category.CategoryId = new Common().getGUID();

            DateTime dt = DateTime.Now;

            category.CreateTime = dt;
            category.ModifyTime = dt;

            category.Enable = 0;

            int ret = new ProductCategoryManager().InsertProductCategory(category);

            return RedirectToRoute("Admin_route3", new { controller = "Home", action = "ProductCategory" });
        }

        public ActionResult GetProductCategorysByName()
        {
            List<ProductCategory> categoryList = new List<ProductCategory>();

            if (Request.Params["name"] == null)
            {
                return RedirectToRoute("Admin_route3", new { controller = "Home", action = "ProductCategory" });
            }
            else
            {
                string name = Request.Params["name"].ToString();

                categoryList = new ProductCategoryManager().GetProductCategorys(name);

                ViewBag.categoryList = categoryList;

                return View("ProductCategory");
            }
        }

        public ContentResult GetProductCategoryDetail()
        {
            string id = Request.Params["id"].ToString();

            ProductCategory category = new ProductCategoryManager().GetProductCategoryDetail(id);

            string objJson = getJsonByObject(category);

            return base.Content(objJson);
        }


        public ActionResult EnableProductCategory()
        {
            string id = Request.Params["id"].ToString();

            int ret = new ProductCategoryManager().EnableProductCategory(id);

            int state = new ProductCategoryManager().GetProductCategoryState(id);

            return base.Content(state.ToString());
        }

        public ActionResult ModifyProductCategory(ProductCategory category)
        {

            category.ModifyTime = DateTime.Now;

            int ret = new ProductCategoryManager().UpdateProductCategory(category);

            return RedirectToRoute("Admin_route3", new { controller = "Home", action = "ProductCategory" });
        }

        public ActionResult DeleteProductCategory()
        {
            string id = Request.Params["id"].ToString();
            id = id.Trim();
            int ret = 0;



            if (id != null && string.Empty != id)
            {
                ret = new ProductCategoryManager().DeleteProductCategoryById(id);
            }

            if (ret <= 0)
            {
                return base.Content("False");
            }
            else
            {
                return base.Content("True");
            }

        }

        public ContentResult GetProductCategoryList()
        {
            List<ProductCategory> list = new ProductCategoryManager().GetAllProductCategorys();

            string jsonObj = getJsonByObject(list);

            return base.Content(jsonObj);
        }

        [HttpPost]
        public ActionResult ProductCategoryExists(string CategoryName)
        {
            bool ret = new ProductCategoryManager().ProductCategoryExists(CategoryName);

            message msg = new message();
            msg.valid = !ret;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 新闻分类管理

        public ActionResult NewsCategory()
        {
            List<NewsCategory> categoryList = new List<NewsCategory>();

            categoryList = new NewsCategoryManager().GetAllNewsCategorys();

            ViewBag.categoryList = categoryList;

            return View("NewsCategory");
        }

        public ActionResult addNewsCategory(NewsCategory category)
        {

            category.CategoryId = new Common().getGUID();

            DateTime dt = DateTime.Now;

            category.CreateTime = dt;
            category.ModifyTime = dt;

            category.Enable = 0;

            int ret = new NewsCategoryManager().InsertNewsCategory(category);

            return RedirectToRoute("Admin_route3", new { controller = "Home", action = "NewsCategory" });
        }

        public ActionResult GetNewsCategorysByName()
        {
            List<NewsCategory> categoryList = new List<NewsCategory>();

            if (Request.Params["name"] == null)
            {
                return RedirectToRoute("Admin_route3", new { controller = "Home", action = "NewsCategory" });
            }
            else
            {
                string name = Request.Params["name"].ToString();

                categoryList = new NewsCategoryManager().GetNewsCategorys(name);

                ViewBag.categoryList = categoryList;

                return View("NewsCategory");
            }
        }

        public ContentResult GetNewsCategoryDetail()
        {
            string id = Request.Params["id"].ToString();

            NewsCategory category = new NewsCategoryManager().GetNewsCategoryDetail(id);

            string objJson = getJsonByObject(category);

            return base.Content(objJson);
        }


        public ActionResult EnableNewsCategory()
        {
            string id = Request.Params["id"].ToString();

            int ret = new NewsCategoryManager().EnableNewsCategory(id);

            int state = new NewsCategoryManager().GetNewsCategoryState(id);

            return base.Content(state.ToString());
        }

        public ActionResult ModifyNewsCategory(NewsCategory category)
        {

            category.ModifyTime = DateTime.Now;

            int ret = new NewsCategoryManager().UpdateNewsCategory(category);

            return RedirectToRoute("Admin_route3", new { controller = "Home", action = "NewsCategory" });
        }

        public ActionResult DeleteNewsCategory()
        {
            string id = Request.Params["id"].ToString();
            id = id.Trim();
            int ret = 0;



            if (id != null && string.Empty != id)
            {
                ret = new NewsCategoryManager().DeleteNewsCategoryById(id);
            }

            if (ret <= 0)
            {
                return base.Content("False");
            }
            else
            {
                return base.Content("True");
            }

        }

        public ContentResult GetNewsCategoryList()
        {
            List<NewsCategory> list = new NewsCategoryManager().GetAllNewsCategorys();

            string jsonObj = getJsonByObject(list);

            return base.Content(jsonObj);
        }

        [HttpPost]
        public ActionResult NewsCategoryExists(string CategoryName)
        {
            bool ret = new NewsCategoryManager().NewsCategoryExists(CategoryName);

            message msg = new message();
            msg.valid = !ret;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 室内质控品管理模块
        public ActionResult LaboratoryQuailtyControl()
        {
            List<LaboratoryQuailtyControl> list = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControls();

            ViewBag.labQualityControlList = list;

            return View("LaboratoryQuailtyControl");
        }

        [HttpPost]
        /// <summary>
        /// 添加室内质控品动作
        /// </summary>
        /// <returns></returns>
        public ActionResult AddLaboratoryQuailtyControl(LaboratoryQuailtyControl lab)
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);

            string[] ProductCodes = Server.UrlDecode(Request.Form["ProductCode"] != null ? Request.Form["ProductCode"] : "").Split(',');
            string[] Concentrations = Server.UrlDecode(Request.Form["Concentration"] != null ? Request.Form["Concentration"] : "").Split(',');
            string[] Specifications = Server.UrlDecode(Request.Form["Specification"] != null ? Request.Form["Specification"] : "").Split(',');
            string[] CertificateNos = Server.UrlDecode(Request.Form["CertificateNo"] != null ? Request.Form["CertificateNo"] : "").Split(',');
            int count = ProductCodes.Length;

            lab.LaboratoryQualityControlId = new Common().getGUID();
            lab.Img = img_path;

            DateTime dt = DateTime.Now;

            lab.CreateTime = dt;
            lab.ModifyTime = dt;
            lab.Enable = 0;

            new LaboratoryQuailtyControlManager().InsertLaboratoryQuailtyControl(lab);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "LaboratoryQuailtyControl" });
        }

        [HttpPost]
        /// <summary>
        /// 删除质控品动作
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteLaboratoryQuailtyControl()
        {
            string id = Request.Params["ProductId"].ToString();

            string filename = new LaboratoryQuailtyControlManager().GetImgPath(id);

            int ret = 0;

            if (id != null && string.Empty != id)
            {
                ret = new LaboratoryQuailtyControlManager().DeleteLaboratoryQuailtyControl(id);
            }

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "LaboratoryQuailtyControl" });
        }

        /// <summary>
        /// 启用室内质控品动作
        /// </summary>
        /// <returns></returns>
        public ContentResult EnableLaboratoryQualityControl()
        {
            string id = Request.Params["id"].ToString();

            int ret = new LaboratoryQuailtyControlManager().EnableLabratoryQualityControl(id);

            return base.Content(ret.ToString());
        }

        public ActionResult ModifyLaboratoryQUalityControl(LaboratoryQuailtyControl lab)
        {
            string _path = Server.MapPath("/Data/Images/") + "" + lab.Img;

            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);

            lab.Img = img_path;

            DateTime dt = DateTime.Now;

            lab.ModifyTime = dt;

            int ret = new LaboratoryQuailtyControlManager().UpdateLaboratoryQuailtyControl(lab);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "LaboratoryQuailtyControl" });
        }


        public ActionResult GetLaboratoryQualityControlByName()
        {
            string name = "";

            if (Request.Params["name"] != null)
            {
                name = Request.Params["name"].ToString();
            }

            List<LaboratoryQuailtyControl> list = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControls(name);

            ViewBag.labQualityControlList = list;

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ContentResult GetLaboratoryQualityControlDetail()
        {
            string id = Request.Params["id"];

            LaboratoryQuailtyControl labQualityControl = new LaboratoryQuailtyControlManager().GetLaboratoryQuailtyControlDetail(id);

            if (labQualityControl != null)
            {
                string objJson = getJsonByObject(labQualityControl);

                return base.Content(objJson);
            }
            else
            {
                return base.Content("null");
            }
        }

        #endregion


        #region 非定值质控品
        public ActionResult NonConstantQuailtyControl()
        {
            return View();
        }
        #endregion


        #region 室间质评品管理模块
        public ActionResult InterroomQualityControl()
        {
            List<InterroomQualityControl> list = new InterroomQualityControlManager().GetLaboratoryQuailtyControls();

            ViewBag.interroomQualityControlList = list;

            return View("InterroomQualityControl");
        }


        public ActionResult AddInterroomQualityControl(InterroomQualityControl interroomQuaControl)
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);

            DateTime dt = DateTime.Now;

            interroomQuaControl.InterroomQualityControlId = new Common().getGUID();
            interroomQuaControl.Img = img_path;

            interroomQuaControl.CreateTime = dt;
            interroomQuaControl.ModifyTime = dt;
            interroomQuaControl.Enable = 0;

            int ret = new InterroomQualityControlManager().InsertInterroomQualityControl(interroomQuaControl);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "InterroomQualityControl" });
        }

        public ActionResult DeleteInterroomQualityControl()
        {
            string id = Request.Params["ProductId"].ToString();

            string filename = new InterroomQualityControlManager().GetImgPath(id);

            int ret = 0;

            if (id != null && string.Empty != id)
            {
                ret = new InterroomQualityControlManager().DeleteInterroomQualityControl(id);
            }

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "InterroomQualityControl" });
        }

        /// <summary>
        /// 启用室间质评品动作
        /// </summary>
        /// <returns></returns>
        public ContentResult EnableInterroomQualityControl()
        {
            string id = Request.Params["id"].ToString();

            int ret = new InterroomQualityControlManager().EnableInterroomQualityControl(id);

            return base.Content(ret.ToString());
        }

        public ActionResult ModifyInterroomQualityControl(InterroomQualityControl interroomQuaControl)
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);

            interroomQuaControl.Img = img_path;

            DateTime dt = DateTime.Now;

            interroomQuaControl.ModifyTime = dt;

            int ret = new InterroomQualityControlManager().UpdateInterroomQualityControl(interroomQuaControl);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "InterroomQualityControl" });
        }

        public ActionResult GetInterroomQualityControlByName()
        {
            string name = "";

            if (Request.Params["name"] != null)
            {
                name = Request.Params["name"].ToString();//获取查查名称
            }

            List<InterroomQualityControl> list = new InterroomQualityControlManager().GetInterroomQualityControls(name);//获取数据

            ViewBag.interroomQualityControlList = list;

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ContentResult GetInterroomQualityControlDetail()
        {
            string id = Request.Params["id"];

            InterroomQualityControl interroomQualityControl = new InterroomQualityControlManager().GetInterroomQualityControlDetail(id);

            if (interroomQualityControl != null)
            {
                string objJson = getJsonByObject(interroomQualityControl);

                return base.Content(objJson);
            }
            else
            {
                return base.Content("null");
            }
        }

        #endregion


        #region 标准物质管理模块
        public ActionResult Material()
        {
            List<Material> materialList = new MaterialManager().GetMaterials();

            ViewBag.materialList = materialList;

            return View("Material");
        }

        [HttpPost]
        public ActionResult AddMaterial(Material material)
        {


            //Dictionary<string, int> ProjectCountDic = new Dictionary<string, int>();

            //if (Request.Form["MaterialProjectName"] != null)
            //{
            //    string ProjectList = (Request.Form["MaterialProjectName"] != null) ? Server.UrlDecode(Request.Form["MaterialProjectName"]) : "";
            //    string[] list = ProjectList.Split(',');

            //    foreach (string value in list)
            //    {
            //        ProjectCountDic.Add(value, Convert.ToInt32(Server.UrlDecode(Request.Form[value])));
            //    }
            //}

            //string[] Units = (Request.Form["Unit"] != null) ? Server.UrlDecode(Request.Form["Unit"]).Split(',') : null;//单位
            //string[] ProductCodes = (Request.Form["ProductCode"] != null) ? Server.UrlDecode(Request.Form["ProductCode"]).Split(',') : null;//货号
            //string[] StandardUncertairtys = (Request.Form["StandardUncertairty"] != null) ? Server.UrlDecode(Request.Form["StandardUncertairty"]).Split(',') : null;//标准及不确定度
            //string[] Specifications = (Request.Form["Specification"] != null) ? Server.UrlDecode(Request.Form["Specification"]).Split(',') : null;//规格
            //string[] CertificateNos = (Request.Form["CertificateNo"] != null) ? Server.UrlDecode(Request.Form["CertificateNo"]).Split(',') : null;//编号



            //material.materialProjects = new List<MaterialProject>();

            //int count = 0;

            //if (ProjectCountDic.Count > 0)
            //{
            //    for (int i = 0; i < ProjectCountDic.Count; ++i)
            //    {
            //        MaterialProject mp = new MaterialProject();
            //        string materialProId = new Common().getGUID();
            //        mp.materialProjectId = materialProId;
            //        mp.materialId = materialId;
            //        mp.materialProjectName = ProjectCountDic.Keys.ElementAt(i);
            //        mp.unit = Units[i + 1];
            //        mp.materialSpecifications = new List<MaterialSpecification>();

            //        for (int j = 0; j < ProjectCountDic[ProjectCountDic.Keys.ElementAt(i)]; ++j)
            //        {
            //            MaterialSpecification ms = new MaterialSpecification();
            //            string specificationId = new Common().getGUID();
            //            ms.SpecificationId = specificationId;
            //            ms.MaterialProjectId = materialProId;
            //            ms.ProductCode = (ProductCodes != null) ? ProductCodes[count] : "";
            //            ms.StardardUncertairty = (StandardUncertairtys != null) ? StandardUncertairtys[count] : "";
            //            ms.Specification = (Specifications != null) ? Specifications[count] : "";
            //            ms.CertificateNo = (CertificateNos != null) ? CertificateNos[count] : "";
            //            count++;
            //            mp.materialSpecifications.Add(ms);
            //        }
            //        material.materialProjects.Add(mp);
            //    }
            //}
            //else
            //{
            //    MaterialProject mp = new MaterialProject();
            //    string materialProId = new Common().getGUID();
            //    mp.materialProjectId = materialProId;
            //    mp.materialId = materialId;
            //    mp.unit = Units[0];
            //    mp.materialSpecifications = new List<MaterialSpecification>();

            //    if (ProductCodes != null)
            //    {
            //        for (int j = 0; j < ProductCodes.Length; ++j)
            //        {
            //            MaterialSpecification ms = new MaterialSpecification();
            //            string specificationId = new Common().getGUID();
            //            ms.SpecificationId = specificationId;
            //            ms.MaterialProjectId = materialProId;
            //            ms.ProductCode = (ProductCodes != null) ? ProductCodes[j] : "";
            //            ms.StardardUncertairty = (StandardUncertairtys != null) ? StandardUncertairtys[j] : "";
            //            ms.Specification = (Specifications != null) ? Specifications[j] : "";
            //            ms.CertificateNo = (CertificateNos != null) ? CertificateNos[j] : "";
            //            count++;
            //            mp.materialSpecifications.Add(ms);
            //        }
            //    }

            //    material.materialProjects.Add(mp);
            //}

            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);
            material.Img = img_path;//图片路径

            string materialId = new Common().getGUID();
            material.MaterialId = materialId;

            DateTime dt = DateTime.Now;
            material.CreateTime = dt;
            material.ModifyTime = dt;
            material.Enable = 0;

            int ret = new MaterialManager().InsertMaterial(material);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Material" });
        }

        public ActionResult GetMaterialsByName()
        {
            string name = "";

            if (Request.Params["name"] != null)
            {
                name = Request.Params["name"].ToString();
            }

            List<Material> materialList = new MaterialManager().GetMaterials(name);

            ViewBag.materialList = materialList;

            return Json(materialList, JsonRequestBehavior.AllowGet);
        }

        public ContentResult GetMaterialDetail()
        {
            string id = Request.Params["id"];

            Material material = new MaterialManager().GetMaterialDetail(id);

            if (material != null)
            {
                string objJson = getJsonByObject(material);

                return base.Content(objJson);
            }
            else
            {
                return base.Content("0");
            }
        }


        public ActionResult GetMaterials()
        {
            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Material" });
        }

        [HttpPost]
        public ActionResult ModifyMaterial(Material material)
        {


            //Dictionary<string, int> ProjectCountDic = new Dictionary<string, int>();

            //if (Request.Form["MaterialProjectName"] != null)
            //{
            //    string ProjectList = (Request.Form["MaterialProjectName"] != null) ? Server.UrlDecode(Request.Form["MaterialProjectName"]) : "";
            //    string[] list = ProjectList.Split(',');

            //    foreach (string value in list)
            //    {
            //        ProjectCountDic.Add(value, Convert.ToInt32(Server.UrlDecode(Request.Form[value])));
            //    }
            //}

            //string[] Units = (Request.Form["Unit"] != null) ? Server.UrlDecode(Request.Form["Unit"]).Split(',') : null;//单位
            //string[] ProductCodes = (Request.Form["ProductCode"] != null) ? Server.UrlDecode(Request.Form["ProductCode"]).Split(',') : null;//货号
            //string[] StandardUncertairtys = (Request.Form["StandardUncertairty"] != null) ? Server.UrlDecode(Request.Form["StandardUncertairty"]).Split(',') : null;//标准及不确定度
            //string[] Specifications = (Request.Form["Specification"] != null) ? Server.UrlDecode(Request.Form["Specification"]).Split(',') : null;//规格
            //string[] CertificateNos = (Request.Form["CertificateNo"] != null) ? Server.UrlDecode(Request.Form["CertificateNo"]).Split(',') : null;//编号



            //material.materialProjects = new List<MaterialProject>();

            //int count = 0;

            //if (ProjectCountDic.Count > 0)
            //{
            //    for (int i = 0; i < ProjectCountDic.Count; ++i)
            //    {
            //        MaterialProject mp = new MaterialProject();
            //        string materialProId = new Common().getGUID();
            //        mp.materialProjectId = materialProId;
            //        mp.materialId = material.MaterialId;
            //        mp.materialProjectName = ProjectCountDic.Keys.ElementAt(i);
            //        mp.unit = Units[i + 1];
            //        mp.materialSpecifications = new List<MaterialSpecification>();

            //        for (int j = 0; j < ProjectCountDic[ProjectCountDic.Keys.ElementAt(i)]; ++j)
            //        {
            //            MaterialSpecification ms = new MaterialSpecification();
            //            string specificationId = new Common().getGUID();
            //            ms.SpecificationId = specificationId;
            //            ms.MaterialProjectId = materialProId;
            //            ms.ProductCode = (ProductCodes != null) ? ProductCodes[count] : "";
            //            ms.StardardUncertairty = (StandardUncertairtys != null) ? StandardUncertairtys[count] : "";
            //            ms.Specification = (Specifications != null) ? Specifications[count] : "";
            //            ms.CertificateNo = (CertificateNos != null) ? CertificateNos[count] : "";
            //            count++;
            //            mp.materialSpecifications.Add(ms);
            //        }
            //        material.materialProjects.Add(mp);
            //    }
            //}
            //else
            //{
            //    MaterialProject mp = new MaterialProject();
            //    string materialProId = new Common().getGUID();
            //    mp.materialProjectId = materialProId;
            //    mp.materialId = material.MaterialId;
            //    mp.unit = Units[0];
            //    mp.materialSpecifications = new List<MaterialSpecification>();

            //    if (ProductCodes != null)
            //    {
            //        for (int j = 0; j < ProductCodes.Length; ++j)
            //        {
            //            MaterialSpecification ms = new MaterialSpecification();
            //            string specificationId = new Common().getGUID();
            //            ms.SpecificationId = specificationId;
            //            ms.MaterialProjectId = materialProId;
            //            ms.ProductCode = (ProductCodes != null) ? ProductCodes[j] : "";
            //            ms.StardardUncertairty = (StandardUncertairtys != null) ? StandardUncertairtys[j] : "";
            //            ms.Specification = (Specifications != null) ? Specifications[j] : "";
            //            ms.CertificateNo = (CertificateNos != null) ? CertificateNos[j] : "";
            //            count++;
            //            mp.materialSpecifications.Add(ms);
            //        }
            //    }

            //    material.materialProjects.Add(mp);
            //}

            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase File = files["Img"] != null ? files["Img"] : null;//根据name属性获取文件
            string img_path = UploadFile(File);

            material.Img = img_path;//图片路径

            DateTime dt = DateTime.Now;

            material.ModifyTime = dt;

            int ret = new MaterialManager().UpdateMaterial(material);

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Material" });
        }

        public ContentResult EnableMaterial()
        {
            string id = Request.Params["id"].ToString();

            int ret = new MaterialManager().EnableMaterial(id);

            return base.Content(ret.ToString());
        }

        [HttpPost]
        public ActionResult DeleteMaterial()
        {
            string id = Request.Params["MaterialId"].ToString();

            string filename = new MaterialManager().GetImgPath(id);

            int ret = 0;

            if (id != null && string.Empty != id)
            {
                ret = new MaterialManager().DeleteMaterialById(id);
            }

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Material" });
        }

        #endregion


        #region 简介管理

        public ActionResult Introduction()
        {
            Introduction introduction = new IntroductionManage().GetIntroductionDetail();

            if (introduction != null)
            {
                ViewBag.bIntroductionExist = true;
            }
            else
            {
                ViewBag.bIntroductionExist = false;
            }

            return View("Introduction", introduction);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddIntroduction(Introduction introduction)
        {
            introduction.id = new Common().getGUID();

            //introduction.companyIntroduction = Server.HtmlEncode(Request.Params["companyIntroduction"]);
            //introduction.corporatePurpose = Server.HtmlEncode(Request.Params["corporatePurpose"]);
            //introduction.corporateVision = Server.HtmlEncode(Request.Params["corporateVision"]);

            int ret = new IntroductionManage().InsertIntroduction(introduction);

            return RedirectToRoute("Admin_route4", new { controller = "Home", action = "Introduction" });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyIntroduction(Introduction introduction)
        {
            int ret = new IntroductionManage().UpdateIntroduction(introduction);

            return RedirectToRoute("Admin_route4", new { controller = "Home", action = "Introduction" });
        }

        public ContentResult GetIntroductionDetail()
        {
            Introduction introduction = new IntroductionManage().GetIntroductionDetail();

            string jsonObj = getJsonByObject(introduction);

            return base.Content(jsonObj);
        }

        #endregion


        #region 新闻管理

        public ActionResult News()
        {

            List<News> newsList = new NewsManager().GetNews("");

            ViewBag.newsList = newsList;

            return View();
        }

        public ActionResult AddNews(News news)
        {
            news.id = new Common().getGUID();

            news.dateTime = DateTime.Now;
            news.enable = 0;

            new NewsManager().InsertNews(news);

            return RedirectToRoute("Admin_route5", new { controller = "Home", action = "News" });
        }

        public ActionResult ModifyNews(News news)
        {
            return RedirectToRoute("Admin_route5", new { controller = "Home", action = "News" });
        }

        public ContentResult GetNewsByTitle()
        {
            return base.Content("");
        }

        public ContentResult GetNewsDetail()
        {
            string id = Request.Params["id"].ToString();

            News news = new NewsManager().GetNewsDetail(id);

            string jsonObj = getJsonByObject(news);

            return base.Content(jsonObj);
        }

        public ActionResult GetNewsByTitleName()
        {
            string titleName = "";

            if (Request.Params["titleName"] != null)
            {
                titleName = Request.Params["titleName"].ToString();
            }

            List<News> newsList = new NewsManager().GetNews(titleName);

            return Json(newsList, JsonRequestBehavior.AllowGet);
        }

        public ContentResult ReleaseNews()
        {
            string id = Request.Params["id"].ToString();

            int ret = new NewsManager().ReleaseNews(id);

            return base.Content(ret.ToString());
        }

        public ActionResult DeleteNews()
        {
            string id = Request.Params["id"].ToString();

            new NewsManager().DeleteNews(id);

            return RedirectToRoute("Admin_route5", new { controller = "Home", action = "News" });
        }

        #endregion


        #region 用户管理模块
        public ActionResult Users()
        {
            List<Models.UserInfo> userList = new UserManager().GetUsers();

            ViewBag.userList = userList;

            return View("User");
        }

        public ActionResult GetUserByLoginName(Models.UserInfo user)
        {

            List<Models.UserInfo> userList = new UserManager().GetUserByLoginName(user.LoginName);

            ViewBag.userList = userList;

            return View("User");
        }


        public ContentResult GetAdminById()
        {
            string id = Request.Params["id"].ToString();

            id = id.Trim();

            Models.UserInfo admin = new UserManager().GetUserById(id);

            if (admin != null)
            {
                string objJson = getJsonByObject(admin);

                return base.Content(objJson);
            }
            else
            {
                return base.Content("0");
            }

        }


        public ActionResult EnableUser()
        {
            string id = Request.Params["id"].ToString();

            int ret = new UserManager().EnableUser(id);

            ViewBag.state = new UserManager().GetUserState(id);

            string result = ret.ToString();

            return base.Content(result);
        }

        public ActionResult AddUser(Models.UserInfo user)
        {
            DateTime dt = DateTime.Now;

            user.UserId = new Common().getGUID();
            user.CreateTime = dt;
            user.ModifyTime = dt;
            user.Enable = 1;
            user.UserRank = 1;

            RegisterType addRet = new UserManager().AddUser(user);

            ViewBag.userExist = false;

            if (addRet == RegisterType.用户已存在)//用户已经存在
            {
                ViewBag.userExist = true;
            }

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Users" });
        }

        public ActionResult DeleteUser()
        {
            string id = Request.Params["id"].ToString();
            id = id.Trim();
            int ret = 0;



            if (id != null && string.Empty != id)
            {
                ret = new UserManager().DeleteUserById(id);
            }

            if (ret <= 0)
            {
                return base.Content("False");
            }
            else
            {
                return base.Content("True");
            }

        }

        public ActionResult ModifyPwd(Models.UserInfo user)
        {
            UserManager manage = new UserManager();

            string OriginPwd = Request.Params["OriginLoginPwd"].ToString();

            DateTime dt = DateTime.Now;

            user.ModifyTime = dt;

            int ret = new UserManager().ModifyUser(user, OriginPwd);

            if (ret > 0)
            {
                //修改成功
            }
            else
            {
                //修改失败
            }

            return RedirectToRoute("Admin_route2", new { controller = "Home", action = "Users" });
        }

        #endregion
    }
}