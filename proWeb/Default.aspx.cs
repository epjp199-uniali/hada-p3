using library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace proWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Metodo para comprobar todos los parametros, lo usa el create y update
        protected void ComprobarTodo()
        {
            try
            {
                string auxdate = text_date.Text;

                string filterfloat = @"^\d+\.\d{2}$";

                string filterdate = @"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2}$";

                //Comprueba que el codigo es valido
                if ((text_code.Text.Length < 1) || (text_code.Text.Length > 16))
                {
                    outputMsg.Text = "El campo code no cumple las restricciones";
                    throw new Exception("El campo code no cumple las restricciones");
                }
                //Comprueba que el nombre es valido
                else if (text_name.Text.Length > 32)
                {
                    outputMsg.Text = "El campo name no cumple las restricciones";
                    throw new Exception("El campo name no cumple las restricciones");
                }
                //Comprueba que la cantidad es valida
                else if ((int.Parse(text_amount.Text) < 0) || (int.Parse(text_amount.Text) > 9999))
                {
                    outputMsg.Text = "El campo amount no cumple las restricciones";
                    throw new Exception("El campo amount no cumple las restricciones");
                }
                //Comprueba que el precio es valido
                else if (((!Regex.IsMatch(text_price.Text, filterfloat))) || (float.Parse(text_price.Text) > 9999.99))
                {
                        outputMsg.Text = "El campo price no cumple las restricciones";
                        throw new Exception("El campo price no cumple las restricciones");  
                }
                //Comprueba que la fecha es valida(de manera estricta, es decir, su sintaxis)
                else if ((!Regex.IsMatch(text_date.Text, filterdate)))
                {
                    outputMsg.Text = "El campo date no cumple las restricciones";
                    throw new Exception("El campo date no cumple las restricciones");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Metodo para comprobar solo el codigo, lo utilizan metodos read y delete
        protected void Comprobar()
        {
            try
            {
                //Comprueba que el codigo es valido
                if ((text_code.Text.Length < 1) || (text_code.Text.Length > 16))
                {
                    outputMsg.Text = "El campo code no cumple las restricciones";
                    throw new Exception("El campo code no cumple las restricciones");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //MEtodo que se ejecuta al cargar la pagina
        protected void Page_Load(object sender, EventArgs e)
        {
            outputMsg.Text = "";

            //Si es la primera vez que se carga esta instancia de la pagina, carga el despegable de categorias
            if (!IsPostBack)
            {
                CargarCat();
            }
        }

        //Metodo de creacion de producto
        protected void _Create(object sender, EventArgs e)
        {
            try 
            { 
                ComprobarTodo();

                ENProduct producto = new ENProduct();
                producto.codigo = text_code.Text;
                producto.nombre = text_name.Text;
                producto.cantidad = int.Parse(text_amount.Text);
                producto.cat = categorys.SelectedIndex + 1;
                producto.precio = float.Parse(text_price.Text);
                producto.date = DateTime.Parse(text_date.Text);

                if (producto.Create())
                { outputMsg.Text = "Producto " + producto.codigo + " creado con exito."; }
                else { outputMsg.Text = "Operacion fallida"; }

            }
            catch(Exception ex)
            {
                outputMsg.Text = ex.Message;
            }

        }

        //Metodo para actualizar producto
        protected void _Update(object sender, EventArgs e)
        {
            ComprobarTodo();
            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;
            producto.nombre = text_name.Text;
            producto.cantidad = int.Parse(text_amount.Text);
            producto.cat = categorys.SelectedIndex + 1;
            producto.precio = float.Parse(text_price.Text);
            producto.date = DateTime.Parse(text_date.Text);

            if (producto.Update())
            {
                outputMsg.Text = "Producto " + producto.codigo + " actualizado con exito.";
            }
            else outputMsg.Text = "Este usuario no existe en la B.D.";

        }

        //Metodo para eliminar producto
        protected void _Delete(object sender, EventArgs e)
        {
            Comprobar();
            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;

            if (producto.Delete())
            {
                text_code.Text = "";
                text_name.Text = "";
                text_amount.Text = "";
                categorys.SelectedIndex = 0;
                text_price.Text = "";
                text_date.Text = "";

                outputMsg.Text = "Producto " + producto.codigo + " borrado con exito.";
            }
            else outputMsg.Text = "Este usuario no existe en la B.D.";

        }

        //Metodo para saber si un producto esta en la B.D(y mostrarlo)
        protected void _Read(object sender, EventArgs e)
        {
            Comprobar();

            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;


            if (producto.Read())
            {
                text_name.Text = producto.nombre;
                text_amount.Text = producto.cantidad.ToString();
                categorys.SelectedIndex = producto.cat - 1;
                text_price.Text = producto.precio.ToString();
                text_date.Text = producto.date.ToString();

                outputMsg.Text = "Producto " + producto.codigo + " existe en la BD."; 
            }
            else { outputMsg.Text = "Este usuario no existe en la B.D."; }
        }

        //Metodo para leer el primer producto de la B.D
        protected void _Read_F(object sender, EventArgs e)
        {
            ENProduct producto = new ENProduct();
            if (producto.ReadFirst())
            {
                text_code.Text = producto.codigo;
                text_name.Text = producto.nombre;
                text_amount.Text = producto.cantidad.ToString();
                categorys.SelectedIndex = producto.cat - 1;
                text_price.Text = producto.precio.ToString();
                text_date.Text = producto.date.ToString();

                outputMsg.Text = "El primer producto " + producto.codigo + " mostrado con éxito.";
            }
            else outputMsg.Text = "La B.D. no contiene producto.";
        }
        
        //Metodo para leer el producto previo del que se ha indicado
        protected void _Read_P(object sender, EventArgs e)
        {
            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;

            if (producto.ReadPrev())
            {
                text_code.Text = producto.codigo;
                text_name.Text = producto.nombre;
                text_amount.Text = producto.cantidad.ToString();
                categorys.SelectedIndex = producto.cat - 1;
                text_price.Text = producto.precio.ToString();
                text_date.Text = producto.date.ToString();

                outputMsg.Text = "Mostrado el producto anterior";
            }
            else outputMsg.Text = "No hay producto anteriores al indicado.";
        }

        //Metodo para leer el producto siguiente del que se ha indicado
        protected void _Read_N(object sender, EventArgs e)
        {
            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;

            if (producto.ReadNext())
            {
                text_code.Text = producto.codigo;
                text_name.Text = producto.nombre;
                text_amount.Text = producto.cantidad.ToString();
                categorys.SelectedIndex = producto.cat - 1;
                text_price.Text = producto.precio.ToString();
                text_date.Text = producto.date.ToString();

                outputMsg.Text = "Mostrado el producto siguiente";
            }
            else outputMsg.Text = "No hay producto posteriores al indicado.";
        }

        //Metodo por defecto de category
        protected void category(object sender, EventArgs e)
        {
            
        }

        //Metodo auxiliar para cargar el desplegable de categorias
        protected void CargarCat()
        {
            ENCategory aux = new ENCategory();

            List<ENCategory> categorias = aux.readAll();

            categorys.Items.Clear();

            foreach (ENCategory categoria in categorias)
            {
                categorys.Items.Add(new ListItem(categoria.nombre, categoria.ID.ToString()));
            }
        }
    }
}