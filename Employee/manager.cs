using System;

namespace ems
{
    public class Manager : Employee
    {
        public int Teamsize { get; set; }

        // base - call the parent class constructor
        public Manager(string name, string dept, int salary, int teamsize)
            : base(name, dept, salary)
        {
            Teamsize = teamsize;
        }

        // Override - to override the parent class method
        public override string Display()
        {
            return $"Manager: {base.Display()}, Team Size: {Teamsize}";
        }
    }
}
