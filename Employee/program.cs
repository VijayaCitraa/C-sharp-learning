using System;

namespace ems
{
    class Program
    {
        public static void Main(string[] args)
        {
            //creating object 
            Menu menu = new Menu();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("=== Employee Management System ===");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Add Manager");
                Console.WriteLine("3. List Employees");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string ch = Console.ReadLine();

                switch (ch)
                {
                    case "1":
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string dept = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        int salary = Convert.ToInt32(Console.ReadLine()); // type casting
                        menu.Add(new Employee(name, dept, salary));
                        break;

                    case "2":
                        Console.Write("Enter Name: ");
                        string mname = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string mdept = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        int msalary = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Team Size: ");
                        int teamSize = Convert.ToInt32(Console.ReadLine());
                        menu.Add(new Manager(mname, mdept, msalary, teamSize));
                        break;

                    case "3":
                        menu.List();
                        break;

                    case "4":
                        Console.Write("Enter Employee ID to update: ");
                        int u_id = Convert.ToInt32(Console.ReadLine());
                        menu.Update(u_id);
                        break;

                    case "5":
                        Console.Write("Enter Employee ID to delete: ");
                        int d_id = Convert.ToInt32(Console.ReadLine());
                        menu.Delete(d_id);
                        break;

                    case "6":
                        exit = true;
                        Console.WriteLine("Bye.....");
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
