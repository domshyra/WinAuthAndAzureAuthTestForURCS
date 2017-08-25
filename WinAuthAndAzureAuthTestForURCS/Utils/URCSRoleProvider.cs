using System;
using System.Linq;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using WinAuthAndAzureAuthTestForURCS.Models;

namespace WinAuthAndAzureAuthTestForURCS.Utils
{
    public class URCSRoleProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (WinAuthAndAzureAuthTestForURCSEntities db = new WinAuthAndAzureAuthTestForURCSEntities())
            {
                UserAccount user = db.UserAccounts.Find(username);

                Role role = db.Roles.Find(roleName);

                bool returnval = false;

                foreach (UserProjectRole u in user.UserProjectRoles)
                {
                    if (u.RoleId == role.RoleID)
                    {
                        returnval = true;
                    }
                }


                return returnval;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!username.IsNullOrWhiteSpace())
            {
                string usernameSplit = username.Split('\\')[1];


                using (WinAuthAndAzureAuthTestForURCSEntities db = new WinAuthAndAzureAuthTestForURCSEntities())
                {
                    UserAccount user = db.UserAccounts.FirstOrDefault(u => u.UserName == usernameSplit);

                    if (user == null || user.UserProjectRoles == null || user.Locked == 1)
                    {
                        return new string[0];
                    }

                    return user.UserProjectRoles.Select(r => r.Role.RoleName).ToArray();
                }
            }
            string[] empty = new string[0];

            return empty;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}