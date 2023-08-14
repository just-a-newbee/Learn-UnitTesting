using Learn_UnitTesting.Service;
using System;
using Learn_UnitTesting.Dao;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Learn_UnitTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入員工編號");
            var EmployeeId = Console.ReadLine();
            while (!Regex.IsMatch(EmployeeId, @"^E\d{9}"))
            {
                Console.WriteLine("請重新輸入員工編號");
                EmployeeId = Console.ReadLine();
            }

            Console.WriteLine("請輸入查詢年分(西元)");
            var YearString = Console.ReadLine();
            int Year = 0;
            while (!int.TryParse(YearString, out Year) || Year < DateTime.MinValue.Year || Year > DateTime.MaxValue.Year)
            {
                Console.WriteLine("請重新輸入查詢年分(西元)");
                YearString = Console.ReadLine();
            }

            Console.WriteLine("請輸入查詢月份");
            var MouthString = Console.ReadLine();
            int Month = 0;
            while (!int.TryParse(MouthString, out Month) || Month < 1 || Month > 12)
            {
                Console.WriteLine("請重新輸入查詢月份");
                MouthString = Console.ReadLine();
            }

            try
            {
                ConfigurationProvider ConfigurationProvider = new ConfigurationProvider();
                string ConnectingString = ConfigurationProvider.GetConnectingString();
                using SqlConnection Connection = new SqlConnection(ConnectingString);

                EmployeesDao EmployeesDao = new EmployeesDao(Connection);
                AttendanceDao AttendanceDao = new AttendanceDao(Connection);

                var Result = new SalaryService(EmployeesDao, AttendanceDao)
                    .GetSalary(EmployeeId, new DateTime(Year, Month, 1));
                Console.WriteLine($"員工：{EmployeeId} {Year}/{Month}薪資單 薪水金額：{Result}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
    }
}