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

        [Test]
        public void GetSalary_EmployeeIdIsNull_ThrowArgumentNullException()
        {
            #region Arrange 

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();

            string EmployeeId = null;
            DateTime Date = new DateTime();
            SalaryService Service = new SalaryService(employeesDao, attendanceDao);

            #endregion

            #region Act 

            #endregion

            #region Assert 

            var Exception = Assert.Catch<ArgumentNullException>(() => Service.GetSalary(EmployeeId, Date));
            StringAssert.Contains("Empolee_Id is Empty.", Exception.Message);

            #endregion
        }

        [Test]
        public void GetSalary_DateOutOfRange_ThrowArgumentOutOfRangeException()
        {
            #region Arrange 

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(1000, 1, 1);
            SalaryService Service = new SalaryService(employeesDao, attendanceDao);

            #endregion

            #region Act 

            #endregion

            #region Assert 

            var Exception = Assert.Catch<ArgumentOutOfRangeException>(() => Service.GetSalary(EmployeeId, Date));
            StringAssert.Contains("Date Out of range.", Exception.Message);

            #endregion
        }

        [Test]
        public void GetHourlyRate_BeCalled_EmployeeIdIsCorrect()
        {
            #region Arrange 

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(2023, 7, 1);
            SalaryService Service = new SalaryService(employeesDao, attendanceDao);

            #endregion

            #region Act 
            Service.GetSalary(EmployeeId, Date);

            #endregion

            #region Assert 

            Assert.AreEqual(EmployeeId, employeesDao.EmployeeId);

            #endregion
        }

        [Test]
        public void GetWorkHour_BeCalled_EmployeeIdIsCorrect()
        {
            #region Arrange 

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(2023, 7, 1);
            SalaryService Service = new SalaryService(employeesDao, attendanceDao);

            #endregion

            #region Act 
            Service.GetSalary(EmployeeId, Date);

            #endregion

            #region Assert 

            Assert.AreEqual(EmployeeId, attendanceDao.EmployeeId);

            #endregion
        }

        [Test]
        public void GetWorkHour_BeCalled_DateIsCorrect()
        {
            #region Arrange 

            FakeEmployeesDao employeesDao = new FakeEmployeesDao();
            FakeAttendanceDao attendanceDao = new FakeAttendanceDao();

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(2023, 7, 1);
            SalaryService Service = new SalaryService(employeesDao, attendanceDao);

            #endregion

            #region Act 
            Service.GetSalary(EmployeeId, Date);

            #endregion

            #region Assert 

            Assert.AreEqual(Date, attendanceDao.Date);

            #endregion
        }
    }

    class FakeEmployeesDao : IEmployeesDao
    {
        public decimal SetHourlyRate = 0;
        public string EmployeeId;
        public decimal GetHourlyRate(string EmployeeId)
        {
            this.EmployeeId = EmployeeId;
            return SetHourlyRate;
        }
    }

    class FakeAttendanceDao : IAttendanceDao
    {
        public decimal SetWorkHour = 0;
        public string EmployeeId;
        public DateTime Date;
        public decimal GetWorkHour(string EmployeeId, DateTime Date)
        {
            this.EmployeeId = EmployeeId;
            this.Date = Date;
            return SetWorkHour;
        }
    }
}