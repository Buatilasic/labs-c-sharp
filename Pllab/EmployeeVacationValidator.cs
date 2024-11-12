using System;

namespace Pllab
{
    public class EmployeeVacationValidator
    {
        public void ValidateVacationDates(EmployeeVacation vacation)
        {
            if (vacation.End < vacation.Start)
            {
                throw new ValidationException("ERROR: Vacation's end date cannot be earlier than start date.");
            }
        }
    }
}
