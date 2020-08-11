using System.Collections.Generic;
using System.Linq;
using BacancyEmiProject.Models;
using Microsoft.AspNetCore.Mvc;
using BacancyEmiProject.Interface;

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
       

        public JsonResult GetTransactionGrid([FromBody] CriteriaInputByUser criteriaInputByUser)
        {
            var loanEmiTransaction = GetEmiTransactionLogs(criteriaInputByUser);

            return Json(new { loanEmiTransaction = loanEmiTransaction });
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
                    Opening = previousOpening,
                    Principal = previousPrincipal,
                    Interest = previousInterest,
                    Emi = EMI,
                    Closing = previousOpening- previousPrincipal,
                    CummulativeInterest = previousInterest + loanEmiTransaction.Sum(a=> a.Interest)
                };
                loanEmiTransaction.Add(loanEmi);

                previousOpening = loanEmi.Opening;
                previousInterest = loanEmi.Opening * rateOfInterest;
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
