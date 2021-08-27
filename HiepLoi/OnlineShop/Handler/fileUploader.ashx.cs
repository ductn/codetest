using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    /// <summary>
    /// Summary description for fileUploader
    /// </summary>
    public class fileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string DestPath = dirFullPath + Convert.ToString(DateTime.Now.Year) + "\\" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                string StringDate = Convert.ToString(DateTime.Now.Year) + "\\" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                if (!Directory.Exists(DestPath)){
                    Directory.CreateDirectory(DestPath);
                }
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(DestPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    Random rand = new Random();
                    fileName = (rand.NextDouble() + 65) + fileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = fileName;
                        string pathToSave_100 = DestPath + "\\" + str_image;
                        file.SaveAs(pathToSave_100);
                    }
                }
                var tenFile = str_image;
                var urlFile = "\\MediaUploader\\" + StringDate + "\\" + str_image;
                var json = "{'tenFile:"+ tenFile + "','urlFile:"+ urlFile + "'}";
                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}