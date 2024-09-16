using API_Padaria_Mouts.Interfaces;
using API_Padaria_Mouts.Models;
using Npgsql;

namespace API_Padaria_Mouts.Repositories
{
    public class SalesRepository : IRepository<Sale>
    {
        private string connectionString = "Host=dpg-crgv7h3v2p9s73du0520-a.oregon-postgres.render.com;Port=5432;Username=apimaster;Password=JxY9V1tkJkja1JLIsiPtU4Ze2y8HZ8tM;Database=api_padaria";
        public Sale Create(Sale t)
        {
            try
            {
                string insertQuery = "INSERT INTO sales (customer_id, points, final_price) VALUES (@customer_id, @points, @final_price) RETURNING id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@customer_id", t.CustomerId);
                    command.Parameters.AddWithValue("@points", t.Points);
                    command.Parameters.AddWithValue("@final_price", t.FinalPrice);
                    t.Id = (int)command.ExecuteScalar();

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
                string deleteQuery = "DELETE FROM sales WHERE id = @id";
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

        public List<Sale> FindAll()
        {
            var sales = new List<Sale>();
            try
            {
                string findAllQuery = "SELECT id, customer_id, points, final_price FROM sales";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(findAllQuery, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sales.Add(new Sale
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                Points = reader.GetInt32(2),
                                FinalPrice = reader.GetDouble(3)
                                
                            });
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sales;
        }

        public Sale FindById(int id)
        {
            Sale sales = null;
            try
            {
                string findByIdQuery = "SELECT id, customer_id, points, final_price FROM sales WHERE id = @id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(findByIdQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sales = new Sale
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                Points = reader.GetInt32(2),
                                FinalPrice = reader.GetDouble(3)
                            };
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sales;
        }

        public Sale Update(Sale t)
        {
            try
            {
                string updateQuery = "UPDATE sales SET customer_id = @customer_id, points = @points, final_price = @final_price WHERE id = @id";
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@customer_id", t.CustomerId);
                    command.Parameters.AddWithValue("@points", t.Points);
                    command.Parameters.AddWithValue("@final_price", t.FinalPrice);
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

        public List<Sale> FindByDocument(string document)
        {
            var sales = new List<Sale>();
            try
            {
                string query = @"SELECT s.id, s.customer_id, s.points, s.final_price FROM sales s
                JOIN customers c ON s.customer_id = c.id
                WHERE c.document = @document";

                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@document", document);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sales.Add(new Sale
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                Points = reader.GetInt32(2),
                                FinalPrice = reader.GetDouble(3)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sales;
        }
    }
}
