using Model.Dao;
using Model.EF;
using Model.ViewModel;
using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexLanhDao()
        {
            return View();
        }
        public ActionResult IndexLanhDaoSo()
        {
            //var itThongKe = new ThongKeBieuDo();
            //var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //var tongHKD = lstThongKe.Where(x => x.LoaiHinh == "HKD").Sum(x => double.Parse(x.GiaTriHo));
            //var tongHTX = lstThongKe.Where(x => x.LoaiHinh == "HTX").Sum(x => double.Parse(x.GiaTriHo));
            //var tongDN = lstThongKe.Where(x => x.LoaiHinh == "DN").Sum(x => double.Parse(x.GiaTriHo));
            //var tongNDT = lstThongKe.Where(x => x.LoaiHinh == "NDT").Sum(x => double.Parse(x.GiaTriHo));
          
            //ViewBag.tongHKD = tongHKD.ToString("#,##0.###").Replace(",", ".");
            //ViewBag.tongHTX = tongHTX.ToString("#,##0.###").Replace(",", ".");
            //ViewBag.tongDN = tongDN.ToString("#,##0.###").Replace(",", ".");
            //ViewBag.tongNDT = tongNDT.ToString("#,##0.###").Replace(",", ".");
            return View();
        }
        //Start Lanhdoso

        //End Lanhdoso
        public ActionResult BieuDoThongKeLanhDao()
        {
            return View();
        }
        public ActionResult MenuLanhDao()
        {
            return View();
        }
        public ActionResult TimKiemLanhDao()
        {
            return View();
        }
        //dashboard1
        [HasCredential(RoleID = "DASHBOARD1_HOME")]
        public ActionResult Dashboard1()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var daoContent = new ContentDao();
            //var daoChinhSachUuDai = new ChinhSachUuDaiDao();
            //int totalRecordTinDangSoan = 0;
            //int totalRecordTinBoSung = 0;
            //int totalRecordTinChoDuyet = 0;
            //int totalRecordChinhSachDangSoan = 0;
            //int totalRecordChinhSachBoSung = 0;
            //int totalRecordChinhSachChoDuyet = 0;
            //if (session.GroupID == CommonConstants.BIENTAP_GROUP) {
            //    //Tin tức
            //    Content itContent = new Content();
            //    itContent.DonViID = session.UnitCode;
            //    //Tin đang soạn
            //    itContent.StatusId = CommonConstants.TINTTUC_DANGSOAN;
            //    var dsTinDangSoan = daoContent.ListAllPaging(itContent, "", ref totalRecordTinDangSoan, 1, 10);
            //    ViewBag.totalRecordTinDangSoan = totalRecordTinDangSoan;
            //    ViewBag.dsTinDangSoan = dsTinDangSoan;
            //    // Tin bổ sung
            //    itContent.StatusId = CommonConstants.TINTTUC_TRAVE;
            //    var dsTinBoSung = daoContent.ListAllPaging(itContent, "", ref totalRecordTinBoSung, 1, 10);
            //    ViewBag.totalRecordTinBoSung = totalRecordTinBoSung;
            //    ViewBag.dsTinBoSung = dsTinBoSung;
            //    //End Tin tức

            //    //Chính sách ưu đãi
            //    ChinhSachUuDai itChinhSach = new ChinhSachUuDai();
            //    itChinhSach.DonViID = session.UnitCode;
            //    //Chinh Sach đang soạn
            //    itChinhSach.StatusId = CommonConstants.CHINHSACHUUDAI_DANGSOAN;
            //    var dsChinhSachDangSoan = daoChinhSachUuDai.ListAllPaging(itChinhSach, "", ref totalRecordChinhSachDangSoan, 1, 10);
            //    ViewBag.totalRecordChinhSachDangSoan = totalRecordChinhSachDangSoan;
            //    ViewBag.dsChinhSachDangSoan = dsChinhSachDangSoan;
            //    // Chinh Sach bổ sung
            //    itChinhSach.StatusId = CommonConstants.CHINHSACHUUDAI_TRAVE;
            //    var dsChinhSachBoSung = daoChinhSachUuDai.ListAllPaging(itChinhSach, "", ref totalRecordChinhSachBoSung, 1, 10);
            //    ViewBag.totalRecordChinhSachBoSung = totalRecordChinhSachBoSung;
            //    ViewBag.dsChinhSachBoSung = dsChinhSachBoSung;
            //    //End Chính sách ưu đãi

            //    // ViewBag chung
            //    ViewBag.ShowDashboardBienTap = "true";
            //}
            //if (session.GroupID == CommonConstants.PHEDUYET_GROUP)
            //{
            //    Content itContent = new Content();
            //    itContent.DonViID = session.UnitCode;
            //    //Tin chờ duyệt
            //    itContent.StatusId = CommonConstants.TINTTUC_TRINHDUYET;
            //    var dsTinChoDuyet= daoContent.ListAllPaging(itContent, "", ref totalRecordTinChoDuyet, 1, 10);
            //    ViewBag.totalRecordTinChoDuyet = totalRecordTinChoDuyet;
            //    ViewBag.dsTinChoDuyet = dsTinChoDuyet;

            //    //Chính sách ưu đãi
            //    ChinhSachUuDai itChinhSach = new ChinhSachUuDai();
            //    itChinhSach.DonViID = session.UnitCode;
            //    //Chinh Sach chờ duyệt
            //    itChinhSach.StatusId = CommonConstants.CHINHSACHUUDAI_TRINHDUYET;
            //    var dsChinhSachChoDuyet = daoChinhSachUuDai.ListAllPaging(itChinhSach, "", ref totalRecordChinhSachChoDuyet, 1, 10);
            //    ViewBag.totalRecordChinhSachChoDuyet = totalRecordChinhSachChoDuyet;
            //    ViewBag.dsChinhSachChoDuyet = dsChinhSachChoDuyet;

            //    ViewBag.ShowDashboardPheDuyet = "true";
            //}
            //if (session.GroupID == CommonConstants.QLCONGKHAI_GROUP) // Quản lý công khai
            //{
            //    ViewBag.ShowDashboardQLCongKhai = "true";
            //    //Chính sách ưu đãi
            //    ChinhSachUuDai itChinhSach = new ChinhSachUuDai();
            //    int totalRecordChinhSachChoCongKhai = 0;
            //    itChinhSach.StatusId = CommonConstants.CHINHSACHUUDAI_XUATBAN;
            //    var dsChinhSachChoCongKhai = daoChinhSachUuDai.ListAllPaging(itChinhSach, "", ref totalRecordChinhSachChoCongKhai, 1, 1000);
            //    ViewBag.totalRecordChinhSachChoCongKhai = totalRecordChinhSachChoCongKhai;

            //    //Tin tức
            //    Content itTinTuc = new Content();
            //    int totalRecordTinTucChoCongKhai = 0;
            //    itTinTuc.StatusId = CommonConstants.TINTTUC_XUATBAN;
            //    var dsTinTucChoCongKhai = daoContent.ListAllPaging(itTinTuc, "", ref totalRecordTinTucChoCongKhai, 1, 1000);
            //    ViewBag.totalRecordTinTucChoCongKhai = totalRecordTinTucChoCongKhai;
            //}
            
            return View();
        }
        [HasCredential(RoleID = "DASHBOARD_LANHDAOSO")]
        public ActionResult DashboardLanhDaoSo()
        {
            HomeSearchViewModel itemSearch = new HomeSearchViewModel();
            //var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            //if (session.GroupID == CommonConstants.LANHDAOSO_GROUP) // Quản lý liên hệ
            //{   
            //    //DropdownList Huyện
            //    Province itemProvince = new Province();
            //    var daoProvince = new ProvinceDao();
            //    itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            //    List<SelectListItem> listHuyen = new List<SelectListItem>();
            //    List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            //    listHuyen.Add(new SelectListItem { Value = "0", Text = "--Toàn tỉnh--" });
            //    foreach (Province item in lstHuyen)
            //    {
            //        listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            //    }
            //    ViewBag.HuyenId = new SelectList(listHuyen, "Value", "Text", null);
            //    //End DropdownList Huyện

            //    string TitleLoaiHinh = "DOANH NGHIỆP";
            //    string TitleToanTinh = "TOÀN TỈNH";
            //    ViewBag.ShowDashboardLanhDaoSo = "true";

            //    BusinessField itBusinessField = new BusinessField();
            //    var daoBusinessField = new BusinessFieldDao();
            //    var lstLV = daoBusinessField.ListAll(itBusinessField).Where(x => x.MapId == null);

            //    //Start DropdownList lĩnh vực
            //    List<SelectListItem> listLinhVuc = new List<SelectListItem>();
            //    listLinhVuc.Add(new SelectListItem { Value = "0", Text = "--Tất cả--" });
            //    foreach (BusinessField item in lstLV)
            //    {
            //        listLinhVuc.Add(new SelectListItem { Value = item.Id + "", Text = item.Name });
            //    }
            //    ViewBag.LinhVuc = new SelectList(listLinhVuc, "Value", "Text", null);
            //    //End DropdownList Lĩnh vực

            //    //string dataChart1 = "";//Hộ theo lĩnh vực
            //    //string dataChart2 = "";// Vốn theo lĩnh vực
            //    double tongHKD = 0;
            //    double tongVonHKD = 0;

            //    var itThongKe = new ThongKeBieuDo();
            //    itThongKe.LoaiHinh = "DN";
            //    var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //    var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //    tongHKD = lstThongKe.Sum(x=> double.Parse(x.GiaTriHo));
            //    //tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //    //foreach (BusinessField lv in lstLV)
            //    //{
            //    //    var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //    //    double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //    //    double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //    //    double phanTramKHD = (SumHKD / tongHKD)*100;
            //    //    double phanTramVon = (TongVonHKDtheoLv/ tongVonHKD)*100;

            //    //    dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //    //    dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //    //}
            //    //string scriptChart = "<script>" +
            //    //                        "window.onload = function () {" +
            //    //                            "var chart1 = new CanvasJS.Chart('chartHo', {" +
            //    //                                "theme: 'light2'," +
            //    //                                "exportEnabled: false," +
            //    //                                "animationEnabled: true," +
            //    //                                "title: {" +
            //    //                                    "text: ''" +
            //    //                                "}," +
            //    //                                "data: [{" +
            //    //                                    "type: 'pie'," +
            //    //                                    "startAngle: 25," +
            //    //                                    "toolTipContent: '<b>{label}</b>: {y}%'," +
            //    //                                    "indexLabelFontSize: 16," +
            //    //                                    "indexLabel: '{label} - {y}%'," +
            //    //                                    "dataPoints: [" +
            //    //                                        dataChart1 +
            //    //                                    "]" +
            //    //                                "}]" +
            //    //                            "});" +
            //    //                            "var chart2 = new CanvasJS.Chart('chartVon', {" +
            //    //                                "theme: 'light2'," +
            //    //                                "exportEnabled: false," +
            //    //                                "animationEnabled: true," +
            //    //                                "title: {" +
            //    //                                    "text: ''" +
            //    //                                "}," +
            //    //                                "data: [{" +
            //    //                                    "type: 'pie'," +
            //    //                                    "startAngle: 25," +
            //    //                                    "toolTipContent: '<b>{label}</b>: {y}%'," +
            //    //                                    "indexLabelFontSize: 16," +
            //    //                                    "indexLabel: '{label} - {y}%'," +
            //    //                                    "dataPoints: [" +
            //    //                                        dataChart2 +
            //    //                                    "]" +
            //    //                                "}]" +
            //    //                            "});" +
            //    //                            "chart1.render();" +
            //    //                            "chart2.render();" +
            //    //                        "}" +
            //    //                    "</script>";

            //    //Bảng thống kê
            //    string tableContent = "";
            //    foreach (Province item in lstHuyen)
            //    {
            //        var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //        tableContent += "<tr>" +
            //                            "<td class='text-left'>" + item.Title+"</td>" +
            //                            "<td class='text-center'>"+ sumByHuyen.ToString() + " </td>" +
            //                        "</tr>";
            //    }
            //    //End Bảng thống kê
            //    ViewBag.headerTable = "DOANH NGHIỆP";
            //    //ViewBag.scriptChart = scriptChart;
            //    ViewBag.tongHKD = tongHKD.ToString("#,##0.###");
            //    ViewBag.tongVonHKD = tongVonHKD.ToString("#,##0.###");
            //    ViewBag.TitleLoaiHinh = TitleLoaiHinh;
            //    ViewBag.TitleToanTinh = TitleToanTinh;
            //    ViewBag.tableContent = tableContent;
            //    ViewBag.LoaiHinh = 3;
            //}

            
            return View(itemSearch);
        }
        [HttpPost]
        [HasCredential(RoleID = "DASHBOARD_LANHDAOSO")]
        public ActionResult DashboardLanhDaoSo(HomeSearchViewModel itemSearch)
        {
            //var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            //Province itemProvince = new Province();
            //var daoProvince = new ProvinceDao();
            //if (session.GroupID == CommonConstants.LANHDAOSO_GROUP) // Quản lý liên hệ
            //{
            //    string headerTable = "DOANH NGHIỆP";
            //    //Dropdownlist Huyện
            //    itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            //    List<SelectListItem> listHuyen = new List<SelectListItem>();
            //    List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            //    listHuyen.Add(new SelectListItem { Value = "0", Text = "--Toàn tỉnh--" });
            //    foreach (Province item in lstHuyen)
            //    {
            //        listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            //    }
            //    ViewBag.HuyenId = new SelectList(listHuyen, "Value", "Text", null);
            //    //End Dropdownlist Huyện

            //    ViewBag.ShowDashboardLanhDaoSo = "true";
            //    string TitleLoaiHinh = "";
            //    string TitleToanTinh = "";
            //    DateTime? fromDate = new DateTime(1900, 1, 1);
            //    DateTime? toDate = DateTime.Now;
            //    string strDiaPhuong = "";

            //    if (itemSearch.HuyenId !=null && itemSearch.HuyenId != 0 && itemSearch.XaPhuongId==0)
            //    {
            //        itemProvince.ParentId = itemSearch.HuyenId;
            //        var lstXa = daoProvince.ListAll(itemProvince);
            //        foreach (Province itXa in lstXa)
            //        {
            //            strDiaPhuong += itXa.Id + ",";
            //        }
            //    }
            //    else if (itemSearch.XaPhuongId != 0)
            //    {
            //        strDiaPhuong = itemSearch.XaPhuongId.ToString();
            //    }
            //    else
            //    {
            //        TitleToanTinh = "TOÀN TỈNH";
            //    }
            //    if (itemSearch.FromDate != null)
            //    {
            //        fromDate = itemSearch.FromDate;
            //    }
            //    if (itemSearch.ToDate !=null)
            //    {
            //        toDate = itemSearch.ToDate;
            //    }
            //    var daoCertifiedHousehold = new CertifiedHouseholdDao();
            //    var daoHopTacXaHousehold = new HopTacXaHouseholdDao();
            //    var daoDoanhNghiep = new DanhSachDoanhNghiepDao();

            //    BusinessField itBusinessField = new BusinessField();
            //    var daoBusinessField = new BusinessFieldDao();

            //    var listLV = daoBusinessField.ListAll(itBusinessField).Where(x => x.MapId == null);

            //    string dataChart1 = "";//Hộ theo lĩnh vực
            //    string dataChart2 = "";// Vốn theo lĩnh vực
            //    double tongHKD = 0;
            //    double tongVonHKD = 0;

            //    int CapDoi = 0;
            //    int CapLai = 0;
            //    int TamNgung = 0;
            //    int ChamDut = 0;
            //    if (itemSearch.LoaiHinh==1) // Loại hình Hộ kinh doanh
            //    {
            //        TitleLoaiHinh = "HỘ KINH DOANH";
            //        if (itemSearch.HuyenId==0 && itemSearch.ToDate ==null && itemSearch.FromDate ==null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "HKD";
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var daoHou = new HouseholdStatisticDao();
            //                var modelH = new HouseholdStatistic();
            //                modelH.HuyenId = item.Id;
            //                var objHo = daoHou.ListAll(modelH).FirstOrDefault();
            //                CapDoi = Convert.ToInt32(objHo.Capdoi);
            //                CapLai = Convert.ToInt32(objHo.Caplai);
            //                TamNgung = Convert.ToInt32(objHo.Tamngung);
            //                ChamDut = Convert.ToInt32(objHo.Chamdut);
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-right'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapDoi.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapLai.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + TamNgung.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + ChamDut.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "HỘ KINH DOANH";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else if (itemSearch.HuyenId != 0 && itemSearch.XaPhuongId == 0 && itemSearch.ToDate == null && itemSearch.FromDate == null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "HKD";
            //            itThongKe.HuyenId = itemSearch.HuyenId.ToString();
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var daoHou = new HouseholdStatisticDao();
            //                var modelH = new HouseholdStatistic();
            //                modelH.HuyenId = item.Id;
            //                var objHo = daoHou.ListAll(modelH).FirstOrDefault();
            //                CapDoi = Convert.ToInt32(objHo.Capdoi);
            //                CapLai = Convert.ToInt32(objHo.Caplai);
            //                TamNgung = Convert.ToInt32(objHo.Tamngung);
            //                ChamDut = Convert.ToInt32(objHo.Chamdut);
            //                var itThongKeI = new ThongKeBieuDo();
            //                itThongKeI.LoaiHinh = "HKD";
            //                lstThongKe = daoThongKeBieuDo.ListAll(itThongKeI);
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-right'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapDoi.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapLai.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + TamNgung.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + ChamDut.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "HỘ KINH DOANH";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else
            //        {
            //            var listHKD = daoCertifiedHousehold.uspdsHoKinhDoanh(strDiaPhuong, "",0, "", fromDate, toDate, 1, 1000000);
            //            foreach (CertifiedHousehold cer in listHKD)
            //            {
            //                tongVonHKD = double.Parse(cer.Biz_InvestCapital.ToString().Replace(".", "").Replace(" ", "")) + tongVonHKD;
            //            }
            //            tongHKD = listHKD.Count();

            //            foreach (BusinessField itemlv in listLV)
            //            {
            //                String lstIDLV = itemlv.Id.ToString();
            //                List<BusinessField> lstLVMapId = new BusinessFieldDao().ListAll(null).Where(x => x.MapId == itemlv.Id).ToList();
            //                if (lstLVMapId != null && lstLVMapId.Count > 0)
            //                {
            //                    foreach (BusinessField lvMap in lstLVMapId)
            //                    {
            //                        lstIDLV = lstIDLV + "," + lvMap.Id;

            //                    }
            //                }
            //                var listHKDtheoLV = daoCertifiedHousehold.uspdsHoKinhDoanh(strDiaPhuong, lstIDLV,0, "", fromDate, toDate, 1, 1000000);
            //                double TongVonHKDtheoLv = 0;
            //                double SumHKD = listHKDtheoLV.Count();
            //                double phanTramKHD = 0;
            //                double phanTramVon = 0;
            //                if (SumHKD > 0)
            //                {
            //                    phanTramKHD = Math.Round((SumHKD / tongHKD) * 100, 3);
            //                }
            //                foreach (CertifiedHousehold cer in listHKDtheoLV)
            //                {
            //                    TongVonHKDtheoLv = double.Parse(cer.Biz_InvestCapital.ToString().Replace(".", "").Replace(" ", "")) + TongVonHKDtheoLv;
            //                }
            //                if (TongVonHKDtheoLv > 0)
            //                {
            //                    phanTramVon = Math.Round((TongVonHKDtheoLv / tongVonHKD) * 100, 4);
            //                }
            //                dataChart1 = "{ y: " + phanTramKHD.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + SumHKD.ToString("#,##0.###") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }

            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "HKD";
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var daoHou = new HouseholdStatisticDao();
            //                var modelH = new HouseholdStatistic();
            //                modelH.HuyenId = item.Id;
            //                var objHo = daoHou.ListAll(modelH).FirstOrDefault();
            //                CapDoi = Convert.ToInt32(objHo.Capdoi);
            //                CapLai = Convert.ToInt32(objHo.Caplai);
            //                TamNgung = Convert.ToInt32(objHo.Tamngung);
            //                ChamDut = Convert.ToInt32(objHo.Chamdut);
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-right'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapDoi.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + CapLai.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + TamNgung.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                    "<td class='text-right'>" + ChamDut.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "HỘ KINH DOANH";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //    }
            //    else if (itemSearch.LoaiHinh == 2) // Loại hình hợp tác xã
            //    {
            //        TitleLoaiHinh = "HỢP TÁC XÃ";
            //        if (itemSearch.HuyenId == 0 && itemSearch.ToDate == null && itemSearch.FromDate == null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "HTX";
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-center'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "HỢP TÁC XÃ";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else if (itemSearch.HuyenId != 0 && itemSearch.XaPhuongId == 0 && itemSearch.ToDate == null && itemSearch.FromDate == null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "HTX";
            //            itThongKe.HuyenId = itemSearch.HuyenId.ToString();
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-center'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "HỢP TÁC XÃ";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else
            //        {
            //            var listHKD = daoHopTacXaHousehold.uspdsHopTacXa(strDiaPhuong, "",0, "", fromDate, toDate, 1, 1000000);
            //            foreach (HopTacXaHousehold cer in listHKD)
            //            {
            //                tongVonHKD = double.Parse(cer.Biz_InvestCapital.ToString().Replace(".", "").Replace(" ", "")) + tongVonHKD;
            //            }
            //            tongHKD = listHKD.Count();

            //            foreach (BusinessField itemlv in listLV)
            //            {
            //                String lstIDLV = itemlv.Id.ToString();
            //                List<BusinessField> lstLVMapId = new BusinessFieldDao().ListAll(null).Where(x => x.MapId == itemlv.Id).ToList();
            //                if (lstLVMapId != null && lstLVMapId.Count > 0)
            //                {
            //                    foreach (BusinessField lvMap in lstLVMapId)
            //                    {
            //                        lstIDLV = lstIDLV + "," + lvMap.Id;

            //                    }
            //                }
            //                var listHKDtheoLV = daoHopTacXaHousehold.uspdsHopTacXa(strDiaPhuong, lstIDLV,0, "", fromDate, toDate, 1, 1000000);
            //                double TongVonHKDtheoLv = 0;
            //                double SumHKD = listHKDtheoLV.Count();
            //                double phanTramKHD = 0;
            //                double phanTramVon = 0;
            //                if (SumHKD > 0)
            //                {
            //                    phanTramKHD = Math.Round((SumHKD / tongHKD) * 100, 3);
            //                }
            //                foreach (HopTacXaHousehold cer in listHKDtheoLV)
            //                {
            //                    TongVonHKDtheoLv = double.Parse(cer.Biz_InvestCapital.ToString().Replace(".", "").Replace(" ", "")) + TongVonHKDtheoLv;
            //                }
            //                if (TongVonHKDtheoLv > 0)
            //                {
            //                    phanTramVon = Math.Round((TongVonHKDtheoLv / tongVonHKD) * 100, 4);
            //                }
            //                dataChart1 = "{ y: " + phanTramKHD.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + SumHKD.ToString("#,##0.###") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //        }
                    
            //    }else if (itemSearch.LoaiHinh == 3) // Doanh nghiệp
            //    {
            //        TitleLoaiHinh = "DOANH NGHIỆP";
            //        if (itemSearch.HuyenId == 0 && itemSearch.ToDate == null && itemSearch.FromDate == null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "DN";
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-center'>" + sumByHuyen.ToString() + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "DOANH NGHIỆP";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else if (itemSearch.HuyenId != 0 && itemSearch.XaPhuongId == 0 && itemSearch.ToDate == null && itemSearch.FromDate == null)
            //        {
            //            var itThongKe = new ThongKeBieuDo();
            //            itThongKe.LoaiHinh = "DN";
            //            itThongKe.HuyenId = itemSearch.HuyenId.ToString();
            //            var daoThongKeBieuDo = new ThongKeBieuDoDao();
            //            var lstThongKe = daoThongKeBieuDo.ListAll(itThongKe);
            //            tongHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriHo));
            //            tongVonHKD = lstThongKe.Sum(x => double.Parse(x.GiaTriVon));

            //            foreach (BusinessField lv in listLV)
            //            {
            //                var lstThongKeTheoLV = lstThongKe.Where(x => x.IdLinhVuc == lv.Id.ToString());
            //                double TongVonHKDtheoLv = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriVon));
            //                double SumHKD = lstThongKeTheoLV.Sum(x => double.Parse(x.GiaTriHo));

            //                double phanTramKHD = (SumHKD / tongHKD) * 100;
            //                double phanTramVon = (TongVonHKDtheoLv / tongVonHKD) * 100;

            //                dataChart1 = "{ y: " + phanTramKHD.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + SumHKD.ToString("#,##0.###").Replace(",", ".") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString("#,##0.###").Replace(",", ".") + ", label: '" + lv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }

            //            //Bảng thống kê
            //            string tableContent = "";
            //            foreach (Province item in lstHuyen)
            //            {
            //                var sumByHuyen = lstThongKe.Where(x => int.Parse(x.HuyenId) == item.Id).Sum(x => double.Parse(x.GiaTriHo));
            //                tableContent += "<tr>" +
            //                                    "<td class='text-left'>" + item.Title + "</td>" +
            //                                    "<td class='text-center'>" + sumByHuyen.ToString("#,##0.###").Replace(",", ".") + " </td>" +
            //                                "</tr>";
            //            }
            //            headerTable = "DOANH NGHIỆP";
            //            ViewBag.tableContent = tableContent;
            //            //End Bảng thống kê
            //        }
            //        else
            //        {
            //            DanhSachDoanhNghiep itDanhSachDoanhNghiep = new DanhSachDoanhNghiep();
            //            itDanhSachDoanhNghiep.HuyenId = itemSearch.HuyenId;
            //            itDanhSachDoanhNghiep.PhuongId = itemSearch.XaPhuongId;

            //            var listHKD = daoDoanhNghiep.ListAll(itDanhSachDoanhNghiep).Where(
            //                                                             x => x.isCheckGiaiThe == null &&
            //                                                             x.NgayCap >= fromDate && x.NgayCap <= toDate
            //                                                            );
            //            tongVonHKD = listHKD.Where(x => x.VonDieuLe != "").Sum(x => long.Parse(x.VonDieuLe));
            //            tongHKD = listHKD.Count();
            //            foreach (BusinessField itemlv in listLV)
            //            {
            //                List<string> LstlvID = new List<string>();
            //                LstlvID.Add(itemlv.Id.ToString());
            //                List<BusinessField> lstLVMapId = new BusinessFieldDao().ListAll(null).Where(x => x.MapId == itemlv.Id).ToList();
            //                if (lstLVMapId != null && lstLVMapId.Count > 0)
            //                {
            //                    foreach (BusinessField lvMap in lstLVMapId)
            //                    {
            //                        LstlvID.Add(lvMap.Id.ToString());
            //                    }
            //                }
            //                itDanhSachDoanhNghiep.lstIDLV = LstlvID;
            //                var listHKDtheoLV = daoDoanhNghiep.ListAll(itDanhSachDoanhNghiep).Where(
            //                                                                    x =>
            //                                                                    x.isCheckGiaiThe == null &&
            //                                                                    x.NgayCap >= fromDate && x.NgayCap <= toDate
            //                                                                    );
            //                double TongVonHKDtheoLv = 0;
            //                double SumHKD = listHKDtheoLV.Count();
            //                double phanTramKHD = 0;
            //                double phanTramVon = 0;
            //                if (SumHKD > 0)
            //                {
            //                    phanTramKHD = Math.Round((SumHKD / tongHKD) * 100, 3);
            //                }
            //                TongVonHKDtheoLv = listHKDtheoLV.Where(x => x.VonDieuLe != "").Sum(x => long.Parse(x.VonDieuLe));
            //                if (TongVonHKDtheoLv > 0)
            //                {
            //                    phanTramVon = Math.Round((TongVonHKDtheoLv / tongVonHKD) * 100, 4);
            //                }
            //                dataChart1 = "{ y: " + phanTramKHD.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + SumHKD.ToString("#,##0.###") + " Hộ)' }," + dataChart1;
            //                dataChart2 = "{ y: " + phanTramVon.ToString().Replace(",", ".") + ", label: '" + itemlv.Name + " (" + TongVonHKDtheoLv.ToString("#,##0.###").Replace(",", ".") + " đồng)' }," + dataChart2;
            //            }
            //        }
                    
            //    }
                
            //    //Tạo script biểu đồ tròn.
            //    string scriptChart = "<script>" +
            //                            "window.onload = function () {" +
            //                                "var chart1 = new CanvasJS.Chart('chartHo', {" +
            //                                    "theme: 'light2'," +
            //                                    "exportEnabled: false," +
            //                                    "animationEnabled: true," +
            //                                    "title: {" +
            //                                        "text: ''" +
            //                                    "}," +
            //                                    "data: [{" +
            //                                        "type: 'pie'," +
            //                                        "startAngle: 25," +
            //                                        "toolTipContent: '<b>{label}</b>: {y}%'," +
            //                                        "indexLabelFontSize: 16," +
            //                                        "indexLabel: '{label} - {y}%'," +
            //                                        "dataPoints: [" +
            //                                            dataChart1 +
            //                                        "]" +
            //                                    "}]" +
            //                                "});" +
            //                                "var chart2 = new CanvasJS.Chart('chartVon', {" +
            //                                    "theme: 'light2'," +
            //                                    "exportEnabled: false," +
            //                                    "animationEnabled: true," +
            //                                    "title: {" +
            //                                        "text: ''" +
            //                                    "}," +
            //                                    "data: [{" +
            //                                        "type: 'pie'," +
            //                                        "startAngle: 25," +
            //                                        "toolTipContent: '<b>{label}</b>: {y}%'," +
            //                                        "indexLabelFontSize: 16," +
            //                                        "indexLabel: '{label} - {y}%'," +
            //                                        "dataPoints: [" +
            //                                            dataChart2 +
            //                                        "]" +
            //                                    "}]" +
            //                                "});" +
            //                                "chart1.render();" +
            //                                "chart2.render();" +
            //                            "}" +
            //                        "</script>";

            //    ViewBag.headerTable = headerTable;
            //    ViewBag.scriptChart = scriptChart;
            //    ViewBag.tongHKD = tongHKD.ToString("#,##0.###");
            //    ViewBag.tongVonHKD = tongVonHKD.ToString("#,##0.###");
            //    ViewBag.TitleLoaiHinh = TitleLoaiHinh;
            //    ViewBag.TitleToanTinh = TitleToanTinh;
            //    ViewBag.LoaiHinh = itemSearch.LoaiHinh;
            //}
           
            
            return View(itemSearch);
        }
        [HasCredential(RoleID = "DASHBOARD2_HOME")]
        public ActionResult Dashboard2()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID == CommonConstants.QLGIANHANG_GROUP) // Quản lý gian hàng
            {
                ViewBag.ShowDashboardQLGianHang = "true";
                //Gian hàng chờ duyệt
                var daoStore = new StoreDao();
                int totalRecordGianHangChoDuyet = 0;
                int totalRecordGianHang = 0;
                Store itStore = new Store();

                itStore.StatusId = CommonConstants.GIANHANG_CHODUYET;
                var dsGianHangChoDuyet = daoStore.ListAllPaging(itStore, "", ref totalRecordGianHangChoDuyet, 1, 1000);
                ViewBag.totalRecordGianHangChoDuyet = totalRecordGianHangChoDuyet;

                var dsGianHang = daoStore.ListAllPaging(null, "", ref totalRecordGianHang, 1, 1000);
                ViewBag.totalRecordGianHang = totalRecordGianHang;
                //End gian hàng chờ duyệt

                //San phẩm chờ duyệt
                var daoProduct = new ProductDao();
                int totalRecordSanPhamChoDuyet = 0;
                int totalRecordSanPham = 0;
                Product itProduct = new Product();

                itProduct.StatusId = CommonConstants.SANPHAM_CHODUYET;
                var dsSanPhamChoDuyet = daoProduct.ListAllPaging(itProduct, "", ref totalRecordSanPhamChoDuyet, 1, 1000);
                ViewBag.totalRecordSanPhamChoDuyet = totalRecordSanPhamChoDuyet;

                var dsSanPham = daoProduct.ListAllPaging(null, "", ref totalRecordSanPham, 1, 1000);
                ViewBag.totalRecordSanPham = totalRecordSanPham;
                //End Sản phẩm chờ duyệt
            }
            if (session.GroupID == CommonConstants.QLLIENHE_GROUP) // Quản lý liên hệ
            {
                ViewBag.ShowDashboardQLLienHe = "true";
                Contact itContact = new Contact();
                var dao = new ContactDao();
                int totalRecordChoTiepNhan = 0;
                int totalRecordDuocDuyet = 0;

                itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_CHOTIEPNHAN;
                var dsChoTiepNhan = dao.ListAllPaging(itContact, "", ref totalRecordChoTiepNhan, 1, 1000);
                ViewBag.totalRecordChoTiepNhan = totalRecordChoTiepNhan;

                itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DUOCDUYET;
                var dsDuocDuyet = dao.ListAllPaging(itContact, "", ref totalRecordDuocDuyet, 1, 1000);
                ViewBag.totalRecordDuocDuyet = totalRecordDuocDuyet;
            }
            return View();
        }

        [HasCredential(RoleID = "DASHBOARD3_HOME")]
        public ActionResult Dashboard3()
        {
            return View();
        }

        [HasCredential(RoleID = "DASHBOARD4_HOME")]
        public ActionResult Dashboard4()
        {
            return View();
        }
        [HasCredential(RoleID = "DASHBOARD5_HOME")]
        public ActionResult Dashboard5()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID==CommonConstants.ADMIN_GROUP)
            {
                ViewBag.ShowDashboardAdmin = true;
            }
            if (session.GroupID == CommonConstants.BIENTAP_GROUP)
            {
                ViewBag.ShowDashboardBienTap = true;
            }
            return View();
        }
     
    }
}