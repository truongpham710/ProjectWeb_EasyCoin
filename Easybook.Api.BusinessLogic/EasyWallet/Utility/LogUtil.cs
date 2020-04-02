using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Easybook.Api.BusinessLogic.EasyWallet.Utility
{

    public class LogWallet {
        public void Log(MethodBase method, object inputParameters,  Exception ex, string error, WalletEntities WalletEntities = null)
        {
            try
            {
                
                var WalletLog = new Wallet_Logs
                {
                    ApiCallerType = "api",
                    ApiMethod = $"{method.ReflectedType.Name}.{method.Name}",
                    AspNetUserId = "",
                    Exception = (ex != null) ? JsonConvert.SerializeObject(ex) : error,
                    Request = JsonConvert.SerializeObject(inputParameters),
                    Status = false,
                    ReturnCode = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code,
                    CreateDate = DateTime.Now,
                    ProductId = 12
                };
                if (WalletEntities == null)
                {
                    WalletEntities = new WalletEntities();
                    using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(WalletEntities))
                    {
                        eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(WalletLog).EndTransaction();
                    }
                }
                else
                {
                    var eWalletTransactionUnitOfWork = new WalletTransactionUow(WalletEntities);
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(WalletLog).EndTransaction();
                }
                  
               
            }
            catch (Exception exe)
            {
                throw exe;
            }
            
        }

        public void Log(MethodBase method, object inputParameters, Exception ex, string error, int returnCode, int status)
        {
            try
            {
                var WalletLog = new Wallet_Logs
                {
                    ApiCallerType = "api",
                    ApiMethod = $"{method.ReflectedType.Name}.{method.Name}",
                    AspNetUserId = "",
                    Exception = (ex != null) ? JsonConvert.SerializeObject(ex) : error,
                    Request = JsonConvert.SerializeObject(inputParameters),
                    Status = (status == 1)? true:false,
                    ReturnCode = returnCode,
                    CreateDate = DateTime.Now,
                    ProductId = 12
                };

                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(WalletLog).EndTransaction();
                }
            }
            catch (Exception exe)
            {
                throw exe;
            }

        }
    }

}
