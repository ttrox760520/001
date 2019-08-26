using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers
{
    public class UserInfoController : Controller
    {
        #region "客戶資料"

        public ActionResult UserList()
        {
            var db = new TextEntities();
            List<UserInfo> dbInfo = db.UserInfo.ToList();
            var viewModelList = new List<UserInfoViewModel>();
            foreach (UserInfo info in dbInfo)
            {
                var tmpInfo = new UserInfoViewModel()
                {
                    UserID = info.UserID,
                    UserName = info.UserName,
                    Phone = info.Phone,
                    Email = info.Email,
                };
                viewModelList.Add(tmpInfo);
            }
            return View(viewModelList);

        }
        #endregion "查詢"

        #region "新增"
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo info)
        {
            var db = new TextEntities();
            List<UserInfo> dbInfo = db.UserInfo.ToList();
            var newuserinfo = new UserInfo();
     
            foreach (UserInfo theinfo in dbInfo)
            {
                if (info.UserID.Equals(theinfo.UserID))
                {
                    ViewData["Message"] = "客戶編號重複，請更改編號！";
                    return View();
                }
            }
            newuserinfo = info;
            db.UserInfo.Add(newuserinfo);
            db.SaveChanges();
            return RedirectToAction("UserList");

        }

        #endregion "新增"

        #region "詳細資料"
        public ActionResult Details(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var db = new TextEntities();
                List<UserInfo> dbInfo = db.UserInfo.ToList();
                UserInfoViewModel a = new UserInfoViewModel();
                a.UserID = dbInfo[int.Parse(id)].UserID;
                a.UserName = dbInfo[int.Parse(id)].UserName;
                a.Phone = dbInfo[int.Parse(id)].Phone;
                a.Email = dbInfo[int.Parse(id)].Email;

                return View(a);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Details(string id, UserInfo info)
        {
            var db = new TextEntities();
            List<UserInfo> dbInfo = db.UserInfo.ToList();
            try
            {
                dbInfo[int.Parse(id)].UserID = info.UserID;
                dbInfo[int.Parse(id)].UserName = info.UserName;
                dbInfo[int.Parse(id)].Phone = info.Phone;
                dbInfo[int.Parse(id)].Email = info.Email;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                ViewData["Message"] = "修改出錯了唷!";
                return RedirectToAction("Edit");
            }


        }
        #endregion "詳細資料"

        #region "修改"

        public ActionResult Edit(string id)
        {
            var db = new TextEntities();
            List<UserInfo> dbInfo = db.UserInfo.ToList();
            var viewModelList = new List<UserInfoViewModel>();
            var tmpInfo = new UserInfoViewModel()
            {
                UserID = dbInfo[int.Parse(id)].UserID,
                UserName = dbInfo[int.Parse(id)].UserName,
                Phone = dbInfo[int.Parse(id)].Phone,
                Email = dbInfo[int.Parse(id)].Email,
            };
            viewModelList.Add(tmpInfo);
            return View(viewModelList);
        }
        [HttpPost]
        public ActionResult Edit(string id, UserInfo info)
        {
            var db = new TextEntities();
            List<UserInfo> dbInfo = db.UserInfo.ToList();
            try
            {
                dbInfo[int.Parse(id)].UserID = info.UserID;
                dbInfo[int.Parse(id)].UserName = info.UserName;
                dbInfo[int.Parse(id)].Phone = info.Phone;
                dbInfo[int.Parse(id)].Email = info.Email;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                ViewData["Message"] = "修改出錯了唷!";
                return RedirectToAction("Edit");
            }
        }

        #endregion "修改"

        #region "刪除"

        public ActionResult Delete(string id)
        {
            try
            {
                var db = new TextEntities();
                List<UserInfo> dbInfo = db.UserInfo.ToList();
                db.UserInfo.Remove(dbInfo[int.Parse(id)]);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                ViewData["Message"] = "刪除失敗了唷!";
                return View("UserList");
            }
        }
        #endregion "刪除"
    }
}