using Model.Dao;
using Model.EF;
using ClsCommon;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CredentialController : BaseController
    {
        // GET: Admin/Credential
        [HasCredential(RoleID = "INDEX_CREDENTIAL")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CredentialDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CREDENTIAL";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_CREDENTIAL")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CREDENTIAL")]
        public ActionResult Create(Credential model)
        {
            if (ModelState.IsValid)
            {
                var daoRole = new RoleDao();
                Role itemrole = new Role();
                itemrole.SysFunctionID = model.FunctionID;
                List<Role> lstRole = daoRole.ListAll(itemrole);
                var daoCredential = new CredentialDao();
                Credential itemCredential = new Credential();
                itemCredential.UserGroupID = model.UserGroupID;
                //Xóa dữ liệu phân quyền sau đó lưu lại bản ghi mới
                foreach (Role item in lstRole)
                {
                    itemCredential.RoleID = item.ID;
                    Credential itemDeleteCredential = daoCredential.ListAll(itemCredential).FirstOrDefault();
                    if (itemDeleteCredential !=null)
                    {
                        daoCredential.DeleteEntity(itemDeleteCredential);
                    }
                }
                //tạo lại dữ liệu sau khi xóa
                if (model.lstRole != null)
                {
                    string tmp = model.lstRole.ToString();
                    string[] lstRoleSave = tmp.Split(new string[] { "|" }, StringSplitOptions.None);
                    try
                    {
                        for (int i = 0; i < lstRoleSave.Length - 1; i++)
                        {
                            model.RoleID = lstRoleSave[i];
                            new CredentialDao().Insert(model);
                        }
                        var session = (UserLogin)Session[CommonConstants.USER_SESSION];

                        //model.Creator = session.UserName;
                        //model.Created = DateTime.Now;

                        Session[CommonConstants.MSG_SUCCESS] = "Lưu thành công";
                    }
                    catch (Exception ex)
                    {
                        Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
                        throw ex;
                    }
                }
                return RedirectToAction("Create");
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_CREDENTIAL")]
        public ActionResult Edit(String id)
        {
            var dao = new CredentialDao();
            var Credential = dao.GetById(id);
            SetViewBag();
            return View(Credential);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_CREDENTIAL")]
        [ValidateInput(false)]
        public ActionResult Edit(Credential model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Modifier = session.UserName;
                    //model.Modified = DateTime.Now;
                    new CredentialDao().Update(model);
                    Session[CommonConstants.MSG_SUCCESS] = "Sửa thành công";
                }
                catch (Exception ex)
                {
                    Session[CommonConstants.MSG_ERROR] = "Sửa thất bại";
                    throw ex;
                }
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Sửa thất bại";
            }
            return RedirectToAction("Index");
        }

        //[HasCredential(RoleID = "VIEW_CREDENTIAL")]
        //public ActionResult View(String id)
        //{
        //    var Credential = new CredentialDao().ViewDetail(id);
        //    ViewBag.Credential = Credential;
        //    return View(Credential);
        //}

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CREDENTIAL")]
        public ActionResult Delete(String id)
        {
            var dao = new CredentialDao();
            var Credential = dao.Delete(id);
            return View(Credential);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_CREDENTIAL")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    String id = i.ToString();
                    result = new CredentialDao().Delete(id);
                }
                Session[CommonConstants.MSG_SUCCESS] = "Xóa thành công";
                return Json(new
                {
                    Credential = result
                });
            }
            catch (Exception ex)
            {
                Session[CommonConstants.MSG_ERROR] = "Xóa thất bại";
                return Json(new
                {
                    Credential = false,
                    message = ex.Message
                });

            }
        }
        public string getListRole(int FunctionID, String UserGroupID)
        {
            var daoRole = new RoleDao();
            var daoCredential = new CredentialDao();
            Credential itemCredential = new Credential();
            itemCredential.UserGroupID = UserGroupID;
            Role itemrole = new Role();
            itemrole.SysFunctionID = FunctionID;
            List<Role> lstRole = daoRole.ListAll(itemrole);
            String html = "";
            String check = "";
            foreach (Role it in lstRole)
            {
                itemCredential.RoleID = it.ID;
                List<Credential> lstCredential = daoCredential.ListAll(itemCredential).ToList();
                if (lstCredential.Count>0) {
                    check = " checked";
                }
                html += "<div style='padding-right:15px;margin-left:200px'>" +
                                "<input id='"+ it.ID +"'"+ check + " type='checkbox' form='formPhanQuyen'  name='skill' value='" + it.ID +"' />" +
                                "<label for='" + it.ID + "'> " + it.ID + " </label>" +
                            "</div>";
                check = "";
            }
            
            return html;
        }
        public void SetViewBag(long? selectedId = null)
        {
            var daoFunction = new SysFunctionDao();
            var daoUserGroup = new UserGroupDao();
            List<SelectListItem> listChucNang = new List<SelectListItem>();
            List<SelectListItem> listVaiTro = new List<SelectListItem>();
            List<UserGroup> lstUserGroup = daoUserGroup.ListAll(null);
            //List<SysFunction> lstSysFunction = daoFunction.ListAll(null).Where(x => x.ParentId != 0).ToList();
            List<SysFunction> lstSysFunction = daoFunction.ListAll(null).ToList();
            foreach (SysFunction item in lstSysFunction)
            {
                listChucNang.Add(new SelectListItem { Value = item.FunctionID + "", Text = item.FunctionName });
            }
            foreach (UserGroup item in lstUserGroup)
            {
                listVaiTro.Add(new SelectListItem { Value = item.ID + "", Text = item.Name });
            }
            ViewBag.lstSkills = daoFunction.ListAll(null).Where(x => x.ParentId != 0).ToList();
            ViewBag.FunctionID = new SelectList(listChucNang, "Value", "Text", selectedId);
            ViewBag.UserGroupID = new SelectList(listVaiTro, "Value", "Text", selectedId);
        }
    }
}