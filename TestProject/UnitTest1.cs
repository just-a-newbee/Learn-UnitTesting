using Learn_UnitTesting.Dao;
using Learn_UnitTesting.Service;
using NUnit.Framework;

namespace TestProject
{
    public class SalaryServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetSalary_HasValue_ReturnCorrectSalary()
        {
            #region Arrange

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            employeesDao.SetHourlyRate = 166;
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();
            attendanceDao.SetWorkHour = 144;

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(2023, 7, 1);

            SalaryService Service = new SalaryService(employeesDao, attendanceDao);
            decimal Expected = 166 * 144;

            #endregion

            #region Act

            decimal Actual = Service.GetSalary(EmployeeId, Date);

            #endregion

            #region Assert

            Assert.AreEqual(Expected, Actual);

            #endregion
        }
    }

    class FakeEmployeesDao : IEmployeesDao
    {
        public decimal SetHourlyRate = 0;
        public decimal GetHourlyRate(string EmployeeId)
        {
            return SetHourlyRate;
        }
    }

    class FakeAttendanceDao : IAttendanceDao
    {
        public decimal SetWorkHour = 0;
        public decimal GetWorkHour(string EmployeeId, DateTime Date)
        {
            return SetWorkHour;
        }
    }
}