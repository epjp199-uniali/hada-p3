using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENProduct
    {
        public string codigo
        {
            get { return code; }
            set
            {
                code = value;
            }
        }
        public string nombre
        {
            get { return nombre; }
            set
            {
               nombre = value;
            }
        }
        public int cantidad
        {
            get { return amount; }
            set
            {
                amount = value;
            }
        }
        public float precio
        {
            get { return precio; }
            set
            {
                precio = value;
            }
        }
        public DateTime date
        {
            get { return creationDate; }
            set
            {
                creationDate = value;
            }
        }



        private string code;
        private string name;
        private int amount;
        private float price;
        private DateTime creationDate;

        public int cat;



        public ENProduct()
        {
            codigo = "";
            nombre = "";
            cantidad = -1;
            precio = -1;
            date = new DateTime();

        }

        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            codigo = code;
            nombre = name;
            cantidad = amount;
            precio = price;
            cat = category; //No se como guardarla de otra manera
            date = creationDate;         
        }

        public bool Create()
        {
            CADProduct producto = new CADProduct();
            bool creado = false;

            if (!producto.Read(this))
            {
                creado = producto.Create(this);
            }
                
            return creado;
        }

        public bool Read()
        {
            CADProduct producto = new CADProduct();
            bool read = producto.Read(this);
            return read;
        }

    }
}
