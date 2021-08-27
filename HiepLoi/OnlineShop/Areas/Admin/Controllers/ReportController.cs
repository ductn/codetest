using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClsCommon;
using CommonConstants = ClsCommon.CommonConstants;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Globalization;
using System.IO;
using Model.ViewModel;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        public ActionResult Index()
        {
            String applicationPath = Server.MapPath(Request.ApplicationPath);
            String licenseFile = Path.Combine(applicationPath, "bin/Aspose.Cells.lic");
            Aspose.Cells.License license = new Aspose.Cells.License();
            license.SetLicense(licenseFile);
            var _workbook = new Aspose.Cells.Workbook(Server.MapPath("~/Template/BaoCaoChiTiet.xls"));
            Aspose.Cells.Worksheet _workSheet = _workbook.Worksheets[0];
            _workSheet.Cells["B1"].PutValue("ABC");
            _workSheet.Cells["B2"].PutValue("PHÒNG TÀI CHÍNH - KẾ HOẠCH");
            _workbook.Save(Server.MapPath("~/Template/ReportDownload/" + "test2" + ".xls"));
            ViewBag.url = "/Template/ReportDownload/" + "test2" + ".xls";
            return View();
        }       
    }
}