using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
   public class WalletCalculationWithdrawFeeLogic
    {
        public class ItemTran
        {
            public decimal? Amount { get; set; }
            public DateTime? CreateDate { get; set; }
        }
        public decimal CalculationFeeA(string currencyCode, decimal amount)
        {
            decimal feeA = 0;
                switch (currencyCode)
                {
                    case "SGD":
                        if (amount < 200)
                        {
                            feeA = 1;
                        }
                        break;
                    case "MYR":
                        if (amount < 200)
                        {
                           feeA = (decimal)1.50;
                        }
                        break;
                    case "USD":
                        if (amount < 200)
                        {
                         feeA = 1;
                        }
                        break;
                    case "Rp":
                        if (amount < 600000)
                        {
                            feeA = 5200;
                        }
                        break;
                    case "THB":
                        if (amount < 1500)
                        {
                        feeA = 20;
                        }
                        break;
                    case "VND":
                        if (amount < 1000000)
                        {
                        feeA = 22000;
                        }
                        break;
                    case "MMK":
                        if (amount < 70000)
                        {
                        feeA = 600;
                        }
                        break;
                    case "KHR":
                        if (amount < 200000)
                        {
                        feeA = 5000;
                        }
                        break;
                    case "LAK":
                        if (amount < 400000)
                        {
                        feeA = 10000;
                        }
                        break;

                }
            
            return feeA;
        }
        public decimal CalculationFeeB(string currencyCode, decimal amount , decimal balanceAmountLeft)
        {
                decimal feeB = 0;
                switch (currencyCode)
                {
                    case "SGD":
                        if (balanceAmountLeft < 500)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 500 && balanceAmountLeft <= 1000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "MYR":
                        if (balanceAmountLeft < 500)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 500 && balanceAmountLeft <= 1000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "USD":
                        if (balanceAmountLeft < 500)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 500 && balanceAmountLeft <= 1000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "Rp":
                        if (balanceAmountLeft < 1700000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 1700000 && balanceAmountLeft <= 3400000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "THB":
                        if (balanceAmountLeft < 4000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 4000 && balanceAmountLeft <= 8000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "VND":
                        if (balanceAmountLeft < 2800000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 2800000 && balanceAmountLeft <= 5600000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "MMK":
                        if (balanceAmountLeft < 180000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 180000 && balanceAmountLeft <= 360000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "KHR":
                        if (balanceAmountLeft < 500000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 500000 && balanceAmountLeft <= 1000000)
                            feeB = amount * (decimal)0.01;
                        break;
                    case "LAK":
                        if (balanceAmountLeft < 1000000)
                            feeB = amount * (decimal)0.018;
                        if (balanceAmountLeft >= 1000000 && balanceAmountLeft <= 2000000)
                            feeB = amount * (decimal)0.01;
                        break;
                }

            return Math.Round(feeB,2);
        }
        public string GetCurrencySymbol(string currencyCode)
        {
            string currencySymbol = string.Empty;
                switch (currencyCode)
                {
                    case "SGD":
                        currencySymbol = "$";
                        break;
                    case "MYR":
                        currencySymbol = "RM";
                        break;
                    case "USD":
                        currencySymbol = "$";
                        break;
                    case "Rp":
                        currencySymbol = "Rp";
                        break;
                    case "THB":
                        currencySymbol = "฿";
                        break;
                    case "VND":
                        currencySymbol = "₫";
                        break;
                    case "MMK":
                        currencySymbol = "K";
                        break;
                    case "KHR":
                        currencySymbol = "៛";
                        break;
                    case "LAK":
                        currencySymbol = "₭";
                        break;

                }
            return currencySymbol;
        }
        public decimal CalculationFeeC(string pUserID,string currencyCode, decimal amountWithdrawalCurrent, decimal balanceAmount, ref string percent)
        {
            decimal feeC = 0;
            //percent = "(0%)";
            //var isExistTopupWithin60Days = new TransactionQueryBuilder(new WalletEntities()).CheckTopupByUserIDnDateTimenCurrency(pUserID, DateTime.Now, DateTime.Now.AddDays(-60), currencyCode);
            //if (isExistTopupWithin60Days)
            //{
            //    feeC = amountWithdrawalCurrent * (decimal)0.03;
            //    percent = "(3%)";
            //}
            //else
            //{
            //    var isExistTopupWithin120Days = new TransactionQueryBuilder(new WalletEntities()).CheckTopupByUserIDnDateTimenCurrency(pUserID, DateTime.Now, DateTime.Now.AddDays(-120), currencyCode);
            //    if (isExistTopupWithin120Days)
            //    {
            //        feeC = amountWithdrawalCurrent * (decimal)0.02;
            //        percent = "(2%)";
            //    }
            //}
            percent = "";
             var historyTopupTranOfUser = new TransactionQueryBuilder(new WalletEntities()).GetHistoryDebitByUserIDnCurrency(pUserID, currencyCode).ToList().Select(x => new ItemTran { Amount = x.Source_Amount, CreateDate = x.CreateDate }).ToList();
            var historyWithdrawTranOfUser = new TransactionQueryBuilder(new WalletEntities()).GetHistoryCreditByUserIDnCurrency(pUserID, currencyCode).ToList().Select(x => new ItemTran { Amount = x.Source_Amount, CreateDate = x.CreateDate }).ToList();
            decimal amountLeft = 0;
            DateTime currentTopupTime = new DateTime();
            var amountWithrawlLeftCurrent = 0.0M;
            var timeWithdrawalCurrent = DateTime.Now;
            var sumFeeC = 0.0M;
            foreach (var itemWithdraw in historyWithdrawTranOfUser.OrderBy(x => x.CreateDate))
            {
                int i = 0;
                amountLeft = FIFO(historyTopupTranOfUser, itemWithdraw.Amount, i, amountLeft, itemWithdraw.CreateDate, out currentTopupTime);
            }
            if (amountLeft < 0)
            {
                if (historyTopupTranOfUser[0].CreateDate == currentTopupTime)
                    historyTopupTranOfUser.RemoveAt(0);
                historyTopupTranOfUser.Insert(0,new ItemTran { Amount = Math.Abs(amountLeft), CreateDate = currentTopupTime });
            }
            foreach (var itemtopup in historyTopupTranOfUser.OrderBy(x => x.CreateDate).ToList())
            {
                //calculate for current withdrawal amount
                if (Math.Abs((timeWithdrawalCurrent - itemtopup.CreateDate.Value).Days) < 61)
                {
                    var amout = amountWithdrawalCurrent - itemtopup.Amount.Value;
                    if (amout <= 0)
                    {
                        sumFeeC += amountWithdrawalCurrent * (decimal)0.03;
                        amountWithdrawalCurrent = 0;
                        break;
                    }
                    else //sufficient topup amount
                    {
                        sumFeeC += (itemtopup.Amount == 0 ? amountWithdrawalCurrent : itemtopup.Amount.Value) * (decimal)0.03;
                        amountWithrawlLeftCurrent = amout;
                    }
                }
                else if (Math.Abs((timeWithdrawalCurrent - itemtopup.CreateDate.Value).Days) < 121)
                {
                    var amout = amountWithdrawalCurrent - itemtopup.Amount.Value;
                    if (amout <= 0)
                    {
                        sumFeeC += amountWithdrawalCurrent * (decimal)0.02;
                        amountWithdrawalCurrent = 0;
                        break;
                    }
                    else //insufficient topup amount
                    {
                        sumFeeC += (itemtopup.Amount == 0 ? amountWithdrawalCurrent : itemtopup.Amount.Value) * (decimal)0.02;
                        amountWithrawlLeftCurrent = amout;
                    }
                }
                else
                {
                    amountWithrawlLeftCurrent = amountWithdrawalCurrent - itemtopup.Amount.Value;
                }
                currentTopupTime = itemtopup.CreateDate.Value;
                amountWithdrawalCurrent = amountWithrawlLeftCurrent;
                if (amountWithrawlLeftCurrent <= 0)
                    break;
            }
            if (amountWithdrawalCurrent > 0)
            {
                if (Math.Abs((timeWithdrawalCurrent - currentTopupTime).Days) < 61)
                {
                    sumFeeC += amountWithrawlLeftCurrent * (decimal)0.03;
                }
                else if (Math.Abs((timeWithdrawalCurrent - currentTopupTime).Days) < 121)
                {
                    sumFeeC += amountWithrawlLeftCurrent * (decimal)0.02;
                }
            }
            feeC = sumFeeC;
            return Math.Round(feeC, 2);
        }
        public static decimal FIFO(List<ItemTran> listTopup, decimal? withdrawAmount, int i, decimal amountLeft, DateTime? withdrawalDate, out DateTime currentTopupTime)
        {

            decimal amountWithdrawLeft = withdrawAmount.Value - (Math.Abs(amountLeft) > 0 ? Math.Abs(amountLeft) : listTopup[i].Amount.Value);
           
            if (amountWithdrawLeft <= 0) // redundancy topup amount (stay)
            {
                currentTopupTime = listTopup[i].CreateDate.Value;
                //if (amountLeft <= 0)
                //{
                //    listTopup.RemoveAt(i);
                //}
                return amountWithdrawLeft;
            }
            else //redundancy withdrawal amount
            {
               
                listTopup.RemoveAt(i);
                currentTopupTime = listTopup[i].CreateDate.Value;
                //after remove (comparing date withdraw and current date of topup[i])
                if (listTopup.Count == 0 || (listTopup.Count > 0 && listTopup[i].CreateDate > withdrawalDate))
                {
                    return 0;
                }
                withdrawAmount = amountWithdrawLeft;
                amountLeft = 0;
                // i++;
                return FIFO(listTopup, withdrawAmount, i, amountLeft, withdrawalDate, out currentTopupTime);

            }


        }

        public int CountWithdrawTime(string pUserID)
        {
            int? WithdrawalTime = 0;
            
            using (var TransactionBuilder = new TransactionQueryBuilder(new WalletEntities()))
            {
                WithdrawalTime = new TransactionQueryBuilder(new WalletEntities()).CountWithdrawTimeByUserIDnDateTimenCurrency(pUserID, DateTime.Now, DateTime.Now.AddMonths(-1));
                
            }
            return WithdrawalTime.Value;
        }

    }
}
