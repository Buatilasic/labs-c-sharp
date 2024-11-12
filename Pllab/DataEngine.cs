using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Pllab;

namespace Pllab
{
    public class DataEngine
    {
        private string _filePath;
        private List<Employee> _employees;

        public DataEngine(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        public void LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            }
            else
            {
                _employees = new List<Employee>();
            }
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(_employees);
            File.WriteAllText(_filePath, json);
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _employees.Find(e => e.Email == email);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            EmployeeValidator validator = new EmployeeValidator();
            validator.ValidateEmployee(employee);
            validator.ValidatePhoneAndEmail(employee);
            validator.ValidateFiredDate(employee);
            _employees.Add(employee);
            SaveData();
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee existingEmployee = GetEmployeeByEmail(employee.Email);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = employee.FullName;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Hired = employee.Hired;
                existingEmployee.Fired = employee.Fired;
                SaveData();
            }
        }


        public void RemoveEmployee(string email)
        {
            Employee employee = GetEmployeeByEmail(email);
            if (employee != null)
            {
                _employees.Remove(employee);
                SaveData();
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }
        public void AddVacation(EmployeeVacation vacation)
        {
            EmployeeVacationValidator validator = new EmployeeVacationValidator();
            validator.ValidateVacationDates(vacation);
        }
        public void RemoveVacation(string email, DateTime startDate)
        {
            var employee = GetEmployeeByEmail(email);
            if (employee != null)
            {
                employee.Vacations.RemoveAll(v => v.Start == startDate);
                SaveData();
            }
        }
        public void AddVacation(string email, EmployeeVacation vacation)
        {
            var validator = new EmployeeVacationValidator();
            validator.ValidateVacationDates(vacation);

            var employee = GetEmployeeByEmail(email);
            if (employee != null)
            {
                employee.Vacations.Add(vacation);
                SaveData();
            }
        }
        public List<EmployeeVacation> GetEmployeeVacations(string email)
        {
            var employee = GetEmployeeByEmail(email);
            if (employee != null)
            {
                return employee.Vacations;
            }
            else
            {
                return new List<EmployeeVacation>();
            }
        }

    }
}
