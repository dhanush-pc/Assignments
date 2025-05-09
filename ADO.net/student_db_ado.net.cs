using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace pract2
{
        
        internal class Program
    {
        private static string connectionString = "Server=LTIN563416\\SQLEXPRESS;Database=MySqlServerDatabase;Integrated Security=True;";

        public static void CreateStudentTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Student')
                    CREATE TABLE Student (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(255) NOT NULL,
                        Age INT
                    );";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Student table created (if it didn't exist).");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error creating table: {ex.Message}");
                }
            }
        }

        public static void InsertStudent(string name, int age)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Student (Name, Age) VALUES (@Name, @Age);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error inserting student: {ex.Message}");
                }
            }
        }

        public static void GetStudentById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT Id, Name, Age FROM Student WHERE Id = @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Student ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}");
                            }
                            else
                            {
                                Console.WriteLine($"Student with ID {id} not found.");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error retrieving student: {ex.Message}");
                }
            }
        }

        public static void GetAllStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT Id, Name, Age FROM Student;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine("All Students:");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No students found.");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error retrieving students: {ex.Message}");
                }
            }
        }

        public static void UpdateStudent(int id, string name, int? age)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Student SET Name = @Name, Age = @Age WHERE Id = @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age.HasValue ? (object)age.Value : DBNull.Value); // Handle nullable age
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error updating student: {ex.Message}");
                }
            }
        }

        public static void DeleteStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Student WHERE Id = @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) deleted.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error deleting student: {ex.Message}");
                }
            }
        }

        static void Main(string[] args)
        {
            CreateStudentTable();

            InsertStudent("Alice", 20);
            InsertStudent("Bob", 22);
            InsertStudent("Charlie", 21);

            GetStudentById(1);
            GetAllStudents();

            UpdateStudent(2, "Robert", 23);
            GetStudentById(2);

            DeleteStudent(3);
            GetAllStudents();


        }
    }
}
