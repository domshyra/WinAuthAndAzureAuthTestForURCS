using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinAuthAndAzureAuthTestForURCS.Models;

namespace WinAuthAndAzureAuthTestForURCS.ViewModels
{
    public class UserAccountViewModel
    {
        public UserAccountViewModel() // MVC can call that
        {
            vm_rList = new List<CurrentUserPermissionListItem>();
        }

        public IList<CurrentUserPermissionListItem> vm_rList;


        public UserAccount vm_UserAccount { get; set; }
        public ICollection<Role> vm_Roles { get; set; }

        public IList<CurrentUserPermissionListItem> vm_RoleList
        {
            get
            {
                IList<CurrentUserPermissionListItem> returnval = new List<CurrentUserPermissionListItem>();
                if (vm_Roles == null)
                    return returnval;
                foreach (Role r in vm_Roles)
                {
                    UserProjectRole tempRole = null;
                    if (vm_UserAccount != null)
                        tempRole = vm_UserAccount.UserProjectRoles.FirstOrDefault(m => m.RoleId == r.RoleID);
                    CurrentUserPermissionListItem i = new CurrentUserPermissionListItem(r.RoleName, r.RoleID, tempRole != null);
                    returnval.Add(i);
                }
                return returnval;
            }

            set { this.vm_rList = value; }

        }



    }

    public class CurrentUserPermissionListItem
    {
        public CurrentUserPermissionListItem() // MVC can call that
        {
        }

        public CurrentUserPermissionListItem(string roleName, int roleID, bool isAssigned)
        {
            RoleName = roleName;
            RoleID = roleID;
            IsAssigned = isAssigned;
        }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
        public bool IsAssigned { get; set; }
    }
}