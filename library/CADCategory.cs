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
    public class CADCategory
    {
        private string constring;

        public CADCategory()
        {
            this.constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }
        public bool Read(ENCategory en)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(constring);
                connection.Open();

                SqlCommand query = new SqlCommand($"SELECT * FROM [dbo].[Categories] WHERE id = {en.ID}", connection);
                SqlDataReader result = query.ExecuteReader();
                result.Read();

                if (result.HasRows)
                {
                    en.ID = int.Parse(result["id"].ToString());
                    en.nombre = result["name"].ToString();
                }
                else
                {
                    return false;
                }

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Categories operation has failed. Error: {0}", ex.Message);
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

        public List<ENCategory> readAll()
        {
            List<ENCategory> categorias = new List<ENCategory>();

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(constring);
                connection.Open();

                SqlCommand query = new SqlCommand($"SELECT * FROM [dbo].[Categories]", connection);
                SqlDataReader result = query.ExecuteReader();

                while (result.Read())
                {
                    ENCategory cat = new ENCategory();

                    cat.ID = int.Parse(result["id"].ToString());
                    cat.nombre = result["name"].ToString();

                    categorias.Add(cat);

                }

            }catch(Exception ex)
            {
                Console.WriteLine("Categories operation has failed. Error: {0}", ex.Message);
                return null;
            }

            return categorias;
        }
    }
}

