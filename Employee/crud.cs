using System;
using System.Collections.Generic;

namespace ems
{
    public class Menu
    {
        private List<Employee> employees = new List<Employee>();
        public void Add(Employee emp)
        {
            employees.Add(emp);
            Console.WriteLine("Added Successfully\n");
        }

        // List all employees
        public void List()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No Employees Found\n");
                return;
            }

            foreach (var emp in employees)
            {
                Console.WriteLine(emp.Display());
            }
            Console.WriteLine();
        }

        // Get employee by ID
        public Employee GetById(int id)
        {
            return employees.Find(e => e.Id == id);
        }

        // Update employee
        public void Update(int id)
        {
            // var - automatically infer variables's type
            var emp = GetById(id);
            if (emp == null)
            {
                Console.WriteLine("Employee Not Found\n");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                emp.Name = name;

            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dept))
                emp.Dept = dept;

            Console.Write("Enter Salary: ");
            string salaryInput = Console.ReadLine();
            if (int.TryParse(salaryInput, out int salary))
                emp.Salary = salary;
            // pattern matching
            if (emp is Manager manager)
            {
                Console.Write("Enter Team Size: ");
                string teamInput = Console.ReadLine();
                if (int.TryParse(teamInput, out int teamSize))
                    manager.Teamsize = teamSize;
            }

            Console.WriteLine("Employee Updated Successfully\n");
        }

        // Delete employee
        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp != null)
            {
                employees.Remove(emp);
                Console.WriteLine("Employee Deleted Successfully\n");
            }
            else
            {
                Console.WriteLine("Employee Not Found\n");
            }
        }
    }
}
