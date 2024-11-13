namespace Pllab
{
    [TestFixture]
    public class EmployeeValidatorTests
    {
        [Test]
        public void ValidateEmployee_ValidEmployee_NoException()
        {
            // Arrange
            var employee = new Employee
            {
                Email = "test@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Now,
                Fired = null
            };

            var validator = new EmployeeValidator();

            // Act
            validator.ValidateEmployee(employee);

            // Assert
            Assert.DoesNotThrow(() => validator.ValidateEmployee(employee));
        }

        [Test]
        public void ValidateEmployee_InvalidEmail_ValidationException()
        {
            // Arrange
            var employee = new Employee
            {
                Email = "invalid email",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Now,
                Fired = null
            };

            var validator = new EmployeeValidator();

            // Act
            var errors = validator.ValidatePhoneAndEmail(employee);

            // Assert
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0].Message, Is.EqualTo("ERROR: Email is not in the correct format. Please, use format '<user>@<server>.<local>' for --email."));
        }


        [Test]
        public void ValidatePhoneAndEmail_ValidPhoneAndEmail_NoException()
        {
            // Arrange
            var employee = new Employee
            {
                Email = "test@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Now,
                Fired = null
            };

            var validator = new EmployeeValidator();

            // Act
            validator.ValidatePhoneAndEmail(employee);

            // Assert
            Assert.DoesNotThrow(() => validator.ValidatePhoneAndEmail(employee));
        }

        [Test]
        public void ValidateFiredDate_FiredDateBeforeHiredDate_ValidationException()
        {
            // Arrange
            var employee = new Employee
            {
                Email = "johdoe@mail.ru",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Now,
                Fired = DateTime.Now.AddDays(-1)
            };

            var validator = new EmployeeValidator();

            // Act
            var errors = validator.ValidateFiredDate(employee);

            // Assert
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0].Message, Is.EqualTo("ERROR: Fired date cannot be earlier than hired date. Please, check dates --hired and --fired"));
        }

        [Test]
        public void ValidateEmployee_InvalidPhone_ValidationException()
        {
            // Arrange
            var employee = new Employee
            {
                Email = "johdoe@mail.ru",
                FullName = "John Doe",
                Phone = "123222",
                Hired = DateTime.Now,
                Fired = null
            };

            var validator = new EmployeeValidator();

            // Act
            var errors = validator.ValidatePhoneAndEmail(employee);

            // Assert
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0].Message, Is.EqualTo("ERROR: Phone number is not in the correct format. Please, check string --phone"));
        }
    }
}
