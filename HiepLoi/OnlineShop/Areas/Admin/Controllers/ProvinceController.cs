using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using ClsCommon;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProvinceController : BaseController
    {
        // GET: Admin/Province
        [HasCredential(RoleID = "INDEX_PROVINCE")]
        public ActionResult Index(string searchString, int parentid = 0, int page = 1, int pageSize = 10)
        {
            var dao = new ProvinceDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(parentid, ref totalRecord, searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            ViewBag.ParentId = parentid;
            if (parentid > 0)
            {
                ViewBag.ParentIdOfParenId = dao.GetById(parentid).ParentId;
            }
            
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PROVINCE";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_PROVINCE")]
        public ActionResult Create(int parentid = 0)
        {
            SetViewBag(null,parentid);
            return View();
        }
        [HasCredential(RoleID = "EDIT_PROVINCE")]
        public ActionResult Edit(int id)
        {
            var province = new ProvinceDao().ViewDetail(id);
            int parentid = province.ParentId.Value;
            SetViewBag(null, parentid);
            return View(province);
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_PROVINCE")]
        public ActionResult Create(Province province)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProvinceDao();

                long id = dao.Insert(province);
                if (id > 0)
                {
                    SetAlert("Thêm địa danh thành công", "success");
                    return RedirectToAction("Index", "Province");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm địa danh không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_PROVINCE")]
        public ActionResult Delete(int id)
        {
            new ProvinceDao().Delete(id);

            return RedirectToAction("Index");
        }
        public void SetViewBag(int? selectedId = null, int parentid = 0)
        {
            var dao = new ProvinceDao();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Province> lstProvine = dao.ListByParentId(parentid);
            list.Add(new SelectListItem { Value =  "0", Text = "--Chọn--" });
            foreach (Province item in lstProvine)
            {
                list.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            //SelectList sl = new SelectList(dao.ListByParentId(parentid), "Id", "Title", selectedId);
            //ViewBag.ParentId = sl;
            ViewBag.ParentId = new SelectList(list, "Value", "Text", selectedId);
        }
        public string getProvinceByParent(int parentid)
        {
            string option = "<option value='0'>--Chọn--</option>";
            if (parentid !=0)
            {
                var dao = new ProvinceDao();
                Province itemProvince = new Province();
                itemProvince.ParentId = parentid;
                List<Province> lstProvine = dao.ListAll(itemProvince);
                foreach (Province item in lstProvine)
                {
                    option = option + "<option value='" + item.Id + "'>" + item.Title + "</option>";
                }
            }
            return option;
        }
    }
}