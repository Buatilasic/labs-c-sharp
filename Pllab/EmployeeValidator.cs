using System.Text.RegularExpressions;
using Pllab;
public class EmployeeValidator
{
    public List<ValidationException> ValidateEmployee(Employee employee)
    {
        var errors = new List<ValidationException>();

        if (string.IsNullOrEmpty(employee.FullName))
        {
            errors.Add(new ValidationException("ERROR: Name is required. Please, specify --name parameter."));
        }

        if (string.IsNullOrEmpty(employee.Phone))
        {
            errors.Add(new ValidationException("ERROR: Phone is required. Please, specify --phone parameter."));
        }

        if (employee.Hired == DateTime.MinValue)
        {
            errors.Add(new ValidationException("ERROR: Hired date is required. Please, specify --hired parameter."));
        }

        return errors;
    }

    public List<ValidationException> ValidatePhoneAndEmail(Employee employee)
    {
        var errors = new List<ValidationException>();

        string phoneRegex = @"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$";
        string emailRegex = @"[^@ \t\r\n]+@[^@ \t\r\n]+.[^@ \t\r\n]+";

        if (!Regex.IsMatch(employee.Phone, phoneRegex))
        {
            errors.Add(new ValidationException("ERROR: Phone number is not in the correct format. Please, check string --phone"));
        }

        if (!Regex.IsMatch(employee.Email, emailRegex))
        {
            errors.Add(new ValidationException("ERROR: Email is not in the correct format. Please, use format '<user>@<server>.<local>' for --email."));
        }

        return errors;
    }

    public List<ValidationException> ValidateFiredDate(Employee employee)
    {
        var errors = new List<ValidationException>();

        if (employee.Fired.HasValue && employee.Fired < employee.Hired)
        {
            errors.Add(new ValidationException("ERROR: Fired date cannot be earlier than hired date. Please, check dates --hired and --fired"));
        }

        return errors;
    }

    public List<ValidationException> ValidateEmployeeFull(Employee employee)
    {
        var errors = ValidateEmployee(employee);
        errors.AddRange(ValidatePhoneAndEmail(employee));
        errors.AddRange(ValidateFiredDate(employee));

        return errors;
    }
}