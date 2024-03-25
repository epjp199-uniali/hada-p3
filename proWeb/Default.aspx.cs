using library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void ComprobarTodo()
        {
            try
            {

                String auxdate = text_date.Text;

                if ((text_code.Text.Length < 1) || (text_code.Text.Length > 16))
                {
                    outputMsg.Text = "El campo code no cumple las restricciones";
                    throw new Exception("El campo code no cumple las restricciones");
                }
                else if (text_name.Text.Length > 32)
                {
                    outputMsg.Text = "El campo name no cumple las restricciones";
                    throw new Exception("El campo name no cumple las restricciones");
                }
                else if ((int.Parse(text_amount.Text) < 0) || (int.Parse(text_amount.Text) > 9999))
                {
                    outputMsg.Text = "El campo amount no cumple las restricciones";
                    throw new Exception("El campo amount no cumple las restricciones");
                }
                else if ((float.Parse(text_price.Text) < 0.01) || (float.Parse(text_price.Text) > 9999.99))
                {
                    outputMsg.Text = "El price amount no cumple las restricciones";
                    throw new Exception("El price amount no cumple las restricciones");
                }
                else if((auxdate[2] != '/')|| (auxdate[5] != '/') || (auxdate[10] != ' ') || (auxdate[13] != ':') || (auxdate[16] != ':'))
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

        protected void Comprobar()
        {
            try
            {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            outputMsg.Text = "";

            if (!IsPostBack)
            {
                CargarCat();
            }
        }

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

        protected void _Delete(object sender, EventArgs e)
        {
            Comprobar();
            ENProduct producto = new ENProduct();
            producto.codigo = text_code.Text;

            if (producto.Delete())
            {
                outputMsg.Text = "Producto " + producto.codigo + " borrado con exito.";
            }
            else outputMsg.Text = "Este usuario no existe en la B.D.";

        }

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

        protected void category(object sender, EventArgs e)
        {
            
        }
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