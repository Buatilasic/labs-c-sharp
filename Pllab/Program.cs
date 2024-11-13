using System.Globalization;

namespace Pllab
{
    public class Program
    {
        private static readonly Log _log = new Log();
        private static readonly DataEngine _dataEngine;

        static Program()
        {
            string filePath = "data.json";
            _dataEngine = new DataEngine(filePath);
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                _log.Error("No arguments provided.");
                return;
            }

            string operation = args[0];

            switch (operation)
            {
                case "--add":
                    AddEmployee(args);
                    break;
                case "--show":
                    ShowEmployee(args);
                    break;
                case "--remove":
                    RemoveEmployee(args);
                    break;
                case "--list":
                    ListEmployees();
                    break;
                case "--update":
                    UpdateEmployee(args);
                    break;
                case "--add-vacation":
                    AddVacation(args);
                    break;
                case "--remove-vacation":
                    RemoveVacation(args);
                    break;
                default:
                    _log.Error("Unknown operation.");
                    break;
            }
        }


        public static void AddEmployee(string[] args)
        {
            string email = null;
            string fullName = null;
            string phone = null;
            DateTime hired = DateTime.MinValue;
            DateTime? fired = null;

            try
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--email"))
                    {
                        email = args[i + 1];
                        i++;
                    }
                    else if (args[i].StartsWith("--name"))
                    {
                        fullName = args[i + 1];
                        i++;
                    }
                    else if (args[i].StartsWith("--phone"))
                    {
                        phone = args[i + 1];
                        i++;
                    }
                    else if (args[i].StartsWith("--hired"))
                    {
                        hired = DateTime.Parse(args[i + 1]);
                        i++;
                    }
                    else if (args[i].StartsWith("--fired"))
                    {
                        fired = DateTime.Parse(args[i + 1]);
                        i++;
                    }
                }
                if (email != null)
                {
                    Employee employee = new Employee
                    {
                        Email = email,
                        FullName = fullName,
                        Phone = phone,
                        Hired = hired,
                        Fired = fired
                    };

                    _dataEngine.AddEmployee(employee);
                    _log.Info($"Employee {email} added.");
                    ShowEmployee(args);
                }
                else
                {
                    _log.Error("Email is required.");
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Validation errors:");
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"- {innerException.Message}");
                }
            }
        }

        public static void RemoveEmployee(string[] args)
        {
            string email = null;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith("--email"))
                {
                    email = args[i + 1];
                    i++;
                }
            }

            if (email != null)
            {
                if (_dataEngine.GetEmployeeByEmail(email) != null)
                {
                    _dataEngine.RemoveEmployee(email);
                    _log.Info($"Employee {email} removed.");
                }
                else
                {
                    _log.Error($"Employee {email} not found.");
                    Console.WriteLine("Employee not found.");
                }
            }
            else
            {
                _log.Error("Email is required.");
            }
        }

        public static void UpdateEmployee(string[] args)
        {
            string email = null;
            string fullName = null;
            string phone = null;
            DateTime hired = DateTime.MinValue;
            DateTime? fired = null;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith("--email"))
                {
                    email = args[i + 1];
                    i++;
                }
                else if (args[i].StartsWith("--name"))
                {
                    fullName = args[i + 1];
                    i++;
                }
                else if (args[i].StartsWith("--phone"))
                {
                    phone = args[i + 1];
                    i++;
                }
                else if (args[i].StartsWith("--hired"))
                {
                    hired = DateTime.Parse(args[i + 1]);
                    i++;
                }
                else if (args[i].StartsWith("--fired"))
                {
                    fired = DateTime.Parse(args[i + 1]);
                    i++;
                }
            }

            if (email != null)
            {
                Employee employee = _dataEngine.GetEmployeeByEmail(email);

                if (employee != null)
                {
                    if (fullName != null)
                    {
                        employee.FullName = fullName;
                    }
                    if (phone != null)
                    {
                        employee.Phone = phone;
                    }
                    if (hired != DateTime.MinValue)
                    {
                        employee.Hired = hired;
                    }
                    if (fired.HasValue)
                    {
                        employee.Fired = fired;
                    }

                    _dataEngine.UpdateEmployee(employee);
                    _log.Info($"Employee {email} updated.");
                    ShowEmployee(args);
                }
                else
                {
                    _log.Error($"Employee {email} not found.");
                }
            }
            else
            {
                _log.Error("Email is required.");
            }
        }

        private static void ShowEmployee(string[] args)
        {
            string email = null;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith("--email"))
                {
                    email = args[i + 1];
                    i++;
                }
            }

            if (email != null)
            {
                Employee employee = _dataEngine.GetEmployeeByEmail(email);

                if (employee != null)
                {
                    _log.Info($"Employee {email} found.");

                    string hired = employee.Hired.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    string fired = employee.Fired.HasValue ? employee.Fired.Value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "-";

                    Console.WriteLine($"Name: {employee.FullName}");
                    Console.WriteLine($"EMail: {employee.Email}");
                    Console.WriteLine($"Phone: {employee.Phone}");
                    Console.WriteLine($"Hired: {hired}");
                    Console.WriteLine($"Fired: {fired}");

                    if (employee.Vacations.Count > 0)
                    {
                        Console.WriteLine("Vacations");
                        Console.WriteLine("| Start      | End        |");

                        foreach (EmployeeVacation vacation in employee.Vacations)
                        {
                            Console.WriteLine($"| {vacation.Start.ToString("dd.MM.yyyy")} | {vacation.End.ToString("dd.MM.yyyy")} |");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No vacations");
                    }

                    var validator = new EmployeeValidator();
                    try
                    {
                        var errors = validator.ValidateEmployeeFull(employee);
                    }
                    catch (AggregateException ex)
                    {
                        Console.WriteLine("Validation errors:");
                        foreach (var innerException in ex.InnerExceptions)
                        {
                            Console.WriteLine($"- {innerException.Message}");
                        }
                    }
                }
                else
                {
                    _log.Error($"Employee {email} not found.");
                }
            }
            else
            {
                _log.Error("Email is required.");
            }
        }


        public static void ListEmployees()
        {
            List<Employee> employees = _dataEngine.GetAllEmployees();
            _log.Info("Employees:");
            TableFormatter formatter = new TableFormatter();
            formatter.PrintEmployees(employees);
        }

        public static void AddVacation(string[] args)
        {
            try
            {
                string email = null;
                DateTime start = DateTime.MinValue;
                DateTime end = DateTime.MinValue;

                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--email"))
                    {
                        email = args[i + 1];
                        i++;
                    }
                    else if (args[i].StartsWith("--start"))
                    {
                        start = DateTime.Parse(args[i + 1]);
                        i++;
                    }
                    else if (args[i].StartsWith("--end"))
                    {
                        end = DateTime.Parse(args[i + 1]);
                        i++;
                    }
                }

                if (email == null)
                {
                    _log.Error("Employee not specified. Specify --email parameter.");
                    return;
                }

                if (start == DateTime.MinValue)
                {
                    _log.Error("Start of vacation is necessary. Specify --start parameter.");
                    return;
                }

                if (end == DateTime.MinValue)
                {
                    _log.Error("End of vacation is necessary. Specify --end parameter.");
                    return;
                }

                Employee employee = _dataEngine.GetEmployeeByEmail(email);

                if (employee != null)
                {
                    employee.Vacations.Add(new EmployeeVacation { Start = start, End = end });
                    _dataEngine.UpdateEmployee(employee);
                    _log.Info("Vacation added successfull.");
                    ShowEmployee(args);
                }
                else
                {
                    _log.Error("Employee not found.");
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void RemoveVacation(string[] args)
        {
            string email = null;
            DateTime start = DateTime.MinValue;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith("--email"))
                {
                    email = args[i + 1];
                    i++;
                }
                else if (args[i].StartsWith("--start"))
                {
                    start = DateTime.Parse(args[i + 1]);
                    i++;
                }
            }

            if (email == null)
            {
                _log.Error("Employee not specified. Specify --email parameter.");
                return;
            }

            if (start == DateTime.MinValue)
            {
                _log.Error("Start of vacation is necessary. Specify --start parameter.");
                return;
            }

            Employee employee = _dataEngine.GetEmployeeByEmail(email);

            if (employee != null)
            {
                EmployeeVacation vacation = employee.Vacations.Find(v => v.Start == start);

                if (vacation != null)
                {
                    employee.Vacations.Remove(vacation);
                    _dataEngine.UpdateEmployee(employee);
                    _log.Info("Vacation was removed.");
                    ShowEmployee(args);
                }
                else
                {
                    _log.Error("Vacation not found.");
                }
            }
            else
            {
                _log.Error("Employee not found.");
            }
        }
    }
}
