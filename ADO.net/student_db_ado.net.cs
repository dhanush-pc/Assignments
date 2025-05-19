using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace pract2
{

    class Program
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
                        Id INT PRIMARY KEY,
                        Name VARCHAR(255) NOT NULL,
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

        public static void InsertStudent(string name, int age,int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Student (Id, Name, Age) VALUES (@Id, @Name, @Age);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
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
            Console.WriteLine("********* WELCOME TO STUDENT MANAGEMENT SYSTEM ********");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("1. Create student\n" +
                              "2. insert student\n" +
                              "3. get student by id \n" +
                              "4. get all students\n" +
                              "5. update\n" +
                              "6. delete\n" +
                              "7.clear\n" +
                              "8. Exit");

            string stay = "Y";

            while (stay == "Y")
            {
                Console.WriteLine("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Program.CreateStudentTable();
                        break;
                    case 2:
                        String name;
                        int age;
                        Console.WriteLine("enter ID");
                        int id0 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter student name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter student age:");
                        age = Convert.ToInt32(Console.ReadLine());
                        Program.InsertStudent(name,age,id0);
                        break;
                    case 3:
                        int id;
                        Console.WriteLine("Enter student ID u want to retrive:");
                        id = Convert.ToInt32(Console.ReadLine());
                        Program.GetStudentById(id);
                        break;
                    case 4:
                        Program.GetAllStudents();
                        break;
                    case 5:
                        int id1;
                        string name1;
                        int age1;
                        Console.WriteLine("Enter student ID to update:");
                        id1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new student name:");
                        name1 = Console.ReadLine();
                        Console.WriteLine("Enter new student age (or leave blank to keep current age):");
                        age1 = Convert.ToInt32(Console.ReadLine());
                        Program.UpdateStudent(id1,name1,age1);
                        break;
                    case 6:
                        Console.WriteLine("Enter student ID to delete:");
                        int id2 = Convert.ToInt32(Console.ReadLine());
                        Program.DeleteStudent(id2);
                        break;
                    case 7:
                        Console.Clear();
                        break;
                    case 8:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("Choose another option or exit? (Y/N)");
                stay = Console.ReadLine().ToUpper();
            }
        }


    }
}
