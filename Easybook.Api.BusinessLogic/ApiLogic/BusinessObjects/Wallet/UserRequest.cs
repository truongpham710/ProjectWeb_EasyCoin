using System;
using System.ComponentModel.DataAnnotations;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class UserRequest 
    {
        public UserRequest()
        {
            IsMemberAgent = false;
        }
        public string User_ID { get; set; }

        public string User_Name { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

     
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

     
        public string NRIC { get; set; }

     
        public string Passport { get; set; }

      
        public string Address { get; set; }

     
        public string City { get; set; }

      
        public int CountryID { get; set; }

       
        public string State { get; set; }

     
        public string Postal { get; set; }

        public bool IsMemberAgent { get; set; }

        public DateTime? CreateDate { get; set; }

     
        public string CreateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

      
        public string UpdateUser { get; set; }
        public string Sign { get; set; }
    }
}
