namespace Pllab
{
    [TestFixture]
    public class TableFormatterTests
    {
        [Test]
        public void PrintEmployees_EmptyList_NoException()
        {
            // Arrange
            var formatter = new TableFormatter();
            var employees = new List<Employee>();

            // Act
            formatter.PrintEmployees(employees);

            // Assert
            Assert.DoesNotThrow(() => formatter.PrintEmployees(employees));
        }

        [Test]
        public void PrintEmployees_NonEmptyList_FormattedTable()
        {
            // Arrange
            var formatter = new TableFormatter();
            var employees = new List<Employee>
    {
        new Employee { Email = "test@example.com", FullName = "John Doe", Phone = "123-456-7890", Hired = DateTime.Now, Fired = null },
        new Employee { Email = "test2@example.com", FullName = "Jane Doe", Phone = "987-654-3210", Hired = DateTime.Now, Fired = null }
    };

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                formatter.PrintEmployees(employees);
                var output = sw.ToString();

                // Assert
                Assert.That(output, Contains.Substring("| Name         | EMail           | Phone           | Hired      | Fired      |"));
                Assert.That(output, Contains.Substring("|--------------|-----------------|-----------------|------------|------------|"));
                Assert.That(output, Contains.Substring("| John Doe      | test@example.com | 123-456-7890  | 13.11.2024 | -          |"));
                Assert.That(output, Contains.Substring("| Jane Doe      | test2@example.com | 987-654-3210  | 13.11.2024 | -          |"));
            }
        }




    }
}
