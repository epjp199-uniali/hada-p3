using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADProduct
    {
        private string constring;

        public CADProduct()
        {
            this.constring = ConfigurationManager.ConnectionStrings["database"].ToString();
        }

        public bool Create(ENProduct en)
        {
            SqlConnection connection = null;
            try
            {
                // Abrimos la conexión
                connection = new SqlConnection(constring);
                connection.Open();

                // Hacemos la query que crea la factura
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
                Console.WriteLine("Factura operation has failed. Error: {0}", ex.Message);
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

                // Almacenamos la factura en ENFactura si existe
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
                    // No existe la factura
                    return false;
                }

                // Fin
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Factura operation has failed. Error: {0}", ex.Message);
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
    }
}
