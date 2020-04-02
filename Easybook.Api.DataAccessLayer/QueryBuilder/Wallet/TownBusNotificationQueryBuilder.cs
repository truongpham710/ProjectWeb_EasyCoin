using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;
using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class TownBusNotificationQueryBuilder : QueryBuilder<TownBusNotification>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TownBusNotificationQueryBuilder"/> class.
        /// </summary>
        public TownBusNotificationQueryBuilder(DbContext dbContext) : base(dbContext) { }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="busId"></param>
       /// <param name="companyId"></param>
       /// <param name="notificationToken"></param>
       /// <returns></returns>
        public bool HasNotifcationToken(string carPlate,int companyId,string notificationToken)
        {
            return Query.Any(token => token.CarPlate== carPlate && token.CompanyID==companyId && token.NotificationToken.Contains(notificationToken));            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="companyId"></param>
        /// <param name="notificationToken"></param>
        /// <returns></returns>
        public bool HasNotificationUniqueId(string carPlate, int companyId, string notificationUniqueId)
        {
            return Query.Any(token => token.CarPlate == carPlate && token.CompanyID == companyId && token.NotificationUniqueId.Contains(notificationUniqueId));
        }
        public TownBusNotificationQueryBuilder GetNotifcationTokenLst(string carPlate, int? companyId)
        {
            Query=Query.Where(token => token.CarPlate == carPlate && token.CompanyID == companyId);
            return this;
        }

    }
}
