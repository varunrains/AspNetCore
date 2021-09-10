using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        //Dont mix and match in your project
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

      

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUsers.Include(u => u.Company).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach(var user in userList)
            {
                var roleId = userRole.FirstOrDefault(d => d.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = userList });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var deleteObj = _unitOfWork.Category.Get(id);

        //    if(deleteObj == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting the category" });
        //    }
        //    else
        //    {
        //        _unitOfWork.Category.Remove(deleteObj);
        //        _unitOfWork.Save();
        //        return Json(new { success = true, message = "Deleted successfully" });
        //    }

        //}
        
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDB = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if(objFromDB == null)
            {
                return Json(new { success = false, message = "error while locking/unlocking" });
            }
            if(objFromDB.LockoutEnd != null && objFromDB.LockoutEnd > DateTime.Now)
            {
                objFromDB.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDB.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _db.SaveChanges();

            return Json(new { success = true, message = "Operation successful!" });
        }

        #endregion
    }
}
