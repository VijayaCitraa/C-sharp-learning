namespace ems
{
    public class Employee
    {
        public int Id { get; set; }  // Comes from database
        public string Name { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }

        public Employee(string name, string dept, int salary)
        {
            Name = name;
            Dept = dept;
            Salary = salary;
        }

        public Employee() { }  // Needed when reading from DB

        public virtual string Display()
        {
            return $"ID: {Id}, Name: {Name}, Dept: {Dept}, Salary: {Salary}";
        }
    }
}
