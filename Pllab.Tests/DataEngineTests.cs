namespace Pllab
{
    [TestFixture]
    public class DataEngineTests
    {
        private const string filePath = "data.json";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        [Test]
        public void DataEngine_GetAllEmployees_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);

            // Act
            var employees = dataEngine.GetAllEmployees();

            // Assert
            Assert.That(employees, Is.Empty);
        }

        [Test]
        public void DataEngine_GetEmployeeByEmail_NonExistingEmail_ReturnsNull()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var email = "non-existing-email@example.com";

            // Act
            var employee = dataEngine.GetEmployeeByEmail(email);

            // Assert
            Assert.That(employee, Is.Null);
        }

        [Test]
        public void DataEngine_AddEmployee_ValidEmployee_AddsEmployeeToList()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };

            // Act
            dataEngine.AddEmployee(employee);

            // Assert
            var employees = dataEngine.GetAllEmployees();
            Assert.That(employees, Has.Count.EqualTo(1));
            Assert.That(employees[0].Email, Is.EqualTo(employee.Email));
        }

        [Test]
        public void DataEngine_AddEmployee_InvalidEmployee_ThrowsValidationException()
        {
            // Arrange
            var dataEngine = new DataEngine("data.json");
            var employee = new Employee { Email = "test@example.com" };

            // Act and Assert
            Assert.Throws<AggregateException>(() => dataEngine.AddEmployee(employee));
        }



        [Test]
        public void DataEngine_UpdateEmployee_ValidEmployee_UpdatesEmployeeInList()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            employee.FullName = "Jane Doe";

            // Act
            dataEngine.UpdateEmployee(employee);

            // Assert
            var updatedEmployee = dataEngine.GetEmployeeByEmail(employee.Email);
            Assert.That(updatedEmployee.FullName, Is.EqualTo(employee.FullName));
        }

        [Test]
        public void DataEngine_UpdateEmployee_ValidEmployee_UpdateEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "89994599258",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);

            // Act
            dataEngine.UpdateEmployee(employee);

            // Assert
            var updatedEmployee = dataEngine.GetEmployeeByEmail(employee.Email);
            Assert.That(updatedEmployee.Phone, Is.EqualTo(employee.Phone));
        }

        [Test]
        public void DataEngine_RemoveEmployee_ExistingEmail_RemovesEmployeeFromList()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);

            // Act
            dataEngine.RemoveEmployee(employee.Email);

            // Assert
            var employees = dataEngine.GetAllEmployees();
            Assert.That(employees, Is.Empty);
        }

        [Test]
        public void DataEngine_GetAllEmployees_MultipleEmployees_ReturnsAllEmployees()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee1 = new Employee
            {
                Email = "example1@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            var employee2 = new Employee
            {
                Email = "example2@example.com",
                FullName = "Jane Doe",
                Phone = "987-654-3210",
                Hired = DateTime.Parse("2020-01-02")
            };
            dataEngine.AddEmployee(employee1);
            dataEngine.AddEmployee(employee2);

            // Act
            var employees = dataEngine.GetAllEmployees();

            // Assert
            Assert.That(employees, Has.Count.EqualTo(2));
            Assert.That(employees[0].Email, Is.EqualTo(employee1.Email));
            Assert.That(employees[1].Email, Is.EqualTo(employee2.Email));
        }

        [Test]
        public void DataEngine_GetEmployeeByEmail_ExistingEmail_ReturnsEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);

            // Act
            var retrievedEmployee = dataEngine.GetEmployeeByEmail(employee.Email);

            // Assert
            Assert.That(retrievedEmployee, Is.Not.Null);
            Assert.That(retrievedEmployee.Email, Is.EqualTo(employee.Email));
        }
        [Test]
        public void DataEngine_AddVacation_ValidVacation_AddsVacationToEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            var vacation = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-01-01"),
                End = DateTime.Parse("2022-01-10")
            };

            // Act
            dataEngine.AddVacation(employee.Email, vacation);

            // Assert
            var employeeVacations = dataEngine.GetEmployeeByEmail(employee.Email).Vacations;
            Assert.That(employeeVacations, Has.Count.EqualTo(1));
            Assert.That(employeeVacations[0].Start, Is.EqualTo(vacation.Start));
        }

        [Test]
        public void DataEngine_AddVacation_InvalidVacation_ThrowsValidationException()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            var vacation = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-01-10"),
                End = DateTime.Parse("2022-01-01")
            };

            // Act and Assert
            Assert.Throws<ValidationException>(() => dataEngine.AddVacation(employee.Email, vacation));
        }

        [Test]
        public void DataEngine_RemoveVacation_ValidVacation_RemovesVacationFromEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            var vacation = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-01-01"),
                End = DateTime.Parse("2022-01-10")
            };
            dataEngine.AddVacation(employee.Email, vacation);

            // Act
            dataEngine.RemoveVacation(employee.Email, vacation.Start);

            // Assert
            var employeeVacations = dataEngine.GetEmployeeByEmail(employee.Email).Vacations;
            Assert.That(employeeVacations, Is.Empty);
        }

        [Test]
        public void DataEngine_GetEmployeeVacations_ValidEmployee_ReturnsVacations()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            var vacation1 = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-01-01"),
                End = DateTime.Parse("2022-01-10")
            };
            var vacation2 = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-02-01"),
                End = DateTime.Parse("2022-02-10")
            };
            dataEngine.AddVacation(employee.Email, vacation1);
            dataEngine.AddVacation(employee.Email, vacation2);

            // Act
            var employeeVacations = dataEngine.GetEmployeeByEmail(employee.Email).Vacations;

            // Assert
            Assert.That(employeeVacations, Has.Count.EqualTo(2));
            Assert.That(employeeVacations[0].Start, Is.EqualTo(vacation1.Start));
            Assert.That(employeeVacations[1].Start, Is.EqualTo(vacation2.Start));
        }
        [Test]
        public void DataEngine_GetAllEmployees_NoEmployees_ReturnsEmptyList()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);

            // Act
            var employees = dataEngine.GetAllEmployees();

            // Assert
            Assert.That(employees, Is.Empty);
        }


        [Test]
        public void DataEngine_UpdateEmployee_ExistingEmail_UpdatesEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            employee.FullName = "Jane Doe";

            // Act
            dataEngine.UpdateEmployee(employee);

            // Assert
            var updatedEmployee = dataEngine.GetEmployeeByEmail(employee.Email);
            Assert.That(updatedEmployee.FullName, Is.EqualTo("Jane Doe"));
        }

        [Test]
        public void DataEngine_RemoveEmployee_ExistingEmail_RemovesEmployee()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);

            // Act
            dataEngine.RemoveEmployee(employee.Email);

            // Assert
            var employees = dataEngine.GetAllEmployees();
            Assert.That(employees, Is.Empty);
        }

        [Test]
        public void DataEngine_GetEmployeeVacations_ExistingEmployee_ReturnsVacations()
        {
            // Arrange
            var dataEngine = new DataEngine(filePath);
            var employee = new Employee
            {
                Email = "example@example.com",
                FullName = "John Doe",
                Phone = "123-456-7890",
                Hired = DateTime.Parse("2020-01-01")
            };
            dataEngine.AddEmployee(employee);
            var vacation = new EmployeeVacation
            {
                Start = DateTime.Parse("2022-01-01"),
                End = DateTime.Parse("2022-01-10")
            };
            dataEngine.AddVacation(employee.Email, vacation);

            // Act
            var employeeVacations = dataEngine.GetEmployeeVacations(employee.Email);

            // Assert
            Assert.That(employeeVacations, Has.Count.EqualTo(1));
        }

        [Test]
        public void DataEngine_AddEmployee_NullEmployee_ThrowsArgumentNullException()
        {
            // Arrange
            var dataEngine = new DataEngine("data.json");
            Employee employee = null;

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => dataEngine.AddEmployee(employee));
        }


    }

}
