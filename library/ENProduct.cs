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
            get { return name; }
            set
            {
               name = value;
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
            get { return price; }
            set
            {
                price = value;
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

        public bool Update()
        {
            CADProduct producto = new CADProduct();

            
            ENProduct aux = new ENProduct(codigo, nombre, cantidad, precio, cat, date);

            // Comprobamos que la factura ya existe
            if (producto.Read(this))
            {
                // Tras el read, THIS tiene los datos antiguos. Actualizamos:

                this.codigo = aux.codigo;
                this.nombre = aux.nombre;
                this.cantidad = aux.cantidad;
                this.precio = aux.precio;
                this.cat = aux.cat;
                this.date = aux.date;

                return producto.Update(this);
            }
            else return false;
        }

        public bool Delete()
        {
            return new CADProduct().Delete(this);
        }

        public bool ReadFirst()
        {
            CADProduct cad = new CADProduct();
            bool read = cad.ReadFirst(this);
            return read;
        }

        public bool ReadPrev()
        {
            CADProduct product = new CADProduct();
            ENProduct aux = new ENProduct(codigo, nombre, cantidad, precio, cat, date);
            bool read = false;

            if (product.ReadPrev(aux))
                read = product.ReadPrev(this);
            return read;
        }

        public bool ReadNext()
        {
            CADProduct product = new CADProduct();
            ENProduct aux = new ENProduct(codigo, nombre, cantidad, precio, cat, date);
            bool read = false;

            if (product.ReadNext(aux))
                read = product.ReadNext(this);
            return read;
        }
    }
}
