//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinAuthAndAzureAuthTestForURCS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProjectRole
    {
        public int UserProjectRoleId { get; set; }
        public int UserAccountID { get; set; }
        public int RoleId { get; set; }
        public int ProjectID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
