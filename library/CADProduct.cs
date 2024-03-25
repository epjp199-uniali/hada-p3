using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace library
{
    public class CADProduct
    {
        private string constring;

        public CADProduct()
        {
            this.constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Create(ENProduct en)
        {
            SqlConnection connection = null;
            try
            {
                // Abrimos la conexión
                connection = new SqlConnection(constring);
                connection.Open();

                SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Products] (name,code,amount,price,category,creationDate) VALUES (@NAME, @CODE, @AMOUNT, @PRICE, @CATEGORY, @CREATIONDATE)", connection);
                query.Parameters.Add("@NAME", SqlDbType.Text).Value = en.nombre;
                query.Parameters.Add("@CODE", SqlDbType.Text).Value = en.codigo;
                query.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = en.cantidad;
                query.Parameters.Add("@PRICE", SqlDbType.Float).Value = en.precio;
                query.Parameters.Add("@CATEGORY", SqlDbType.Int).Value = en.cat;
                query.Parameters.Add("@CREATIONDATE", SqlDbType.DateTime).Value = en.date;

                query.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Products operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public bool Read(ENProduct en)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(constring);
                connection.Open();

                SqlCommand query = new SqlCommand($"SELECT * FROM [dbo].[Products] WHERE code = {en.codigo}", connection);
                SqlDataReader result = query.ExecuteReader();
                result.Read();

                if (result.HasRows)
                {
                    en.codigo = result["code"].ToString();
                    en.nombre = result["name"].ToString();
                    en.cantidad = int.Parse(result["amount"].ToString());
                    en.precio = float.Parse(result["price"].ToString());
                    en.date = DateTime.Parse(result["creationDate"].ToString());
                    en.cat = int.Parse(result["category"].ToString());
                }   
                else
                {
                    return false;
                }

                // Fin
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Products operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public bool Update(ENProduct en)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(constring);
                connection.Open();

                SqlCommand query = new SqlCommand($"UPDATE [dbo].[Products] SET name = @NAME, amount = @AMOUNT, price = @PRICE, category = @CATEGORY, creationDate = @CREATIONDATE WHERE code = {en.codigo}", connection);
                query.Parameters.Add("@NAME", SqlDbType.Text).Value = en.nombre;
                query.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = en.cantidad;
                query.Parameters.Add("@PRICE", SqlDbType.Float).Value = en.precio;
                query.Parameters.Add("@CATEGORY", SqlDbType.Int).Value = en.cat;
                query.Parameters.Add("@CREATIONDATE", SqlDbType.DateTime).Value = en.date;


                query.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Products operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public bool Delete(ENProduct en)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(constring);
                connection.Open();

                // Hacemos la query de borrado
                SqlCommand query = new SqlCommand($"DELETE FROM [dbo].[Products] WHERE code = {en.codigo}", connection);
                query.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Products operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public bool ReadFirst(ENProduct en)
        {
            bool created = false;
            try
            {
                SqlConnection conection = null;
                conection = new SqlConnection(constring);
                conection.Open();

                string query = "Select * From [dbo].[Products]";
                SqlCommand consulta = new SqlCommand(query, conection);
                SqlDataReader search = consulta.ExecuteReader();

                search.Read();
                en.codigo = search["code"].ToString();
                en.nombre = search["name"].ToString();
                en.cantidad = int.Parse(search["amount"].ToString());
                en.precio = float.Parse(search["price"].ToString());
                en.date = DateTime.Parse(search["creationDate"].ToString());
                en.cat = int.Parse(search["category"].ToString());

                created = true;
                search.Close();
                conection.Close();
            }
            catch (Exception ex)
            {
                created = false;
                Console.WriteLine("Products operation has failed.Error: {0}", ex.Message);
            }
            return created;
        }

        public bool ReadPrev(ENProduct en)
        {
            bool created = false;
            try
            {
                SqlConnection conection = null;
                conection = new SqlConnection(constring);
                conection.Open();

                string query = "Select * From [dbo].[Products]";
                SqlCommand consulta = new SqlCommand(query, conection);
                SqlDataReader search = consulta.ExecuteReader();

                ENProduct aux = new ENProduct();

                while (search.Read() && !created)
                {
                    if (search["code"].ToString() == en.codigo)
                    {
                        created = true;
                        break;
                    }

                    aux.codigo = search["code"].ToString();
                    aux.nombre = search["name"].ToString();
                    aux.cantidad = int.Parse(search["amount"].ToString());
                    aux.precio = float.Parse(search["price"].ToString());
                    aux.date = DateTime.Parse(search["creationDate"].ToString());
                    aux.cat = int.Parse(search["category"].ToString());
                }

                if(aux.codigo == "")
                {
                    throw new Exception("No existe producto un anterior en la B.D.");
                }

                en.codigo = aux.codigo;
                en.nombre = aux.nombre;
                en.cantidad = aux.cantidad;
                en.precio = aux.precio;
                en.date = aux.date;
                en.cat = aux.cat;


                search.Close();
                conection.Close();
            }
            catch (Exception e)
            {
                created = false;
                Console.WriteLine("Products operation has failed.Error: {0}", e.Message);
            }
            return created;
        }

        public bool ReadNext(ENProduct en)
        {
            bool created = true;
            bool encontrado = false;
            try
            {
                SqlConnection conection = null;
                conection = new SqlConnection(constring);
                conection.Open();

                string query = "Select * From [dbo].[Products]";
                SqlCommand consulta = new SqlCommand(query, conection);
                SqlDataReader search = consulta.ExecuteReader();

                ENProduct aux = new ENProduct();

                while (search.Read())
                {
                    if (encontrado)
                    {
                        aux.codigo = search["code"].ToString();
                        aux.nombre = search["name"].ToString();
                        aux.cantidad = int.Parse(search["amount"].ToString());
                        aux.precio = float.Parse(search["price"].ToString());
                        aux.date = DateTime.Parse(search["creationDate"].ToString());
                        aux.cat = int.Parse(search["category"].ToString());
                        break;
                    }
                    else if (en.codigo == search["code"].ToString())
                        encontrado = true;

                };

                if (aux.codigo == "")
                {
                    throw new Exception("No existe producto un siguiente B.D.");
                }

                en.codigo = aux.codigo;
                en.nombre = aux.nombre;
                en.cantidad = aux.cantidad;
                en.precio = aux.precio;
                en.date = aux.date;
                en.cat = aux.cat;


                created = true;
                search.Close();
                conection.Close();
            }
            catch (Exception e)
            {
                created = false;
                Console.WriteLine("Products operation has failed.Error: {0}", e.Message);
            }
            return created;
        }
    }
}
