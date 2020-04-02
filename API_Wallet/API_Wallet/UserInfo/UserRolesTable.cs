using System;
using System.Collections.Generic;

namespace API_Wallet.UserInfo
{
    /// <summary>
    /// Class that represents the UserRoles table in the Database
    /// </summary>
    public class UserRolesTable
    {
        //private IdentityUserQueryBuilder _identityUserQueryBuilder = new IdentityUserQueryBuilder();
        //private readonly UserTransactionUow _userTransctionUow = new UserTransactionUow();

        /// <summary>
        /// Constructor that takes a LINQ to SQL DataContext instance 
        /// </summary>
        public UserRolesTable()
        {
        }

        /// <summary>
        /// Returns a list of user's roles which match the given user's Id.
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>List of user's role name</returns>
        public List<string> FindByUserId(string userId)
        {
            var roles = new List<string>();

            //if (string.IsNullOrEmpty(userId)) return new List<string>();
            ////SingmayAdminService singmayAdminService = new SingmayAdminService();

            //// get a list of user role's name for given user from database
            ////string[] roleArray = singmayAdminService.UserRoles_FindByUserId(userId, common.getCallerID(), common.getCallerPWD());

            ////// convert array to list
            ////if (roleArray != null)
            ////{
            ////    roles = roleArray.ToList();
            ////}

            ////no use no test yet
            //using(var identityUserQueryBuilder = new IdentityUserQueryBuilder())
            //{
            //    IEnumerable<AspNetUsers> user = identityUserQueryBuilder.HasId(userId).IncludeRole().ToList();

            //    if (user == null) return roles;
            //    foreach(var rowUser in user)
            //    {
            //        roles.AddRange(rowUser.AspNetRoles.Select(rowRole => rowRole.Name));
            //    }
            //}
            return roles;
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles which match the given userId.
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>0</returns>
        public int Delete(string userId)
        {
            //int returnValue = 0;

            throw new NotImplementedException();
            // Not implemented
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    SingmayAdminService singmayAdminService = new SingmayAdminService();

            //    // delete all role assigned to the user in database
            //    returnValue = singmayAdminService.UserRoles_Delete(userId, common.getCallerID(), common.getCallerPWD());
            //}

            //return returnValue;
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles.
        /// </summary>
        /// <param name="user">The target user</param>
        /// <param name="roleId">The designated role's id</param>
        /// <returns>0</returns>
        public int Insert(IdentityUser user, string roleId)
        {
            var returnValue = 0;

            //if (user != null && !string.IsNullOrEmpty(roleId))
            //{
            //    //SingmayAdminService singmayAdminService = new SingmayAdminService();

            //    // assign new role for user to database
            //    //returnValue = singmayAdminService.InsertRole(user.Id, roleId, common.getCallerID(), common.getCallerPWD());

            //    //no use no test yet
            //    using (var userTransctionUow = new UserTransactionUow())
            //    {
            //       returnValue = userTransctionUow.InsertRole(user.Id, roleId);
            //    }
                    
            //}
            return returnValue;
        }
    }
}