using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategory
    {
        private int id;

        private string name;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
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

        public ENCategory()
        {
            ID = -1;
            nombre = "";


        }

        public ENCategory(int id, string name)
        {
            ID = id;
            nombre = name;
        }


        public bool Read()
        {
            return new CADCategory().Read(this);
        }

        public List<ENCategory> readAll()
        {
            CADCategory cad = new CADCategory();

            List<ENCategory> categorias = cad.readAll();

            return categorias;
        }
    }
}
