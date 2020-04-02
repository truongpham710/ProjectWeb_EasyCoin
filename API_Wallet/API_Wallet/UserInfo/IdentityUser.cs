using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace API_Wallet.UserInfo
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface 
    /// </summary>
    public class IdentityUser : IUser
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public IdentityUser()
        {
            Id = System.Guid.NewGuid().ToString();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Constructor that takes user name as argument
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// User ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        public virtual int? CountryPhonePrefixId { get; set; }
        
        /// <summary>
        /// PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// PhoneNumber for the user
        /// </summary>
        public virtual string OldPhoneNumber { get; set; } //addon properties

        /// <summary>
        /// True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Member type (Still figuring
        /// </summary>
        public virtual char? MemberType { get; set; } //addon properties

        /// <summary>
        /// User's first name.
        /// </summary>
        public virtual string FirstName { get; set; } //addon properties

        /// <summary>
        /// User's last name.
        /// </summary>
        public virtual string LastName { get; set; } //addon properties

        /// <summary>
        /// User's salutation.
        /// </summary>
        public virtual string Salutation { get; set; } //addon properties

        /// <summary>
        /// User's Date of birth.
        /// </summary>
        public virtual DateTime? Dob { get; set; } //addon properties

        /// <summary>
        /// User's national registration identification card number.
        /// </summary>
        public virtual string Nric { get; set; } //addon properties

        /// <summary>
        /// User's passport number.
        /// </summary>
        public virtual string Passport { get; set; } //addon properties

        /// <summary>
        /// User's address (street).
        /// </summary>
        public virtual string Address { get; set; } //addon properties

        /// <summary>
        /// User's address (city).
        /// </summary>
        public virtual string City { get; set; } //addon properties

        /// <summary>
        /// User's address (state).
        /// </summary>
        public virtual string State { get; set; } //addon properties

        /// <summary>
        /// User's address (postal).
        /// </summary>
        public virtual string Postal { get; set; } //addon properties

        /// <summary>
        /// User's address (country).
        /// </summary>
        public virtual int? CountryId { get; set; } //addon properties

        /// <summary>
        /// User's commission rate.
        /// </summary>
        public virtual int? Commission { get; set; } //addon properties

        /// <summary>
        /// Prepaid value in SGD currency.
        /// </summary>
        public virtual decimal? Sgd { get; set; } //addon properties

        /// <summary>
        /// Points. 
        /// Not sure the different between Points and Rewards Point, added because previous table got this column.
        /// </summary>
        public virtual decimal? Points { get; set; } //addon properties

        /// <summary>
        /// Rewards points. 
        /// Not sure the different between Points and Rewards Point, added because previous table got this column.
        /// </summary>
        public virtual decimal? RewardsPoint { get; set; } //addon properties

        /// <summary>
        /// No idea what is this property are designed for, added because previous table got this column.
        /// </summary>
        public virtual string Guid { get; set; } //addon properties

        /// <summary>
        /// No idea what is this property are designed for, added because previous table got this column.
        /// </summary>
        public virtual int? Status { get; set; } //addon properties

        /// <summary>
        /// Id of this user belong to which company.
        /// </summary>
        public virtual int? FromCompanyId { get; set; } //addon properties

        /// <summary>
        /// Date and time when this user created. 
        /// No body in the company seem to know what is this for, but was ordered to included this.
        /// </summary>
        public virtual DateTime? CreateDate { get; set; } //addon properties

        /// <summary>
        /// User who created this user. 
        /// No body in the company seem to know what is this for, but was ordered to included this.
        /// </summary>
        public virtual string CreateUser { get; set; } //addon properties

        /// <summary>
        /// Date and time when this user updated. 
        /// No body in the company seem to know what is this for, but was ordered to included this.
        /// </summary>
        public virtual DateTime? UpdateDate { get; set; } //addon properties

        /// <summary>
        /// User who updated this user. 
        /// No body in the company seem to know what is this for, but was ordered to included this.
        /// </summary>
        public virtual string UpdateUser { get; set; } //addon properties

        /// <summary>
        /// Id (int) used by old Member table. Included here for compatibility purpose.
        /// </summary>
        //public virtual int OldMemberId { get; set; } //Move to MigrationLinks table

        ///// <summary>
        ///// The salted/hashed form of the user's username
        ///// </summary>
        //public virtual string UserNameHash
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(UserName))
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            return simpleAES.Encrypt(UserName);
        //        }
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            UserName = string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            UserName = simpleAES.Decrypt(value);
        //        }
        //    }
        //}

        /// <summary>
        /// The salted/hashed form of the user's email
        /// </summary>
        //public virtual string EmailHash
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(Email))
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            return simpleAES.Encrypt(Email);
        //        }
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            Email = string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            Email = simpleAES.Decrypt(value);
        //        }
        //    }
        //}

        ///// <summary>
        ///// The salted/hashed form of the user's phone number
        ///// </summary>
        //public virtual string PhoneNumberHash
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(PhoneNumber))
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            return simpleAES.Encrypt(PhoneNumber);
        //        }
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            PhoneNumber = string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            PhoneNumber = simpleAES.Decrypt(value);
        //        }
        //    }
        //}

        ///// <summary>
        ///// The salted/hashed form of the user's old phone number
        ///// </summary>
        //public virtual string OldPhoneNumberHash
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(OldPhoneNumber))
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            return simpleAES.Encrypt(OldPhoneNumber);
        //        }
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            OldPhoneNumber = string.Empty;
        //        }
        //        else
        //        {
        //            SimpleAES simpleAES = new SimpleAES();
        //            OldPhoneNumber = simpleAES.Decrypt(value);
        //        }
        //    }
        //}

        /// <summary>
        /// Convert from DataSet format (use by web services) to User (use by ASP .NET Identity).
        /// </summary>
        /// <param name="dataSet">DataSet format (use by web services)</param>
        //public virtual void FromDataSet(DataSet dataSet)
        //{
        //    if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        FromDataRow(dataSet.Tables[0].Rows[0]);
        //    }
        //}

        /// <summary>
        /// Convert from DataRow format (use by web services) to User (use by ASP .NET Identity).
        /// </summary>
        /// <param name="dateRow">DataRow format (use by web services)</param>
        //public virtual void FromDataRow(DataRow dateRow)
        //{
        //    if (dateRow != null)
        //    {
        //        string id, userName, passwordHash, securityStamp, email, phoneNumber, oldPhoneNumber, firstName, lastName, salutation, nric, passport, address, city, state, postal, country, guid, create_User, update_User;
        //        bool emailConfirmed, phoneNumberConfirmed, lockoutEnabled, twoFactorEnabled;
        //        DateTime tempLockoutEndDateUtc, tempDOB, tempCreateDate, tempUpdateDate;
        //        DateTime? lockoutEndDateUtc, dob, create_Date, update_Date;
        //        char memberType;
        //        int accessFailedCount, tempCommission, tempStatus, tempFrom_CompanyID, oldMemberId;
        //        int? commission, status, from_CompanyID;
        //        decimal tempSGD, tempPoints, tempRewardsPoint;
        //        decimal? sgd, points, rewardsPoint;

        //        covert the data set return from web service into proper data type
        //        id = dateRow["Id"].ToString();
        //        userName = dateRow["UserName"].ToString();
        //        passwordHash = dateRow["PasswordHash"].ToString();
        //        securityStamp = dateRow["SecurityStamp"].ToString();
        //        email = dateRow["Email"].ToString();
        //        if (!bool.TryParse(dateRow["EmailConfirmed"].ToString(), out emailConfirmed))
        //        {
        //            emailConfirmed = false;
        //        }
        //        phoneNumber = dateRow["PhoneNumber"].ToString();
        //        if (!bool.TryParse(dateRow["PhoneNumberConfirmed"].ToString(), out phoneNumberConfirmed))
        //        {
        //            phoneNumberConfirmed = false;
        //        }
        //        oldPhoneNumber = dateRow["OldPhoneNumber"].ToString();
        //        if (!bool.TryParse(dateRow["LockoutEnabled"].ToString(), out lockoutEnabled))
        //        {
        //            lockoutEnabled = false;
        //        }
        //        if (DateTime.TryParse(dateRow["LockoutEndDateUtc"].ToString(), out tempLockoutEndDateUtc))
        //        {
        //            lockoutEndDateUtc = tempLockoutEndDateUtc;
        //        }
        //        else
        //        {
        //            lockoutEndDateUtc = null;
        //        }
        //        if (!int.TryParse(dateRow["AccessFailedCount"].ToString(), out accessFailedCount))
        //        {
        //            accessFailedCount = 0;
        //        }
        //        if (!bool.TryParse(dateRow["TwoFactorEnabled"].ToString(), out twoFactorEnabled))
        //        {
        //            twoFactorEnabled = false;
        //        }
        //        if (!char.TryParse(dateRow["MemberType"].ToString(), out memberType))
        //        {
        //            memberType = 'I';
        //        }
        //        firstName = dateRow["FirstName"].ToString();
        //        lastName = dateRow["LastName"].ToString();
        //        salutation = dateRow["Salutation"].ToString();
        //        if (DateTime.TryParse(dateRow["DOB"].ToString(), out tempDOB))
        //        {
        //            dob = tempDOB;
        //        }
        //        else
        //        {
        //            dob = null;
        //        }
        //        nric = dateRow["NRIC"].ToString();
        //        passport = dateRow["Passport"].ToString();
        //        address = dateRow["Address"].ToString();
        //        city = dateRow["City"].ToString();
        //        state = dateRow["State"].ToString();
        //        postal = dateRow["Postal"].ToString();
        //        country = dateRow["Country"].ToString();
        //        if (int.TryParse(dateRow["Commission"].ToString(), out tempCommission))
        //        {
        //            commission = tempCommission;
        //        }
        //        else
        //        {
        //            commission = null;
        //        }
        //        if (decimal.TryParse(dateRow["SGD"].ToString(), out tempSGD))
        //        {
        //            sgd = tempSGD;
        //        }
        //        else
        //        {
        //            sgd = null;
        //        }
        //        if (decimal.TryParse(dateRow["Points"].ToString(), out tempPoints))
        //        {
        //            points = tempPoints;
        //        }
        //        else
        //        {
        //            points = null;
        //        }
        //        if (decimal.TryParse(dateRow["RewardsPoint"].ToString(), out tempRewardsPoint))
        //        {
        //            rewardsPoint = tempRewardsPoint;
        //        }
        //        else
        //        {
        //            rewardsPoint = null;
        //        }
        //        guid = dateRow["GUID"].ToString();
        //        if (int.TryParse(dateRow["Status"].ToString(), out tempStatus))
        //        {
        //            status = tempStatus;
        //        }
        //        else
        //        {
        //            status = null;
        //        }
        //        if (int.TryParse(dateRow["From_CompanyID"].ToString(), out tempFrom_CompanyID))
        //        {
        //            from_CompanyID = tempFrom_CompanyID;
        //        }
        //        else
        //        {
        //            from_CompanyID = null;
        //        }
        //        if (DateTime.TryParse(dateRow["Create_Date"].ToString(), out tempCreateDate))
        //        {
        //            create_Date = tempCreateDate;
        //        }
        //        else
        //        {
        //            create_Date = null;
        //        }
        //        create_User = dateRow["Create_User"].ToString();
        //        if (DateTime.TryParse(dateRow["Update_Date"].ToString(), out tempUpdateDate))
        //        {
        //            update_Date = tempUpdateDate;
        //        }
        //        else
        //        {
        //            update_Date = null;
        //        }
        //        update_User = dateRow["Update_User"].ToString();
        //        if (!int.TryParse(dateRow["OldMemberId"].ToString(), out oldMemberId))
        //        {
        //            oldMemberId = 0;
        //        }

        //        convert the user data to TUser type
        //       Id = id;
        //        UserNameHash = userName;
        //        PasswordHash = string.IsNullOrEmpty(passwordHash) ? null : passwordHash;
        //        SecurityStamp = string.IsNullOrEmpty(securityStamp) ? null : securityStamp;
        //        EmailHash = string.IsNullOrEmpty(email) ? null : email;
        //        EmailConfirmed = emailConfirmed;
        //        PhoneNumberHash = string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber;
        //        PhoneNumberConfirmed = phoneNumberConfirmed;
        //        OldPhoneNumber = string.IsNullOrEmpty(oldPhoneNumber) ? null : oldPhoneNumber;
        //        LockoutEnabled = lockoutEnabled;
        //        LockoutEndDateUtc = lockoutEndDateUtc;
        //        AccessFailedCount = accessFailedCount;
        //        TwoFactorEnabled = twoFactorEnabled;
        //        MemberType = memberType;
        //        FirstName = firstName;
        //        LastName = lastName;
        //        Salutation = salutation;
        //        DOB = dob;
        //        NRIC = nric;
        //        Passport = passport;
        //        Address = address;
        //        City = city;
        //        State = state;
        //        Postal = postal;
        //        Country = country;
        //        Commission = commission;
        //        SGD = sgd;
        //        Points = points;
        //        RewardsPoint = rewardsPoint;
        //        GUID = guid;
        //        Status = status;
        //        From_CompanyID = from_CompanyID;
        //        Create_Date = create_Date;
        //        Create_User = create_User;
        //        Update_Date = update_Date;
        //        Update_User = update_User;
        //        OldMemberId = oldMemberId;
        //    }
        //}

        /// <summary>
        /// Convert existing Member format (use by previous old table) data to User format (use by ASP .NET Identity).
        /// </summary>
        /// <param name="dateRow">DataRow format (use by previous old table)</param>
        //public virtual void FromMemberDataRow(DataRow dateRow)
        //{
        //    if (dateRow != null)
        //    {
        //        string id, userName, passwordHash, securityStamp, email, phoneNumber, oldPhoneNumber , firstName, lastName, salutation, nric, passport, address, city, state, postal, country, guid, create_User, update_User;
        //        bool emailConfirmed, phoneNumberConfirmed, lockoutEnabled, twoFactorEnabled;
        //        DateTime tempDOB, tempCreateDate, tempUpdateDate;
        //        DateTime? lockoutEndDateUtc, dob, create_Date, update_Date;
        //        char memberType;
        //        int accessFailedCount, tempCommission, tempStatus, tempFrom_CompanyID, oldMemberId;
        //        int? commission, status, from_CompanyID;
        //        decimal tempSGD, tempPoints, tempRewardsPoint;
        //        decimal? sgd, points, rewardsPoint;

        //        PasswordHasher ph = new PasswordHasher();

        //        // covert the member data set return from web service into proper user data type
        //        id = dateRow["Member_ID"].ToString();
        //        userName = dateRow["Login_ID"].ToString();
        //        passwordHash = ph.HashPassword(dateRow["Password"].ToString());
        //        securityStamp = null;
        //        email = dateRow["Email"].ToString();
        //        emailConfirmed = false;
        //        phoneNumber = dateRow["Contact"].ToString();
        //        phoneNumberConfirmed = false;
        //        oldPhoneNumber = dateRow["Contact"].ToString();
        //        lockoutEnabled = false;
        //        lockoutEndDateUtc = null;
        //        accessFailedCount = 0;
        //        twoFactorEnabled = false;
        //        if (!char.TryParse(dateRow["MemberType"].ToString(), out memberType))
        //        {
        //            memberType = 'I';
        //        }
        //        firstName = dateRow["FirstName"].ToString();
        //        lastName = dateRow["LastName"].ToString();
        //        salutation = dateRow["Salutation"].ToString();
        //        if (DateTime.TryParse(dateRow["DOB"].ToString(), out tempDOB))
        //        {
        //            dob = tempDOB;
        //        }
        //        else
        //        {
        //            dob = null;
        //        }
        //        nric = dateRow["NRIC"].ToString();
        //        passport = dateRow["Passport"].ToString();
        //        address = dateRow["Address"].ToString();
        //        city = dateRow["City"].ToString();
        //        state = dateRow["State"].ToString();
        //        postal = dateRow["Postal"].ToString();
        //        country = dateRow["Country"].ToString();
        //        if (int.TryParse(dateRow["Commission"].ToString(), out tempCommission))
        //        {
        //            commission = tempCommission;
        //        }
        //        else
        //        {
        //            commission = null;
        //        }
        //        if (decimal.TryParse(dateRow["SGD"].ToString(), out tempSGD))
        //        {
        //            sgd = tempSGD;
        //        }
        //        else
        //        {
        //            sgd = null;
        //        }
        //        if (decimal.TryParse(dateRow["Points"].ToString(), out tempPoints))
        //        {
        //            points = tempPoints;
        //        }
        //        else
        //        {
        //            points = null;
        //        }
        //        if (decimal.TryParse(dateRow["RewardsPoint"].ToString(), out tempRewardsPoint))
        //        {
        //            rewardsPoint = tempRewardsPoint;
        //        }
        //        else
        //        {
        //            rewardsPoint = null;
        //        }
        //        guid = dateRow["GUID"].ToString();
        //        if (int.TryParse(dateRow["Status"].ToString(), out tempStatus))
        //        {
        //            status = tempStatus;
        //        }
        //        else
        //        {
        //            status = null;
        //        }
        //        if (int.TryParse(dateRow["From_CompanyID"].ToString(), out tempFrom_CompanyID))
        //        {
        //            from_CompanyID = tempFrom_CompanyID;
        //        }
        //        else
        //        {
        //            from_CompanyID = null;
        //        }
        //        if (DateTime.TryParse(dateRow["Create_Date"].ToString(), out tempCreateDate))
        //        {
        //            create_Date = tempCreateDate;
        //        }
        //        else
        //        {
        //            create_Date = null;
        //        }
        //        create_User = dateRow["Create_User"].ToString();
        //        if (DateTime.TryParse(dateRow["Update_Date"].ToString(), out tempUpdateDate))
        //        {
        //            update_Date = tempUpdateDate;
        //        }
        //        else
        //        {
        //            update_Date = null;
        //        }
        //        update_User = dateRow["Update_User"].ToString();
        //        if (!int.TryParse(dateRow["Member_ID"].ToString(), out oldMemberId))
        //        {
        //            oldMemberId = 0;
        //        }

        //        // convert the user data to TUser type
        //        Id = id;
        //        UserName = userName;
        //        PasswordHash = string.IsNullOrEmpty(passwordHash) ? null : passwordHash;
        //        SecurityStamp = string.IsNullOrEmpty(securityStamp) ? null : securityStamp;
        //        Email = string.IsNullOrEmpty(email) ? null : email;
        //        EmailConfirmed = emailConfirmed;
        //        PhoneNumber = string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber;
        //        PhoneNumberConfirmed = phoneNumberConfirmed;
        //        OldPhoneNumber = string.IsNullOrEmpty(oldPhoneNumber) ? null : oldPhoneNumber;
        //        LockoutEnabled = lockoutEnabled;
        //        LockoutEndDateUtc = lockoutEndDateUtc;
        //        AccessFailedCount = accessFailedCount;
        //        TwoFactorEnabled = twoFactorEnabled;
        //        MemberType = memberType;
        //        FirstName = firstName;
        //        LastName = lastName;
        //        Salutation = salutation;
        //        DOB = dob;
        //        NRIC = nric;
        //        Passport = passport;
        //        Address = address;
        //        City = city;
        //        State = state;
        //        Postal = postal;
        //        Country = country;
        //        Commission = commission;
        //        SGD = sgd;
        //        Points = points;
        //        RewardsPoint = rewardsPoint;
        //        GUID = guid;
        //        Status = status;
        //        From_CompanyID = from_CompanyID;
        //        Create_Date = create_Date;
        //        Create_User = create_User;
        //        Update_Date = update_Date;
        //        Update_User = update_User;
        //        OldMemberId = oldMemberId;
        //    }
        //}

        /// <summary>
        /// Convert to Member DataSet format which is use by old authentication code for compatibility purpose.
        /// </summary>
        /// <returns>Member DataSet format used in old authentication way</returns>
        //public virtual DataSet ToMemberDataSet()
        //{
        //    DataTable proxyMemberTable = new DataTable("Members");
        //    proxyMemberTable.Columns.Add("Member_ID");
        //    proxyMemberTable.Columns.Add("Email");
        //    proxyMemberTable.Columns.Add("Contact");
        //    proxyMemberTable.Columns.Add("MemberType");
        //    proxyMemberTable.Columns.Add("FirstName");
        //    proxyMemberTable.Columns.Add("LastName");
        //    proxyMemberTable.Columns.Add("Salutation");
        //    proxyMemberTable.Columns.Add("DOB");
        //    proxyMemberTable.Columns.Add("NRIC");
        //    proxyMemberTable.Columns.Add("Passport");
        //    proxyMemberTable.Columns.Add("Create_Date");
        //    proxyMemberTable.Columns.Add("Create_User");
        //    proxyMemberTable.Columns.Add("Update_Date");
        //    proxyMemberTable.Columns.Add("Update_User");
        //    proxyMemberTable.Columns.Add("Address");
        //    proxyMemberTable.Columns.Add("City");
        //    proxyMemberTable.Columns.Add("State");
        //    proxyMemberTable.Columns.Add("Postal");
        //    proxyMemberTable.Columns.Add("Country");
        //    proxyMemberTable.Columns.Add("Commission");
        //    proxyMemberTable.Columns.Add("SGD");
        //    proxyMemberTable.Columns.Add("Points");
        //    proxyMemberTable.Columns.Add("RewardsPoint");
        //    proxyMemberTable.Columns.Add("GUID");
        //    proxyMemberTable.Columns.Add("Status");
        //    proxyMemberTable.Columns.Add("From_CompanyID");
        //    proxyMemberTable.Rows.Add(
        //        OldMemberId, 
        //        Email,
        //        PhoneNumber,
        //        MemberType,
        //        FirstName,
        //        LastName,
        //        Salutation,
        //        DOB,
        //        NRIC,
        //        Passport,
        //        Create_Date,
        //        Create_User,
        //        Update_Date,
        //        Update_User,
        //        Address,
        //        City,
        //        State,
        //        Postal,
        //        Country,
        //        Commission,
        //        SGD,
        //        Points,
        //        RewardsPoint,
        //        GUID,
        //        Status,
        //        From_CompanyID);

        //    DataSet proxyMemberDataSet = new DataSet();
        //    proxyMemberDataSet.Tables.Add(proxyMemberTable);

        //    return proxyMemberDataSet;
        //}

        /// <summary>
        /// Existing Members table use by old authentication system use int as id.
        /// This is a temporary testing code that convert GUID to int to make the whole system work for testing purpose only.
        /// </summary>
        /// <param name="guid">GUID in string format</param>
        /// <returns>Id in integer format</returns>
        //private int ConvertGuidToInt(string guid)
        //{
        //    byte[] bytes = new byte[guid.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(guid.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return BitConverter.ToInt32(bytes, 0);
        //}
    }
}