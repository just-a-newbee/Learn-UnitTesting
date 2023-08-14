using Dapper;
using System.Data.SqlClient;
using System.Text.Json;

namespace Learn_UnitTesting.Service
{
    public class SalaryService
    {
        public decimal GetSalary(string EmpolyeeId, DateTime Date)
        {
            if (string.IsNullOrWhiteSpace(EmpolyeeId))
                throw new ArgumentNullException("Empolee_Id is Empty.");

            if (Date < new DateTime(1753, 1, 1) || Date > new DateTime(9999, 12, 31))
                throw new ArgumentOutOfRangeException("Date Out of range.");

            var ConfigPath = Path.Combine(Environment.CurrentDirectory, "appsetting.json");
            var ConfigAllText = File.ReadAllText(ConfigPath);
            var ConfigModel = JsonSerializer.Deserialize<ConfigModel>(ConfigAllText);
            var ConnectingString = string.Format(ConfigModel.ConnectingString, Path.Combine(Environment.CurrentDirectory, "Database.mdf"));

            using SqlConnection Connection = new SqlConnection(ConnectingString);
            Connection.Open();

            var GetHourlyRateSqlCommand = @"Select Hourly_Rate From Employees
                                                Where Employee_Id = @Id";
            var HourlyRate = Connection.QueryFirstOrDefault<decimal>(GetHourlyRateSqlCommand,
                new
                {
                    Id = EmpolyeeId
                });

            var GetWorkHourSqlCommand = @"Select SUM(A.Hour) From
                                            (
                                            	Select Hour From Attendance
                                            	Where Employee_Id = @Id
                                            	And Date >= @StartDate
                                            	And Date < @EndDate
                                            	Union ALL
                                            	Select 0 Hour 
                                            ) As A";
            var WorkHour = Connection.QueryFirstOrDefault<decimal>(GetWorkHourSqlCommand,
                new
                {
                    Id = EmpolyeeId,
                    StartDate = new DateTime(Date.Year, Date.Month, 1),
                    EndDate = new DateTime(Date.Year, Date.Month, 1).AddMonths(1)
                });

            var Result = HourlyRate * WorkHour;

            return Result;
        }
    }
}
