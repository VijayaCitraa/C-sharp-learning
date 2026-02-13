namespace ems
{
    public class Manager : Employee
    {
        public int TeamSize { get; set; }

        public Manager(string name, string dept, int salary, int teamSize)
            : base(name, dept, salary)
        {
            TeamSize = teamSize;
        }

        public Manager() { } // Needed for DB

        public override string Display()
        {
            return base.Display() + $", Team Size: {TeamSize}";
        }
    }
}
