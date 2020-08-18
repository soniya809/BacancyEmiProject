using System.Collections.Generic;
using System.Linq;
using BacancyEmiProject.Models;
using Microsoft.AspNetCore.Mvc;
using BacancyEmiProject.Interface;
using System;

namespace BacancyEmiProject.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILoanEmi _loanEmi;
        public HomeController(ILoanEmi loanEmi)
        {
            _loanEmi = loanEmi;
        }

        public IActionResult Index()
        {
            return (IActionResult)View();
        }
       
        [HttpPost]
        public JsonResult GetTransactionGrid([FromBody] CriteriaInputByUser criteriaInputByUser)
        {
            var loanEmiTransaction = GetEmiTransactionLogs(criteriaInputByUser);

            return Json(loanEmiTransaction);
        }

        private List<LoanEmiTransaction> GetEmiTransactionLogs(CriteriaInputByUser criteriaInputByUser)
        {
            var loanEmiTransaction = new List<LoanEmiTransaction>();

            decimal noOfMonthlyInstallment = 1;
            noOfMonthlyInstallment = criteriaInputByUser.NoOfYear * 12;
            var rateOfInterest = criteriaInputByUser.LoanInterest / (12 * 100);
            decimal noOfMonthlyIst = (1 + rateOfInterest);
            var EMI = criteriaInputByUser.LoanAmount * rateOfInterest * noOfMonthlyIst * noOfMonthlyInstallment / (noOfMonthlyIst * noOfMonthlyInstallment - 1);

            decimal previousOpening = criteriaInputByUser.LoanAmount;
            decimal previousInterest = previousOpening * rateOfInterest;
            decimal previousPrincipal = EMI - previousInterest;

            for (int i = 0; i < noOfMonthlyInstallment; i++)
            {
                
                var loanEmi = new LoanEmiTransaction
                {
                    Opening = Math.Round(previousOpening,2),
                    Principal = Math.Round(previousPrincipal,2),
                    Interest = Math.Round(previousInterest, 2),
                    Emi = Math.Round(EMI, 2),
                    Closing = Math.Round(previousOpening - previousPrincipal, 2),
                    CummulativeInterest = Math.Round(previousInterest + loanEmiTransaction.Sum(a=> a.Interest), 2)
                };
                loanEmiTransaction.Add(loanEmi);

                previousOpening = previousOpening - previousPrincipal;
                previousInterest = previousOpening * rateOfInterest;
                previousPrincipal = EMI - previousInterest;
            }
            
            return loanEmiTransaction;
        }

        [HttpPost]
        public IActionResult Index(CriteriaInputByUser criteriaInputByUser)
        {
            var loanEmiTransaction = GetEmiTransactionLogs(criteriaInputByUser);
            _loanEmi.SaveEmiDetail(criteriaInputByUser, loanEmiTransaction);

            return View();
        }
    }
}
