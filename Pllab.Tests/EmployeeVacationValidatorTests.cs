namespace Pllab
{
    [TestFixture]
    public class EmployeeVacationValidatorTests
    {
        [Test]
        public void ValidateVacationDates_ValidDates_NoException()
        {
            // Arrange
            var validator = new EmployeeVacationValidator();
            var vacation = new EmployeeVacation { Start = DateTime.Now, End = DateTime.Now.AddDays(7) };

            // Act
            validator.ValidateVacationDates(vacation);

            // Assert
            Assert.DoesNotThrow(() => validator.ValidateVacationDates(vacation));
        }

        [Test]
        public void ValidateVacationDates_EndDateBeforeStartDate_ValidationException()
        {
            // Arrange
            var validator = new EmployeeVacationValidator();
            var vacation = new EmployeeVacation { Start = DateTime.Now, End = DateTime.Now.AddDays(-7) };

            // Act and Assert
            Assert.Throws<ValidationException>(() => validator.ValidateVacationDates(vacation));
        }
    }

}
