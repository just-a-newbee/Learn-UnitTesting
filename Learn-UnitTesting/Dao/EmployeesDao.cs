using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_UnitTesting.Dao
{
    public class EmployeesDao
    {
        IDbConnection _connection;
        public EmployeesDao(IDbConnection Connection)
        {
            _connection = Connection;
            if (_connection?.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public decimal GetHourlyRate(string EmployeeId)
        {
            var SqlCommand = @"Select Hourly_Rate From Employees
                               Where Employee_Id = @Id";
            var HourlyRate = _connection.QueryFirstOrDefault<decimal>(SqlCommand,
                new
                {
                    Id = EmployeeId
                });

            return HourlyRate;
        }
    }
}
