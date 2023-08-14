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

            string EmployeeId = "E123456789";
            DateTime Date = new DateTime(2023, 7, 1);

            SalaryService Service = new SalaryService();
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
}