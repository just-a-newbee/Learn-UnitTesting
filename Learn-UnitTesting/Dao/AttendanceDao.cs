using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_UnitTesting.Dao
{
    public interface IAttendanceDao
    {
        decimal GetWorkHour(string EmployeeId, DateTime Date);
    }

    public class AttendanceDao : IAttendanceDao
    {
        IDbConnection _connection;
        public AttendanceDao(IDbConnection Connection)
        {
            _connection = Connection;
            if (_connection?.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public decimal GetWorkHour(string EmployeeId, DateTime Date)
        {
            var SqlCommand = @"Select SUM(A.Hour) From
                                (
                                	Select Hour From Attendance
                                	Where Employee_Id = @Id
                                	And Date >= @StartDate
                                	And Date < @EndDate
                                	Union ALL
                                	Select 0 Hour 
                                ) As A";
            var WorkHour = _connection.QueryFirstOrDefault<decimal>(SqlCommand,
                new
                {
                    Id = EmployeeId,
                    StartDate = new DateTime(Date.Year, Date.Month, 1),
                    EndDate = new DateTime(Date.Year, Date.Month, 1).AddMonths(1)
                });

            return WorkHour;
        }
    }
}
