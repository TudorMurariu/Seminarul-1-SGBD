using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Seminarul_1_SGBD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;

            try
            {
                string connectionString = @"Server=DESKTOP-DV079PR\SQLEXPRESS;" +
                    " Database=Seminar_1; Integrated Security = true;" +
                    " TrustServerCertificate=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine(connection.State);
                    SqlCommand insertIntoTable = new SqlCommand("INSERT INTO AnimaleZoo (nume, specie, data_nasterii) " +
                        "VALUES (@nume1, @specie1, @data_nasterii1)," +
                        "(@nume2, @specie2, @data_nasterii2);", connection);

                    insertIntoTable.Parameters.AddWithValue("@nume1", "Bob");
                    insertIntoTable.Parameters.AddWithValue("@specie1", "Hiena");
                    insertIntoTable.Parameters.AddWithValue("@data_nasterii1", DateTime.Now);

                    insertIntoTable.Parameters.AddWithValue("@nume2", "Marian");
                    insertIntoTable.Parameters.AddWithValue("@specie2", "Girafa");
                    insertIntoTable.Parameters.AddWithValue("@data_nasterii2", DateTime.Now);

                    int insertRowCount = insertIntoTable.ExecuteNonQuery();
                    Console.WriteLine(insertRowCount);

                    SqlCommand selectCommand = new SqlCommand("Select nume, specie, data_nasterii " +
                        "FROM AnimaleZoo;", connection);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if(reader.HasRows)
                    {
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            String? d = reader["data_nasterii"].ToString();
                            Console.WriteLine(reader.GetString(0) + "\t" + reader.GetString(1) + "\t" + d);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
            
            Console.WriteLine("Hello, World!");
        }
    }
}