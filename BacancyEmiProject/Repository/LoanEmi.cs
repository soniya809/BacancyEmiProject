using BacancyEmiProject.Interface;
using BacancyEmiProject.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BacancyEmiProject.Repository
{
    public class LoanEmi : ILoanEmi
    {
        private readonly ConnectionString _connectionString;
        public LoanEmi(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        public async void SaveEmiDetail(CriteriaInputByUser criteriaInputByUser, List<LoanEmiTransaction> loanEmiTransactions)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@LoanAmount", criteriaInputByUser.LoanAmount);
            dynamicParameters.Add("@LoanInterest", criteriaInputByUser.LoanInterest);
            dynamicParameters.Add("@NoOfYear", criteriaInputByUser.NoOfYear);
            dynamicParameters.Add("@MonthlyEmi", criteriaInputByUser.MonthlyEmi);
            dynamicParameters.Add("@RateOfInterest", criteriaInputByUser.RateOfInterest);
            dynamicParameters.Add("@LoanEmiTransactionType", DataTables.ToDataTable(loanEmiTransactions).AsTableValuedParameter("LoanEmiTransactionType"));

            using (var conn = new SqlConnection(_connectionString.Value))
            {
                var result = await conn.QueryAsync("be_InsertEmiDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

       
    }
}
