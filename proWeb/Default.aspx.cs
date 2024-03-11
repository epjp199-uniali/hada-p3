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

        protected void Comprobar()
        {
            try
            {
                if ((text_code.Text.Length < 1) || (text_code.Text.Length > 16))
                {
                    outputMsg.Text = "El campo code no cumple las restricciones";
                    throw new Exception("El campo code no cumple las restricciones");
                }

                if (text_name.Text.Length > 32)
                {
                    outputMsg.Text = "El campo name no cumple las restricciones";
                    throw new Exception("El campo name no cumple las restricciones");
                }

                if ((int.Parse(text_amount.Text) < 1) || (int.Parse(text_amount.Text) > 9999))
                {
                    outputMsg.Text = "El campo amount no cumple las restricciones";
                    throw new Exception("El campo amount no cumple las restricciones");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed.Error: {0}", ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _Create(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Create

            outputMsg.Text = "Operacion Realizada";
            
        }

        protected void _Update(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Update

            outputMsg.Text = "Operacion Realizada";
        }

        protected void _Delete(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Delete

            outputMsg.Text = "Operacion Realizada";
        }

        protected void _Read(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Read

            outputMsg.Text = "Operacion Realizada";
        }

        protected void _Read_F(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Read F

            outputMsg.Text = "Operacion Realizada";
        }

        protected void _Read_P(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Read P

            outputMsg.Text = "Operacion Realizada";
        }

        protected void _Read_N(object sender, EventArgs e)
        {
            Comprobar();

            //Realizar Read N

            outputMsg.Text = "Operacion Realizada";
        }
    }
}