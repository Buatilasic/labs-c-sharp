public class TableFormatter
{
    public void PrintEmployees(List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No records in database.");
            return;
        }

        Console.WriteLine("| Name         | EMail           | Phone           | Hired      | Fired      |");
        Console.WriteLine("|--------------|-----------------|-----------------|------------|------------|");

        foreach (Employee employee in employees)
        {
            string hired = employee.Hired.Year == 1 ? "-" : employee.Hired.ToString("dd.MM.yyyy");
            string fired = employee.Fired.HasValue ? employee.Fired.Value.ToString("dd.MM.yyyy") : "-";

            Console.WriteLine($"| {employee.FullName,-13} | {employee.Email,-15} | {employee.Phone,-13} | {hired,-10} | {fired,-10} |");
        }
    }


}
