using System;
using System.Globalization;

public class TableFormatter
{
    public void PrintEmployees(List<Employee> employees)
    {
        Console.WriteLine("| Name         | EMail           | Phone           | Hired      | Fired      |");
        Console.WriteLine("|--------------|-----------------|-----------------|------------|------------|");

        foreach (Employee employee in employees)
        {
            string hired = employee.Hired.Year == 1 ? "-" : employee.Hired.ToString("dd.MM.yyyy");
            string fired = employee.Hired.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);


            Console.WriteLine($"| {employee.FullName,-12} | {employee.Email,-15} | {employee.Phone,-12} | {hired,-10} | {fired,-10} |");
        }
    }
}
