using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SoftUniContext contex = new SoftUniContext())
            {
                var result = AddNewAddressToEmployee(contex);
                Console.WriteLine(result);
            }
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            //can ommit
            //context.Addresses.Add(address); 

            var nakov = context.Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            nakov.Address = address;
            context.SaveChanges();

            var employeeAddresses = context.Employees
                .OrderByDescending(x => x.AddressId)
                .Select(a => a.Address.AddressText)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employeeAddress in employeeAddresses)
            {
                sb.AppendLine($"{employeeAddress}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(d => d.Department.Name == "Research and Development ")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                })
                .OrderBy(s => s.Salary)
                .ThenByDescending(f => f.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development " +
                    $" - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(s => s.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })                
                .OrderBy(x => x.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                    e.EmployeeId
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} " +
                    $"{employee.JobTitle} {employee.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

    }
}
