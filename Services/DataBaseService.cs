using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Zadanie4.Models;

namespace Zadanie4.Services
{
    public interface IDatabaseService 
    {
        Task<IEnumerable<Animal>> GetAnimals(string orderCategory);
        void AddAnimal(Animal animal);
        void DeleteAnimal(int index);
        void UpdateAnimal(Animal animal, int index);
    }
    public class DataBaseService : IDatabaseService

    {
        private String server = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s21009;Integrated Security=True";
        SqlCommand command = new SqlCommand();

        public string findOrderCategory(string orderCategory) {
            string result = "Name";
            if (orderCategory == "Description") {
                result = "Description";
            }
            else if (orderCategory == "Category")
            {
                result = "Category";
            }
            else if (orderCategory == "Area")
            {
                result = "Area";
            }
            return result;        
        }

        public void AddAnimal(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(server)) {
                command.Connection = connection;
                command.CommandText = "insert into animal values(@name, @description, @category ,@area)";
                command.Parameters.AddWithValue("@name", animal.Name);
                command.Parameters.AddWithValue("@description", animal.Description);
                command.Parameters.AddWithValue("@category", animal.Category);
                command.Parameters.AddWithValue("@area", animal.Area);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAnimal(int index)
        {
            using (SqlConnection connection = new SqlConnection(server))
            {
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "delete from animal where idanimal = @id";
                command.Parameters.AddWithValue("@id", index);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task<IEnumerable<Animal>> GetAnimals(string orderCategory)
        {
            string category = findOrderCategory(orderCategory);
            using var connection = new SqlConnection(server);
            using var comand = new SqlCommand("select * from animal order by " + category, connection);
            await connection.OpenAsync();
            var dataReader = await comand.ExecuteReaderAsync();
            var result = new List<Animal>();
            while (await dataReader.ReadAsync())
            {
                await Task.Delay(300);
                    result.Add(new Animal
                    {
                        Name = dataReader["Name"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Area = dataReader["Area"].ToString()
                    });
                }
            return result;            
        }

        public void UpdateAnimal(Animal animal, int index)
        {
            using (SqlConnection connection = new SqlConnection(server))
            {
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "update animal set Name = @name, Description = @description, Category = @category, " +
                                      "Area = @area Where IdAnimal = @id";
                command.Parameters.AddWithValue("@id", index);
                command.Parameters.AddWithValue("@name", animal.Name);
                command.Parameters.AddWithValue("@description", animal.Description);
                command.Parameters.AddWithValue("@category", animal.Category);
                command.Parameters.AddWithValue("@area", animal.Area);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
