using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ems
{
    public class Menu
    {
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=EMS;Trusted_Connection=True;";

        // ADD EMPLOYEE
        public void AddEmployee(string name, string dept, int salary)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", name ?? string.Empty);
            cmd.Parameters.AddWithValue("@Dept", dept ?? string.Empty);
            cmd.Parameters.AddWithValue("@Salary", salary);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Employee Added Successfully" : "Insert Failed");
        }

        // ADD MANAGER
        public void AddManager(string name, string dept, int salary, int teamSize)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AddManager", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", name ?? string.Empty);
            cmd.Parameters.AddWithValue("@Dept", dept ?? string.Empty);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@TeamSize", teamSize);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Manager Added Successfully" : "Insert Failed");
        }

        // LIST EMPLOYEES
        public void ListEmployees()
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ListEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                Console.WriteLine("No Employees Found");
                return;
            }

            while (dr.Read())
            {
                object teamSize = dr["TeamSize"];
                Console.WriteLine(
                    $"ID: {dr["Id"]}, Name: {dr["Name"]}, Dept: {dr["Department"]}, Salary: {dr["Salary"]}, Type: {dr["EmployeeType"]}, TeamSize: {teamSize}"
                );
            }
        }

        // UPDATE EMPLOYEE
        public void UpdateEmployee(int id)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();

            Console.Write("Enter Salary: ");
            if (!int.TryParse(Console.ReadLine(), out int salary))
            {
                Console.WriteLine("Invalid salary. Aborting update.");
                return;
            }

            Console.Write("Enter Team Size (0 if not manager): ");
            if (!int.TryParse(Console.ReadLine(), out int teamSize))
            {
                Console.WriteLine("Invalid team size. Aborting update.");
                return;
            }

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", name ?? string.Empty);
            cmd.Parameters.AddWithValue("@Dept", dept ?? string.Empty);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@TeamSize", teamSize == 0 ? (object)DBNull.Value : teamSize);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Employee Updated Successfully" : "Employee Not Found");
        }

        // DELETE EMPLOYEE
        public void DeleteEmployee(int id)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Employee Deleted Successfully" : "Employee Not Found");
        }
    }
}
