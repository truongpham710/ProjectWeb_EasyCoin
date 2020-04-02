using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;
using API_Wallet.Models;
using API_Wallet.Providers;

namespace API_Wallet.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository
    {
        private static string _userRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        public UserRepository()
        {
            _userRoot = Path.Combine(HostingEnvironment.ApplicationPhysicalPath ?? Directory.GetCurrentDirectory(), @"App_Data\UserManager.xml");
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<User> FindById(string userId)
        {
            try
            {
                XElement xelement = XElement.Load(_userRoot);
                XElement xmlUser = xelement.Elements("user").FirstOrDefault(nm => (string)nm.Element("username") == userId);

                if (xmlUser == null) return null;

                var user = new User
                {
                    Id = xmlUser.Element("id").Value,
                    UserName = userId,
                    Password = xmlUser.Element("password").Value,
                    Role = xmlUser.Element("role").Value,
                    Permission = xmlUser.Element("permission").Value,
                    ApiSecretkey = xmlUser.Element("secretkey").Value,
                };

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="role">The role.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="locked">The locked.</param>
        /// <returns></returns>
        public async Task<bool> CreateUser(string userName, string password, string role, string permission, string locked)
        {
            try
            {
                XElement xelement = XElement.Load(_userRoot);
                xelement.Add(new XElement("user",
                         new XElement("id", Guid.NewGuid()),
                         new XElement("username", userName),
                         new XElement("role", role),
                         new XElement("permission", permission),
                         new XElement("locked", locked),
                         new XElement("password", new EasybookPasswordHasher().HashPassword(password)),
                         new XElement("secretkey", "12345")));

                xelement.Save(_userRoot);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}