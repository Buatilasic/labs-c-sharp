public class EmployeeVacation
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class Employee
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Hired { get; set; }
    public DateTime? Fired { get; set; }

    public List<EmployeeVacation> Vacations { get; set; }

    public Employee()
    {
        Vacations = new List<EmployeeVacation>();
    }
}