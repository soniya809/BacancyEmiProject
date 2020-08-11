using BacancyEmiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BacancyEmiProject.Interface
{
    public interface ILoanEmi
    {
        void SaveEmiDetail(CriteriaInputByUser criteriaInputByUser, List<LoanEmiTransaction> loanEmiTransactions);
    }
}
