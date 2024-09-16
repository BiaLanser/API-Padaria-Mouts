using API_Padaria_Mouts.Interfaces;
using API_Padaria_Mouts.Models;
using Npgsql;

namespace API_Padaria_Mouts.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private string connectionString = "Host=dpg-crgv7h3v2p9s73du0520-a.oregon-postgres.render.com;Port=5432;Username=apimaster;Password=JxY9V1tkJkja1JLIsiPtU4Ze2y8HZ8tM;Database=api_padaria";
        public Customer Create(Customer t)
        {
            try
            {
                string insertQuery = "INSERT INTO customers (id, name, document) VALUES (@id, @name, @document) RETURNING id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
					command.Parameters.AddWithValue("@id", t.Id);
					command.Parameters.AddWithValue("@name", t.Name);
                    command.Parameters.AddWithValue("@document", t.Document);
                    command.ExecuteScalar();

                }
                return t;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM customers WHERE id = @id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(deleteQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Customer> FindAll()
        {
            var customers = new List<Customer>();
            try
            {
                string findAllQuery = "SELECT id, name, document FROM customers";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(findAllQuery, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Document = reader.GetString(2)
                            });
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return customers;
        }

        public Customer FindById(int id)
        {
            Customer customer = null;
            try
            {
                string findByIdQuery = "SELECT id, name, document FROM customers WHERE id = @id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(findByIdQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Document = reader.GetString(2),
                                Points = reader.GetInt32(3)
                            };
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return customer;
        }

        public Customer Update(Customer t)
        {
            try
            {
                string updateQuery = "UPDATE customers SET name = @name, document = @document WHERE id = @id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", t.Name);
                    command.Parameters.AddWithValue("@document", t.Document);
                    command.Parameters.AddWithValue("@id", t.Id);
                    command.ExecuteNonQuery();
                }

                return t;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
