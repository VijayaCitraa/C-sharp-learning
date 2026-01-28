using System;

namespace ems
{
    public class Employee
    {
        private static int idCounter = 1; // static counter for unique IDs
        private int id;
        private string name;

        // parametrized constructor
        public Employee(string name, string dept, int salary)
        {
            id = idCounter++;
            Name = name;
            Dept = dept;
            Salary = salary;
        }

        // Encapsulation
        public int Id => id; 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Dept { get; set; }
        public int Salary { get; set; }

        // Virtual - polymorphism
        public virtual string Display()
        {
            return $"ID: {Id}, Name: {Name}, Department: {Dept}, Salary: {Salary}";
        }
    }
}
