﻿using System;
using System.Linq;
using System.Web;
using System.Net;
using WinAuthAndAzureAuthTestForURCS.Models;
using System.Security.Claims;
using System.Web.Security;

namespace WinAuthAndAzureAuthTestForURCS.Utils
{
    public class URCSHelpers
    {
        public static int setUserSession()
        {
            WinAuthAndAzureAuthTestForURCSEntities db = new WinAuthAndAzureAuthTestForURCSEntities();
            
            //Gobal vars for user info
            string username = "";
            string usernameSplit;

            //Used for Windows auth

            username = HttpContext.Current.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(username))
            {
                usernameSplit = username.Split('\\')[1];

                HttpContext.Current.Session["username"] = usernameSplit;
                try
                {
                    UserAccount user = db.UserAccounts.Where(u => u.UserName == usernameSplit).FirstOrDefault();
                    HttpContext.Current.Session["userID"] = user.UserAccountID;
                    HttpContext.Current.Session["firstName"] = user.FirstName;
                    HttpContext.Current.Session["lastName"] = user.LastName;
                    HttpContext.Current.Session["RequestURL"] = string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
                    return (Int32)HttpStatusCode.OK;
                }
                catch
                {
                    HttpContext.Current.Session["firstName"] = "";
                    HttpContext.Current.Session["lastName"] = username;
                    return (Int32)HttpStatusCode.Unauthorized;
                }
            }

            //If win auth fails use OAuth
            else
            {
                try
                {
                    var claimsPrincipalCurrent = ClaimsPrincipal.Current;
                    var email = claimsPrincipalCurrent.FindFirst("preferred_username").Value;
                    try
                    {
                        UserAccount user = db.UserAccounts.Where(u => u.Email == email).FirstOrDefault();
                        AddCurrentUserToRoles(user);
                        HttpContext.Current.Session["username"] = user.UserName;
                        HttpContext.Current.Session["userID"] = user.UserAccountID;
                        HttpContext.Current.Session["firstName"] = user.FirstName;
                        HttpContext.Current.Session["lastName"] = user.LastName;
                        HttpContext.Current.Session["RequestURL"] = string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
                        return (Int32)HttpStatusCode.OK;
                    }
                    catch
                    {
                        return (Int32)HttpStatusCode.Unauthorized;
                    }

                }
                catch (NullReferenceException e)
                {

                }
            }

            return (Int32)HttpStatusCode.Unauthorized;

        }
        public static string GetFirstAndLastNameForCurrentUser()
        {
            return HttpContext.Current.Session["firstName"] + " " + HttpContext.Current.Session["lastName"];
        }

        public static void AddCurrentUserToRoles(UserAccount user)
        {

            foreach (UserProjectRole projRole in user.UserProjectRoles)
            {
                // Code doesn't work, but we need something like it
                Roles.AddUserToRole(user.UserName, projRole.Role.RoleName);
            }
        }
    }
}