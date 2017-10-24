using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcBlogPortfolio.App_Classes
{
    using Models;
    public class CustumRolProvider : RoleProvider
    {
        dbModelblog db = new dbModelblog();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username) //Git Kullanıcı Adına göre Kullanıcının rollerini getir.Kullanıcının hangi rollere sahip olduğuna bakacak eğer admin yoksa girişe izin vermicek admin varsa girişe izin verecek.

        {
            if (!string.IsNullOrEmpty(username))//KullanıcıAdı Boş veya null değilse işlemleri yap.
            {
                Kullanicilar kl = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == username);
                if (kl!=null)
                {
                    return kl.KullaniciRol == null ? new string[] { }:kl.KullaniciRol.Select(x => x.Rol).Select(x => x.RolAdi).ToArray();
                }
            }
            return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}