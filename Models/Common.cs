using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Security.Cryptography;

namespace Models
{
    public enum fileType : byte
    {
        [Description("图片类型")]
        ImageType = 0,
        [Description("证书类型")]
        CertificateType = 1,
    }

    public class Common
    {
        /// <summary>
        /// 随机生成GUID
        /// </summary>
        /// <returns></returns>
        public string getGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString();
            return str;
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public void DeleteFile(string filename, fileType type = fileType.ImageType)
        {
            string path = "";
            if (type == fileType.ImageType)
            {
                path = "/Data/Images/" + filename;
            }
            else if (type == fileType.CertificateType)
            {
                path = "/Data/Certificates/" + filename;
            }
            
            if(File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(path));
            }
            
        }

        public string GeneratePassword()
        {
            string pws = "";
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < 8; i++)
            {
                str += chars[randrom.Next(chars.Length)];//randrom.Next(int i)返回一个小于所指定最大值的非负随机数
            }
            if (IsNumber(str) || IsLetter(str))//判断是否全是数字或全是字母
                str = GeneratePassword();

            return str;
        }

        static bool IsNumber(string str)
        {
            if (str.Trim("0123456789".ToCharArray()) == "")
                return true;
            return false;
        }
        //判断是否全是字母
        static bool IsLetter(string str)
        {
            if (str.Trim("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray()) == "")
                return true;
            return false;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字元</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.GetEncoding("GB2312").GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            for (int i = 0; i < targetData.Length; i++)
            {
                strEncrypt += targetData[i].ToString("X2");
            }
            if (code == 16)
            {
                strEncrypt = strEncrypt.Substring(8, 16);
            }
            return strEncrypt;
        }


    }


    public class message
    {
        public bool valid { get; set; }
    }
}
