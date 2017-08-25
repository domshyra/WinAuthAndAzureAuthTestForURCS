using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using WinAuthAndAzureAuthTestForURCS.Models;
using WinAuthAndAzureAuthTestForURCS.ViewModels;

namespace WinAuthAndAzureAuthTestForURCS.Controllers
{
    [Authorize(Roles = "Site Admin")]
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        private WinAuthAndAzureAuthTestForURCSEntities db = new WinAuthAndAzureAuthTestForURCSEntities();
        // GET: UserAccount
        public ActionResult Index()
        {
            string Name = User.Identity.Name.ToString();
            ViewBag.Name = Name;
            var users = db.UserAccounts.Include(x => x.UserProjectRoles);
            return View(users.ToList());
        }
        public ActionResult Edit(int? id)
        {
            //   UserAccount user = db.UserAccounts.Find(id);
            UserAccountViewModel user = new UserAccountViewModel();
            user.vm_UserAccount = db.UserAccounts.Find(id);
            user.vm_Roles = db.Roles.ToArray();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserAccountViewModel user)
        {
            UserAccount tempUser = db.UserAccounts.FirstOrDefault(x => x.UserAccountID == user.vm_UserAccount.UserAccountID);

            foreach (CurrentUserPermissionListItem cpli in user.vm_rList)
            {
                var oldRoleAssignment = tempUser.UserProjectRoles.FirstOrDefault(m => m.RoleId == cpli.RoleID);
                if (oldRoleAssignment != null && cpli.IsAssigned == false)   //user is currently assigned and the box was unchecked
                {
                    db.UserProjectRoles.Remove(oldRoleAssignment);
                }
                else if (oldRoleAssignment == null && cpli.IsAssigned == true) //user was not assigned and the box for this role was checked
                {
                    UserProjectRole newRole = new UserProjectRole();
                    newRole.UserAccountID = user.vm_UserAccount.UserAccountID;
                    newRole.RoleId = cpli.RoleID;
                    db.UserProjectRoles.Add(newRole);
                }
            }

            tempUser.UserName = user.vm_UserAccount.UserName;
            tempUser.FirstName = user.vm_UserAccount.FirstName;
            tempUser.LastName = user.vm_UserAccount.LastName;
            tempUser.Email = user.vm_UserAccount.Email;
            tempUser.Locked = user.vm_UserAccount.Locked;
            tempUser.UserAccountID = user.vm_UserAccount.UserAccountID;

            db.Entry(tempUser).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "User successfully updated";
            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {

            UserAccountViewModel user = new UserAccountViewModel();
            user.vm_Roles = db.Roles.ToArray();
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(UserAccountViewModel user)
        {

            UserAccount tempUser = new UserAccount();

            foreach (CurrentUserPermissionListItem cpli in user.vm_rList)
            {
                if (cpli.IsAssigned == true) //user was not assigned and the box for this role was checked
                {
                    UserProjectRole newRole = new UserProjectRole();
                    newRole.UserAccountID = tempUser.UserAccountID;
                    newRole.RoleId = cpli.RoleID;
                    db.UserProjectRoles.Add(newRole);
                }
            }

            tempUser.UserName = user.vm_UserAccount.UserName;
            tempUser.FirstName = user.vm_UserAccount.FirstName;
            tempUser.LastName = user.vm_UserAccount.LastName;
            tempUser.Email = user.vm_UserAccount.Email;
            tempUser.Locked = user.vm_UserAccount.Locked == null ? 0 : user.vm_UserAccount.Locked;
            tempUser.UserAccountID = user.vm_UserAccount.UserAccountID;

            db.UserAccounts.Add(tempUser);
            db.SaveChanges();
            TempData["Message"] = "User successfully created";
            return RedirectToAction("Index");

        }


    }

}