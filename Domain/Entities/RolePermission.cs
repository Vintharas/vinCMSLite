//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Domain.Entities
{
    public partial class RolePermission : StateObject
    {
        #region Primitive Properties
    
        public int RolePermissionsID
        {
            get { return _rolePermissionsID; }
            set
            {
                if (_rolePermissionsID != value)
                {
                    if (UserRole != null && UserRole.UserRoleID != value)
                    {
                        UserRole = null;
                    }
                    _rolePermissionsID = value;
                }
            }
        }
        private int _rolePermissionsID;
    
        public bool IsAdmin
        {
            get;
            set;
        }
    
        public bool CanCreateContent
        {
            get;
            set;
        }
    
        public bool CanDeleteContent
        {
            get;
            set;
        }
    
        public bool CanEditContent
        {
            get;
            set;
        }
    
        public bool CanCreateUser
        {
            get;
            set;
        }
    
        public bool CanDeleteUser
        {
            get;
            set;
        }
    
        public bool CanEditUser
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public UserRole UserRole
        {
            get { return _userRole; }
            set
            {
                if (!ReferenceEquals(_userRole, value))
                {
                    var previousValue = _userRole;
                    _userRole = value;
                    FixupUserRole(previousValue);
                }
            }
        }
        private UserRole _userRole;

        #endregion
        #region Association Fixup
    
        private void FixupUserRole(UserRole previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.RolePermission, this))
            {
                previousValue.RolePermission = null;
            }
    
            if (UserRole != null)
            {
                UserRole.RolePermission = this;
                if (RolePermissionsID != UserRole.UserRoleID)
                {
                    RolePermissionsID = UserRole.UserRoleID;
                }
            }
        }

        #endregion
    }
}
