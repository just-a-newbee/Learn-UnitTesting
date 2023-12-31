﻿using Dapper;
using Learn_UnitTesting.Dao;
using System.Data.SqlClient;
using System.Text.Json;

namespace Learn_UnitTesting.Service
{
    public class SalaryService
    {
        IEmployeesDao _employeesDao;
        IAttendanceDao _attendanceDao;
        public SalaryService(IEmployeesDao EmployeesDao, IAttendanceDao AttendanceDao)
        {
            _employeesDao = EmployeesDao;
            _attendanceDao = AttendanceDao;
        }

        public decimal GetSalary(string EmpolyeeId, DateTime Date)
        {
            if (string.IsNullOrWhiteSpace(EmpolyeeId))
                throw new ArgumentNullException("Empolee_Id is Empty.");

            if (Date < new DateTime(1753, 1, 1) || Date > new DateTime(9999, 12, 31))
                throw new ArgumentOutOfRangeException("Date Out of range.");

            string ConnectingString = new ConfigurationProvider().GetConnectingString();

            using SqlConnection Connection = new SqlConnection(ConnectingString);
            Connection.Open();

            decimal HourlyRate = _employeesDao.GetHourlyRate(EmpolyeeId);

            decimal WorkHour = _attendanceDao.GetWorkHour(EmpolyeeId, Date);

            var Result = HourlyRate * WorkHour;

            return Result;
        }
    }
}
